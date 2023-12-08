using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net;
namespace Lap.Models;

public class VendorProduct
{
    [Key]
    public int VendorProductID { get; set; }

    [ForeignKey("Vendor")]
    public int VendorID { get; set; }

    [ForeignKey("Product")]
    public int ProductID { get; set; }

    [Required]
    public decimal Price { get; set; }

    [Required]
    public int Quantity { get; set; }

    [Required]
    public string Description { get; set; }

    public virtual Vendor Vendor { get; set; }
    public virtual Product Product { get; set; }
}
