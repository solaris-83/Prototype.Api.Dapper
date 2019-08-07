using System;
using System.Collections.Generic;
using System.Text;

namespace Prototype.API.Domain.ApiModels
{
    public class PagingApiModel
    {
        public int PageSize { get; set; } = 25;
        public int Page { get; set; } = 1;

    }
}
