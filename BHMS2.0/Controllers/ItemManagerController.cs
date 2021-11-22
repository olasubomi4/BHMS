using BHMS.CORE.Contract;
using BHMS.CORE.Models;
using BHMS.CORE.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BHMS2._0.Controllers
{
    public class ItemManagerController : Controller
    {
        IRepository<Item> context;
        IRepository<ItemCategory> itemCategories;

        public ItemManagerController(IRepository<Item> itemContext, IRepository<ItemCategory> itemCategoryContext)
        {
            context = itemContext;
            itemCategories = itemCategoryContext;
        }
        // GET: ProductManager
        public ActionResult Index()
        {
            List<Item> items = context.Collection().ToList();
            return View(items);
        }

        public ActionResult Create()
        {
            ItemManagerViewModel viewModel = new ItemManagerViewModel();
            viewModel.Item = new Item();
            viewModel.ItemCategories = itemCategories.Collection();



            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Create(Item item, HttpPostedFileBase file)
        {
            if (!ModelState.IsValid)
            {
                return View(item);
            }
            else
            {
                if (file != null)
                {

                    item.Image = item.Id + Path.GetExtension(file.FileName);
                    file.SaveAs(Server.MapPath("//Content//ItemImages//") + item.Image);
                }

                context.Insert(item);
                context.Commit();

                return RedirectToAction("index");
            }
        }

        public ActionResult Edit(string Id)
        {
            Item item = context.Find(Id);
            if (item == null)
            {
                return HttpNotFound();
            }
            else
            {
                ItemManagerViewModel viewModel = new ItemManagerViewModel();
                viewModel.Item = item;
                viewModel.ItemCategories = itemCategories.Collection();


                return View(viewModel);
            }
        }
        [HttpPost]
        public ActionResult Edit(Item item, string Id, HttpPostedFileBase file)
        {
            Item itemToEdit = context.Find(Id);
            if (itemToEdit == null)
            {
                return HttpNotFound();
            }
            else
            {

                if (!ModelState.IsValid)
                {
                    return View(item);
                }
                if (file != null)
                {

                    itemToEdit.Image = item.Id + Path.GetExtension(file.FileName);
                    file.SaveAs(Server.MapPath("//Content//ItemImages//") + itemToEdit.Image);
                }




                itemToEdit.SerialNumber = item.SerialNumber;


                itemToEdit.Name = item.Name;
                itemToEdit.Color = item.Color;

                itemToEdit.Category = item.Category;

                context.Commit();

                return RedirectToAction("Index");

            }
        }
        public ActionResult Delete(string Id)
        {
            Item itemToDelete = context.Find(Id);

            if (itemToDelete == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(itemToDelete);
            }
        }

        [HttpPost]
        [ActionName("Delete")]
        public ActionResult confirmDelete(string Id)
        {
            Item itemToDelete = context.Find(Id);
            if (itemToDelete == null)
            {
                return HttpNotFound();
            }
            else
            {
                context.Delete(Id);
                context.Commit();
                return RedirectToAction("index");
            }
        }
    }
}