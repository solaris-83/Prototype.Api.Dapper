﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Prototype.API.Domain.ApiModels
{
    public class ErrorResource
    {
        public List<string> Messages { get; private set; }

        public ErrorResource(List<string> messages)
        {
            this.Messages = messages ?? new List<string>();
        }

        public ErrorResource(string message)
        {
            this.Messages = new List<string>();

            if (!string.IsNullOrWhiteSpace(message))
            {
                this.Messages.Add(message);
            }
        }
    }
}
