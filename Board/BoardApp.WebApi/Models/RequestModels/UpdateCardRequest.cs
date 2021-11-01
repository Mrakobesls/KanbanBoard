namespace BoardApp.WebApi.Models.RequestModels
{
    public class UpdateCardRequest
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }
}
