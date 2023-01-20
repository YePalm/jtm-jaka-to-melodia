using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using static jtm.Pages.IndexModel;

namespace jtm.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        public UserInfo userInfo = new UserInfo();
        public String errorMessage = "";
        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {

        }

        public void OnPost()
        {
            userInfo.login = Request.Form["login"];
            userInfo.password = Request.Form["password"];

            String connectionString = "Data Source=.\\SQLEXPRESS;Initial Catalog=jtm-database;Integrated Security=True";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                String sql = "SELECT id, name FROM users where name = @login and password = @password";
                using (SqlCommand cmd = new SqlCommand(sql, connection))
                {
                    cmd.Parameters.AddWithValue("@login", userInfo.login);
                    cmd.Parameters.AddWithValue("@password", userInfo.password);

                    SqlDataReader sdr = cmd.ExecuteReader();

                    if (sdr.Read())
                    {
                        var userId = sdr["id"].ToString();
                        var cookieOptions = new CookieOptions();
                        cookieOptions.Expires = DateTime.Now.AddDays(2);
                        Response.Cookies.Append("id", userId, cookieOptions);

                        var userName = sdr["name"].ToString();
                        var nameCookieOptions = new CookieOptions();
                        nameCookieOptions.Expires = DateTime.Now.AddDays(2);
                        Response.Cookies.Append("name", userName, nameCookieOptions);

                        Response.Redirect("/MyPages/Index");

                        //String sqlQuery = "SELECT id FROM users where name = @login";
                        //using (SqlCommand cmdId = new SqlCommand(sqlQuery, connection))
                        //{
                        //    String userId = "";
                        //    cmdId.Parameters.AddWithValue(sqlQuery, userId);
                        //    var cookieOptions = new CookieOptions();
                        //    cookieOptions.Expires = DateTime.Now.AddDays(2);
                        //    Response.Cookies.Append("id", userId, cookieOptions);

                        //    Response.Redirect("/MyPages/Index");
                        //}
                    }
                    else
                    {
                        errorMessage = "Incorrect Login or Password";
                        return;
                    }
                    connection.Close();
                }
            }
        }

        public class UserInfo
        {
            public String id;
            public String login;
            public String password;
        }
    }
}