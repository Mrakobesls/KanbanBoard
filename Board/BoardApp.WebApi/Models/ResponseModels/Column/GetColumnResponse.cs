using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BoardApp.WebApi.Models.ResponseModels.Column
{
    public class GetColumnResponse
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int BoardId { get; set; }
    }
}
