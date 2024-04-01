
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace LapZone.Models;

[Table("_Address")]
[Index("UserId", "Country", "Governorate", "City", "AddressLine", Name = "UQ_User_Address", IsUnique = true)]
public partial class Address
{
    [Key]
    [Column("AddressID")]
    public int AddressId { get; set; }

    [Column("UserID")]
    public int? UserId { get; set; }

    [Required]
    [StringLength(50)]
    public string Country { get; set; }

    [Required]  
    [StringLength(50)]
    public string Governorate { get; set; }

    [Required]

    [StringLength(50)]
    public string City { get; set; }

    [Required]
    [StringLength(255)]
    public string AddressLine { get; set; }

    [ForeignKey("UserId")]
    [InverseProperty("Addresses")]
    public virtual User User { get; set; }
    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

}