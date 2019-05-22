/*
* 是否是测试登录
*/
var isTestLogin = true;

/*
* 验证IC Card是否还连接，是否是登录时的卡
*/
function ValidateICCardKeeping()
{
    if(isTestLogin)
    {
        return true;
    }
    var ca = null;
    try
    {
        ca = new ActiveXObject("SecSnapIn.Authentication");
    }
    catch(e)
    {
        alert("请先安装登录控件！");
        return false;
    }
    var pin = ReadCookie('lpspincode');
    var loginCardID = ReadCookie('lpscardid');
    if(pin == '' || loginCardID == '')
    {
        alert('您尚未登录！');
        return false;
    }
    loginCardID = unescape(loginCardID);
    pin = unescape(pin);
    var cardID = ca.GetICID(pin);
    if(!IsNumber(cardID))
    {
        alert("IC Card 没有插好，请从新登录系统。");
        return false;
    }
    
    
    if(cardID == loginCardID)
    {        
        return true;
    }
    alert('错误的IC Card号，请从新登录系统。');
    return false;
}

function IsNumber(str)
{
    exp = /^\s*[-\+]?\d+\s*$/;
    if (str.match(exp) == null)
        return false;
    num = parseInt(op, 10);
    return isNaN(num);
}

/*
* 获得Picker的列表缓存数据
*/
function GetPickerData(key)
{
    if(window.parent)
    {
        if(window.parent["PickerData"])
            return window.parent["PickerData"][key];
    }
    return null;
}

/*
* 保存Picker的列表缓存数据
*/
function SetPickerData(key,value)
{
    if(window.parent)
    {
        if(!window.parent["PickerData"])
            window.parent["PickerData"] = new Object();
        window.parent["PickerData"][key] = value;
    }
}

function HideControlForNotPopWin(ctrlID)
{
    if(document.all(ctrlID) && !window.opener)
        document.all(ctrlID).style.display = 'none';
}

/*
*   防按钮双击处理
*/
function DisableButton(btn)
{
    var exp = "$('" + btn.id + "').disabled = true;";
    window.setTimeout(exp,100);
    var exp1 = "$('" + btn.id + "').disabled = false;";
   window.setTimeout(exp1,2000); 
}

/*
*   显示物品查询页
*   type:0.行邮物品 , 1.h2000物品 重点物品
*   code:税号 后模糊查询
*   name:品名 全模糊查询
*/
function ShowGoods(type,code,name)
{
    var newWin = window.open("../Common/GoodsQuery.aspx?Code=" + code + "&Type=" + type + "&Name=" + escape(name),
    "GoodsQuery","height=600,width=800,status=no,toolbar=no,menubar=no,location=no,resizable=1");
    newWin.focus();
}

function ShowBoxInfo(approveNo)
{
    var newWin = window.open("../Box/SetApproveNo.aspx?ApproveNo=" + approveNo,
    "BoxInfo","height=400,width=800,status=no,toolbar=no,menubar=no,location=no,resizable=1");
    newWin.focus();
}


function SetDorpDownListSelectItem(ctrl,key)
{
    for(i=0; i<ctrl.options.length; i++)
    {
        if(ctrl.options(i).value == key)
        {
            ctrl.selectedIndex = i;
        }
    }
}

/*
*  显示操作日志页
*/
function ShowOperateLog(signNo)
{
    var url = "../OperateLog/OpLogList.aspx?SignNo=" + signNo;
    var newWin = window.open(url,"Log","height=600,width=800,status=no,toolbar=no,menubar=no,location=no,resizable=yes");
    newWin.focus();
}

function Confirm(msg)
{
    var str = "是否确认当前操作";
    if(msg)
        str = msg;
    return confirm(str);
}

/*  
*    Round(Dight,How):数值格式化函数，Dight要  
*    格式化的  数字，How要保留的小数位数。  
*/  
function Round(Dight,How)  
{  
	Dight = Math.round  (Dight*Math.pow(10,How))/Math.pow(10,How);  
	return Dight;  
}  

function GetFixLengthString(src,length,prefix)
{
    var tmp = new String(src);
    if(prefix == null)
        prefix = "0";
    if(tmp.length < length)
    {
        for(var i=0;i<length - tmp.length;i++)
        {
            tmp = prefix + tmp;
        }
    }
    return tmp;
}

function GetFixLenStringWithPostfix(src,length,posfix)
{
    var tmp = new String(src);
    if(posfix == null)
        posfix = "0";
    if(tmp.length < length)
    {
        while(length>tmp.length)
        {
        tmp = tmp + posfix;
        }
    }
    return tmp;
}

