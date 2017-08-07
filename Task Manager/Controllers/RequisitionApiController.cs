using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using Task_Manager.Models;
using Task_Manager.viewModels;
using Task_Manager.viewModels.Requisition;

namespace Task_Manager.Controllers
{
    public class RequisitionApiController : ApiController
    {
        TaskContext db = new TaskContext();
        [HttpGet]
        public dropdown dropdownList(int id)
        {
            dropdown toReturn = new dropdown();
            var dropcustomer = db.customer.Where(o => o.enable == true).Select(o => new dropCust { name = o.customer_name, id = o.customerId }).ToList();
            toReturn.customer = dropcustomer;
            List<dropProd> dropProj = new List<dropProd>();
            dropProj = db.project.Where(o => o.Enable == true).Select(o => new dropProd { name = o.work_order + "-" + o.Project_Name, id = o.id, custId = o.customer.customerId }).ToList();
            toReturn.project = dropProj;
            return toReturn;
        }
        [HttpPost]
        public void CreateRequisition(List<createRequisition> create)
        {
            var Session = HttpContext.Current.Session;
            int id = Convert.ToInt32(Session["UserID"]);
            foreach (var entity in create)
            {
                Requisition req = new Requisition();
                req.customer = db.customer.Find(entity.customerid);
                req.project = db.project.Find(entity.projectid);
                req.Sno = entity.sno;
                req.itemName = entity.itemName;
                req.itemCode = entity.itemCode;
                req.units = entity.units;
                req.requiredQuantity = entity.quantity;
                req.requiredDate = entity.date;
                req.createdBy = db.user.Find(id);
                req.createdDate = DateTime.Now;
                req.approvedDate = DateTime.Now;
                req.issuedDate = DateTime.Now;
                req.enable = true;

                db.requisition.Add(req);
                db.SaveChanges();
            }
            //if (db.SaveChanges() > 0)
            //{
            //    return "Saved";
            //}
            //else
            //{
            //    return "Not Saved";
            //}

        }
        [HttpGet]
        public List< viewRequisition> Grid()
        {
            List<viewRequisition> toReturn = new List<viewRequisition>();
            var req = db.requisition.Where(a => a.enable == true).ToList();
            viewRequisition view = new viewRequisition();
            foreach (var entity in req)
            {
                view.Sno = entity.Sno;
                view.customerName = entity.customer.customer_name;
                view.projectName = entity.project.Project_Name;
                view.itemName = entity.itemName;
                view.itemCode = entity.itemCode;
                view.units = entity.units;
                view.requiredQuantity = entity.requiredQuantity;
                view.requiredDate = entity.requiredDate.ToString();
                view.issuedQuantity = entity.issuedQuanatity;
                view.createdBy = entity.createdBy.user_Name;
                view.createdDate = entity.createdDate.ToString();
                view.approvedBy = entity.approvedBy.user_Name;
                view.approvedDate = entity.approvedDate.ToString();
                view.issuedBy = entity.issuedBy.user_Name;
                view.issuedTo = entity.issuedTo.user_Name;
                view.issuedDate = entity.issuedDate.ToString();
                toReturn.Add(view);
            }
            return toReturn;
        }


    }
}
