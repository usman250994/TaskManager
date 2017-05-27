using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Task_Manager.viewModels
{
    public class clientsTicketRespone
    {
           public int id {get;set;}
           public DateTime  date {get;set;}
           public string   status{get;set;}
           public string   description {get;set;}
           public string name { get; set; }

    }
}