function KeyIndexOfArray(key,array)
{
    for(var i=0;i<array.length;i++)
    {
        if(key == array[i])
            return i;
    }
    return -1;
}

function GetXmlDocument()
{
	var xmlData;
	try
	{
		xmlData = new ActiveXObject("Msxml2.DOMDocument");
	}
	catch(e)
	{
		xmlData = ActiveXObject("Msxml.DOMDocument");
	}	
	//xmlData.async = false;
	if (arguments.length > 0)
	{
		var xml = arguments[0];
		if (typeof(xml) == "string")
			xmlData.loadXML(xml);
		else
		if (typeof(xml) == "object")
			xmlData.loadXML(xml.xml);
	}
	return xmlData;
}

function SetCookie(cookieName,cookieValue,nDays) {
var cookieStr = cookieName+"="+escape(cookieValue) + ";path=/";
if(nDays)
{
 var today = new Date();
 var expire = new Date();
 if (nDays==null || nDays==0) nDays=1;
 expire.setTime(today.getTime() + 3600000*24*nDays);
 cookieStr = cookieStr + "expires="+expire.toGMTString();
 }
 document.cookie = cookieStr;
}
function ReadCookie(cookieName) {
 var theCookie=""+document.cookie;
 var ind=theCookie.indexOf(cookieName);
 if (ind==-1 || cookieName=="") return ""; 
 var ind1=theCookie.indexOf(';',ind);
 if (ind1==-1) ind1=theCookie.length; 
 return unescape(theCookie.substring(ind+cookieName.length+1,ind1));
}


String.prototype.Trim = function()
{
    return this.replace(/(^\s*)|(\s*$)/g, "");
}
String.prototype.LTrim = function()
{
    return this.replace(/(^\s*)/g, "");
}
String.prototype.RTrim = function()
{
    return this.replace(/(\s*$)/g, "");
}

function ParseDateFromYYMMDD(str)
{
    var strY = str.substring(0,2);
    var strM = str.substring(2,4);
    var strD = str.substring(4,6);
    var d = new Date(strM + "-" + strD + "-" + strY);
    return d;
}

function GetYMDDateString(date)
{
    var strY = date.getFullYear().toString();
    var strM = date.getMonth()+1;
    var strD = date.getDate().toString();
    return strY + "-" + strM.toString() + "-" + strD;
}

function GoToPrint(pageName,signNo)
{
    var url = pageName + "?signno=" + signNo;
    window.open(url);
}

//
//向服务器发送特定页面打印标志码
//
function SendPrintMessage(url)
{
    url = url + '&code=ok&' + '&stamp=' + new Date();
    AjaxRequest(url);
}

//var xmlhttpreq = null;
function AjaxRequest(url)
{
    
    var xmlhttpreq = GetXmlRequrest();
    
    //xmlhttpreq.onreadystatechange = onready;
    xmlhttpreq.open("get",url,true,null,null);
    xmlhttpreq.send();
}

function onready()
{
//    if(xmlhttpreq.readyState == 4)
//    {
//        alert(xmlhttpreq.statusText);
//        //alert(xmlhttpreq.responseText);
//    }
//    else
//        alert(xmlhttpreq.readyState);
}

function GetXmlRequrest()
{
    var xmlHttp = false;
    
    try {
      xmlHttp = new ActiveXObject("Msxml2.XMLHTTP");
    } catch (e) {
      try {
        xmlHttp = new ActiveXObject("Microsoft.XMLHTTP");
      } catch (e2) {
        xmlHttp = false;
      }
    }
    
    if (!xmlHttp && typeof XMLHttpRequest != 'undefined') {
      xmlHttp = new XMLHttpRequest();
    }

    return xmlHttp;
}

//日期自定义控件验证（yyyy-MM-dd）
var dateType = {
    dateorder:'ymd'
}
function ValidInputDate(source,arguments)
{
   var dateStr = arguments.Value; 
   arguments.IsValid =  ValidatorConvert(dateStr,"Date",dateType) != null;
   return;
}

function ValidateBirthday(source,arguments)
{
    var dateStr = arguments.Value; 
    var dt =ValidatorConvert(dateStr,"Date",dateType)
    arguments.IsValid = dt<new Date();
}

//浮点自定义控件验证
var floatType = {
    decimalchar:'.'
}
function FloatValidate(source, arguments)
{
      var result = ValidatorConvert(arguments.Value,"Double",floatType);
      if(result != null && result > 0)
        arguments.IsValid = true;
      else
        arguments.IsValid = false;
}

