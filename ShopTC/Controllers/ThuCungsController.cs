﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ShopTC.Models;

namespace ShopTC.Controllers
{
    public class ThuCungsController : Controller
    {
        private QLBHTCEntities db = new QLBHTCEntities();

        // GET: ThuCungs
        public ActionResult Index()
        {
            if (Session["ID"] == null || Session["ID"].ToString() == "")
            {
                return RedirectToAction("Index2", "ThuCungs");

            }
            else
            {
                var thuCungs = db.ThuCung.Include(t => t.LoaiThuCung);
                ViewBag.MaLoai = new SelectList(db.LoaiThuCung, "MaLoai", "TenLoai");
                return View(thuCungs.ToList());

            }
        }

        public ActionResult Index2()
        {

            return View(db.ThuCung.ToList());
        }

        // GET: ThuCungs/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ThuCung thuCung = db.ThuCung.Find(id);
            if (thuCung == null)
            {
                return HttpNotFound();
            }
            return View(thuCung);
        }

        // GET: ThuCungs/Create
        public ActionResult Create()
        {
            ViewBag.MaLoai = new SelectList(db.LoaiThuCung, "MaLoai", "TenLoai");
            ViewBag.MaCC = new SelectList(db.NHACC, "MaCC", "TenCC");
            return View();
        }

        // POST: ThuCungs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaTC,TenTC,GiaTC,SoLuong,GT,MoTa,HinhAnhMH,MaLoai,MaCC,NgaySinh")] ThuCung thuCung)
        {
            if (ModelState.IsValid)
            {
                db.ThuCung.Add(thuCung);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MaLoai = new SelectList(db.LoaiThuCung, "MaLoai", "TenLoai", thuCung.MaLoai);
            ViewBag.MaCC = new SelectList(db.NHACC, "MaCC", "TenCC", thuCung.MaCC);
            return View(thuCung);
        }

        // GET: ThuCungs/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ThuCung thuCung = db.ThuCung.Find(id);
            if (thuCung == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaLoai = new SelectList(db.LoaiThuCung, "MaLoai", "TenLoai", thuCung.MaLoai);
            ViewBag.MaCC = new SelectList(db.NHACC, "MaCC", "TenCC", thuCung.MaCC);
            return View(thuCung);
        }

        // POST: ThuCungs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaTC,TenTC,GiaTC,SoLuong,GT,MoTa,HinhAnhMH,MaLoai,MaCC,NgaySinh")] ThuCung thuCung)
        {
            if (ModelState.IsValid)
            {
                db.Entry(thuCung).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaLoai = new SelectList(db.LoaiThuCung, "MaLoai", "TenLoai", thuCung.MaLoai);
            ViewBag.MaCC = new SelectList(db.NHACC, "MaCC", "TenCC", thuCung.MaCC);
            return View(thuCung);
        }

        // GET: ThuCungs/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ThuCung thuCung = db.ThuCung.Find(id);
            if (thuCung == null)
            {
                return HttpNotFound();
            }
            return View(thuCung);
        }

        // POST: ThuCungs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            ThuCung thuCung = db.ThuCung.Find(id);
            db.ThuCung.Remove(thuCung);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
