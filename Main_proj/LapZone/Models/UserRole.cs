﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace LapZone.Models;

[Table("UserRole")]
public partial class UserRole
{
    [Key]
    [Column("RoleID")]
    public int RoleId { get; set; }

    [Required]
    [StringLength(50)]
    public string RoleName { get; set; }

    [InverseProperty("Role")]
    public virtual ICollection<User> Users { get; set; } = new List<User>();
}