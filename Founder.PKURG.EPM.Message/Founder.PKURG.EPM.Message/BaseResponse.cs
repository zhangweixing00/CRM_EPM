using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;

namespace Founder.PKURG.EPM.Message
{
    [DataContract]
    public class BaseResponse
    {
        [DataMember]
        public string message { get; set; }

        [DataMember]
        public bool returnValue { get; set; }

        [DataMember]
        public dynamic MainData { get; set; }
    }
}
