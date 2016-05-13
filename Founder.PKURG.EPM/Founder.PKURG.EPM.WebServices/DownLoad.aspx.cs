using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Founder.PKURG.EPM.CustomPage
{
    public partial class DownLoad : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                Founder.PKURG.Common.DAL.ExecuteService service = new PKURG.Common.DAL.ExecuteService();
                Message.BaseResponse response = service.GetListRequest("custom_GetAttachments", new Object[] { "epm_ProjectInfo", "4BE388FB-575E-E311-898C-00155D141823" });
            }
            catch (Exception ex)
            {
                //LoggerWorker.logger.DebugFormat("Ex--AddProject:{0}\r\n{1}", ex.Message, ex.StackTrace);
                //return false;
            }
        }
    }
}