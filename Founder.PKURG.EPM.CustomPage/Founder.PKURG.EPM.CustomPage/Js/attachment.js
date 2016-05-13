var DownLoadList = function () {
    var Request = new Object();
    Request = GetRequest();
    var viewName=Request["viewName"];
        var pId =Request["id"];
        //alert(pId);
        pId = pId.replace('{', '').replace('}', '');
        $.ajax({
            async: false,
            type: "Post",
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            url: '/_emp_custom/InterfaceService.svc/GetListRequest',
            data: '{"requestName":"custom_GetAttachments","parms":["' + viewName + '","' + pId + '"]}',
            success: function (data) {
                // $("#CategoryDataDiv").html(data);
                //alert(data);
                var responseData = data.d;
                if (responseData.returnValue == true) {
                    //alert(responseData.MainData);
                    
                   // Xrm.Page.data.entity.attributes.get("epm_name").setValue(responseData.MainData);
                }
                else {
                    alert("失败");
                }
            },
            error: function (data) {
            }
        });
}