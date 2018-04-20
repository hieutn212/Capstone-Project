using CapstoneData.Models.Entities;
using CapstoneData.Models.Entities.Services;
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
    public class ReportController : Controller
    {
        // GET: Admin/Product
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult DateReportTable(JQueryDataTableParamModel param, DateTime startTime, DateTime endTime)
        {
            var listDateReport = new List<ReportChartView>();
            IHistoryService historyService = this.Service<IHistoryService>();
            try
            {
                //lay du lieu tu database table datereport
                var listReport = historyService.getAllList(startTime, endTime).ToList();
                for (DateTime i = startTime; i < endTime; i = i.AddDays(1))
                {
                    DateTime check = i.AddDays(1);
                    double normal = 0, vip = 0;
                    var listNormal = historyService.GetActive(q => q.CreatedDate > i && q.CreatedDate <= check && q.LicenseType.PackageId == 1).ToList();
                    var listVip = historyService.GetActive(q => q.CreatedDate > i && q.CreatedDate <= check && q.LicenseType.PackageId == 2).ToList();
                    if (listNormal.Count > 0)
                    {
                        normal = listNormal.Sum(a => a.LicenseType.Price).Value;
                    }
                    if (listVip.Count > 0)
                    {
                        vip = listVip.Sum(a => a.LicenseType.Price).Value;
                    }
                    listDateReport.Add(new ReportChartView
                    {
                        Date = i.ToString("dd/MM/yyyy"),
                        TotalNormal =normal,
                        TotalVip = vip,

                    });
                }
                var total = listDateReport.Count();
                var rp = listDateReport
                    .Skip(param.iDisplayStart).Take(param.iDisplayLength);
                return Json(new
                {
                        sEcho = param.sEcho,
                        iTotalRecords = total,
                        iTotalDisplayRecords = total,
                        aaData = rp,
                        dataList = listDateReport,
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return null;
            }
        }

        //public async Task<ActionResult> CreateProduct(string name, int price, List<int> categoryIds)
        //{
        //    try
        //    {
        //        var productService = this.Service<IProductService>();
        //        var categoryService = this.Service<ICategoryService>();
        //        var productCategoryService = this.Service<IProduct_CategoryService>();

        //        Product newProduct = new Product()
        //        {
        //            Name = name,
        //            Status = (int)ProductStatusEnum.Stocking,
        //            Price = price,
        //        };
        //        await productService.CreateAsync(newProduct);
        //        foreach (var categoryId in categoryIds)
        //        {
        //            productCategoryService.Create(new Product_Category()
        //            {
        //                ProductId = newProduct.ProductId,
        //                CategoryId = categoryId,
        //            });
        //        }

        //        return Json(new
        //        {
        //            success = true,
        //        });
        //    }
        //    catch (Exception e)
        //    {
        //        return Json(new
        //        {
        //            success = false,
        //        });
        //    }
        //}

        //[HttpPost]
        //public ActionResult GetDetail(int productId)
        //{
        //    try
        //    {
        //        var productService = this.Service<IProductService>();
        //        var productCategoryService = this.Service<IProduct_CategoryService>();
        //        var product = productService.GetById(productId);
        //        if (product != null)
        //        {
        //            return Json(new
        //            {
        //                success = true,
        //                id = product.ProductId,
        //                name = product.Name,
        //                status = product.Status,
        //                price = product.Price,
        //                categories = product.Product_Category.Select(pc => pc.CategoryId),
        //            });
        //        }
        //        return Json(new
        //        {
        //            success = false,
        //        });
        //    }
        //    catch (Exception)
        //    {
        //        return Json(new
        //        {
        //            success = false,
        //        });
        //    }
        //}

        //[HttpPost]
        //public async Task<ActionResult> EditProduct(int id, string name, int price, List<int> categoryIds, int status)
        //{
        //    try
        //    {
        //        var productService = this.Service<IProductService>();
        //        var productCategoryService = this.Service<IProduct_CategoryService>();
        //        var listNotProductCategory = productCategoryService.GetNotExist(categoryIds, id).ToList();

        //        //update product_category

        //        foreach (var categoryId in categoryIds)
        //        {
        //            var pc = productCategoryService.GetByProductIdAndCategory(id, categoryId);
        //            if (pc == null)
        //            {
        //                productCategoryService.Create(new Product_Category()
        //                {
        //                    ProductId = id,
        //                    CategoryId = categoryId,
        //                });
        //            }
        //        }

        //        if (listNotProductCategory != null && listNotProductCategory.Any())
        //        {
        //            foreach (var notPC in listNotProductCategory)
        //            {
        //                await productCategoryService.DeleteAsync(notPC);
        //            }
        //        }

        //        //update product
        //        Product product = productService.GetById(id);
        //        product.Name = name;
        //        product.Price = price;
        //        product.Status = status;
        //        productService.Update(product);


        //        return Json(new
        //        {
        //            success = true,
        //        });
        //    }
        //    catch (Exception e)
        //    {
        //        return Json(new
        //        {
        //            success = false,
        //        });
        //    }
        //}

        //[HttpPost]
        //public ActionResult Delete(int productId)
        //{
        //    try
        //    {
        //        var productCategoryService = this.Service<IProduct_CategoryService>();
        //        var listProductCategry = productCategoryService.GetByProductId(productId).ToList();
        //        foreach (var pc in listProductCategry)
        //        {
        //            productCategoryService.Delete(pc);
        //        }

        //        var productService = this.Service<IProductService>();
        //        var product = productService.GetById(productId);
        //        if (product != null)
        //        {
        //            productService.Delete(product);

        //            return Json(new
        //            {
        //                success = true,
        //            });
        //        }
        //        return Json(new
        //        {
        //            success = false,
        //        });
        //    }
        //    catch (Exception e)
        //    {
        //        return Json(new
        //        {
        //            success = false,
        //        });
        //    }
        //}
    }
    public class ReportTableView
    {
        public string Date { get; set; }
        public string Type { get; set; }
        public double TotalAmount { get; set; }
    }
    public class ReportChartView
    {
        public string Date { get; set; }
        public double TotalNormal { get; set; }
        public double TotalVip { get; set; }
    }
}