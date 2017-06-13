using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using Task_Manager.Models;
using Task_Manager.viewModels;

namespace Task_Manager.Controllers
{
    public class InventoryApiController : ApiController
    {
        TaskContext db = new TaskContext();
        [HttpGet]
        public List<Category> dropdown()
        {
            List<Category> cat = new List<Category>();
            cat = db.caterory.ToList();
            return cat;
        }

        [HttpPost]
        public string CreateProduct(requestproduct prod)
        {

            if (System.Web.HttpContext.Current.Request.Files.AllKeys.Any())
            {
                var picture = System.Web.HttpContext.Current.Request.Files["logo"];
            }

            Product pro = new Product();
            var session = HttpContext.Current.Session;
            pro.enable = true;
            pro.barcode = prod.barcode;
            pro.brand = prod.brand;
            pro.product_name = prod.name;
            pro.quantity = prod.quantity;
            pro.user = db.user.Find(Convert.ToInt32(session["UserID"]));
            pro.category = db.caterory.Find(prod.catogrey);
            pro.inward_date = prod.inwarddate;
            pro.created_on = DateTime.Now;
            pro.description = prod.description;


            pro.id = 0;
            if (prod.type == 0)
                pro.islocal = true;
            else
                pro.islocal = false;
            pro.model = prod.model;
            pro.vendor_name = prod.name;

            db.product.Add(pro);

            if (db.SaveChanges() > 0)
            {
                var id = db.product.OrderByDescending(p => p.id).FirstOrDefault().id;
                if (prod.imagepath == "")
                {
                    pro.image = "/Images/0.jpg";

                }
                else
                {
                    pro.image = "/Images/" + id + ".jpg";
                }
                db.SaveChanges();
                return "Product Detail Added";
            }

            return "Product Not Added";
        }

        [HttpPut]
        public List<responseInventory> list()
        {
            List<responseInventory> res = new List<responseInventory>();



            var prod = db.product.Where(p => p.enable == true).ToList();
            foreach (var entity in prod)
            {
                responseInventory respon = new responseInventory();
                respon.barcode = entity.barcode;
                respon.quantity = entity.quantity;
                respon.brand = entity.brand;
                respon.catogrey = entity.category.name;
                respon.description = entity.description;
                respon.name = entity.product_name;
                respon.inward = entity.inward_date;
                if (entity.islocal == true)
                {
                    respon.type = "Local";
                }
                else
                {
                    respon.type = "Imported";
                }
                //  respon.type = entity.islocal;
                respon.vendorName = entity.vendor_name;
                respon.model = entity.model;
                respon.action = @"<button value='Update' class='btn btn-primary fa fa-cog' id='upd' onclick='preUpdate(" + entity.id + ")'/> <button  class='btn btn-danger  fa fa-times' onclick='deleteUser(" + entity.id + ")'/> <button  style= margin-right: 5px;   class='btn btn-Primary  fa fa-eye' onclick='Detailproduct(" + entity.id + ")'/>";
                res.Add(respon);
            }
            return res;
        }

        [HttpPost]
        public int set(int id)
        {
            var Session = HttpContext.Current.Session;
            Session["Product_id"] = id;
            return 0;
        }
        [HttpGet]
        public ViewProductDetail DetailProduct(int id)
        {
            string type, catogrey;
            var Session = HttpContext.Current.Session;
            string abc = Session["Product_id"].ToString();
            ViewProductDetail view = new ViewProductDetail();
            Product prod = new Product();
            prod = db.product.Find(Convert.ToInt32(abc));
            view.name = prod.product_name;
            if (prod.islocal == true)
            {
                type = "Local";
            }
            else
            {
                type = "Imported";
            }
            view.type = type;
            view.Catogrey = prod.category.ToString();
            view.brand = prod.brand;
            view.model = prod.model;
            view.barcode = prod.barcode;
            view.inward_date = prod.inward_date;
            view.description = prod.description;
            view.quantity = prod.quantity;
            view.vendor_name = prod.vendor_name;
            view.img_path = prod.image;
            return view;
        }
    }
}
