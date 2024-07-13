using CoffeeShop.Models.Interfaces;
using Microsoft.AspNetCore.Mvc;

public class HomeController : Controller
{
    private readonly IProductRepository productRepository;

    public HomeController(IProductRepository productRepository)
    {
        this.productRepository = productRepository;
    }

    public IActionResult Index()
    {
        return View(productRepository.GetTrendingProducts());
    }
}