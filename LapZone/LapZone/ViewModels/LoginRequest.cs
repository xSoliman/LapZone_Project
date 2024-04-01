using System.ComponentModel.DataAnnotations;
using System.Text;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace LapZone.ViewModels;

public class LoginRequest
{

    [StringLength(100)]
    [Required(ErrorMessage = "Email is required.")]
    [MaxLength(255)]
    [RegularExpression(".+\\@.+\\..+", ErrorMessage = "Please Enter Valid E-mail")]
    public string Email { get; set; }

    [StringLength(255)]
    [MaxLength(255)]
    [MinLength(8, ErrorMessage = "Password must be at least 8 characters long")]
    [Required(ErrorMessage = "Password is required.")]

    public string PasswordHash { get; set; }
}
