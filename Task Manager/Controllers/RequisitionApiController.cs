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
            toReturn.serialNo = (db.requisition.Count() + 8000).ToString();
            return toReturn;
        }
        [HttpPost]
        public void CreateRequisition(createRequisition create)
        {
            var Session = HttpContext.Current.Session;
            int id = Convert.ToInt32(Session["UserID"]);
            Requisition req = new Requisition();
            req.customer = db.customer.Find(create.customerId);
            req.project = db.project.Find(create.projectId);
            req.serialNo = create.serialNo;
            req.createdBy = db.user.Find(id);
            req.createdDate = DateTime.Now;
            req.issued_received_Date = DateTime.Now;
            req.approvedDate = DateTime.Now;
            req.enable = true;
            // now create requisitionItems

            foreach (var entity in create.items)
            {
                requisitionItems items = new requisitionItems();
                items.enable = true;
                items.issuedQuanatity = entity.issueQuant;
                items.itemCode = entity.itemCode;
                items.itemName = entity.itemName;
                items.requiredDate = entity.dateReq;
                items.units = entity.units;
                items.quantity = entity.quantity;
                items.requisition = req;
                db.requisitionItem.Add(items);
            }
            db.requisition.Add(req);
            db.SaveChanges();
        }
        [HttpGet]
        public List<viewRequisition> Grid()
        {
            List<viewRequisition> toReturn = new List<viewRequisition>();
            var req = db.requisition.Where(x => x.enable == true).ToList();
            foreach (var entity in req)
            {
                var item = db.requisitionItem.Where(x => x.requisition.id == entity.id).ToList();
                viewRequisition view = new viewRequisition();
                if (item != null && item.Count > 0)
                {
                    List<string> list = new List<string>();
                    for (int i = 0; i < item.Count; i++)
                    {
                        string item_name = item[i].itemName;
                        string issuedQuanatity = item[i].issuedQuanatity;
                        string itemCode = item[i].itemCode;
                        string quantity = item[i].quantity;
                        string units = item[i].units;
                        view.itemName = view.itemName + item_name + ",";
                        view.itemCode = view.itemCode + itemCode + ",";
                        view.units = view.units + units + ",";
                        view.requiredQuantity = view.requiredQuantity + quantity + ",";
                        view.issuedQuantity = view.issuedQuantity + issuedQuanatity + ",";
                        list.Add(item_name);
                        list.Add(itemCode);
                        list.Add(quantity);
                        list.Add(units);
                        list.Add(issuedQuanatity);
                        view.requiredDate = item[i].requiredDate.ToShortDateString();
                    }

                }

                view.serialNo = entity.serialNo;
                view.customerName = entity.customer.customer_name;
                view.projectName = entity.project.Project_Name;
                view.createdBy = entity.createdBy.user_Name;
                view.createdDate = entity.createdDate.ToShortDateString();
                if (entity.approvedBy == null)
                {
                    view.approvedBy = "Not Approved";
                    view.approvedDate = "Not Approved";
                }
                else
                {
                    view.approvedBy = entity.approvedBy.user_Name;
                    view.approvedDate = entity.approvedDate.ToShortDateString();
                }

                if (entity.issuedBy == null)
                {
                    view.issuedBy = "Not Issued";
                    view.issuedTo = "Not Issued";
                    view.issuedDate = "Not Issued";
                }
                else
                {
                    view.issuedBy = entity.issuedBy.user_Name;
                    view.issuedTo = entity.receivedBy.user_Name;
                    view.issuedDate = entity.issued_received_Date.ToShortDateString();
                }
                view.action = @" <button  class='btn btn-danger  fa fa-times' onclick='deleteRequisition(" + entity.id + ")'/>";
                toReturn.Add(view);
            }
            return toReturn;
        }
        [HttpDelete]
        public void DeleteRequisition(int id)
        {
            var req = db.requisitionItem.Find(id);
            req.enable = false;
            db.SaveChanges();
        }

    }
}
