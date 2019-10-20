using MyShop.Core.Models;
using MyShop.DataAcess.InMemory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyShop.WebUI.Controllers
{
    public class ProductCategoryController : Controller
    {


        InMemoryRepository<ProductCategory> context;

        public ProductCategoryController()
        {
            context = new InMemoryRepository<ProductCategory>();
        }

        // GET: Product
        public ActionResult Index()
        {
            List<ProductCategory> products = context.Collection().ToList();
            

            return View(products);
        }

        public ActionResult Create()
        {
            ProductCategory product = new ProductCategory();

            return View();
        }

        [HttpPost]
        public ActionResult Create(ProductCategory product)
        {
            if (!ModelState.IsValid)
            {
                return View(product);
            }
            else
            {
                context.Insert(product);
                context.Commit();

                return RedirectToAction("Create");
            }
        }



        public ActionResult Edit(string Id)
        {
            ProductCategory product = context.Find(Id);

            if (product == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(product);
            }
        }


        [HttpPost]
        public ActionResult Edit(ProductCategory product, string Id)
        {
            ProductCategory productToEdit = context.Find(Id);


            if (productToEdit == null)
            {
                return HttpNotFound();
            }
            else
            {
                if (!ModelState.IsValid)
                {
                    return View(product);
                }

                productToEdit.Category = product.Category;
                

                context.Commit();

                return RedirectToAction("Index");
            }

        }


        public ActionResult Delete(String Id)
        {
            ProductCategory productToDelete = context.Find(Id);

            if (productToDelete == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(productToDelete);
            }
        }


        [HttpPost]
        [ActionName("Delete")]
        public ActionResult ConfirmDelete(string Id)
        {
            ProductCategory productToDelete = context.Find(Id);

            if (productToDelete == null)
            {
                return HttpNotFound();
            }
            else
            {
                context.Delete(Id);
                return RedirectToAction("Index");
            }

        }
    }
}