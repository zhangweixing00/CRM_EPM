var CreateStage = function () {
    var pId = window.parent.Xrm.Page.data.entity.getId();
    $.ajax({
        async: false,
        type: "Post",
        contentType: "application/json;charset=utf-8",
        url: '/ISV/StartEvaluateService/StartEvaluateService.svc/GetCategoryList',
        data: '{"id":' + pId + '"}',
        dataType: "text",
        success: function (data) {
            // $("#CategoryDataDiv").html(data);
        },
        error: function (data) {
        }
    });
}
var SettingShow = function () {
    //var pId = window.parent.Xrm.Page.data.entity.getId();
    var pId = Xrm.Page.data.entity.attributes.get("epm_project").getValue()[0].id;
    pId = pId.replace('{', '').replace('}', '')
    $.ajax({
        async: false,
        type: "Post",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        url: '/_emp_custom/services/InterfaceService.svc/GetValueRequest',
        data: '{"requestName":"custom_GetProjectType","parms":["' + pId + '"]}',
        success: function (data) {
            // $("#CategoryDataDiv").html(data);
            //alert(data);
            var responseData = data.d;
            if (responseData.returnValue == true) {
                if (responseData.MainData == '919900000') {
                    //alert(responseData.MainData);
                    hiddenTab("tab_4");
                    hiddenTab("tab_9");
                    hiddenTab("tab_8");
                    hiddenTab("tab_5");
                }
                else if (responseData.MainData == '919900001') {
                    hiddenTab("tab_3");
                    hiddenTab("tab_7");
                    hiddenTab("tab_5");
                }
                else if (responseData.MainData == '919900002') {
                    hiddenTab("tab_4");
                    hiddenTab("tab_9");
                    hiddenTab("tab_8");
                    hiddenTab("tab_3");
                    hiddenTab("tab_7");
                }
            }
            else {
                alert("失败");
            }
        },
        error: function (data) {
        }
    });


}
var hiddenTab = function (name) {
    var firstTabSections = Xrm.Page.ui.tabs.get(name);
    firstTabSections.setVisible(false);
}

var autoSetData = function () {

    var buildareaupground = Xrm.Page.data.entity.attributes.get("epm_buildareaupground").getValue();
    var buildareaoverground = Xrm.Page.data.entity.attributes.get("epm_buildareaoverground").getValue();

    //var totalbuildusearea = Xrm.Page.data.entity.attributes.get("epm_totalbuildusearea");
    if (buildareaupground != '' && buildareaoverground != '') {
        Xrm.Page.data.entity.attributes.get("epm_totalbuildusearea").setValue(buildareaoverground + buildareaupground);
    }


    var buildusearea = Xrm.Page.data.entity.attributes.get("epm_buildusearea").getValue();

    if (buildusearea != '' && buildareaoverground != '') {
        var rate = parseFloat(buildareaoverground  / buildusearea).toFixed(2) * 100/100;
        //alert(rate);
        if (rate < 1) {
            Xrm.Page.data.entity.attributes.get("epm_plotratio").setValue(rate);
        }
        else {
            Xrm.Page.data.entity.attributes.get("epm_plotratio").setValue(1);
        }

    }
}
var yearCheck = function () {
    var year = Xrm.Page.data.entity.attributes.get("epm_newyear").getValue();
    var currentYear = new Date().getFullYear();
    //alert(year);
    if (!isNaN(year) && parseInt(year) > 2000 && parseInt(year) < 2050) {
        //alert("请输入合法年份");
    }
    else {
        alert("请输入合法年份");
        Xrm.Page.data.entity.attributes.get("epm_newyear").setValue("");
        //ExecutionObj.getEventArgs().preventDefault();
    }

}
/*
var SetDefaultStageName = function () {
    //var pId = window.parent.Xrm.Page.data.entity.getId();
    var fromType = Xrm.Page.ui.getFormType();
    if (fromType == 1) {
        var pId = Xrm.Page.data.entity.attributes.get("epm_project").getValue()[0].id;
        //alert(pId);
        pId = pId.replace('{', '').replace('}', '');
        $.ajax({
            async: false,
            type: "Post",
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            url: '/_emp_custom/InterfaceService.svc/GetValueRequest',
            data: '{"requestName":"custom_GetStageName","parms":["' + pId + '"]}',
            success: function (data) {
                // $("#CategoryDataDiv").html(data);
                //alert(data);
                var responseData = data.d;
                if (responseData.returnValue == true) {
                    //alert(responseData.MainData);
                    Xrm.Page.data.entity.attributes.get("epm_stagename").setValue(responseData.MainData);
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

}*/

