using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Founder.PKURG.Common.DAL;

namespace Founder.PKURG.EPM.Common
{
    public static class ReturnInfoConvert
    {
        public static Founder.PKURG.EPM.Message.BaseResponse ToBaseResponse(this ReturnInfo info)
        {
            return new Message.BaseResponse()
            {
                returnValue = info.returnValue,
                message = info.message,
                MainData = info.MainData
            };
        }
    }
}