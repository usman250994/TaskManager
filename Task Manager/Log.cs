using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Task_Manager.Models;

namespace Task_Manager
{
    public class Log
    {
        TaskContext db = new TaskContext();
        public void Create(string tablename, int activid, string createdBy)
        {
            AuditLog audit = new AuditLog();
            audit.actionCode = "Create";
            audit.activityDate = DateTime.Now;
            audit.tableName = tablename;
            audit.activityId = activid;
            audit.createdBy = createdBy;
            db.auditLog.Add(audit);
            db.SaveChanges();
        }
        public void Update(string tablename ,int activid ,string createdBy)
        {
            AuditLog audit = new AuditLog();
            audit.actionCode = "Update";
            audit.activityDate = DateTime.Now;
            audit.tableName = tablename;
            audit.activityId = activid;
            audit.createdBy = createdBy;
            db.auditLog.Add(audit);
            db.SaveChanges();        
        }
        public void Delete(string tablename, int activid, string createdBy)
        {
            AuditLog audit = new AuditLog(); 
            audit.actionCode = "Delete";
            audit.activityDate = DateTime.Now;
            audit.tableName = tablename;
            audit.activityId = activid;
            audit.createdBy = createdBy;
            db.auditLog.Add(audit);
            db.SaveChanges();
        }
    }
}