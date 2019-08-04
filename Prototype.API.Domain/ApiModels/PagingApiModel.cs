using System;
using System.Collections.Generic;
using System.Text;

namespace Prototype.API.Domain.ApiModels
{
    public class PagingApiModel
    {
        public int Limit { get; set; } = 25;
        public int Offset { get; set; } = 1;

    }
}
