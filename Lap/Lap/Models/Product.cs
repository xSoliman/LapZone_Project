using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net;
namespace Lap.Models;

public class Product
{
    [Key]
    public int ProductID { get; set; }

    [Required]
    [MaxLength(255)]
    public string ProductName { get; set; }

    [ForeignKey("Category")]
    public int CategoryID { get; set; }

    public virtual Category Category { get; set; }

    public virtual ICollection<VendorProduct> VendorProducts { get; set; }
}
