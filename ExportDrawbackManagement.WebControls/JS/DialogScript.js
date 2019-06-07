function $(str) {
    return document.getElementById(str);
}
//类似Alert，弹出消息
function MyAlert(boxtitle, msg, isFresh, imgurl1, imgurl2, imgurl5, imgurl6, imgurl13, imgurl7, imgurl8, imgurl9, imgurl10, imgurl11, imgurlBtn, contenttype, width, height) {
    //设置长宽
    var m = $('msg_div_main');
    m.style.width = width + 'px';
    var tdContent = $('tdContent');
    if(height>=80)
        tdContent.style.height = (height - 80) + 'px';
    //
    var img_bg_01 = $('img_bg_01');
    var td_bg_02 = $('td_bg_02');
    var td_bg_02_1 = $('td_bg_02_1');
    //var img_bg_05 = $('img_bg_05');
    var img_bg_06 = $('img_bg_06');
    var img_bg_09 = $('img_bg_09');
    var img_bg_10 = $('img_bg_10');
    var td_bg_07 = $('td_bg_07');
    var td_bg_08 = $('td_bg_08');
    var td_bg_11 = $('td_bg_11');
    img_bg_01.src = imgurl1;
 //   img_bg_05.src = imgurl5;
    img_bg_09.src = imgurl9;
    img_bg_10.src = imgurl10; 
    img_bg_06.src = imgurl6;
    td_bg_07.style.backgroundImage = "url(" + imgurl7 + ")";
    td_bg_08.style.backgroundImage = "url(" + imgurl8 + ")";
    td_bg_02.style.backgroundImage = "url(" + imgurl2 + ")";
    td_bg_02_1.style.backgroundImage = "url(" + imgurl2 + ")";
    td_bg_11.style.backgroundImage = "url(" + imgurl11 + ")";
    td_bg_02_1.innerHTML = "<img  id='img_bg_05' src='" + imgurl5 + "' width='21' height='21' alt='关闭' "
	+ "onMouseover='this.src=\"" + imgurl13 + "\"' "
	+ "onMouseout='this.src=\"" + imgurl5 + "\"' onMousedown='this.src=\"" + imgurl13 + "\"'"
	+ "onMouseup='BOX_close();' >";
    if (m == null) { return; }
    //标题
    $("msg_div_main_title").innerHTML = boxtitle;
    $("msg_div_main_title").style.backgroundImage = "url(" + imgurl2 + ")";
    document.writeln("<style type='text/css'>.msg_div_main_but {background:url(" + imgurlBtn + ");width:65px;heigt:20px;border:none;padding-top:3px;font-size:12px;}</style>");
    //内容和地址
    if (contenttype == 1) {
        if (isFresh != null && isFresh == 'true') {
            $("msg_div_main_content").innerHTML += msg + "<br /><br /><button class='msg_div_main_but' onclick='BOX_close();window.location.href=window.location.href;'>确 定</button>";
        }
        else {
            $("msg_div_main_content").innerHTML += msg + "<br /><br /><button class='msg_div_main_but' onclick='BOX_close();'>确 定</button>";
        }
    }
    else if (contenttype == 2) {
        if (isFresh != null && isFresh == 'true') {
            $("msg_div_main_content").innerHTML += "<iframe width=100% src='" + msg + "' frameborder=no scrolling=no></iframe><button class='msg_div_main_but' onclick='BOX_close();window.location.href=window.location.href;'>关 闭</button>";
        }
        else {
            $("msg_div_main_content").innerHTML += "<iframe width=100% src='" + msg + "' frameborder=no scrolling=no></iframe><button class='msg_div_main_but' onclick='BOX_close();'>关 闭</button>";
        }
    }
    else {
        if (isFresh != null && isFresh == 'true') {
            $("msg_div_main_content").innerHTML += msg + "<button class='msg_div_main_but' onclick='BOX_close();window.location.href=window.location.href;'>关 闭</button>";
        }
        else {
            $("msg_div_main_content").innerHTML += msg + "<button class='msg_div_main_but' onclick='BOX_close();'>关 闭</button>";
        }
    }
    BOX_layout();
    window.onresize = function() { BOX_layout(); } //改变窗体重新调整位置
    window.onscroll = function() { BOX_layout(); } //滚动窗体重新调整位置
    $("msg_div_all").style.zIndex = 100;
    $("msg_div_main").style.zIndex = 200;
    $("msg_div_all").oncontextmenu = function() {
        return false;
    }
    $("msg_div_main").oncontextmenu = function() {
        return false;
    }
}
//类似Alert，弹出消息并跳转页面
function MyAlertUrl(boxtitle, msg, url, imgurl1, imgurl2, imgurl5, imgurl6, imgurl13, imgurl7, imgurl8, imgurl9, imgurl10, imgurl11, imgurlBtn, contenttype, width, height) {
    //设置长宽
    var m = $('msg_div_main');
    m.style.width = width + 'px';
    var tdContent = $('tdContent');
    if (height >= 80)
        tdContent.style.height = (height - 80) + 'px';
    //
    var img_bg_01 = $('img_bg_01');
    var td_bg_02 = $('td_bg_02');
    var td_bg_02_1 = $('td_bg_02_1');
    //var img_bg_05 = $('img_bg_05');
    var img_bg_06 = $('img_bg_06');
    var img_bg_09 = $('img_bg_09');
    var img_bg_10 = $('img_bg_10');
    var td_bg_07 = $('td_bg_07');
    var td_bg_08 = $('td_bg_08');
    var td_bg_11 = $('td_bg_11');
    img_bg_01.src = imgurl1;
    //   img_bg_05.src = imgurl5;
    img_bg_09.src = imgurl9;
    img_bg_10.src = imgurl10;
    img_bg_06.src = imgurl6;
    td_bg_07.style.backgroundImage = "url(" + imgurl7 + ")";
    td_bg_08.style.backgroundImage = "url(" + imgurl8 + ")";
    td_bg_02.style.backgroundImage = "url(" + imgurl2 + ")";
    td_bg_02_1.style.backgroundImage = "url(" + imgurl2 + ")";
    td_bg_11.style.backgroundImage = "url(" + imgurl11 + ")";
    td_bg_02_1.innerHTML = "<img  id='img_bg_05' src='" + imgurl5 + "' width='21' height='21' alt='关闭' "
	+ "onMouseover='this.src=\"" + imgurl13 + "\"' "
	+ "onMouseout='this.src=\"" + imgurl5 + "\"' onMousedown='this.src=\"" + imgurl13 + "\"'"
	+ "onMouseup='BOX_close();' >";
    if (m == null) { return; }
    //标题
    $("msg_div_main_title").innerHTML = boxtitle;
    $("msg_div_main_title").style.backgroundImage = "url(" + imgurl2 + ")";
    document.writeln("<style type='text/css'>.msg_div_main_but {background:url(" + imgurlBtn + ");width:65px;heigt:20px;border:none;padding-top:3px;font-size:12px;}</style>");
    //内容和地址
    if (contenttype == 1) {
        if (url == null || url == '') {
            $("msg_div_main_content").innerHTML += msg + "<br /><br /><button class='msg_div_main_but' onclick='BOX_close();'>确 定</button>";
        }
        else {
            $("msg_div_main_content").innerHTML += msg + "<br /><br /><button class='msg_div_main_but' onclick=\"BOX_close();window.location.href='" + url + "';\">确 定</button>";
        }
    }
    else if (contenttype == 2) {
        if (url == null || url == '') {
            $("msg_div_main_content").innerHTML += "<iframe width=100% src='" + msg + "' frameborder=no scrolling=no></iframe><button class='msg_div_main_but' onclick='BOX_close();'>关 闭</button>";
        }
        else {
            $("msg_div_main_content").innerHTML += "<iframe width=100% src='" + msg + "' frameborder=no scrolling=no></iframe><button class='msg_div_main_but' onclick=\"BOX_close();window.location.href='" + url + "';\">关 闭</button>";
        }
    }
    else {
        if (url == null || url == '') {
            $("msg_div_main_content").innerHTML += msg + "<button class='msg_div_main_but' onclick='BOX_close();'>关 闭</button>";
        }
        else {
            $("msg_div_main_content").innerHTML += msg + "<button class='msg_div_main_but' onclick=\"BOX_close();window.location.href='" + url + "';\">关 闭</button>";
        }
    }
    BOX_layout();
    window.onresize = function() { BOX_layout(); } //改变窗体重新调整位置
    window.onscroll = function() { BOX_layout(); } //滚动窗体重新调整位置
    $("msg_div_all").style.zIndex = 100;
    $("msg_div_main").style.zIndex = 200;
    $("msg_div_all").oncontextmenu = function() {
        return false;
    }
    $("msg_div_main").oncontextmenu = function() {
        return false;
    }
}
function BOX_close() {       //移除层
    var m = $('msg_div_main');
    var e = $('msg_div_all');
    window.onscroll = null;
    window.onresize = null;
    m.style.display = "none";
    e.style.display = "none";
}
function BOX_layout() {       //调整位置
    var m = $('msg_div_main');
    var e = $('msg_div_all');
    if (e == null) { //判断是否新建遮掩层
        var overlay = document.createElement("div");
        overlay.setAttribute('id', 'BOX_overlay');
        overlay.onclick = function() { BOX_close(); };
        m.parentNode.appendChild(overlay);
    }
    //取客户端左上坐标，宽，高
    var scrollLeft = (document.documentElement.scrollLeft ? document.documentElement.scrollLeft : document.body.scrollLeft);
    var scrollTop = (document.documentElement.scrollTop ? document.documentElement.scrollTop : document.body.scrollTop);
    var clientWidth;
    clientWidth = document.documentElement.clientWidth;
    var clientHeight;
    clientHeight = document.documentElement.clientHeight;
    e.style.left = scrollLeft + 'px';
    e.style.top = scrollTop + 'px';
    e.style.width = clientWidth + 'px';
    e.style.height = clientHeight + 'px';
    e.style.display = "";
    //Popup窗口定位
    m.style.position = 'absolute';
    m.style.zIndex = 101;
    m.style.display = "";
    m.style.left = scrollLeft + ((clientWidth - m.offsetWidth) / 2) + 'px';
    m.style.top = scrollTop + ((clientHeight - m.offsetHeight) / 2) + 'px';
}

