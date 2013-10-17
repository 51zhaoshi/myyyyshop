namespace Maticsoft.Web.Ajax_Handle
{
    using Maticsoft.BLL.CMS;
    using Maticsoft.Model.CMS;
    using System;
    using System.Data;
    using System.Web;

    public class EditPhotoHandle : IHttpHandler
    {
        public void EditAlbumName(HttpRequest Request, HttpResponse Response)
        {
            if (!string.IsNullOrWhiteSpace(Request.Params["AlbumName"]) && !string.IsNullOrWhiteSpace(Request.Params["AlbumId"]))
            {
                string str = Request.Params["AlbumName"];
                int albumID = int.Parse(Request.Params["AlbumID"]);
                Maticsoft.BLL.CMS.PhotoAlbum album = new Maticsoft.BLL.CMS.PhotoAlbum();
                Maticsoft.Model.CMS.PhotoAlbum model = album.GetModel(albumID);
                model.AlbumName = str;
                Response.Write(!album.Update(model) ? "" : str);
            }
        }

        public void EditCover(HttpRequest Request, HttpResponse Response)
        {
            Maticsoft.BLL.CMS.Photo photo = new Maticsoft.BLL.CMS.Photo();
            Maticsoft.BLL.CMS.PhotoAlbum album = new Maticsoft.BLL.CMS.PhotoAlbum();
            DataSet list = photo.GetList("photoid=" + Request.Params["PhotoId"]);
            if (list != null)
            {
                string s = list.Tables[0].Rows[0]["AlbumID"].ToString();
                Maticsoft.Model.CMS.PhotoAlbum model = album.GetModel(int.Parse(s));
                model.CoverPhoto = new int?(int.Parse(Request.Params["PhotoId"]));
                Response.Write(album.Update(model) ? "Success" : "Fail");
            }
        }

        public void EditPhotoName(HttpRequest Request, HttpResponse Response)
        {
            if (!string.IsNullOrWhiteSpace(Request.Params["PhotoName"]) && !string.IsNullOrWhiteSpace(Request.Params["PhotoId"]))
            {
                string str = Request.Params["PhotoName"];
                int photoID = int.Parse(Request.Params["PhotoId"]);
                Maticsoft.BLL.CMS.Photo photo = new Maticsoft.BLL.CMS.Photo();
                Maticsoft.Model.CMS.Photo model = photo.GetModel(photoID);
                model.PhotoName = str;
                Response.Write(!photo.Update(model) ? "" : str);
            }
        }

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            HttpRequest request = context.Request;
            HttpResponse response = context.Response;
            string str = request.Params["Action"];
            string str2 = str;
            if (str2 != null)
            {
                if (!(str2 == "EditPhotoName"))
                {
                    if (!(str2 == "EditCover"))
                    {
                        if (str2 == "EditAlbumName")
                        {
                            this.EditAlbumName(request, response);
                        }
                        return;
                    }
                }
                else
                {
                    this.EditPhotoName(request, response);
                    return;
                }
                this.EditCover(request, response);
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
}

