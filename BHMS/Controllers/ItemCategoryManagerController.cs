using BHMS.CORE.Contract;
using BHMS.CORE.Models;
using Microsoft.AspNet.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace BHMS.Controllers
{
    public class ItemCategoryManagerController : Controller
    {
        IRepository<ItemCategory> context;

        public ItemCategoryManagerController(IRepository<ItemCategory> context)
        {
            this.context = context;
        }
        // GET: ProductManager
        [Authorize]
        public ActionResult Index()
        {

            List<ItemCategory> itemCategories = context.Collection().ToList();
            return View(itemCategories);
        }
        [Authorize]
        public ActionResult Create()
        {
            ItemCategory itemCategory = new ItemCategory();
            
            return View(itemCategory);
        }
       
        [HttpPost]
        public ActionResult Create(ItemCategory itemCategory)
        {
            if (!ModelState.IsValid)
            {
                return View(itemCategory);
            }
            else
            {
                context.Insert(itemCategory);
                context.Commit();

                return RedirectToAction("Index");
            }

        }
        [Authorize]
        public ActionResult Edit(string Id)
        {
            ItemCategory itemCategory = context.Find(Id);
            if (itemCategory == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(itemCategory);
            }
        }

        [HttpPost]
        public ActionResult Edit(ItemCategory item, string Id)
        {
            ItemCategory itemCategoryToEdit = context.Find(Id);

            if (itemCategoryToEdit == null)
            {
                return HttpNotFound();
            }
            else
            {
                if (!ModelState.IsValid)
                {
                    return View(item);
                }

                itemCategoryToEdit.Category = item.Category;

                context.Commit();

                return RedirectToAction("Index");
            }
        }
        [Authorize]
        public ActionResult Delete(string Id)
        {
            ItemCategory itemCategoryToDelete = context.Find(Id);

            if (itemCategoryToDelete == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(itemCategoryToDelete);
            }
        }

        [HttpPost]
        [ActionName("Delete")]
        public ActionResult ConfirmDelete(string Id)
        {
            ItemCategory itemCategoryToDelete = context.Find(Id);

            if (itemCategoryToDelete == null)
            {
                return HttpNotFound();
            }
            else
            {
                context.Delete(Id);
                context.Commit();
                return RedirectToAction("Index");
            }
        }
    }
}