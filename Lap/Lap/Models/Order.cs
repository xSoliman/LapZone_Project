using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net;
namespace Lap.Models;


public class Order
{
    [Key]
    public int OrderID { get; set; }

    [ForeignKey("Customer")]
    public int CustomerID { get; set; }

    public DateTime OrderDate { get; set; }

    [ForeignKey("Address")]
    public int AddressID { get; set; }

    public virtual Customer Customer { get; set; }
    public virtual Address Address { get; set; }

    public virtual ICollection<OrderedProduct> OrderedProducts { get; set; }
}
