using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using System.Security.Cryptography.X509Certificates;

namespace jtm.Pages.MyPages
{
    public class IndexModel : PageModel
    {
        public String userProfile = "";
        public void OnGet()
        {
            var cookie = Request.Cookies["name"];
            userProfile = cookie;
        }

        //public ActionResult OnPost()
        //{
        //    Response.Redirect("../index");
        //}

        public void OnGetLogout()
        {
            if (Request.Cookies["name"] != null || Request.Cookies["id"] != null)
            {
                Response.Cookies.Delete("name");
                Response.Cookies.Delete("id");
            }

            Response.Redirect("../index");
        }

        public void OnGetUserProfile()
        {
            Response.Redirect("/MyPages/UserProfile");
        }

        public void OnGetBack()
        {
            Response.Redirect("../MyPages/Index");
        }

        public void OnGetStartGame()
        {
            Response.Redirect("/MyPages/game");
        }
    }
}
