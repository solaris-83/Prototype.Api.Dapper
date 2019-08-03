using FluentValidation;
using Prototype.API.Domain.ApiModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Prototype.API.Domain.Validators
{
    public class AlbumValidator : AbstractValidator<AlbumApiModel>
    {
        public AlbumValidator()
        {
            RuleFor(a => a.Title).NotEmpty().WithMessage("Title cannot be empty").MaximumLength(160).WithMessage("Maximum length is 160"); 
            RuleFor(a => a.ArtistId).NotNull().WithMessage("ArtistId cannot be empty").GreaterThan(0).WithMessage("ArtistId must be a positive integer");
        }
    }
}
