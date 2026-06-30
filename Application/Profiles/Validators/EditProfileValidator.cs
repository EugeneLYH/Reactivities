using System;
using Application.Profiles.Commands;
using Application.Profiles.DTOs;
using FluentValidation;

namespace Application.Profiles.Validators;

public class EditProfileValidator : AbstractValidator<EditProfile.Command>
{
    public EditProfileValidator()
    {
        RuleFor(x => x.EditProfileDto.DisplayName)
            .NotEmpty().WithMessage("Display name is required.");
    }
}
