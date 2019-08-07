using System;
using System.Collections.Generic;
using System.Text;

namespace Prototype.API.Domain.ApiModels
{
    public abstract class BaseApiModel
    {
        public bool Success { get; set; }
        public string Message { get; set; }

        public BaseApiModel()
        {

        }

        public BaseApiModel(bool success, string message)
        {
            Success = success;
            Message = message;
        }
        
    }
}
