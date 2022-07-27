using System.Globalization;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Place.API.Controllers;

public class BaseController : ControllerBase
{
    //protected string GetIp()
    //{
    //    return GetClientIp();
    //}
    //private string GetClientIp()
    //{
    //    try
    //    {
    //        var ipAddress = HttpContext.Request.HttpContext.Connection.RemoteIpAddress;
    //        var ipv4Addresses = Array.FindAll(Dns.GetHostEntry(ipAddress).AddressList, a => a.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork);

    //        return ipv4Addresses.Any() ? ipv4Addresses.Last().ToString() : null;
    //    }
    //    catch
    //    {
    //        return null;
    //    }
    //}

    public long UserId => User.GetUserId() ?? 0;
}

public static class JwtExtension
{
    public static long? GetUserId(this ClaimsPrincipal user)
    {
        return user.FindFirst(ClaimTypes.NameIdentifier)?.Value.ToLong();
    }

    public static long ToLong(this string number)
    {
        try
        {
            return Int64.Parse(number, CultureInfo.InvariantCulture);
        }
        catch
        {
            return 0;
        }
    }
}