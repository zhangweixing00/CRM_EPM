using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using Founder.PKURG.CRMExtend;

namespace Founder.PKURG.EPM.WebServices
{
    /// <summary>
    /// WebService 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。
    // [System.Web.Script.Services.ScriptService]
    public class WebService : System.Web.Services.WebService
    {
        CRMInfoOP op = new CRMInfoOP();

        /// <summary>
        /// 增加立项审批事项
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="itemname"></param>
        /// <param name="blockName"></param>
        /// <param name="approvalPerson"></param>
        /// <param name="oa"></param>
        /// <returns></returns>
        [WebMethod]
        public Guid CreateProjectApproval(Guid projectId, long itemname, string blockName, string approvalPerson, string oa)
        {
            return op.Add(new CRMInfo()
            {
                Name = "epm_projectapproval",
                Fields = new List<CRMFieldInfo>()
                {
                      new CRMFieldInfo()
                      {
                            Name="epm_project",
                            Value=projectId,
                            IsForeign=true,
                            ForeignName="epm_projectinfo"
                      },
                      new CRMFieldInfo()
                      {
                            Name="epm_itemname",
                            Value=itemname,
                            IsOptionValue=true
                      },
                      new CRMFieldInfo()
                      {
                            Name="epm_block",
                            Value=blockName
                      },
                      new CRMFieldInfo()
                      {
                            Name="epm_newapproveperson",
                            Value=approvalPerson
                      },
                      new CRMFieldInfo()
                      {
                            Name="epm_oa",
                            Value=oa
                      },
                      new CRMFieldInfo()
                      {
                            Name="epm_starttime",
                            Value=DateTime.Now
                      }
                }
            });
        }

        /// <summary>
        /// 修改立项审批事项
        /// </summary>
        /// <param name="id"></param>
        /// <param name="status">当前状态：0～3</param>
        /// <param name="approvalPerson"></param>
        /// <returns></returns>
        [WebMethod]
        public bool UpDateProjectApproval(Guid id, int status, string approvalPerson)
        {
            CRMInfo info = new CRMInfo();
            info.Name = "epm_projectapproval";
            info.PrimaryField = new CRMFieldInfo()
            {
                Name = "epm_projectapprovalid",
                Value = id
            };
            info.Fields = new List<CRMFieldInfo>();
            info.Fields.Add(new CRMFieldInfo()
            {
                Name = "epm_approvalitemtype",
                Value = status + 919900000,
                 IsOptionValue = true
            });
            info.Fields.Add(new CRMFieldInfo()
            {
                Name = "epm_newapproveperson",
                Value = approvalPerson
            });
            if (status == 3)
            {
                info.Fields.Add(new CRMFieldInfo()
                {
                    Name = "epm_endtime",
                    Value = DateTime.Now
                });
            }
            return op.Update(info);
        }

        [WebMethod]
        public bool UpDateProjectApprovalCustom(string parms)
        {
            ProjectApprovalInfo approvalInfo = ConvertStringToObject<ProjectApprovalInfo>(parms);

            CRMInfo info = new CRMInfo();
            info.Name = "epm_projectapproval";
            info.PrimaryField = new CRMFieldInfo()
            {
                Name = "epm_projectapprovalid",
                Value = approvalInfo.ID
            };

            info.Fields = new List<CRMFieldInfo>();
            info.Fields.Add(new CRMFieldInfo()
            {
                Name = "epm_approvalitemtype",
                Value = approvalInfo.Status + 919900000,
                IsOptionValue = true
            });
            info.Fields.Add(new CRMFieldInfo()
            {
                Name = "epm_newapproveperson",
                Value = approvalInfo.ApprovalPerson
            });
            info.Fields.Add(new CRMFieldInfo()
            {
                Name = "epm_oa",
                Value = approvalInfo.OALink
            });
            if (approvalInfo.Status == 3)
            {
                info.Fields.Add(new CRMFieldInfo()
                {
                    Name = "epm_endtime",
                    Value = DateTime.Now
                });
            }
            return op.Update(info);
        }

        public TTT ConvertStringToObject<TTT>(string xml)
        {
            System.Xml.Serialization.XmlSerializer dcs = new System.Xml.Serialization.XmlSerializer(typeof(TTT));
            using (System.Xml.XmlReader reader = System.Xml.XmlReader.Create(new System.IO.StringReader(xml)))
            {
                TTT result = (TTT)dcs.Deserialize(reader);
                return result;
            }
        }

    }



    public class ProjectApprovalInfo
    {
        public Guid ID { get; set; }
        public int Status { get; set; }
        public string ApprovalPerson { get; set; }
        public string OALink { get; set; }
    }

    //public class ProjectApprovalInfo
    //{
    //    public ProjectApprovalInfo();

    //    public string BlockName { get; set; }
    //    public Guid CaseId { get; set; }
    //    public Guid ID { get; set; }
    //    public ProjectApprovalItem ItemName { get; set; }
    //    public Guid ProjectId { get; set; }
    //    public string UserName { get; set; }
    //}
    //public enum ProjectApprovalItem
    //{
    //    投资立项申请 = 919900000,
    //    立项通知书下发 = 919900001,
    //    立项考核指标调整 = 919900002,
    //}
}
