namespace Shop.Application.Services.HomePage.Queries.GetSliderForAdminService
{
    public class SliderForAdminDto
    {
        public int RowCount { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }

        public List<SliderForAdminListDto> Sliders { get; set; }
    }
}
