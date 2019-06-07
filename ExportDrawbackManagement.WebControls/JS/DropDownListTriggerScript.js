var xmlHttp;
var TargetControl;

function CreateXMLHttpRequest() {
    try {
        xmlHttp = new XMLHttpRequest;
    }
    catch (e) {
        try {
            xmlHttp = ActiveXObject("MSXML2.XMLHTTP");
        }
        catch (e2) {
            try {
                xmlHttp = ActiveXObject("Microsoft.XMLHTTP");
            }
            catch (e3) {
                xmlHttp = false;
            }
        }
    }
    if (!xmlHttp) {
        alert("创建 XMLHttpRequest 对象失败");
    }
}


function Change(RequestURL) {
    
    CreateXMLHttpRequest();
    
    var url = encodeURI(RequestURL);
    var xmldom = new ActiveXObject("Microsoft.XMLDOM")

    xmlHttp.onreadystatechange = handleStateChangeData;
    xmlHttp.open("GET", url, true);
    xmlHttp.send(null);
}


//获得选项数据
function handleStateChangeData() {
    if (xmlHttp.readyState == 4) {
        if (xmlHttp.status == 200) {
            xmldom = xmlHttp.responseXML;
            ChangeValue(xmldom, TargetControl);
        }
    }
}

function ChangeValue(xmldom, control) {

    var ddl = document.getElementById(control);
    var elements = xmldom.getElementsByTagName("element");

    if (elements.length != 0) {
        if (ddl.tagName == "SELECT" && ddl.type == "select-one") {
            
            for (var i = 0; i < elements.length; i++) {

                ddl.options[i] = new Option(elements[i].childNodes[0].text, elements[i].childNodes[1].text);

            }

        }
        else if (ddl.tagName == "INPUT" && ddl.type == "text") {

             ddl.value = elements[0].childNodes[0].text;

         }
         if (ddl.onchange != null) {
             ddl.onchange();
         }
    }
    else {
        Clear(ddl);
    }
    var xmlHttp = '';
    var TargetControl = '';
}

function SelectIndexChange(RequestURL, thisControl, control) {
    if (document.getElementById(control) != undefined && document.getElementById(thisControl).value != '') {
        TargetControl = control;
        Change(RequestURL +"&Value="+ document.getElementById(thisControl).value);
    }
    else {
        Clear(document.getElementById(control));
    }
}

function Clear(control) {
    if (control.options != undefined) {
        if (control.tagName == "SELECT" && control.type == "select-one") {

            var count = control.options.length;
            for (var i = 0; i < count; i++) {
                control.removeChild(control.options[0]);
            }

        }
        else if (control.tagName == "INPUT" && control.type == "text") {
            control.value = '';
        }
        if (control.onchange != null) {
            control.onchange();
        }
    }
}