var SetDefaultStageName = function () {
    //var pId = window.parent.Xrm.Page.data.entity.getId();
    var fromType = Xrm.Page.ui.getFormType();
    if (fromType == 1) {
        var pId = Xrm.Page.data.entity.attributes.get("epm_block").getValue()[0].id;
        //alert(pId);
        pId = pId.replace('{', '').replace('}', '');
        $.ajax({
            async: false,
            type: "Post",
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            url: '/_emp_custom/services/InterfaceService.svc/GetValueRequest',
            data: '{"requestName":"custom_GetStageName","parms":["' + pId + '"]}',
            success: function (data) {
                // $("#CategoryDataDiv").html(data);
                //alert(data);
                var responseData = data.d;
                if (responseData.returnValue == true) {
                    //alert(responseData.MainData);
                    Xrm.Page.data.entity.attributes.get("epm_stagename").setValue(responseData.MainData);
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

}

var autoSetEPMName = function () {
    var currentControl = Xrm.Page.ui.getCurrentControl();
    //alert(currentControl.getAttribute().getValue());
    Xrm.Page.data.entity.attributes.get("epm_name").setValue(currentControl.getAttribute().getValue());
}
String.prototype.StringFormat = function () {
    if (arguments.length == 0) {
        return this;
    }
    arguments = arguments[0];
    for (var StringFormat_s = this, StringFormat_i = 0; StringFormat_i < arguments.length; StringFormat_i++) {
        StringFormat_s = StringFormat_s.replace(new RegExp("\\{" + StringFormat_i + "\\}", "g"), arguments[StringFormat_i]);
    }
    return StringFormat_s;
};
var autoSetEPMNameCustom = function (template, attrNames) {

    var arrayAttrText = attrNames.split("|");

    var texts = new Array();
    for (var attr in arrayAttrText) {
        //alert(arrayAttrText[attr]);
        var att = Xrm.Page.data.entity.attributes.get(arrayAttrText[attr]);
        var txt = GetAttrText(att);
        //alert(txt);
        texts.push(txt);
    }
    var StringFormat_s = template;
    for (StringFormat_i = 0; StringFormat_i < texts.length; StringFormat_i++) {
        StringFormat_s = StringFormat_s.replace(new RegExp("\\{" + StringFormat_i + "\\}", "g"), texts[StringFormat_i]);
    }

    //var result = template.format(texts);
    //alert(StringFormat_s);
    Xrm.Page.data.entity.attributes.get("epm_name").setValue(StringFormat_s);
}

var format=function (format) {
    var args = $.makeArray(arguments, 1);
    return format.replace(/\{(\d+)\}/g, function (m, i) {
        return args[i];
    });
}
var GetAttrText = function (attribute) {
    var type = attribute.getAttributeType();
    var value = attribute.getValue();
    var fields = "";
    switch (type) {
        case "boolean":
        case "decimal":
        case "double":
        case "integer":
        case "money":
        case "optionset":
        case "memo":
        case "string":
        case "datetime":
            fields = value;
            break;
        case "lookup":
            if (value != null) {
                if (value.length > 0) {
                    fields = value[0].name;
                }
            }
            break;
    }
    return fields;
}

var hiddenControls = function () {
    var control_name = Xrm.Page.ui.controls.get("epm_name");
    var control_owner = Xrm.Page.ui.controls.get("ownerid");
    control_name.setVisible(false);
    control_owner.setVisible(false);
}