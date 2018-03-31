using CocShopData.Models.Entities;
using CocShopData.Models.Entities.Service;
using CocShopData.Models.ViewModels;
using CocShopData.Utility;
using SkyWeb.DatVM.Mvc.Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Wisky.Models;
using Wisky.Utility;

namespace Wisky.Areas.Admin.Controllers
{
    public class ProductController : Controller
    {
        // GET: Admin/Product
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult ProductDatatable(JQueryDataTableParamModel param, int categoryId)
        {
            try
            {
                var productService = this.Service<IProductService>();
                IQueryable<Product> listProducts = listProducts = productService.GetAllProductWithCategoryId(categoryId); ;
               
                if (listProducts == null)
                {
                    return Json(new
                    {
                        sEcho = param.sEcho,
                        iTotalRecords = 0,
                        iTotalDisplayRecords = 0,
                        aaData = new List<Product>()
                    }, JsonRequestBehavior.AllowGet);
                }
                var productList = listProducts.AsEnumerable()
                    .Where(a => (string.IsNullOrEmpty(param.sSearch) || StringConvert.EscapeName(a.Name).ToLower()
                                     .Contains(StringConvert.EscapeName(param.sSearch).ToLower())));
                int count = 1;
                var rp = productList
                    .Skip(param.iDisplayStart).Take(param.iDisplayLength)
                    .Select(p => new IConvertible[]
                    {
                    count++,
                    p.Name,
                    p.Price,
                    p.Status,
                    p.ProductId
                    });
                var total = listProducts.Count();
                return Json(new
                {
                    sEcho = param.sEcho,
                    iTotalRecords = total,
                    iTotalDisplayRecords = total,
                    aaData = rp
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new
                {
                    sEcho = param.sEcho,
                    iTotalRecords = 0,
                    iTotalDisplayRecords = 0,
                    aaData = new List<Product>()
                }, JsonRequestBehavior.AllowGet);
            }
        }

        public async Task<ActionResult> CreateProduct(string name, int price, List<int> categoryIds)
        {
            try
            {
                var productService = this.Service<IProductService>();
                var categoryService = this.Service<ICategoryService>();
                var productCategoryService = this.Service<IProduct_CategoryService>();

                Product newProduct = new Product()
                {
                    Name = name,
                    Status = (int)ProductStatusEnum.Stocking,
                    Price = price,
                };
                await productService.CreateAsync(newProduct);
                foreach (var categoryId in categoryIds)
                {
                    productCategoryService.Create(new Product_Category()
                    {
                        ProductId = newProduct.ProductId,
                        CategoryId = categoryId,
                    });
                }

                return Json(new
                {
                    success = true,
                });
            }
            catch (Exception e)
            {
                return Json(new
                {
                    success = false,
                });
            }
        }

        [HttpPost]
        public ActionResult GetDetail(int productId)
        {
            try
            {
                var productService = this.Service<IProductService>();
                var productCategoryService = this.Service<IProduct_CategoryService>();
                var product = productService.GetById(productId);
                if (product != null)
                {
                    return Json(new
                    {
                        success = true,
                        id = product.ProductId,
                        name = product.Name,
                        status = product.Status,
                        price = product.Price,
                        categories = product.Product_Category.Select(pc => pc.CategoryId),
                    });
                }
                return Json(new
                {
                    success = false,
                });
            }
            catch (Exception)
            {
                return Json(new
                {
                    success = false,
                });
            }
        }

        [HttpPost]
        public async Task<ActionResult> EditProduct(int id, string name, int price, List<int> categoryIds, int status)
        {
            try
            {
                var productService = this.Service<IProductService>();
                var productCategoryService = this.Service<IProduct_CategoryService>();
                var listNotProductCategory = productCategoryService.GetNotExist(categoryIds, id).ToList();

                //update product_category

                foreach (var categoryId in categoryIds)
                {
                    var pc = productCategoryService.GetByProductIdAndCategory(id, categoryId);
                    if (pc == null)
                    {
                        productCategoryService.Create(new Product_Category()
                        {
                            ProductId = id,
                            CategoryId = categoryId,
                        });
                    }
                }

                if (listNotProductCategory != null && listNotProductCategory.Any())
                {
                    foreach (var notPC in listNotProductCategory)
                    {
                        await productCategoryService.DeleteAsync(notPC);
                    }
                }

                //update product
                Product product = productService.GetById(id);
                product.Name = name;
                product.Price = price;
                product.Status = status;
                productService.Update(product);


                return Json(new
                {
                    success = true,
                });
            }
            catch (Exception e)
            {
                return Json(new
                {
                    success = false,
                });
            }
        }

        [HttpPost]
        public ActionResult Delete(int productId)
        {
            try
            {
                var productCategoryService = this.Service<IProduct_CategoryService>();
                var listProductCategry = productCategoryService.GetByProductId(productId).ToList();
                foreach (var pc in listProductCategry)
                {
                    productCategoryService.Delete(pc);
                }

                var productService = this.Service<IProductService>();
                var product = productService.GetById(productId);
                if (product != null)
                {
                    productService.Delete(product);

                    return Json(new
                    {
                        success = true,
                    });
                }
                return Json(new
                {
                    success = false,
                });
            }
            catch (Exception e)
            {
                return Json(new
                {
                    success = false,
                });
            }
        }
    }
}