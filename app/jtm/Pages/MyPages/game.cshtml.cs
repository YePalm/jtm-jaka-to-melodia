using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using System.Security.Cryptography.X509Certificates;

namespace jtm.Pages.MyPages
{
    public class gameModel : PageModel
    {
        //int rndNumeber = random.Next(1, 7);
        public string sourcePath = "";
        private int songCount = 3;
        public List<SongModel> Songs = new List<SongModel>();
        public void OnGet()
        {
            if (!AuthHelper.IsLoggedIn(Request))
            {
                Response.Redirect("../index");
            }
            
            String connectionString = "Data Source=.\\SQLEXPRESS;Initial Catalog=jtm-database;Integrated Security=True";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                String sql = $"SELECT TOP {songCount} * FROM songs ORDER BY NEWID()";
                using (SqlCommand cmd = new SqlCommand(sql, connection))
                {
                    SqlDataReader sdr = cmd.ExecuteReader();

                    while (sdr.Read())
                    {
                        var songPath = sdr["path"].ToString();
                        var songId = (int)sdr["songId"];
                        var songTitle = sdr["title"].ToString();
                        Songs.Add(new SongModel(songId, songTitle, songPath));
                    }


                    //var songIdcookieOptions = new CookieOptions();
                    //songIdcookieOptions.Expires = DateTime.Now.AddDays(2);
                    //Response.Cookies.Append("songId", songId, songIdcookieOptions);
                }
            }
        }
    }
    public class SongModel
    {
        public SongModel(int songId, string songTitle, string songPath)
        {
            SongId = songId;
            SongTitle = songTitle;
            SongPath = songPath;
        }

        public SongModel()
        {

        }

        public int SongId { get; set; }
        public string SongTitle { get; set; }
        public string SongPath { get; set; }
    }
}
