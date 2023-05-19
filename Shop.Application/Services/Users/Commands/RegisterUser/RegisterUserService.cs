using Shop.Application.Interfaces.Contexts;
using Shop.Common;
using Shop.Common.Dto;
using Shop.Domain.Entities.Users;
using System.Net;
using System.Net.Mail;
using System.Text.RegularExpressions;

namespace Shop.Application.Services.Users.Commands.RegisterUser
{
    public class RegisterUserService : IRegisterUserService
    {
        #region Fields
        private readonly IDataBaseContext _context;
        private readonly string SenderMail = ConstString.SenderMail;
        private readonly string DisplayName = ConstString.DisplayName;
        private readonly string SenderPassword = ConstString.MailPassword;
        private readonly string MailHost = ConstString.MailHost;
        private readonly int MailPort = ConstString.MailPort == null ? 0 : int.Parse(ConstString.MailPort);
        #endregion

        #region Constructor
        public RegisterUserService(IDataBaseContext context)
        {
            _context = context;
        }
        #endregion

        #region Methods
        public ResultDto<ResultRegisterUserDto> Execute(RequsetRegisterUserDto _request)
        {

            try
            {
                if (string.IsNullOrWhiteSpace(_request.FullName))
                {
                    return new ResultDto<ResultRegisterUserDto>()
                    {
                        Data = new ResultRegisterUserDto()
                        {
                            UserId = 0,
                        },
                        IsSuccess = false,
                        Message = "نام و نام خانوادگی را وارد نمایید"
                    };
                }
                if (string.IsNullOrWhiteSpace(_request.Email))
                {
                    return new ResultDto<ResultRegisterUserDto>()
                    {
                        Data = new ResultRegisterUserDto()
                        {
                            UserId = 0,
                        },
                        IsSuccess = false,
                        Message = "پست الکترونیک را وارد نمایید"
                    };
                }
                if (string.IsNullOrWhiteSpace(_request.Password))
                {
                    return new ResultDto<ResultRegisterUserDto>()
                    {
                        Data = new ResultRegisterUserDto()
                        {
                            UserId = 0,
                        },
                        IsSuccess = false,
                        Message = "رمز عبور را وارد نمایید"
                    };
                }

                var FindUser = _context.Users.Where(o => o.Email.Equals(_request.Email)).FirstOrDefault();
                if (FindUser != null)
                {
                    return new ResultDto<ResultRegisterUserDto>()
                    {
                        Data = new ResultRegisterUserDto()
                        {
                            UserId = 0,
                        },
                        IsSuccess = false,
                        Message = "ایمیل وارد شده قبلا در سایت ثبت نام کرده است!",
                    };
                }
                if (_request.Password != _request.RePasword)
                {
                    return new ResultDto<ResultRegisterUserDto>()
                    {
                        Data = new ResultRegisterUserDto()
                        {
                            UserId = 0,
                        },
                        IsSuccess = false,
                        Message = "رمز عبور و تکرار آن برابر نیست"
                    };
                }
                string EmailRegex = @"^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[A-Z0-9.-]+\.[A-Z]{2,}$";

                var match = Regex.Match(_request.Email, EmailRegex, RegexOptions.IgnoreCase);
                if (!match.Success)
                {
                    return new ResultDto<ResultRegisterUserDto>()
                    {
                        Data = new ResultRegisterUserDto()
                        {
                            UserId = 0,
                        },
                        IsSuccess = false,
                        Message = "ایمیل خود را به درستی وارد نمایید"
                    };
                }
                var passwordHasher = new HashPassword();
                var hashedPassword = passwordHasher.PasswordHash(_request.Password);
                User user = new User()
                {
                    Email = _request.Email,
                    FullName = _request.FullName,
                    Password = hashedPassword,
                    IsActive = true,
                };
                
                List<UserInRole> userInRoles = new List<UserInRole>();
                foreach (var item in _request.Roles)
                {
                    var roles = _context.Roles.Find(item.RoleId);
                    userInRoles.Add(new UserInRole
                    {
                        Role = roles,
                        RoleId = roles.Id,
                        User = user,
                        UserId = user.Id,
                    });
                }
                user.UserInRoles = userInRoles;
                _context.Users.Add(user);
                _context.SaveChanges();

                return new ResultDto<ResultRegisterUserDto>()
                {
                    Data = new ResultRegisterUserDto()
                    {
                        UserId = user.Id
                    },
                    IsSuccess = true,
                    Message = "ثبت نام کاربر انجام شد.",
                };
            }
            catch (Exception ex)
            {
                return new ResultDto<ResultRegisterUserDto>()
                {
                    Data = new ResultRegisterUserDto()
                    {
                        UserId = 0,
                    },
                    IsSuccess = false,
                    Message = "ثبت نام انجام نشد !"
                };
            }
        }

        public void SendWelcomeMail(int RandomCode, string UserEmail)
        {
            string FilePath = Directory.GetCurrentDirectory() + "\\wwwroot\\EmailTemplates\\WelcomeTemplate.html";
            StreamReader str = new StreamReader(FilePath);
            string MailText = str.ReadToEnd();
            str.Close();
            MailText = MailText.Replace("[ConfirmCode]", RandomCode.ToString()).Replace("[email]", UserEmail);
            string Subject = $"خوش آمدید {UserEmail}";

            SendByConfigMail(UserEmail, Subject, MailText);

        }
        private bool SendByConfigMail(string email, string subject, string body)
        {
            MailAddress sender = new MailAddress(SenderMail, DisplayName);
            MailAddress reciever = new MailAddress(email);
            try
            {
                using (MailMessage mm = new MailMessage(sender, reciever))
                {
                    mm.Subject = subject;
                    mm.Body = body;
                    mm.IsBodyHtml = true;
                    using (SmtpClient smtp = new SmtpClient())
                    {
                        smtp.Host = MailHost;
                        smtp.EnableSsl = false;
                        NetworkCredential NetworkCred = new NetworkCredential(SenderMail, SenderPassword);
                        smtp.UseDefaultCredentials = false;
                        smtp.Credentials = NetworkCred;
                        smtp.Port = MailPort;
                        smtp.EnableSsl = true;

                        smtp.Send(mm);
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        #endregion
    }
}
