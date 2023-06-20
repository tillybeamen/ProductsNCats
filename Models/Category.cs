#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;

namespace ProductsNCats.Models;

public class Category
{
    [Key]
    public int CategoryId {get;set;}

    [Required]
    [MinLength(2, ErrorMessage = "Must be atleast 2 characters")]
    public string Name {get;set;}

    public DateTime CreatedAt {get;set;}

    public DateTime UpdatedAt {get;set;}

    public List<Association> ProductsInCat {get;set;} = new List<Association>();
}