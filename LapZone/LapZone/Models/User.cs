using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace LapZone.Models;

[Table("_User")]
[Index("Email", Name = "UQ___User__A9D1053460F40BD9", IsUnique = true)]
public partial class User
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("UserID")]
    public int UserId { get; set; }


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

    [InverseProperty("User")]
    public virtual ICollection<Address> Addresses { get; set; } = new List<Address>();

    public virtual Cart Cart { get; set; }

    [InverseProperty("User")]
    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    [ForeignKey("RoleId")]
    [InverseProperty("Users")]
    public virtual UserRole Role { get; set; }

    [InverseProperty("User")]
    public virtual ICollection<Wishlist> Wishlists { get; set; } = new List<Wishlist>();

    [NotMapped]
    public IFormFile? clientFile { get; set; }

}