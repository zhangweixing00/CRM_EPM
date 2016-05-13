using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication1
{
    public partial class _Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ServerControl11.EvaluateInfos = new List<ControlQuadrant.EvaluateInfo>()
            {
                new ControlQuadrant.EvaluateInfo()
                {
                     SupplierName="123"
                },
                new ControlQuadrant.EvaluateInfo()
                {
                    SupplierName="456"
                }
            };
        }
    }
}
