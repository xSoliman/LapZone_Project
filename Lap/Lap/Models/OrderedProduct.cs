using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net;
namespace Lap.Models;


public class OrderedProduct
{
    [Key]
    public int OrderedProductID { get; set; }

    [ForeignKey("VendorProduct")]
    public int VendorProductID { get; set; }

    [ForeignKey("Order")]
    public int OrderID { get; set; }

    [Required]
    public int Quantity { get; set; }

    public virtual VendorProduct VendorProduct { get; set; }
    public virtual Order Order { get; set; }

    public virtual ICollection<Review> Reviews { get; set; }
}