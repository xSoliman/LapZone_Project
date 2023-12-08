using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net;
namespace Lap.Models;

public class Review
{
    [Key]
    public int ReviewID { get; set; }

    [Required]
    public byte Rating { get; set; }

    public string Comment { get; set; }

    [ForeignKey("Customer")]
    public int CustomerID { get; set; }

    [ForeignKey("OrderedProduct")]
    public int OrderedProductID { get; set; }

    public virtual Customer Customer { get; set; }
    public virtual OrderedProduct OrderedProduct { get; set; }
}
