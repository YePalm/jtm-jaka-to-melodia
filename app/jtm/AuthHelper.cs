namespace jtm
{
    public static class AuthHelper
    {
        public static bool IsLoggedIn(HttpRequest request)
        {
            if (request.Cookies["name"] != null || request.Cookies["id"] != null)
            {
                return true;
            }

            else
            {
                return false;
            }
        }
    }
}
