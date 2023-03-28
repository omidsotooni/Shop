namespace Shop.Application.Services.HomePage.Queries.GetHomePageImagesService
{
    public class GetHomePageImaesAdminDto
    {
        public int RowCount { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public List<GetHomePageImaesAdminListDto> HomePageImaes { get; set; }
    }
}
