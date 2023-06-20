﻿using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductsNCats.Models;

namespace ProductsNCats.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private MyContext _context;

    public HomeController(ILogger<HomeController> logger, MyContext context)
    {
        _logger = logger;
        _context = context;
    }

    [HttpGet("")]
    public IActionResult Index()
    {
        ViewBag.AllProducts = _context.Products.ToList();
        return View();
    }

    [HttpPost("products/create")]
    public IActionResult CreateProduct(Product newProduct)
    {
        if (ModelState.IsValid)
        {
            _context.Add(newProduct);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        else
        {
            ViewBag.AllProducts = _context.Products.ToList();
            return View("Index");
        }
    }

    [HttpGet("products/{id}")]
    public IActionResult OneProduct(int id)
    {
        ViewBag.AProduct = _context.Products.Include(p => p.CategoryOfProd).ThenInclude(a => a.Category).FirstOrDefault(p => p.ProductId == id);
        ViewBag.AllCategories = _context.Categories.ToList();
        return View();
    }

    [HttpPost("associations/product/create")]
    public IActionResult CreateProductAssociation(Association newAssociation)
    {
        if (ModelState.IsValid)
        {
            _context.Add(newAssociation);
            _context.SaveChanges();
            return Redirect($"/products/{newAssociation.ProductId}");
        }
        else
        {
            ViewBag.AProduct = _context.Products.Include(p => p.CategoryOfProd).ThenInclude(a => a.Category).FirstOrDefault(p => p.ProductId == newAssociation.ProductId);
            ViewBag.AllCategories = _context.Categories.ToList();
            return View("OneProduct");
        }
    }

    [HttpPost("associations/category/create")]
    public IActionResult CreateCategoryAssociation(Association newAssociation)
    {
        if (ModelState.IsValid)
        {
            _context.Add(newAssociation);
            _context.SaveChanges();
            return Redirect($"/categories/{newAssociation.CategoryId}");
        }
        else
        {
            ViewBag.ACategory = _context.Categories.Include(c => c.ProductsInCat).ThenInclude(a => a.Product).FirstOrDefault(c => c.CategoryId == newAssociation.CategoryId);
        ViewBag.AllProducts = _context.Products.ToList();
            return View("OneCategory");
        }
    }

    [HttpGet("categories")]
    public IActionResult Categories()
    {
        ViewBag.AllCategories = _context.Categories.ToList();
        return View();
    }

    [HttpPost("categories/create")]
    public IActionResult CreateCategory(Category newCategory)
    {
        if (ModelState.IsValid)
        {
            _context.Add(newCategory);
            _context.SaveChanges();
            return RedirectToAction("Categories");
        }
        else
        {
            ViewBag.AllCategories = _context.Categories.ToList();
            return View("Categories");
        }
    }

    [HttpGet("categories/{id}")]
    public IActionResult OneCategory(int id)
    {
        ViewBag.ACategory = _context.Categories.Include(c => c.ProductsInCat).ThenInclude(a => a.Product).FirstOrDefault(c => c.CategoryId == id);
        ViewBag.AllProducts = _context.Products.ToList();
        return View();
    }

    [HttpPost("association/{aid}/{rid}/{id}/destroy")]
    public IActionResult DeleteAssociation(int aid, int rid, int id)
    {
        Association? AssociationToDelete = _context.Associations.SingleOrDefault(a => a.AssociationId == aid);
        _context.Associations.Remove(AssociationToDelete);
        _context.SaveChanges();
        if(rid==0)
        {
            return Redirect($"/products/{id}");
        }
        else
        {
            return Redirect($"/categories/{id}");
        }
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}