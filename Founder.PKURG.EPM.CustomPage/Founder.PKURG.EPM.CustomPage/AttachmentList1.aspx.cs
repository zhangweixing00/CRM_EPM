using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using Founder.PKURG.EPM.Common;

namespace Founder.PKURG.EPM.CustomPage
{
    public partial class AttachmentList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                Page.Title = "附件列表";
                string viewName = "epm_ProjectInfo";
                string id = "4BE388FB-575E-E311-898C-00155D141823";
               
                StringBuilder viewContent = new StringBuilder();

                //string viewName =Request.QueryString["viewName"];
                //string id = Request.QueryString["id"];

                if (string.IsNullOrEmpty(viewName) || string.IsNullOrEmpty(id))
                {
                    viewContent.Append("<br/>参数错误<br/>");
                }
                else
                {
                    Founder.PKURG.Common.DAL.ExecuteService service = new PKURG.Common.DAL.ExecuteService();
                    Message.BaseResponse response = service.GetListRequest("custom_GetAttachments", new Object[] { viewName, id }).ToBaseResponse();

                    if (response.returnValue)
                    {
                        if (response.MainData is List<Object>)
                        {
                            foreach (var item in response.MainData)
                            {
                                viewContent.AppendFormat("<a href='DownLoad.aspx?id={3}'>{0}</a>(大小：{1}，创建时间：{2})<br/>",
                                    item.FileName, item.FileSize, item.CreatedOn, item.AnnotationId);
                            }
                        }
                        else
                        {
                            viewContent.Append("<br/>找不到相关附件<br/>");
                        }
                    }
                    else
                    {
                        viewContent.Append("<br/>找不到相关附件<br/>");
                    }
                }
                //Response.Write(viewContent.ToString());
                contentDiv.InnerHtml = viewContent.ToString();
            }
        }
    }
}