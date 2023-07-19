using CoreWebAPI.Data;
using CoreWebAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections;

namespace CoreWebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]

    public class ProductsController : Controller
    {
        private readonly CoreWebAPIContext _context;
        private readonly ILogger<ProductsController> _logger;

        public ProductsController(CoreWebAPIContext context, ILogger<ProductsController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            return Ok(await _context.Products.ToListAsync());
        }


        [HttpPost]
        public async Task<IActionResult> AddProduct([Bind("Id,Name,Description,Price,Image")] Product product)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _context.Products.AddAsync(product);
                    await _context.SaveChangesAsync();
                    return Ok(product);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }

            return BadRequest();
        }


        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> GetContact([FromRoute] int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }


        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> UpdateProduct(int id, [Bind("Id,Name,Description,Price,Image")] Product product)
        {
            if (id != product.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(product);
                    await _context.SaveChangesAsync();
                    return Ok(product);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists((int)product.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }
            return NotFound();
        }


        [HttpDelete]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Products == null)
            {
                return Problem("Entity set 'CoreWebAPIContext.Products'  is null.");
            }
            var product = await _context.Products.FindAsync(id);
            if (product != null)
            {
                _context.Products.Remove(product);
            }

            await _context.SaveChangesAsync();
            return Ok(product);
        }

        private bool ProductExists(int id)
        {
            return (_context.Products?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
