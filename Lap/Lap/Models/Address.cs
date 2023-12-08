using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net;
namespace Lap.Models;

public class Address
{
    [Key]
    public int AddressID { get; set; }

    [MaxLength(255)]
    public string City { get; set; }    

    [MaxLength(255)]
    public string Governorate { get; set; }

    [MaxLength(255)]
    public string Street { get; set; }

    [MaxLength(255)]
    public string HouseNo { get; set; }

    [ForeignKey("Customer")]
    public int CustomerID { get; set; }

    public virtual Customer Customer { get; set; }
}

public class Category
{
    [Key]
    public int CategoryID { get; set; }

    [Required]
    [MaxLength(255)]
    public string CategoryName { get; set; }

    public virtual ICollection<Product> Products { get; set; }
}