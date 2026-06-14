using System;
using System.ComponentModel.DataAnnotations;

namespace Application.Profiles.DTOs;

public class EditProfileDto
{
    [Required(ErrorMessage="Display name is required")]
    public required string DisplayName { get; set; }
    public string? Bio { get; set; } = "";
}
