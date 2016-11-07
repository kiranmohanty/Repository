using CompanyWeb.Models.Box8;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

namespace CompanyWeb.Controllers
{   
    public class Box8Controller : BaseController
    {        
        // GET: Box8
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult GetsearchedLocations(string seachedLocation)
        {
            seachedLocation.Replace("'", "''");
            seachedLocation = "'" + seachedLocation + "'";
            IList<LocationDetails> AvailableLocations = null;            
            DataSet tables = DataBaseFactory.ExecuteCommand("exec GetSearchedLocationdetails " + seachedLocation);
            if (tables != null && tables.Tables.Count>0 && tables.Tables[0].Rows.Count > 0)
            {
                AvailableLocations = new List<LocationDetails>();
                foreach (DataRow row in tables.Tables[0].Rows)
                {
                    AvailableLocations.Add(new LocationDetails() { ID = row["AreaID"].ToIntFromObject(), Name = row["AreaName"].ToTrimedString(), LocationType = (int)row["IsLocalArea"] });
                }
            }
            return Json(AvailableLocations, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ShowAvailableMenu(int LocationID)
        {
            IList<Category> AvailableCategories = null;
            DataSet tables = DataBaseFactory.ExecuteCommand("exec GetLocationWiseAvailableMenu " + LocationID);
            if (tables != null && tables.Tables.Count > 0 && tables.Tables[0].Rows.Count > 0)
            {
                string ImagePath = ConfigurationManager.AppSettings["ImagePath"].ToTrimedString();
                AvailableCategories = new List<Category>();
                foreach (DataRow row in tables.Tables[0].Rows)
                {
                    AvailableCategories.Add(new Category() { ID = row["CategoryID"].ToIntFromObject(), Name = row["CategoryName"].ToTrimedString(), ImagePath = ImagePath+ row["CategoryImage"].ToTrimedString() });
                }
            }            
            return Json(AvailableCategories, JsonRequestBehavior.AllowGet);
        }

        public ActionResult RenderAvailableMenuView()
        {
            return View("Menu", null);
        }
    }
}