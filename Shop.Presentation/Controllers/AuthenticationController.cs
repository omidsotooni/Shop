using Microsoft.AspNetCore.Mvc;
using Shop.Application.Services.Users.Commands.RegisterUser;
using Shop.Application.Services.Users.Commands.UserLogin;
using System.Security.Claims;
using System.Text.RegularExpressions;
using Shop.Common.Dto;
using EndPoint.Site.Models.ViewModels.AuthenticationViewModel;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Shop.Common;
using Hangfire;

namespace EndPoint.Site.Controllers
{
    public class AuthenticationController : Controller
    {
        private readonly IRegisterUserService _registerUserService;
        private readonly IUserLoginService _userLoginService;
        private readonly IBackgroundJobClient _backgroundJobClient;
        private readonly IRecurringJobManager _recurringJobManager;
        public AuthenticationController(IRegisterUserService registerUserService, 
            IUserLoginService userLoginService, IBackgroundJobClient backgroundJobClient,
            IRecurringJobManager recurringJobManager)
        {
            _registerUserService = registerUserService;
            _userLoginService = userLoginService;
            _backgroundJobClient = backgroundJobClient;
            _recurringJobManager = recurringJobManager;
        }

        [HttpGet]
        public IActionResult Signup()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Signup(SignupViewModel request)
        {
            if (string.IsNullOrWhiteSpace(request.FullName) ||
                string.IsNullOrWhiteSpace(request.Email) ||
                string.IsNullOrWhiteSpace(request.Password) ||
                string.IsNullOrWhiteSpace(request.RePassword))
            {
                return Json(new ResultDto { IsSuccess = false, Message = "لطفا تمامی موارد رو ارسال نمایید" });
            }

            if (User.Identity.IsAuthenticated == true)
            {
                return Json(new ResultDto { IsSuccess = false, Message = "شما به حساب کاربری خود وارد شده اید! و در حال حاضر نمیتوانید ثبت نام مجدد نمایید" });
            }
            if (request.Password != request.RePassword)
            {
                return Json(new ResultDto { IsSuccess = false, Message = "رمز عبور و تکرار آن برابر نیست" });
            }
            if (request.Password.Length < 8)
            {
                return Json(new ResultDto { IsSuccess = false, Message = "رمز عبور باید حداقل 8 کاراکتر باشد" });
            }

            string emailRegex = @"^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[A-Z0-9.-]+\.[A-Z]{2,}$";

            var match = Regex.Match(request.Email, emailRegex, RegexOptions.IgnoreCase);
            if (!match.Success)
            {
                return Json(new ResultDto { IsSuccess = true, Message = "ایمیل خودرا به درستی وارد نمایید" });
            }
            var signeupResult = _registerUserService.Execute(new RequsetRegisterUserDto
            {
                Email = request.Email,
                FullName = request.FullName,
                Password = request.Password,
                RePasword = request.RePassword,
                Roles = new List<RolesInRegisterUserDto>()
                {
                     new RolesInRegisterUserDto { RoleId = 3},
                }
            });

            if (signeupResult.IsSuccess == true)
            {
                var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier,signeupResult.Data.UserId.ToString()),
                new Claim(ClaimTypes.Email, request.Email),
                new Claim(ClaimTypes.Name, request.FullName),
                new Claim(ClaimTypes.Role, "Customer"),
            };

                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(identity);
                var properties = new AuthenticationProperties()
                {
                    IsPersistent = true
                };
                HttpContext.SignInAsync(principal, properties);

            }
            return Json(signeupResult);
        }
        public IActionResult Signin(string ReturnUrl = "/")
        {
            ViewBag.url = ReturnUrl;
            return View();
        }
        [HttpPost]
        public IActionResult Signin(string Email, string Password, string url = "/")
        {
            var oneNextDateTime = DateTime.Today.AddDays(1);
            var nextMonthMiladi = DateTime.Today.AddDays(31);

            var signupResult = _userLoginService.Execute(Email, Password);
            if (signupResult.IsSuccess == true)
            {
                var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, signupResult.Data.UserId.ToString()),
                new Claim(ClaimTypes.Email, Email),
                new Claim(ClaimTypes.Name, signupResult.Data.Name),
                new Claim(ClaimTypes.Role, signupResult.Data.Roles.ToString()),
            };
                foreach (var item in signupResult.Data.Roles)
                {
                    claims.Add(new Claim(ClaimTypes.Role, item));
                }

                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(identity);
                var properties = new AuthenticationProperties()
                {
                    IsPersistent = true,
                    ExpiresUtc = DateTime.Now.AddDays(5),
                };
                HttpContext.SignInAsync(principal, properties);

                // send mail by hangfire
                int randCode = Utility.NewRandomCode();
                //_registerUserService.SendWelcomeMail(randCode, Email);

                // Fire-And-Forget Task | immediately
                BackgroundJob.Enqueue<IRegisterUserService>(o => o.SendWelcomeMail(randCode, Email));
                _backgroundJobClient.Enqueue<IRegisterUserService>(o => o.SendWelcomeMail(randCode, Email));

                // Delayed Task | send mail after 5 minutes
                _backgroundJobClient.Schedule<IRegisterUserService>(o => o.SendWelcomeMail(randCode, Email), TimeSpan.FromMinutes(5));

                // Delayed Task | send mail on special  time
                _backgroundJobClient.Schedule<IRegisterUserService>(o => o.SendWelcomeMail(randCode, Email), (DateTimeOffset)oneNextDateTime);

                //Run at the Nth minute of every hour.
                // Recurring Task | Should be execute in a period of time - Minutely, Hourly, Xth minute of every hour, Daily, Weekly, Monthly, Yearly, 
                _recurringJobManager.AddOrUpdate("Recurring Minutely", () => _registerUserService.SendWelcomeMail(randCode, Email), Cron.Minutely);
                _recurringJobManager.AddOrUpdate("Recurring Hourly", () => _registerUserService.SendWelcomeMail(randCode, Email), Cron.Hourly);
                _recurringJobManager.AddOrUpdate("Recurring Xth minute of every hour", () => _registerUserService.SendWelcomeMail(randCode, Email), Cron.Hourly(13));
                _recurringJobManager.AddOrUpdate("Recurring Daily", () => _registerUserService.SendWelcomeMail(randCode, Email), Cron.Daily);
                _recurringJobManager.AddOrUpdate("Recurring Daily(18, 18)", () => _registerUserService.SendWelcomeMail(randCode, Email), Cron.Daily(18, 18));
                _recurringJobManager.AddOrUpdate("Recurring Weekly", () => _registerUserService.SendWelcomeMail(randCode, Email), Cron.Weekly);
                _recurringJobManager.AddOrUpdate("Recurring Weekly (DayOfWeek.Monday, 9, 00)", () => _registerUserService.SendWelcomeMail(randCode, Email), Cron.Weekly(DayOfWeek.Monday, 9, 00));
                _recurringJobManager.AddOrUpdate("Recurring Monthly", () => _registerUserService.SendWelcomeMail(randCode, Email), Cron.Monthly);
                _recurringJobManager.AddOrUpdate("Recurring Monthly(2, 10, 45)", () => _registerUserService.SendWelcomeMail(randCode, Email), Cron.Monthly(2, 10, 45));
                _recurringJobManager.AddOrUpdate("Recurring Yearly", () => _registerUserService.SendWelcomeMail(randCode, Email), Cron.Yearly);
                _recurringJobManager.AddOrUpdate("Recurring Yearly", () => _registerUserService.SendWelcomeMail(randCode, Email), Cron.Yearly(10, 5, 11, 15));
                _recurringJobManager.AddOrUpdate("Recurring Cron Expression", () => _registerUserService.SendWelcomeMail(randCode, Email), "0 4 10-15 * *");

                // Continuations Task | Two schedules first and second step like Delayed
                //var firstStep = _backgroundJobClient.Schedule<IRegisterUserService>(o => o.SendWelcomeMail(randCode, Email), TimeSpan.FromMinutes(15));
                //var secondStep = _backgroundJobClient.ContinueJobWith(firstStep, 
                //    () => _backgroundJobClient.Schedule(() => _registerUserService.SendWelcomeMail(randCode, Email), TimeSpan.FromHours(2)));

                // Trigger
                string recurringJobId = _backgroundJobClient.Schedule<IRegisterUserService>(o => o.SendWelcomeMail(randCode, Email), (DateTimeOffset)oneNextDateTime);
                _recurringJobManager.Trigger(recurringJobId);
                // ExpireTime
                _recurringJobManager.AddOrUpdate("Test Recurring", () => Console.WriteLine("Recurring Job"), Cron.Monthly);
                _backgroundJobClient.Schedule(() => _recurringJobManager.RemoveIfExists("Test Recurring"), (DateTimeOffset)nextMonthMiladi);

                // For testing Cron Expression Yearly - Monthly - Weekly - Daily - Hourly - Minutely : https://crontab.guru/
            }
            return Json(signupResult);
        }
        public IActionResult SignOut()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }
        
    }
}
