<%@ WebHandler Language="C#" Class="Photo" %>

using System;
using System.Web;
using System.Linq;

public class Photo : IHttpHandler
{
    public void ProcessRequest(HttpContext context)
    {
        string UserName = string.Empty;
        byte[] content = null;

        HttpResponse Response = context.Response;
        HttpRequest Request = context.Request;

        int id = int.Parse(Request.QueryString["id"]);

        int type = 0;
        Int32.TryParse(context.Request.QueryString["t"], out type);

        var eTag = Convert.ToString(Request.Headers["if-none-match"]) ?? string.Empty;
        string newEtag = null;
        bool isChanged = false;

        if (type == 0)
        {
            using (var file = System.IO.File.Open("App_Data/image.jpg", System.IO.FileMode.Open))
            {
                using(var reader = new System.IO.BinaryReader(file))
	            {
                    content = reader.ReadBytes((int)file.Length);
	            }
            }
            //using (var service = new Onyx.Ess.Services.EmployeePhotoService())
            //{
                //isChanged = service.IsChanged(id, photoSizeID, eTag, ref newEtag);

                //if (isChanged)
                //{
                //    content = service.GetPhotoContent(id, photoSizeID);
                //}

                //if (content != null)
                //{
                //    Response.OutputStream.Write(content, 0, content.Length);
                //}
                //else if (string.IsNullOrEmpty(newEtag))
                //{
                //    var noImageFileName = GetNoPhotoFileName(id, photoSizeID);

                //    newEtag = noImageFileName;

                //    isChanged = newEtag != eTag;

                //    if (isChanged)
                //    {
                //        string filePath = context.Server.MapPath(System.IO.Path.Combine(System.Configuration.ConfigurationManager.AppSettings["NoImagePhotoUrl"], noImageFileName));
                //        using (var stream = new System.IO.FileStream(filePath, System.IO.FileMode.Open, System.IO.FileAccess.Read))
                //        {
                //            content = new byte[stream.Length];
                //            stream.Read(content, 0, content.Length);
                //            Response.OutputStream.Write(content, 0, content.Length);
                //        }
                //    }
                //}

                //if (!isChanged)
                //{
                //    Response.Clear();
                //    Response.StatusCode = (int)System.Net.HttpStatusCode.NotModified;
                //    Response.SuppressContent = true;
                //}
                //else
                //{
                    Response.ContentType = "image/jpeg";
                    Response.Cache.SetETag(newEtag);
                    Response.Cache.SetCacheability(HttpCacheability.Public);
                    Response.BufferOutput = false;
                //}
            //}
        }
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }
}