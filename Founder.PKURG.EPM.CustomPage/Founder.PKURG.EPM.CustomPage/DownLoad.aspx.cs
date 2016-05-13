using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Founder.PKURG.EPM.Common;
using System.Text;

namespace Founder.PKURG.EPM.CustomPage
{
    public partial class DownLoad : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                string attachmentId = Request.QueryString["id"];
                Founder.PKURG.Common.DAL.ExecuteService service = new PKURG.Common.DAL.ExecuteService();
                Message.BaseResponse response = service.GetObjectRequest("custom_GetAttachmentInfo", new Object[] { attachmentId }).ToBaseResponse();

                StringBuilder viewContent = new StringBuilder();

                if (response.returnValue)
                {
                    var data = response.MainData;
                    byte[] fileContent = Convert.FromBase64String(data.DocumentBody);
                    string fileName = data.FileName;
                    if (Request.ServerVariables["http_user_agent"].ToLower().IndexOf("firefox") == -1)
                        fileName = HttpUtility.UrlEncode(fileName, Encoding.UTF8);

                    Response.ContentType = data.MimeType;
                    Response.ContentEncoding = System.Text.Encoding.UTF8;
                    Response.AddHeader("Content-Disposition", "attachment;filename=" + fileName);
                    Response.HeaderEncoding = System.Text.Encoding.UTF8;
                    Response.AddHeader("Content-Length", data.FileSize);
                    Response.BinaryWrite(fileContent);
                    Response.Flush();
                    Response.End();
                }
            }
            catch (Exception ex)
            {
                //LoggerWorker.logger.DebugFormat("Ex--AddProject:{0}\r\n{1}", ex.Message, ex.StackTrace);
                //return false;
            }
        }
    }
}
// Founder.PKURG.EPM.Common.ReturnInfoConvert.

//        IOrganizationService service = Founder.PKURG.CRMCommon.AdminCrmConnect.OrgService;

//        Annotation retrievedAnnotation =
//                (Annotation)_serviceProxy.Retrieve("annotation", _annotationId, cols);
//        Console.WriteLine(", and retrieved.");
//        _fileName = retrievedAnnotation.FileName;


//        if (project != null)
//        {
//            Microsoft.Xrm.Sdk.Entity entity = new Microsoft.Xrm.Sdk.Entity("founder_project");
//            entity["founder_name"] = project.Name;
//            entity["founder_contract_num"] = project.ContractId.ToString();
//            entity["founder_projectabstract"] = project.ContractName;
//            entity["founder_comment"] = project.Remark;
//            entity["founder_lastupdatetime"] = project.LastUpdateTime;
//            entity["founder_constructstate"] = GetOptionSet(project.Status);

//            entity["founder_supplier"] = new EntityReference("founder_supplierbasicinformation", project.SupplierId);
//            if (project.CompanyId != Guid.Empty)
//            {
//                LoggerWorker.logger.Debug(project.CompanyId);
//                entity["founder_company"] = new EntityReference("businessunit", project.CompanyId);
//            }
//            else
//                entity["founder_company"] = project.CompanyId;

//            entity["founder_cooperationtype"] = new EntityReference("founder_secondtypes", project.SecondTypeId);

//            Guid projectId = service.Create(entity);
//            if (projectId.ToString() != "" && projectId.ToString() != "00000000-0000-0000-0000-000000000000")
//            {
//                return true;
//            }

//        }
//        return false;