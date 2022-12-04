using efCoreRelationships.Data;
using efCoreRelationships.Models.Dtos;
using efCoreRelationships.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace efCoreRelationships.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly DatabaseContext _ctx;
        public CategoriesController(DatabaseContext context)
        {
            _ctx = context;
        }
       
        public async Task<IActionResult> Index()
        {
            return Ok(await _ctx.Categories.ToListAsync()) ;
        }
        
        public async Task<IActionResult> Add()
        {
            var categorie = await _ctx.Categories.AddAsync(new Categorie
            {
                Name = "category4"
            });
            await _ctx.SaveChangesAsync();  
            return RedirectToAction(nameof(Index)); 
        }
      
        public async Task<IActionResult> Details(int id) {
            return Ok(await _ctx.Categories.FindAsync(id));
        }
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _ctx.Categories.FindAsync(id);
            if(result!= null) _ctx.Remove(result);
            await _ctx.SaveChangesAsync();
         return RedirectToAction(nameof(Index));

        }

        public async Task<IActionResult> ProductDetails()
        {
            var product = await _ctx.Products.Join(_ctx.Categories, p => p.categId, c => c.Id, (product, categorie) =>
                 
                    new ProductDto
                     {
                         Name = product.Name,
                         Price = product.Price,
                         CategoryId = categorie.Id,    
                         CategoryName = categorie.Name
                     }
                 ).ToListAsync();
            return Ok(product);
        }
    }
}
