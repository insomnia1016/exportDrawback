var regexEnum =
{
    intege: "^-?[1-9]\\d*$", 				//整数
    num: "^([+-]?)\\d*\\.?\\d+$", 		//数字
    email: "^\\w+((-\\w+)|(\\.\\w+))*\\@[A-Za-z0-9]+((\\.|-)[A-Za-z0-9]+)*\\.[A-Za-z0-9]+$", //邮件
    url: "^http[s]?:\\/\\/([\\w-]+\\.)+[\\w-]+([\\w-./?%&=]*)?$", //url
    chinese: "^[\\u4E00-\\u9FA5\\uF900-\\uFA2D]+$", 				//仅中文
    zipcode: "^\\d{6}$", 					//邮编
    mobile: "^(13|15)[0-9]{9}$", 			//手机
    ip4: "^(25[0-5]|2[0-4]\\d|[0-1]\\d{2}|[1-9]?\\d)\\.(25[0-5]|2[0-4]\\d|[0-1]\\d{2}|[1-9]?\\d)\\.(25[0-5]|2[0-4]\\d|[0-1]\\d{2}|[1-9]?\\d)\\.(25[0-5]|2[0-4]\\d|[0-1]\\d{2}|[1-9]?\\d)$", //ip地址
    date: "^\\d{4}(\\-|\\/|\.)\\d{1,2}\\1\\d{1,2}$", 				//日期
    qq: "^[1-9]*[1-9][0-9]*$", 			//QQ号码
    tel: "^(([0\\+]\\d{2,3}-)?(0\\d{2,3})-)?(\\d{7,8})(-(\\d{3,}))?$", //电话号码的函数(包括验证国内区号,国际区号,分机号)
    username: "^\\w+$", 					//用来用户注册。匹配由数字、26个英文字母或者下划线组成的字符串
    letter: "^[A-Za-z]+$", 				//字母
    idcard: "^[1-9]([0-9]{14}|[0-9]{17})$"	//身份证
}

function ValidatData(objId, dataType, objleftoffset, objtopoffset, title, info, objheight, showtype, timeOut) {
    var value = $("#" + objId).val();
    var reg = '';
    var msg = '';
    
    if (value != undefined && value != '') {
        switch (dataType) {
            case "intege":
                reg = new RegExp(regexEnum.intege);
                msg = '只能为整数.';
                break;
           case "num":
               reg = new RegExp(regexEnum.num);
               msg = '只能为数字.';
               break;
           case "email":
               reg = new RegExp(regexEnum.email);
               msg = '邮箱格式错误.';
               break;
           case "url":
               reg = new RegExp(regexEnum.url);
               msg = 'URL格式错误.';
               break;
           case "chinese":
               reg = new RegExp(regexEnum.chinese);
               msg = '只能是中文.';
               break;
           case "zipcode":
               reg = new RegExp(regexEnum.zipcode);
               msg = '邮编格式错误.';
               break;
           case "mobile":
               reg = new RegExp(regexEnum.mobile);
               msg = '手机号码格式错误.';
               break;
           case "ip4":
               reg = new RegExp(regexEnum.ip4);
               msg = 'IP地址格式错误.';
               break;
           case "date":
               reg = new RegExp(regexEnum.date);
               msg = '日期格式错误.';
               break;
           case "qq":
               reg = new RegExp(regexEnum.qq);
               msg = 'QQ格式错误.';
               break;
           case "tel":
               reg = new RegExp(regexEnum.tel);
               msg = '电话格式错误.';
               break;
           case "username":
               reg = new RegExp(regexEnum.username);
               msg = '用户名格式错误应由数字、26个英文字母或者下划线组成.';
               break;
           case "letter":
               reg = new RegExp(regexEnum.letter);
               msg = '只能为英文字母.';                
               break;
           case "idcard":
               reg = new RegExp(regexEnum.idcard);
               msg = '身份证格式错误.';  
               break;
        }
        if (!reg.test(value)) {
            if (info != null && info != '') {
                msg = info;
            }
            if (showtype == '') {
                showtype = 'up';
            }
            showhintinfo(document.getElementById(objId), objleftoffset, objtopoffset, '提示信息', msg, objheight, showtype);
            setTimeout(hidehintinfo, timeOut);
            document.getElementById(objId).value = '';
        }
    }
}

//显示提示层
function showhintinfo(obj, objleftoffset, objtopoffset, title, info, objheight, showtype, objtopfirefoxoffset) {

    var p = getposition(obj);

    if ((showtype == null) || (showtype == "")) {
        showtype == "up";
    }
    document.getElementById('hintiframe' + showtype).style.height = objheight + "px";
    document.getElementById('hintinfo' + showtype).innerHTML = info;
    document.getElementById('hintdiv' + showtype).style.display = 'block';

    if (objtopfirefoxoffset != null && objtopfirefoxoffset != 0 && !isie()) {
        document.getElementById('hintdiv' + showtype).style.top = p['y'] + parseInt(objtopfirefoxoffset) + "px";
    }
    else {
        if (objtopoffset == 0) {
            if (showtype == "up") {
                document.getElementById('hintdiv' + showtype).style.top = p['y'] - document.getElementById('hintinfo' + showtype).offsetHeight - 40 + "px";
            }
            else {
                document.getElementById('hintdiv' + showtype).style.top = p['y'] + obj.offsetHeight + 5 + "px";
            }
        }
        else {
            document.getElementById('hintdiv' + showtype).style.top = p['y'] + objtopoffset + "px";
        }
    }
    document.getElementById('hintdiv' + showtype).style.left = p['x'] + objleftoffset + "px";
}



//隐藏提示层
function hidehintinfo() {
    document.getElementById('hintdivup').style.display = 'none';
    document.getElementById('hintdivdown').style.display = 'none';
}
function getposition(obj) {
    var r = new Array();
    r['x'] = obj.offsetLeft;
    r['y'] = obj.offsetTop;
    while (obj = obj.offsetParent) {
        r['x'] += obj.offsetLeft;
        r['y'] += obj.offsetTop;
    }
    return r;
}