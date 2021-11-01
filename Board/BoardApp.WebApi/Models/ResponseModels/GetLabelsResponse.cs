using System.Collections.Generic;

namespace BoardApp.WebApi.Models.ResponseModels
{
    public class GetLabelsResponse
    {
        public IList<LabelModel> Labels { get; set; }
    }
}
