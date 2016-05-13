using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Founder.PKURG.CRM;

namespace Founder.PKURG.EPM.PlugIn
{
    public class Common
    {
        public static void CreateEmptyObject(CRMInfoOP op, dynamic obj)
        {
            op.Add(new CRMInfo()
            {
                Name = obj.tName,//"epm_projectplanningindex",
                Fields = new List<CRMFieldInfo>()
                {
                    new  CRMFieldInfo()
                    { 
                        Name=obj.rFName,//"epm_project",
                        Value=obj.id,
                        IsForeign=true,
                        ForeignName=obj.rTName//"epm_projectinfo"
                    },
                    new CRMFieldInfo()
                    { 
                        Name="epm_name",
                        Value="-"
                    }
                }
            });
        }
    }
}
