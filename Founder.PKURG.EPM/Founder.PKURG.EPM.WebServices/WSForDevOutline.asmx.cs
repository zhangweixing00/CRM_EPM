using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace Founder.PKURG.EPM.WebServices
{
    /// <summary>
    /// WSForDevOutline 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。
    // [System.Web.Script.Services.ScriptService]
    public class WSForDevOutline : System.Web.Services.WebService
    {
        EPM_MSCRMEntities context = new EPM_MSCRMEntities();

        [WebMethod]
        public ProjectApprovalIndexs GetProjectApprovalIndexsById(string projectIdString)
        {
            Guid projectId = new Guid(projectIdString);
            ProjectApprovalIndexs indexs = new ProjectApprovalIndexs();

            epm_ProjectApproval projectApproval = context.epm_ProjectApproval.Where(x => x.epm_Project.HasValue && x.epm_Project.Value == projectId)
                 .OrderByDescending(x => x.epm_EndTime)
                 .FirstOrDefault();
            if (projectApproval != null)
            {
                indexs.DirectDevCost = projectApproval.epm_directdevcost;
                indexs.FinishTime = projectApproval.epm_finishbackup;
                indexs.TotalBuildingArea = projectApproval.epm_totalbuildingarea;
                indexs.TotalInvestment = projectApproval.epm_totalinvestment;
                indexs.TotalSaleArea = projectApproval.epm_totalsalearea;

                epm_incomeindex_second incomeIndex = context.epm_incomeindex_second.Where(x => x.epm_item == projectApproval.epm_ProjectApprovalId).SingleOrDefault();
                if (incomeIndex != null)
                {
                    indexs.PrepareProfileTax = incomeIndex.epm_prepareprofiletax;
                    indexs.AfterTaxRate = incomeIndex.epm_aftertaxrate;
                    indexs.AfterTaxRateInner = incomeIndex.epm_aftertaxrateinner;
                }

                List<epm_salemoneyplan_second> salePlan = context.epm_salemoneyplan_second.Where(x => x.epm_item == projectApproval.epm_ProjectApprovalId).ToList();
                if (salePlan != null)
                {
                    salePlan.ForEach(x =>
                        {
                            indexs.YearSalePlan.Add(new FeeInfo()
                            {
                                Year = int.Parse(x.epm_newyear),
                                Fee = x.epm_money
                            });
                        });
                }

                List<epm_salebackmoney_second> saleSaleReturnPlan = context.epm_salebackmoney_second.Where(x => x.epm_item == projectApproval.epm_ProjectApprovalId).ToList();
                if (saleSaleReturnPlan != null)
                {
                    saleSaleReturnPlan.ForEach(x =>
                        {
                            indexs.YearSaleReturnPlan.Add(new FeeInfo()
                            {
                                Year = int.Parse(x.epm_newyear),
                                Fee = x.epm_money
                            });
                        });
                }

               

                List<epm_ProjectApprovalMoney> lendToGroupPlan = context.epm_ProjectApprovalMoney.Where(x => x.epm_ProjectApprovalItem == projectApproval.epm_ProjectApprovalId&&
                    x.epm_type == (int)ExchangeWithGroupType.向集团借款).ToList();
                if (lendToGroupPlan != null)
                {
                    lendToGroupPlan.ForEach(x =>
                        {
                            indexs.YearlendToGroupPlan.Add(new FeeInfo()
                            {
                                Year = int.Parse(x.epm_newYear),
                                Fee = x.epm_Money
                            });
                        });
                }

                List<epm_ProjectApprovalMoney> refundToGroupPlan = context.epm_ProjectApprovalMoney.Where(x => x.epm_ProjectApprovalItem == projectApproval.epm_ProjectApprovalId &&
                    x.epm_type == (int)ExchangeWithGroupType.归还集团借款).ToList();
                if (refundToGroupPlan != null)
                {
                    refundToGroupPlan.ForEach(x =>
                        {
                            indexs.YearRefundToGroupPlan.Add(new FeeInfo()
                            {
                                Year = int.Parse(x.epm_newYear),
                                Fee = x.epm_Money
                            });
                        });
                }
            }

            return indexs;
        }
    }

    public enum ExchangeWithGroupType
    {
        归还集团借款=919900002,
        向集团借款 = 919900003
    }

    public class ProjectApprovalIndexs
    {
        /// <summary>
        /// 预征税收利润额（万元）
        /// </summary>
        public decimal? PrepareProfileTax { get; set; }

        /// <summary>
        /// 预征税后利润率(%)
        /// </summary>
        public decimal? AfterTaxRate { get; set; }

        /// <summary>
        /// 预征税后全投资内部收益率(%)
        /// </summary>
        public decimal? AfterTaxRateInner { get; set; }

        /// <summary>
        /// 建设总投资（万元）
        /// </summary>
        public decimal? TotalInvestment { get; set; }

        /// <summary>
        /// 直接开发成本
        /// </summary>
        public decimal? DirectDevCost { get; set; }

        /// <summary>
        /// 总建筑面积（平米）
        /// </summary>
        public decimal? TotalBuildingArea { get; set; }

        /// <summary>
        /// 总销售面积（平米）
        /// </summary>
        public decimal? TotalSaleArea { get; set; }

        /// <summary>
        /// 整体竣工备案
        /// </summary>
        public DateTime? FinishTime { get; set; }

        /// <summary>
        /// 年度销售计划
        /// </summary>
        public List<FeeInfo> YearSalePlan { get; set; }

        /// <summary>
        /// 年度销售回款计划
        /// </summary>
        public List<FeeInfo> YearSaleReturnPlan { get; set; }

        /// <summary>
        /// 年度向集团借款计划
        /// </summary>
        public List<FeeInfo> YearlendToGroupPlan { get; set; }

        /// <summary>
        /// 年度向集团还款计划
        /// </summary>
        public List<FeeInfo> YearRefundToGroupPlan { get; set; }

        public ProjectApprovalIndexs()
        {
            YearlendToGroupPlan = new List<FeeInfo>();
            YearRefundToGroupPlan = new List<FeeInfo>();
            YearSalePlan = new List<FeeInfo>();
            YearSaleReturnPlan = new List<FeeInfo>();
        }
    }

    public class FeeInfo
    {
        /// <summary>
        /// 年度
        /// </summary>
        public int? Year { get; set; }

        /// <summary>
        /// 金额
        /// </summary>
        public decimal? Fee { get; set; }
    }
}
