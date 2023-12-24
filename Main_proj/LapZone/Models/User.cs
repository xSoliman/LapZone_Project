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

    public string FullName { get; set; }

    public string Email { get; set; }



    public string PasswordHash { get; set; }

    [Required(ErrorMessage = "Phone is required.")]

    public string PhoneNumber { get; set; }

    public string? ImagePath { get; set; }

    [Column("RoleID")]
    [DefaultValue(2)]
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