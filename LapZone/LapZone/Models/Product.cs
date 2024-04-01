using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace LapZone.Models;

[Table("Product")]
public partial class Product
{ 
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("ProductID")]
    public int ProductId { get; set; }

    [Required]
    [StringLength(100)]
    public string ProductName { get; set; }


    [Column("_Description", TypeName = "text")]
    public string Description { get; set; }


    [Required]
    [Column(TypeName = "decimal(10, 2)")]
    [Range(0, int.MaxValue, ErrorMessage = "Please enter a positive value.")]

    public decimal Price { get; set; }

    [Required]

    [Column("CategoryID")] 
    public int? CategoryId { get; set; }

    [Required]
    [Range(0, int.MaxValue, ErrorMessage = "Please enter a positive value.")]
    public int StockQuantity { get; set; }

    
    public string ImagePath { get; set; }

    [InverseProperty("Product")]
    public virtual ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();

    [ForeignKey("CategoryId")]
    [InverseProperty("Products")]
    public virtual Category Category { get; set; }


    [InverseProperty("Product")]
    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();


    [InverseProperty("Product")]
    public virtual ICollection<Wishlist> Wishlists { get; set; } = new List<Wishlist>();


    [Required(ErrorMessage ="Photo is Required")]
    [NotMapped]
    public IFormFile clientFile { get; set; }  


}