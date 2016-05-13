using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xrm.Sdk;
//using Founder.PKURG.CRM;
using System.IO;
using Microsoft.Xrm.Sdk.Client;

namespace Founder.PKURG.EPM.PlugIn.CreateIndex
{
    public class CreateProjectIndexs : PluginsBase, IPlugin
    {
        public void Execute(IServiceProvider serviceProvider)
        {
            base.Initialize(serviceProvider);
            //查询
            Microsoft.Xrm.Sdk.Entity entity = (Microsoft.Xrm.Sdk.Entity)base.Context.InputParameters["Target"];
            //File.AppendAllText("C:\\111.txt", string.Format("{0}\r\n", DateTime.Now));
            OrganizationServiceProxy adminServiceProxy = (OrganizationServiceProxy)this.OrgService;
            adminServiceProxy.CallerId = new Guid("D94BBBD1-2445-E311-9C7B-00155D141823");
            CRM.CRMInfoOP op = new CRM.CRMInfoOP(adminServiceProxy);

            try
            {
                Guid projectId = (Guid)entity["epm_projectinfoid"];

                Common.CreateEmptyObject(op, new
                {
                    tName = "epm_projectplanningindex",
                    id = projectId,
                    rFName = "epm_project",
                    rTName = "epm_projectinfo"
                });

                Common.CreateEmptyObject(op, new
                {
                    tName = "epm_projectdevindex",
                    id = projectId,
                    rFName = "epm_project",
                    rTName = "epm_projectinfo"
                });

                Common.CreateEmptyObject(op, new
                {
                    tName = "epm_projectdevstatus",
                    id = projectId,
                    rFName = "epm_project",
                    rTName = "epm_projectinfo"
                });

            }
            catch (Exception ex)
            {
                File.AppendAllText("C:\\crmlog\\111.txt", string.Format("{2}---{0}{1}\r\n", ex.Message, ex.StackTrace, DateTime.Now));
            }
        }
    }
}
