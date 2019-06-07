var VerifyArray = new Array();
var RegArray = new Array();
var CheckNullArray = new Array();
var DisabledArray = new Array();

function Verify(control, regexString) {
    if (!control.jquery) {
        control = $(control);
    }
    if (control.css("backgroundColor") != '#6495ed'){
        var regex = new RegExp(regexString);
        if (regex.test(control.val())) {
            control.css("backgroundColor", "");
        }
        else {
            control.css("backgroundColor", "#ff7f50")
        }
    }
}

function CheckNull(control) {
    if (!control.jquery) {
        control = $(control);
    }
    if (control.css("backgroundColor") != "#ff7f50") {
        if (control.val() == "") {
            if (control.find("input:checked").length > 0) {
                control.css("backgroundColor", "");
            }
            else {
                control.css("backgroundColor", "#6495ed");
            }
        }        
        else {
            control.css("backgroundColor", "");
        }
    }
}

function EnableAllDisabled() {
    for (var i = 0; i < DisabledArray.length; i++) {
        $(DisabledArray[i]).attr("disabled",false);
    }
}

function DisableAllControl() {
    for (var i = 0; i < DisabledArray.length; i++) {
        $(DisabledArray[i]).attr("disabled", true);
    }
}

function AddToDisabledArray(control) {
    if (!control.jquery)
    {
        control = $(control);
    }
    DisabledArray = DisabledArray.append(control);
}

function AddToVerifyArray(control, reg) {
    if (!control.jquery) {
        control = $(control);
    }
    reg || (reg = "");
    VerifyArray = VerifyArray.append(control);
    RegArray = RegArray.append(reg);
}

function RemoveFromVerifyArray(control) {
    RegArray = RegArray.removeAt(GetItemIndex(VerifyArray,control));
    VerifyArray = RemoveItemFromArray(VerifyArray,control);
}

function AddToCheckNullArray(control) {
    if (!control.jquery) {
        control = $(control);
    }
    CheckNullArray = CheckNullArray.append(control);
}
function RemoveFromCheckNullArray(control) {
    CheckNullArray = RemoveItemFromArray(CheckNullArray,control);
}

function CheckAll() {
    var flag = 0;
    for (var i = 0; i < VerifyArray.length; i++) {
        Verify(VerifyArray[i], RegArray[i]);
        if (VerifyArray[i].css("backgroundColor") == "#ff7f50") {
            flag++;
        }
    }
    for (var i = 0; i < CheckNullArray.length; i++) {
        CheckNull(CheckNullArray[i]);
        if (CheckNullArray[i].css("backgroundColor") == "#6495ed") {
            flag++;
        }
    }
    if (flag > 0) {
        //        alert("请检查输入值是否合法.");
        dialog("提示", "text:请检查输入值是否合法", "400px", "auto", "text");
        return false;
    }
    else {
        dialog("提示", "text:正在处理您的操作,请稍候...", "400px", "auto", "text");
        EnableAllDisabled();
        return true;
    }
}

function RemoveItemFromArray(array, item) {
    var index = GetItemIndex(array,item);
    if (index >= 0) {
        array.removeAt(index);
    }

    return array;
}

function GetItemIndex(array, item) {
    for (var i = 0; i < array.length; i++)
        if (array[i].selector == item.selector) return i;
    return -1;
}

String.prototype.getQuery = function(name) {
    var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)");
    var r = this.substr(this.indexOf("\?") + 1).match(reg);
    if (r != null) return unescape(r[2]); return null;
}

Array.prototype.indexOf = function(item, i) {
    i || (i = 0);
    var length = this.length;
    if (i < 0) i = length + i;
    for (; i < length; i++)
        if (this[i] === item) return i;
    return -1;
}

Array.prototype.remove = function(obj) {
    var index = this.indexOf(obj);
    if (index >= 0) {
        this.removeAt(index);
    }
}

Array.prototype.insertAt = function(index, obj) {
    this.splice(index, 0, obj);
}

Array.prototype.removeAt = function(index) {
    this.splice(index, 1);
}

Array.prototype.append = function(obj) {
    return this.concat(new Array(obj));
}

function OpenInputSearch(url, controlId) {
    window.open(encodeURI(url + "?value=" + document.getElementById(controlId).value + "&control=" + controlId));
}

function ReturnValue(value) {
    window.opener.document.getElementById(window.location.href.getQuery('control')).value = value;
}

function FormatFigure(value) {
    if (!/^(\+|-)?(\d+)(\.\d+)?$/.test(value)) {
        return value;
    }
    else {
        var a = RegExp.$1, b = RegExp.$2, c = RegExp.$3;
        var re = new RegExp().compile("(\\d)(\\d{3})(,|$)");
        while (re.test(b)) b = b.replace(re, "$1,$2$3");
        return a + "" + b + "" + c;
    }
}

function UnFormatFigure(value) {
    return value.replace(/,/gi, '');
}
