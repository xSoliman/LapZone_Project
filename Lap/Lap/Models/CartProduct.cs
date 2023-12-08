using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net;

namespace Lap.Models;

public class CartProduct
{
    [Key]
    public int CartProductID { get; set; }

    [ForeignKey("VendorProduct")]
    public int VendorProductID { get; set; }

    [Required]
    public int Quantity { get; set; }

    [ForeignKey("Cart")]
    public int CartID { get; set; }

    public virtual VendorProduct VendorProduct { get; set; }
    public virtual Cart Cart { get; set; }
}