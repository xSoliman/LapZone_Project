﻿
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace LapZone.Models;

[Table("CartItem")]
[Index("CartId", "ProductId", Name = "UQ_CartItem", IsUnique = true)]
public partial class CartItem
{
    [Key]
    [Column("CartItemID")]
    public int CartItemId { get; set; }

    [Column("CartID")]
    public int CartId { get; set; }

    [Column("ProductID")]
    public int ProductId { get; set; }

    public int Quantity { get; set; }

    [ForeignKey("CartId")]
    [InverseProperty("CartItems")]
    public virtual Cart Cart { get; set; }

    [ForeignKey("ProductId")]
    [InverseProperty("CartItems")]
    public virtual Product Product { get; set; }
}