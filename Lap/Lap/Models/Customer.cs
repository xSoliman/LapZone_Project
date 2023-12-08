using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net;
using System.Text;
using Lap.Models;
using System.Security.Cryptography;
using System.Text;
namespace Lap.Models;


public class Customer
{
    [Key]
    public int CustomerID { get; set; }
    [Required(ErrorMessage = "First Name is required.")]
    [RegularExpression(@"^[^\d]+$", ErrorMessage = "First Name cannot contain numbers.")]
    public string FirstName { get; set; }

    [Required(ErrorMessage = "Last Name is required.")]
    [RegularExpression(@"^[^\d]+$", ErrorMessage = "Last Name cannot contain numbers.")]
    public string LastName { get; set; }

    [Required(ErrorMessage = "Email is required.")]
    [MaxLength(255)]
    [RegularExpression(".+\\@.+\\..+", ErrorMessage = "Please Enter Valid E-mail")]
    public string Email { get; set; }

    [Required(ErrorMessage = "Password is required.")]
    [MaxLength(255)]
    [DataType(DataType.Password)]

    public string Password { get; set; }


    [Required(ErrorMessage = "Phone is required.")]
    [MaxLength(255)]
    public string Phone { get; set; }


    [Required(ErrorMessage = "Photo is required.")]

    public string Photo { get; set; }

    public virtual Address Address { get; set; }

    public virtual ICollection<Address> Addresses { get; set; } 

    public virtual ICollection<Order> Orders { get; set; }
    public virtual ICollection<Review> Reviews { get; set; }
    public virtual ICollection<Cart> Carts { get; set; }

    public static void AddCustomer(Customer customer)
    {
   
        
       
    }
}
