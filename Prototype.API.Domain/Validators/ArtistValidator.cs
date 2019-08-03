using FluentValidation;
using Prototype.API.Domain.ApiModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Prototype.API.Domain.Validators
{
    public class ArtistValidator : AbstractValidator<ArtistApiModel>
    {
        public ArtistValidator()
        {
            RuleFor(a => a.Name).NotEmpty().WithMessage("Title cannot be empty").MaximumLength(120).WithMessage("Maximum length is 120");
        }
    }
}
