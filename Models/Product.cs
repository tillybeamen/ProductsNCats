#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;

namespace ProductsNCats.Models;

public class Product
{
    [Key]
    public int ProductId {get;set;}

    [Required]
    [MinLength(2, ErrorMessage = "Must be atleast 2 characters")]
    public string Name {get;set;}

    [Required]
    [MinLength(5, ErrorMessage = "Must be atleast 5 characters")]
    public string Description {get;set;}

    [Required]
    [Range(0.1, double.MaxValue, ErrorMessage = "Must be atleast 0.1")]
    public double Price {get;set;}

    public DateTime CreatedAt {get;set;} = DateTime.Now;

    public DateTime UpdatedAt {get;set;} = DateTime.Now;

    public List<Association> CategoryOfProd {get;set;} = new List<Association>();
}