using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net;
namespace Lap.Models;

public class Cart
{
    [Key]
    public int CartID { get; set; }

    public DateTime DateCreated { get; set; }

    [ForeignKey("Customer")]
    public int CustomerID { get; set; }

    public virtual Customer Customer { get; set; }

    public virtual ICollection<CartProduct> CartProducts { get; set; }
}   