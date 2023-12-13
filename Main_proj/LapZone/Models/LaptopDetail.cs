
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace LapZone.Models;

[Table("LaptopDetail")]
[Index("ProductId", Name = "UQ__LaptopDe__B40CC6ECC4FF7DEB", IsUnique = true)]
public partial class LaptopDetail
{
    [Key]
    [Column("LaptopID")]
    public int LaptopId { get; set; }

    [Column("ProductID")]
    public int ProductId { get; set; }

    [StringLength(50)]
    public string Brand { get; set; }

    [Column("CPU")]
    [StringLength(100)]
    public string CPU { get; set; }

    [StringLength(50)]
    public string Storage { get; set; }

    [Column("RAM")]
    [StringLength(50)]
    public string RAM { get; set; }

    [Column("GPU")]
    [StringLength(50)]
    public string GPU { get; set; }

    [Column(TypeName = "decimal(4, 2)")]
    public decimal? ScreenSize { get; set; }

    [Column("_Weight", TypeName = "decimal(5, 2)")]
    public decimal? Weight { get; set; }

    public bool? HasWebcam { get; set; }

    [ForeignKey("ProductId")]
    [InverseProperty("LaptopDetail")]
    public virtual Product Product { get; set; }
}