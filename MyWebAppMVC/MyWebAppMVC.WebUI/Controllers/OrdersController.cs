using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MyWebAppMVC.DAL;
using MyWebAppMVC.Models;
using MyWebAppMVC.Contracts.Repositories;

namespace MyWebAppMVC.WebUI.Controllers
{
    public class OrdersController : Controller
    {
        IRepositoryBase<Order> orders;
        IRepositoryBase<Product> products;
        public OrdersController(IRepositoryBase<Order> orders, IRepositoryBase<Product> products)
        {
            this.orders = orders;
            this.products = products;
        }

        // GET: Orders
        public ActionResult Index()
        {
            var orders = this.orders.GetAll().Include(o => o.Product);
            return View(orders.ToList());
        }

        // GET: Orders/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = orders.GetById(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // GET: Orders/Create
        public ActionResult Create()
        {
            ViewBag.ProductID = new SelectList(products.GetAll(), "ProductID", "Name");
            return View();
        }

        // POST: Orders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "OrderID,ProductID,Quantity,OrderedDate")] Order order)
        {
            if (ModelState.IsValid)
            {
                orders.Insert(order);
                orders.Commit();
                return RedirectToAction("Index");
            }

            ViewBag.ProductID = new SelectList(products.GetAll(), "ProductID", "Name", order.ProductID);
            return View(order);
        }

        // GET: Orders/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = orders.GetById(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProductID = new SelectList(products.GetAll(), "ProductID", "Name", order.ProductID);
            return View(order);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "OrderID,ProductID,Quantity,OrderedDate")] Order order)
        {
            if (ModelState.IsValid)
            {
                orders.Update(order);
                orders.Commit();
                return RedirectToAction("Index");
            }
            ViewBag.ProductID = new SelectList(products.GetAll(), "ProductID", "Name", order.ProductID);
            return View(order);
        }

        // GET: Orders/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = orders.GetById(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            orders.Delete(id);
            orders.Commit();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                orders.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
