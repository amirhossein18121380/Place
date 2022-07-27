
using System.Security.Claims;
using System.Net;


namespace Place.API.ExtensionMethods
{
    public static class HttpContextExtension
    {
        public static string GetIp(this HttpContext context)
        {
            var remoteIpAddress = context.Connection.RemoteIpAddress;
            return remoteIpAddress.ToString();
        }

        public static string GetDeviceName(this HttpContext context)
        {
            var deviceName = Dns.GetHostName();
            return deviceName;
        }

        public static string GetBrowserName(this HttpContext context)
        {
            if (context.Request.Headers.Any(x => x.Key == "User-Agent"))
                return context.Request.Headers["User-Agent"].ToString();
            return "USER-AGENT-NOT-FOUND";
        }

        public static AthenticatedUser GetUser(this HttpContext httpContext)
        {
            if (httpContext.User == null)
                return null;
            if (!httpContext.User.Claims.Any())
                return null;
            var claims = httpContext.User.Claims;
            var MobileNumber = claims?.FirstOrDefault(x => x.Type == ClaimTypes.MobilePhone)?.Value;
            var UserId = long.Parse(claims?.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value);
            var result = new AthenticatedUser()
            {
                UserName = MobileNumber,
                UserId = UserId
            };

            return result;
        }
    }


    public class AthenticatedUser
    {
        public string FullName { get; set; }
        public long UserId { get; set; }
        public string UserName { get; set; }
        public List<string> Roles { get; set; }
    }
}
