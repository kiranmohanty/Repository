using CompanyWeb.HomeService;
using CompanyWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CompanyWeb.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        public ActionResult RedirectToEmployeeFormPage(LoginInfo model)
        {
            try
            {
                if (model != null )
                {
                    if (model.Username == "Admin" && model.Password == "123")
                    {
                        model.EmailID = "a@c.com";
                        return View("EmployeeDetails", model);
                    }
                    else
                    {
                        ViewData["ErrorMessage"] = "Invalid User";
                    }

                }
                {
                    ViewData["ErrorMessage"] = "Invalid Data Binding";
                }               
            }
            catch (Exception ex)
            {
                CompanyWeb.Models.Error.LogError(ex.Message);
            }
            return View("Login", model);
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
       

        [HttpPost]
        [ActionName("ValidateLogIn")]
        public ActionResult Login(LoginInfo loginInfo)
        {
            HomeService.HomeServiceClient client = new HomeServiceClient();
            try
            {
                //using (client = new HomeServiceClient())
                //{
                //    client.Open();
                //    if (client.ValidateLogIn(loginInfo) == 0)
                //    {
                //        loginInfo.Username = string.Empty;                        
                //        loginInfo.ValidationMessage = "Invalid User";
                //        return View("Login", loginInfo);
                //    }
                //    loginInfo.Password = string.Empty;
                //    client.Close();                    
                //}
                return View("main", loginInfo);
            }
            catch (Exception ex)
            {
                if (client != null)
                    client.Close();
                return View("Error", ex.Message);
            }
        }

        public ActionResult Error(string errorMessage)
        {
            ViewBag.ErrorMessage = errorMessage;
            return View();
        }

        public ActionResult Main(LoginInfo loginInfo)
        {
            try
            {
                return View(loginInfo);
            }
            catch (Exception ex)
            {

                return View("Error", ex.Message);
            }
        }

               

        [HttpPost]
        [ActionName("SaveEmployeeDetails")]
        public ActionResult RedirectToEmployeeFormPage(EmployeeAdvanceInfo empInfo)
        {
            HomeServiceClient service = null;
            try
            {
                if (ModelState.IsValid)
                {
                    service = new HomeServiceClient();
                    service.Open();
                    service.SaveEmployeeDetails(empInfo);
                    service.Close();

                    empInfo.ID = -1;
                    empInfo.FirstName = string.Empty;
                    empInfo.LastName = string.Empty;
                    empInfo.EmailID = string.Empty;
                    empInfo.MobileNumber = string.Empty;
                }                
                return  View("EmployeeForm", empInfo);
            }
            catch(Exception ex)
            {
                CompanyWeb.Models.Error.LogError( ex.Message);
                ViewBag.ErrorMessage = ex.Message;
                return View("Error");
            }
        }
    }
}