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
        public bool CreateProduct(requestproduct prod)
        {

            Product pro = new Product();
            var session = HttpContext.Current.Session;
            pro.enable = true;
             pro.barcode=prod.barcode;
            pro.brand=prod.brand;
            pro.product_name = prod.name;
            pro.user = db.user.Find(Convert.ToInt32(session["UserID"]));
            pro.category=db.caterory.Find(prod.catogrey);
            pro.description=prod.description;
            pro.id=0;
            if(prod.type==0)
            pro.islocal=true;
            else
             pro.islocal=false;
            pro.model=prod.model;
            pro.vendor_name=prod.name;

                db.product.Add(pro);
            
            if(db.SaveChanges()>0)
            {
                return true;
            }

            return false;
        }


        [HttpPut]
        public List<responseInventory> list()
        {
            List<responseInventory> res = new List<responseInventory>();

            responseInventory respon = new responseInventory();

            var prod = db.product.Where(p => p.enable == true).ToList();
            foreach (var entity in prod)
            {            
            respon.barcode=entity.barcode;
                respon.quantity=entity.quantity;
                respon.brand   =entity.brand;
                    respon.catogrey  =entity.category.name;
                    respon.description   =entity.description;
                    respon.name   =entity.product_name;
                    respon.type   =entity.islocal;
                    respon.vendorName   =entity.vendor_name;
                        respon.model   =entity.model;
                        res.Add(respon);
            }
            return res;
        }


    }
}
