using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Place.Application.Helper;

public static class HttpHelper
{
    public static ContentResult ForbiddenContent(string contentMessage)
    {
        return new ContentResult
        {
            Content = contentMessage,
            StatusCode = (int)HttpStatusCode.Forbidden,
            ContentType = "text/html"
        };
    }

    public static ContentResult FoundContent(string contentMessage)
    {
        return new ContentResult
        {
            Content = contentMessage,
            StatusCode = (int)HttpStatusCode.Found,
            ContentType = "text/html"
        };
    }


    public static ContentResult CreatedContent(string contentMessage)
    {
        return new ContentResult
        {
            Content = contentMessage,
            StatusCode = (int)HttpStatusCode.Created,
            ContentType = "text/html"
        };
    }

    public static ContentResult NotModifiedContent(string contentMessage)
    {
        return new ContentResult
        {
            Content = contentMessage,
            StatusCode = (int)HttpStatusCode.NotModified,
            ContentType = "text/html"
        };
    }

    public static ContentResult NotImplementedContent(string contentMessage)
    {
        return new ContentResult
        {
            Content = contentMessage,
            StatusCode = (int)HttpStatusCode.NotImplemented,
            ContentType = "text/html"
        };
    }

    public static ContentResult ExceptionContent(string contentMessage)
    {
        return new ContentResult
        {
            Content = contentMessage,
            StatusCode = (int)HttpStatusCode.InternalServerError,
            ContentType = "text/html"
        };
    }

    public static ContentResult AccessDeniedContent(string contentMessage)
    {
        return new ContentResult
        {
            Content = contentMessage,
            StatusCode = (int)HttpStatusCode.BadRequest,
            ContentType = "text/html"
        };
    }

    public static ContentResult InvalidContent(string contentMessage)
    {
        return new ContentResult
        {
            Content = contentMessage,
            StatusCode = (int)HttpStatusCode.BadRequest,
            ContentType = "text/html"
        };
    }

    public static ContentResult FailedContent(string msgContent)
    {
        return new ContentResult
        {
            Content = msgContent,
            StatusCode = (int)HttpStatusCode.BadRequest,
            ContentType = "text/html"
        };
    }

    public static ContentResult NotFoundContent(string contentMessage)
    {
        return new ContentResult
        {
            Content = contentMessage,
            StatusCode = (int)HttpStatusCode.NotFound,
            ContentType = "text/html"
        };
    }
}
