using System.ComponentModel.DataAnnotations;
using System.Text;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using LapZone.Models;

namespace LapZone.ViewModels;

public class SignUpRequest
{


    [StringLength(100)]
    [Required(ErrorMessage = "Name is required.")]
    [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "First name should only contain letters and spaces.")]
    public string FullName { get; set; }

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

    [StringLength(20)]
    [Required(ErrorMessage = "Phone is required.")]
    public string PhoneNumber { get; set; }

    public string? ImagePath { get; set; }

    [Column("RoleID")]
    public int RoleId { get; set; }

 

    [NotMapped]
    public IFormFile? clientFile { get; set; }
}
