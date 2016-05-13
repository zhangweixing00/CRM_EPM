using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ControlQuadrant
{
    [DefaultProperty("Text")]
    [ToolboxData("<{0}:ServerControl1 runat=server></{0}:ServerControl1>")]
    public class Quadrant : WebControl
    {
        [Bindable(true)]
        [Category("Appearance")]
        [DefaultValue("")]
        [Localizable(true)]
        public string Text
        {
            get
            {
                String s = (String)ViewState["Text"];
                return ((s == null) ? "[" + this.ID + "]" : s);
            }

            set
            {
                ViewState["Text"] = value;
            }
        }

        /// <summary>
        /// 象限ID
        /// </summary>
        [Bindable(true)]
        [Category("Appearance")]
        [DefaultValue("")]
        [Localizable(true)]
        public int QId
        {
            get
            {
                object obj = ViewState["QId"];
                return obj == null ? 0 : Convert.ToInt32(obj);
            }
            set
            {
                ViewState["QId"] = value;
            }
        }

        [Bindable(true)]
        [Category("Appearance")]
        [DefaultValue("")]
        [Localizable(true)]
        public List<EvaluateInfo> EvaluateInfos
        {
            get
            {
                object obj = ViewState["EvaluateList"];
                if (obj== null)
                {
                    return new List<EvaluateInfo>();
                }
                return obj as List<EvaluateInfo>;
            }
            set
            {
                ViewState["EvaluateList"] = value;
            }
        }

        protected override void CreateChildControls()
        {
            base.CreateChildControls();

           // CreateControlHierarchy();
        }

        private void CreateControlHierarchy()
        {
            throw new NotImplementedException();
        }

        protected override void RenderContents(HtmlTextWriter output)
        {
            StringBuilder builder = new StringBuilder();
            foreach (EvaluateInfo item in EvaluateInfos)
            {
                builder.AppendFormat("{0}<br/>", item.SupplierName);
            }
            output.Write(builder.ToString());
        }
    }
}
