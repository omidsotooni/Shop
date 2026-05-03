namespace Shop.Application.Dtos.BlogDtos
{
    public class BlogsForSitetDto
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Slug { get; set; }       
        public string Description { get; set; }
        public string CategoryText { get; set; }
        public string PictureSrc { get; set; }        
        public long ViewCount { get; set; }        
        public DateTime InsertDate { get; set; }
        public string UserName { get;set; }
        public string AuthorUrl { get; set; }
    }
}