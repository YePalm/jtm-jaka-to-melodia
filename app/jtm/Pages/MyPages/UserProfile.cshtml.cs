using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace jtm.Pages.MyPages
{
    public class UserProfileModel : PageModel
    {
        public String userProfile = "";
        public void OnGet()
        {
            var cookie = Request.Cookies["name"];
            userProfile = cookie;
        }

        public void OnGetLogout()
        {
            if (Request.Cookies["name"] != null || Request.Cookies["id"] != null)
            {
                Response.Cookies.Delete("name");
                Response.Cookies.Delete("id");
            }

            Response.Redirect("../index");
        }

        public void OnGetBack()
        {
            Response.Redirect("../MyPages/Index");
        }
    }
}
