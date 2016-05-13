using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using System.Text;
using Founder.PKURG.EPM.Message;
using System.Data;
using Founder.PKURG.EPM.Common;

namespace Founder.PKURG.EPM.WebServices
{
    [ServiceContract(Namespace = "")]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class InterfaceService
    {
        // 要使用 HTTP GET，请添加 [WebGet] 特性。(默认 ResponseFormat 为 WebMessageFormat.Json)
        // 要创建返回 XML 的操作，
        //     请添加 [WebGet(ResponseFormat=WebMessageFormat.Xml)]，
        //     并在操作正文中包括以下行:
        //         WebOperationContext.Current.OutgoingResponse.ContentType = "text/xml";

        Founder.PKURG.Common.DAL.ExecuteService service = new PKURG.Common.DAL.ExecuteService();

        [OperationContract]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        public BaseResponse GetValueRequest(string requestName, object[] parms)
        {
            return service.GetValueRequest(requestName, parms).ToBaseResponse();
        }
        [OperationContract]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        public BaseResponse GetObjectRequest(string requestName, object[] parms)
        {
            return service.GetObjectRequest(requestName, parms).ToBaseResponse();
        }
        [OperationContract]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        public BaseResponse GetListRequest(string requestName,params object[] parms)
        {
            return service.GetListRequest(requestName, parms).ToBaseResponse();
        }
    }
}
