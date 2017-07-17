using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using Task_Manager.BLL;
using Task_Manager.Models;
using Task_Manager.viewModels.Vendor;

namespace Task_Manager.Controllers
{
    public class VendorApiController : ApiController
    {
        Vendorcreate vendorclass = new Vendorcreate();
        TaskContext db = new TaskContext();

        [HttpGet]
        public VendorDropdown Dropdown(int id)
        {
            VendorDropdown toreturn = new VendorDropdown();
            //For Category
            List<Category> category = new List<Category>();
            category = db.caterory.Where(p => p.enable == true).ToList();
            toreturn.systemCategory = category;
            //For Type
            List<VendorType> types = new List<VendorType>();
            types = db.vendorType.Where(x => x.enable == true).ToList();
            toreturn.type = types;
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
            vendor_Contacts.mobNo = create.usermobNo;
            vendor_Contacts.fax = create.userfax;
            db.vendorContact.Add(vendor_Contacts);
            //*************** Vendor Detail *********************
            Vendor vendors = new Vendor();
            vendors.vendorName = create.vendorName;
            vendors.type = db.vendorType.Where(p => p.id == create.vendortype).FirstOrDefault();
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
            vendors.preferredCourier = create.vendorpreferredCourier;
            vendors.syestemCategory = create.vendorsyestemCategory;
            vendors.vendorContact = vendor_Contacts;
            vendors.createdBy = db.user.Find(userId);
            db.vendor.Add(vendors);
            if (db.SaveChanges() > 0)
            {
                return "Vendor Added Successfully";
            }
            else
            {
                return "Vendor Not Added";
            }
        }
    }
}