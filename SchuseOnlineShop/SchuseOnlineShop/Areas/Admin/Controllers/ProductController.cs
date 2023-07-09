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

        public async Task<IActionResult> Index(  int page = 1, int take = 5)
        {
            List<Product> datas = await _productService.GetPaginatedDatasAsync(page, take, null, null, null, null, null,null,null);
            List<ProductListVM> mappedDatas = GetDatas(datas);
            ViewBag.page = take;
            int pageCount = await GetPageCountAsync(take);

            Paginate<ProductListVM> paginatedDatas = new(mappedDatas, page, pageCount);

            return View(paginatedDatas);
        }


        private async Task<int> GetPageCountAsync(int take)
        {
            var productCount = await _productService.GetCountAsync();
            return (int)Math.Ceiling((decimal)productCount / take);
        }
        private List<ProductListVM> GetDatas(List<Product> products)
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
                        ImgName = photo.CreateFile(_env, "assets/img/shoes/product-img")
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
                var convertedDiscountPrice = decimal.Parse(model.DiscountPrice);

                Random random = new();


                newProduct.Name = model.Name;
                newProduct.Description = model.Description;
                newProduct.Price = convertedPrice;
                newProduct.DiscountPrice = convertedDiscountPrice;
                newProduct.StockCount = model.StockCount;
                newProduct.SKU = model.Name.Substring(0, 3) + "-" + random.Next();
                newProduct.BrandId = model.BrandId;
                newProduct.CategoryId = model.CategoryId;
                newProduct.SubCategoryId = model.SubCategoryId;
                newProduct.Rating = model.Rating;
                newProduct.Video = model.Video;



                await _crudService.CreateAsync(newProduct);
                await _crudService.SaveAsync();

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.error = ex.Message;
                return View();
            }
        }



        [HttpGet]
        public async Task<IActionResult> Edit(int? id, int page)
        {
            try
            {
                if (id is null) return BadRequest();
                Product dbProduct = await _productService.GetFullDataByIdAsync((int)id);
                if (dbProduct is null) return NotFound();

                ViewBag.colors = await GetColorsAsync();
                ViewBag.brands = await GetBrandsAsync();
                ViewBag.sizes = await GetSizesAsync();
                ViewBag.categories = await GetCategoriesAsync();
                ViewBag.subcategories = await GetSubCategoriesAsync();
                ViewBag.page = page;


                ProductUpdateVM model = new()
                {
                    Id = dbProduct.Id,
                    Name = dbProduct.Name,
                    Description = dbProduct.Description,
                    Price = dbProduct.Price,
                    DiscountPrice = dbProduct.DiscountPrice,
                    SKU = dbProduct.SKU,
                    Rating = dbProduct.Rating,
                    StockCount = dbProduct.StockCount,
                    ProductImages = dbProduct.ProductImages,
                    CategoryId = dbProduct.CategoryId,
                    SubCategoryId = dbProduct.SubCategoryId,
                    BrandId = dbProduct.BrandId,
                    Video = dbProduct.Video,
                    ColorIds = dbProduct.ProductColors.Select(t => t.Color.Id).ToList(),
                    SizeIds = dbProduct.ProductSizes.Select(s => s.Size.Id).ToList(),
                };
                return View(model);
            }
            catch (Exception ex)
            {
                ViewBag.error = ex.Message;
                return View();
            }
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, ProductUpdateVM model)
        {
            try
            {
                ViewBag.colors = await GetColorsAsync();
                ViewBag.brands = await GetBrandsAsync();
                ViewBag.sizes = await GetSizesAsync();
                ViewBag.categories = await GetCategoriesAsync();
                ViewBag.subcategories = await GetSubCategoriesAsync();

                if (id is null) return BadRequest();
                Product dbProduct = await _productService.GetFullDataByIdAsync((int)id);
                if (dbProduct is null) return NotFound();

                if (!ModelState.IsValid)
                {
                    model.ProductImages = dbProduct.ProductImages;
                    return View(model);
                }


                if (model.Photos is not null)
                {
                    List<ProductImage> productImages = new();


                    foreach (var photo in model.Photos)
                    {
                        if (!photo.CheckFileType("image/"))
                        {
                            ModelState.AddModelError("Photos", "File type must be image");
                            model.ProductImages = dbProduct.ProductImages;
                            return View(model);
                        }
                        if (!photo.CheckFileSize(200))
                        {
                            ModelState.AddModelError("Photos", "Image size must be max 200kb");
                            model.ProductImages = dbProduct.ProductImages;
                            return View(model);
                        }
                    }

                    foreach (var photo in model.Photos)
                    {
                        ProductImage productImage = new()
                        {
                            ImgName = photo.CreateFile(_env, "assets/img/shoes/product-img")
                        };
                        productImages.Add(productImage);
                    }
                    dbProduct.ProductImages = productImages;
                    dbProduct.ProductImages.FirstOrDefault().IsMain = true;
                    dbProduct.ProductImages.Skip(1).FirstOrDefault().IsHover = true;
                }
                else
                {
                    model.ProductImages = dbProduct.ProductImages;
                }

                if (model.ColorIds.Count > 0)
                {
                    List<ProductColor> productColors = new();

                    foreach (var item in model.ColorIds)
                    {
                        ProductColor productColor = new()
                        {
                            ColorId = item
                        };
                        productColors.Add(productColor);
                    }
                    dbProduct.ProductColors = productColors;
                }

                if (model.SizeIds.Count > 0)
                {
                    List<ProductSize> productSizes = new();

                    foreach (var item in model.SizeIds)
                    {
                        ProductSize productSize = new()
                        {
                            SizeId = item
                        };
                        productSizes.Add(productSize);
                    }
                    dbProduct.ProductSizes = productSizes;
                }



                dbProduct.Name = model.Name;
                dbProduct.Description = model.Description;
                dbProduct.Price = model.Price;
                dbProduct.DiscountPrice = model.DiscountPrice;
                dbProduct.StockCount = model.StockCount;
                dbProduct.BrandId = model.BrandId;
                dbProduct.Rating = model.Rating;
                dbProduct.Video = model.Video;
                dbProduct.SubCategoryId = model.SubCategoryId;
                dbProduct.CategoryId = model.CategoryId;


                await _crudService.SaveAsync();

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.error = ex.Message;
                return View();
            }
        }




        [HttpPost]
        public async Task<IActionResult> Delete(int? id)
        {
            try
            {
                if (id is null) return BadRequest();
                Product dbProduct = await _productService.GetByIdAsync((int)id);
                if (dbProduct is null) return NotFound();

                foreach (var productImage in dbProduct.ProductImages)
                {
                    string imagePath = FileHelper.GetFilePath(_env.WebRootPath, "assets/img/shoes/product-img", productImage.ImgName);
                    FileHelper.DeleteFile(imagePath);
                }

                _crudService.Delete(dbProduct);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.error = ex.Message;
                return View();
            }
        }

        [HttpGet]
        public async Task<IActionResult> Detail(int? id, int page)
        {
            try
            {
                if (id is null) return BadRequest();
                Product dbProduct = await _productService.GetFullDataByIdAsync((int)id);
                if (dbProduct is null) return NotFound();
                ViewBag.page = page;

                ProductDetailsVM model = new()
                {
                    Id = dbProduct.Id,
                    SKU = dbProduct.SKU,
                    Name = dbProduct.Name,
                    Description = dbProduct.Description,
                    Price = dbProduct.Price,
                    DiscountPrice = dbProduct.DiscountPrice,
                    StockCount = dbProduct.StockCount,
                    SaleCount = dbProduct.SaleCount,
                    Images = dbProduct.ProductImages,
                    CategoryNames = dbProduct.Category.Name,
                    SubCategoryNames = dbProduct.SubCategory.Name,
                    ColorNames = dbProduct.ProductColors,
                    SizeNames = dbProduct.ProductSizes,
                    BrandName = dbProduct.Brand.Name,
                    Rating = dbProduct.Rating,
                    Video = dbProduct.Video
                };
                return View(model);
            }
            catch (Exception ex)
            {
                ViewBag.error = ex.Message;
                return View();
            }
        }





        [HttpPost]
        public async Task<IActionResult> DeleteProductImage(int? id)
        {
            try
            {
                if (id is null) return BadRequest();
                ProductImage image = await _productService.GetImageById((int)id);

                if (image is null) return NotFound();
                Product dbProduct = await _productService.GetProductByImageId((int)id);
                if (dbProduct is null) return NotFound();

                DeleteResponse response = new();
                response.Result = false;

                if (dbProduct.ProductImages.Count > 1)
                {
                    string path = FileHelper.GetFilePath(_env.WebRootPath, "assets/img/shoes/product-img", image.ImgName);
                    FileHelper.DeleteFile(path);
                    _productService.RemoveImage(image);

                    await _crudService.SaveAsync();
                    response.Result = true;
                }

                dbProduct.ProductImages.FirstOrDefault().IsMain = true;

                response.Id = dbProduct.ProductImages.FirstOrDefault().Id;

                await _crudService.SaveAsync();

                return Ok(response);
            }
            catch (Exception ex)
            {
                ViewBag.error = ex.Message;
                return View();
            }
        }



        [HttpPost]
        public async Task<IActionResult> SetStatus(int? id)
        {
            try
            {
                if (id == null) return BadRequest();

                ProductImage image = await _productService.GetImageById((int)id);

                if (image is null) return NotFound();

                Product dbProduct = await _productService.GetProductByImageId((int)id);
                if (dbProduct is null) return NotFound();

                if (!image.IsMain)
                {
                    image.IsMain = true;
                    await _crudService.SaveAsync();
                }

                var images = dbProduct.ProductImages.Where(m => m.Id != id).ToList();

                foreach (var item in images)
                {
                    if (item.IsMain)
                    {
                        item.IsMain = false;
                        await _crudService.SaveAsync();
                    }
                }

                return Ok(image.IsMain);
            }
            catch (Exception ex)
            {
                ViewBag.error = ex.Message;
                return View();
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
