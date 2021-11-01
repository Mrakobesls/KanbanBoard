using System;

namespace BoardApp.WebApi.Models.RequestModels.Comments
{
    public class AddCommentRequest
    {
        public string Text { get; set; }
        public int CardId { get; set; }
    }
}
