using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchuseOnlineShop.Models;
using SchuseOnlineShop.Services.Interfaces;

namespace SchuseOnlineShop.Areas.Admin.Controllers
{
    [Area("Admin")]
	[Authorize(Roles = "SuperAdmin,Admin")]
	public class ProductCommentController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICrudService<ProductComment> _crudService;
        public ProductCommentController(IProductService productService, ICrudService<ProductComment> crudService)
        {
            _productService = productService;
            _crudService = crudService;
        }

        public async Task<IActionResult> Index()
        {
            var comments = await _productService.GetComments();
            return View(comments);
        }


        [HttpGet]
        public async Task<IActionResult> Detail(int? id)
        {
            if (id == null) return BadRequest();
            ProductComment dbcomment = await _productService.GetCommentByIdWithProduct((int)id);
            if (dbcomment is null) return NotFound();
            return View(dbcomment);
        }


        [HttpPost]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return BadRequest();
            ProductComment dbcomment = await _productService.GetCommentById((int)id);
            if (dbcomment is null) return NotFound();

            _crudService.Delete(dbcomment);
            return Ok();
        }
    }
}
