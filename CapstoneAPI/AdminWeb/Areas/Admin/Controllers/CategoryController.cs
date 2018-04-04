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
    public class CategoryController : Controller
    {
        // GET: Admin/Category
        public ActionResult Index()
        {
            return View();
        }

        //public JsonResult CategoryDatatable(JQueryDataTableParamModel param)
        //{
        //    try
        //    {
        //        //var categoryService = this.Service<ICategoryService>();
        //        //var listCategories = categoryService.Get();
        //        if (listCategories == null)
        //        {
        //            return Json(new
        //            {
        //                sEcho = param.sEcho,
        //                iTotalRecords = 0,
        //                iTotalDisplayRecords = 0,
        //                aaData = new List<Product>()
        //            }, JsonRequestBehavior.AllowGet);
        //        }
        //        var categoryList = listCategories.AsEnumerable()
        //            .Where(a => (string.IsNullOrEmpty(param.sSearch) || StringConvert.EscapeName(a.Name).ToLower()
        //                             .Contains(StringConvert.EscapeName(param.sSearch).ToLower())));
        //        int count = 1;
        //        var rp = categoryList
        //            .Skip(param.iDisplayStart).Take(param.iDisplayLength)
        //            .Select(p => new IConvertible[]
        //            {
        //            count++,
        //            p.Name,
        //            p.CategoryId,
        //            });
        //        var total = listCategories.Count();
        //        return Json(new
        //        {
        //            sEcho = param.sEcho,
        //            iTotalRecords = total,
        //            iTotalDisplayRecords = total,
        //            aaData = rp
        //        }, JsonRequestBehavior.AllowGet);
        //    }
        //    catch (Exception e)
        //    {
        //        return Json(new
        //        {
        //            sEcho = param.sEcho,
        //            iTotalRecords = 0,
        //            iTotalDisplayRecords = 0,
        //            aaData = new List<Product>()
        //        }, JsonRequestBehavior.AllowGet);
        //    }
        //}

        //[HttpPost]
        //public ActionResult GetAllCategory()
        //{
        //    try
        //    {
        //        var categoryService = this.Service<ICategoryService>();
        //        var listCategory = categoryService.Get().Select(c => new
        //        {
        //            CategoryId = c.CategoryId,
        //            Name = c.Name,
        //        }).ToList();
        //        if (listCategory == null)
        //        {
        //            return Json(new
        //            {
        //                success = true,
        //                aoData = new List<Category>(),
        //            }, JsonRequestBehavior.AllowGet);
        //        }
        //        return Json(new
        //        {
        //            success = true,
        //            aoData = listCategory,
        //        }, JsonRequestBehavior.AllowGet);
        //    }
        //    catch (Exception e)
        //    {
        //        return Json(new
        //        {
        //            success = true,
        //            aoData = new List<Category>(),
        //        }, JsonRequestBehavior.AllowGet);
        //    }
        //}

        //public async Task<ActionResult> CreateCategory(string name)
        //{
        //    try
        //    {
        //        var categoryService = this.Service<ICategoryService>();
        //        await categoryService.CreateAsync(new Category()
        //        {
        //            Name = name,
        //        });
        //        return Json(new
        //        {
        //            success = true,
        //        });
        //    }
        //    catch (Exception)
        //    {
        //        return Json(new
        //        {
        //            success = true,
        //        });
        //    }
        //}

        //[HttpPost]
        //public ActionResult Delete(int categoryId)
        //{
        //    try
        //    {
        //        var productCategoryService = this.Service<IProduct_CategoryService>();
        //        var product_category = productCategoryService.GetByCategoryId(categoryId);
        //        if (product_category.Count() == 0)
        //        {
        //            var categoryService = this.Service<ICategoryService>();
        //            var category = categoryService.GetById(categoryId).FirstOrDefault();
        //            if (category != null)
        //            {
        //                categoryService.Delete(category);
        //                return Json(new
        //                {
        //                    success = true,
        //                });
        //            }
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

        //[HttpPost]
        //public ActionResult GetDetail(int categoryId)
        //{
        //    try
        //    {
        //        var categoryService = this.Service<ICategoryService>();
        //        var category = categoryService.GetById(categoryId).FirstOrDefault();
        //        if (category != null)
        //        {
        //            return Json(new
        //            {
        //                success = true,
        //                id = category.CategoryId,
        //                name = category.Name,
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
        //public async Task<ActionResult> EditCategory(string name, int id)
        //{
        //    try
        //    {
        //        var categoryService = this.Service<ICategoryService>();
        //        var category = categoryService.GetById(id).FirstOrDefault();
        //        if (category != null)
        //        {
        //            category.Name = name;
        //            await categoryService.UpdateAsync(category);
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
        //    catch (Exception)
        //    {
        //        return Json(new
        //        {
        //            success = true,
        //        });
        //    }
        //}
    }
}