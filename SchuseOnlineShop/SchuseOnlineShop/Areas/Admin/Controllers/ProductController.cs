using Microsoft.AspNetCore.Mvc;
using SchuseOnlineShop.Helpers;
using SchuseOnlineShop.Areas.Admin.ViewModels.Product;
using SchuseOnlineShop.Models;
using SchuseOnlineShop.Services.Interfaces;
using SchuseOnlineShop.ViewModels.Product;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace SchuseOnlineShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {

        private readonly IWebHostEnvironment _env;
        private readonly IProductService _productService;
        private readonly IColorService _colorService;
        private readonly ISizeService _sizeService;
        private readonly ICategoryService _categoryService;
        private readonly ISubCategoryService _subCategoryService;
        private readonly IBrandService _brandService;
        private readonly ICrudService<Product> _crudService;

        public ProductController(IWebHostEnvironment env, 
                                 IProductService productService,
                                 IColorService colorService, 
                                 ISizeService sizeService, 
                                 ICategoryService categoryService,
                                 IBrandService brandService,
                                 ISubCategoryService subCategoryService,
                                 ICrudService<Product> crudService)
        {
            _env = env;
            _productService = productService;
            _colorService = colorService;
            _sizeService = sizeService;
            _categoryService = categoryService;
            _brandService = brandService;
            _subCategoryService = subCategoryService;
            _crudService = crudService;
        }

        public async Task<IActionResult> Index(int page = 1,int take = 5)
        {
            List<Product> datas = await _productService.GetPaginatedDatasAsync(page, take, null, null, null,null);
            List<ProductListVM> mappedDatas = addGetDatas(datas);

            int pageCount = await GetPageCountAsync(take);

            Paginate<ProductListVM> paginatedDatas = new(mappedDatas, page, pageCount);

            return View(paginatedDatas);
        }


        private async Task<int> GetPageCountAsync(int take)
        {
            var productCount = await _productService.GetCountAsync();
            return (int)Math.Ceiling((decimal)productCount / take);
        }
        private List<ProductListVM> addGetDatas(List<Product> products)
        {
            List<ProductListVM> mappedDatas = new();
            foreach (var product in products)
            {
                ProductListVM productList = new()
                {
                    Id = product.Id,
                    Name = product.Name,
                    Price = product.Price,
                    Images = product.ProductImages,
                    SKU = product.SKU
                };
                mappedDatas.Add(productList);
            }
            return mappedDatas;
        }



        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewBag.colors = await GetColorsAsync();
            ViewBag.brands = await GetBrandsAsync();
            ViewBag.sizes = await GetSizesAsync();
            ViewBag.categories = await GetCategoriesAsync();
            ViewBag.subcategories = await GetSubCategoriesAsync();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductCreateVM model)
        {
            try
            {
                ViewBag.colors = await GetColorsAsync();
                ViewBag.brands = await GetBrandsAsync();
                ViewBag.sizes = await GetSizesAsync();
                ViewBag.categories = await GetCategoriesAsync();
                ViewBag.subcategories = await GetSubCategoriesAsync();

                if (!ModelState.IsValid) return View(model);

                Product newProduct = new();
                List<ProductImage> productImages = new();
                List<ProductColor> productColors = new();
                List<ProductSize> productSizes = new();
                Category newCategory = new();
                SubCategory newSubCategory = new();

                //List<Category> categories = new();
                //List<SubCategory> Subcategories = new();

                int canUploadImg = 6 - model.Photos.Count;

                if (canUploadImg < 0)
                {
                    ModelState.AddModelError("Photos", $"The maximum number of images you can upload is 6");
                    return View();
                }

                foreach (var photo in model.Photos)
                {
                    if (!photo.CheckFileType("image/"))
                    {
                        ModelState.AddModelError("Photos", "File type must be image");
                        return View();
                    }
                    if (!photo.CheckFileSize(200))
                    {
                        ModelState.AddModelError("Photos", "Image size must be max 200kb");
                        return View();
                    }
                }

                foreach (var photo in model.Photos)
                {
                    ProductImage productImage = new()
                    {
                        ImgName = photo.CreateFile(_env, "assets/img/home/product-img")
                    };
                    productImages.Add(productImage);
                }
                newProduct.ProductImages = productImages;
                newProduct.ProductImages.FirstOrDefault().IsMain = true;
                newProduct.ProductImages.Skip(1).FirstOrDefault().IsHover = true;

                if (model.ColorIds.Count > 0)
                {
                    foreach (var item in model.ColorIds)
                    {
                        ProductColor productColor = new()
                        {
                            ColorId = item
                        };
                        productColors.Add(productColor);
                    }
                    newProduct.ProductColors = productColors;
                }
                else
                {
                    ModelState.AddModelError("ColorId", "Don`t be empty");
                    return View();
                }

                if (model.SizeIds.Count > 0)
                {
                    foreach (var item in model.SizeIds)
                    {
                        ProductSize productSize = new()
                        {
                            SizeId = item
                        };

                        productSizes.Add(productSize);
                    }
                    newProduct.ProductSizes = productSizes;
                }
                else
                {
                    ModelState.AddModelError("SizeIds", "Don`t be empty");
                    return View();
                }

                
                    //foreach (var item in model.CategoryIds)
                    //{
                    //    Category category = new()
                    //    {
                    //        Id = item
                    //    };

                    //    newCategory.Add(category);
                    //}
                    //newProduct.Category = newCategory;
                
            


              
                    //foreach (var item in model.SubCategoryId)
                    //{
                    //    SubCategory subCategory = new()
                    //    {
                    //        Id = item
                    //    };

                    //    newSubCategory = subCategory;
                    //}
                    //newProduct.SubCategory = newSubCategory;
                
               

                var convertedPrice = decimal.Parse(model.Price);
                Random random = new();

                newProduct.Name = model.Name;
                newProduct.Description = model.Description;
                newProduct.Price = convertedPrice;
                newProduct.StockCount = model.StockCount;
                newProduct.SKU = model.Name.Substring(0, 3) + "-" + random.Next();
                newProduct.BrandId = model.BrandId;
                newProduct.CategoryId = model.CategoryId;
                newProduct.SubCategoryId = model.SubCategoryId;
                newProduct.Rating = model.Rating;
                newProduct.SaleCount = model.SaleCount;

                await _crudService.CreateAsync(newProduct);
                await _crudService.SaveAsync();

                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {

                throw;
            }
        }























        private async Task<SelectList> GetColorsAsync()
        {
            IEnumerable<Color> colors = await _colorService.GetAllAsync();
            return new SelectList(colors, "Id", "Name");
        }
        private async Task<SelectList> GetBrandsAsync()
        {
            IEnumerable<Brand> brands = await _brandService.GetAllAsync();
            return new SelectList(brands, "Id", "Name");
        }
        private async Task<SelectList> GetSizesAsync()
        {
            IEnumerable<Size> sizes = await _sizeService.GetAllAsync();
            return new SelectList(sizes, "Id", "Number");
        }
        private async Task<SelectList> GetCategoriesAsync()
        {
            IEnumerable<Category> categories = await _categoryService.GetAllAsync();
            return new SelectList(categories, "Id", "Name");
        }

        private async Task<SelectList> GetSubCategoriesAsync()
        {
            IEnumerable<SubCategory> subCategories = await _subCategoryService.GetAllAsync();
            return new SelectList(subCategories, "Id", "Name");
        }
    }
}
