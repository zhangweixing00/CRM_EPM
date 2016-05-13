using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Client;
using System.IO;

namespace Founder.PKURG.EPM.PlugIn.CreateIndex
{
    public class CreateProjectBlockStageIndexs : PluginsBase, IPlugin
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
                Guid projectId = (Guid)entity["epm_projectstageid"];

                Common.CreateEmptyObject(op, new
                {
                    id = projectId,
                    tName = "epm_projectstageplanningindex",
                    rFName = "epm_stage",
                    rTName = "epm_projectstage"
                });

                Common.CreateEmptyObject(op, new
                {
                    id = projectId,
                    tName = "epm_projectstagedevindex",
                    rFName = "epm_stage",
                    rTName = "epm_projectstage"
                });

                Common.CreateEmptyObject(op, new
                {
                    id = projectId,
                    tName = "epm_projectstagedevstatus",
                    rFName = "epm_stage",
                    rTName = "epm_projectstage"
                });

            }
            catch (Exception ex)
            {
                File.AppendAllText("C:\\crmlog\\111.txt", string.Format("{2}---{0}{1}\r\n", ex.Message, ex.StackTrace, DateTime.Now));
            }
        }
    }
}
