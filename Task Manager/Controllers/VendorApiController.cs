using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using Task_Manager.BLL;
using Task_Manager.Models;
using Task_Manager.viewModels;
using Task_Manager.viewModels.Vendor;

namespace Task_Manager.Controllers
{
    public class VendorApiController : ApiController
    {
        Log log = new Log();
        Vendorcreate vendorclass = new Vendorcreate();
        TaskContext db = new TaskContext();

        [HttpGet]
        public VendorDropdown Dropdown(int id)
        {
            VendorDropdown toreturn = new VendorDropdown();
            //For Category
            List<dropCust> category = new List<dropCust>();
            category = db.caterory.Where(p => p.enable == true).Select(o => new dropCust { id = o.id, name = o.name }).ToList();
            toreturn.systemCategory = category;
            //For Type
            List<dropCust> types = new List<dropCust>();
            types = db.vendorType.Where(p => p.enable == true).Select(o => new dropCust { id = o.id, name = o.type_Name }).ToList();
            toreturn.type = types;
            //For Update
            CreateVendor obj = new CreateVendor();
            var Session = HttpContext.Current.Session;
            int vendorid = Convert.ToInt32(Session["vendorId"]);
            Session["vendorId"] = null;
            var vendor = db.vendor.Find(vendorid);
            if (vendor != null)
            {
                obj.vendorid = vendorid;
                obj.vendorName = vendor.vendorName;
                obj.vendorNote = vendor.note;
                obj.vendorcity = vendor.city;
                obj.payType = vendor.type.id;
                obj.vendoraddress = vendor.address;
                obj.vendormobNo = vendor.mobNo.Substring(3, 10);
                obj.vendorteleNo = vendor.teleNo;
                obj.vendorEmail = vendor.Email;
                obj.vendorwebsite = vendor.website;
                obj.vendorfax = vendor.fax;
                obj.isActive = vendor.isActive;
                obj.isApprove = vendor.isActive;
                obj.vendorcreditLimit = vendor.creditLimit;
                obj.vendordays = vendor.days;
                obj.vendorpreferredCourier = vendor.preferredCourier;
                obj.user_name = vendor.vendorContact.name;
                obj.userregNo = vendor.vendorContact.regNo;
                obj.usernic = vendor.vendorContact.nic;
                obj.usertitle = vendor.vendorContact.title;
                obj.useremail = vendor.vendorContact.email;
                obj.usermobNo = vendor.vendorContact.mobNo.Substring(3, 10);
                obj.usertelephoneNo = vendor.vendorContact.telephoneNo;
                obj.userfax = vendor.vendorContact.fax;


                foreach (var ent in vendor.categories)
                {

                    obj.vendorsystemCategory.Add(new dropCust() { id = ent.id, name = ent.name });

                }
                toreturn.vend = obj;
            }
            return toreturn;
        }

        [HttpPost]
        public string CreateVendor(CreateVendor create)
        {
            var sessionId = HttpContext.Current.Session;
            int userId = Convert.ToInt32(sessionId["UserID"]);


            //*************** Vendor Contact Detail *********************
            VendorContact vendor_Contacts = new VendorContact();
            vendor_Contacts.name = create.user_name;
            vendor_Contacts.nic = create.usernic;
            vendor_Contacts.regNo = create.userregNo;
            vendor_Contacts.telephoneNo = create.usertelephoneNo;
            vendor_Contacts.title = create.usertitle;
            vendor_Contacts.email = create.useremail;
            vendor_Contacts.mobNo = "+92" + create.usermobNo;
            vendor_Contacts.fax = create.userfax;

            //*************** Vendor Detail *********************
            Vendor vendors = new Vendor();
            vendors.vendorName = create.vendorName;
            vendors.type = db.vendorType.Find(create.payType);
            vendors.address = create.vendoraddress;
            vendors.city = create.vendorcity;
            vendors.mobNo = "+92" + create.vendormobNo;
            vendors.teleNo = create.vendorteleNo;
            vendors.Email = create.vendorEmail;
            vendors.website = create.vendorwebsite;
            vendors.fax = create.vendorfax;
            vendors.isActive = create.isActive;
            vendors.isApprove = create.isApprove;
            vendors.creditLimit = create.vendorcreditLimit;
            vendors.days = create.vendordays;
            vendors.enable = true;
            vendors.note = create.vendorNote;
            vendors.preferredCourier = create.vendorpreferredCourier;
            vendors.vendorContact = vendor_Contacts;
            vendors.createdBy = db.user.Find(userId);
            foreach (var item in create.vendorsystemCategory)
            {
                var items = db.caterory.Find(item.id);
                vendors.categories.Add(items);
            }

            var vendor = db.vendor.Find(create.vendorid);
            if (vendor == null)
            {
                db.vendorContact.Add(vendor_Contacts);
                db.vendor.Add(vendors);
            }
            else
            {
                vendors.id = create.vendorid;
                db.Entry(vendor).CurrentValues.SetValues(vendors);
            }


            if (db.SaveChanges() > 0)
            {
                log.Create("Vendor", vendors.id, userId.ToString());
                return "Successfully";
            }
            else
            {
                return "Vendor Not Added";
            }


        }

        [HttpGet]
        public List<VendorDetail> VendorGrid()
        {
            var vednorList = db.vendor.Where(p => p.enable == true).ToList();
            List<VendorDetail> toReturn = new List<VendorDetail>();
            foreach (var entity in vednorList)
            {
                VendorDetail vd = new VendorDetail();
                vd.vendorName = entity.vendorName;
                vd.vendorAddress = entity.address;
                vd.vendorCity = entity.city;
                vd.vendorEmail = entity.Email;
                vd.vendorMobile = entity.mobNo;
                vd.Note = entity.note;
                vd.vendorCreditlimit = entity.creditLimit;
                vd.vendorPrefereedcourier = entity.preferredCourier;
                vd.vendorWebsite = entity.website;
                vd.vendorDays = entity.days;
                vd.vendorPaymenttype = entity.type.type_Name;
                vd.vendorActive = entity.isActive;
                vd.action = @"<button value='Update' class='btn btn-primary fa fa-cog' id='upd' onclick='UpdateVendor(" + entity.id + ")'/><button  class='btn btn-danger fa fa-times' onclick='DeleteVendor(" + entity.id + ")'></button> <button  class='btn btn-info fa fa-comments-o' onclick='DetailVendor(" + entity.id + ")'></button>";
                toReturn.Add(vd);
            }
            return toReturn;
        }

        [HttpDelete]
        public void Deletevendor(int id)
        {
            var vendor = db.vendor.Find(id);
            vendor.enable = false;
            db.SaveChanges();

        }
        [HttpPost]
        public void vendorId(int id)
        {
            var Session = HttpContext.Current.Session;
            Session["vendorId"] = id;
        }
    }
}