var msg_md = false, msg_mobj, msg_ox, msg_oy;
document.onmousedown = function() {
    if (typeof (event.srcElement.msg_canmove) == "undefined") {
        return;
    }
    if (event.srcElement.msg_canmove) {
        msg_md = true;
        msg_mobj = $(event.srcElement.msg_forid);
        msg_ox = msg_mobj.offsetLeft - event.x;
        msg_oy = msg_mobj.offsetTop - event.y;
    }
}
document.onmouseup = function() {
    msg_md = false;
}
document.onmousemove = function() {
    if (msg_md) {
        msg_mobj.style.left = event.x + msg_ox;
        msg_mobj.style.top = event.y + msg_oy;
    }
}

document.writeln("<style type='text/css'>"
	+ "#msg_div_main {position:absolute;}"
	+ "#msg_div_main_title {font-size:12px;color:#2C71AF;font-family:verdana;cursor:default;text-align:left }"
	+ "#msg_div_main_content {font-size:14px;color:#2C71AF;padding-left:8px;}"
	+ "</style>"
	+ "<div id='msg_div_all' style='background:#EFEFEF; position: absolute; z-index: 100;filter: alpha(opacity=50); -moz-opacity: 0.6; opacity: 0.6;'></div>"
    + "<div id='msg_div_main' style='display: none;'>"
    + "<div style='text-align: center; margin-top: 5px;'><label id='lblCss'></label>"
    + "<table width='100%' height='29' border='0' cellspacing='0' cellpadding='0'>"
	+ "<tr>"
	+ "<td width='25'><img id='img_bg_01' width='25' height='29' alt='' /></td><td id='td_bg_02' width='3'></td>"
	+ "<td background='img/bg_02.gif' msg_canmove='true' msg_forid='msg_div_main' id='msg_div_main_title'></td>"
	+ "<td background='img/bg_02.gif' align='right' style='padding-top:4px' id='td_bg_02_1'>"
//	+ "<img  id='img_bg_05' src='img/bg_05.gif' width='21' height='21' alt='关闭' "
//	+ "onMouseover='this.src=\"img/bg_13.gif\"' "
//	+ "onMouseout='this.src=\"img/bg_05.gif\"' onMousedown='this.src=\"img/bg_18.gif\"'"
//	+ "onMouseup='BOX_close();' />
	+ "</td>"
	+ "<td width='6'><img id='img_bg_06' width='6' height='29' alt='' /></td>"
	+ "</tr>"
	+ "</table>"
	+ "<table width='100%' border='0' cellspacing='0' cellpadding='0'>"
	+ "<tr>"
	+ "<td width='3' id='td_bg_07'></td>"
	+ "<td id='tdContent' bgcolor='#ffffff' align='center'><br /><span id='msg_div_main_content'></span><br /><br /></td>"
	+ "<td width='3' id='td_bg_08'></td>"
	+ "</tr>"
	+ "<tr>"
	+ "<td width='3' height='3'><img id='img_bg_09' width='3' height='3' /></td>"
	+ "<td id='td_bg_11' background='img/bg_11.gif'></td>"
	+ "<td width='3' height='3'><img id='img_bg_10' width='3' height='3' /></td>"
	+ "</tr>"
	+ "</table>"
    + "</div>"
    + "</div>");


//F7F7F7