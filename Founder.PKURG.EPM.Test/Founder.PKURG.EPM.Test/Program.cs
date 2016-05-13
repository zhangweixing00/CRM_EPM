using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Founder.PKURG.EPM.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            //Founder.PKURG.Common.CustomType type = new Common.CustomType();
            //Type tt=type.CreateType(new List<string>() {"aa","bb","cc" });
            //Dictionary<string, string> dict = new Dictionary<string, string>();
            //dict.Add("aa", "123");
            //dict.Add("bb", "456");
            //dict.Add("cc", "789");
            //Object aa= type.GetInstance(tt, dict);
            EPMServiceReference.WebServiceSoapClient client = new EPMServiceReference.WebServiceSoapClient();
         Guid ss=   client.CreateProjectApproval(new Guid("3ACB01CB-CC9E-E311-8288-00155D141823"), 919900000, "地块1", "OA", "审批人");

         client.UpDateProjectApproval(ss, 2, "袅袅娜娜");
        }
    }
}
