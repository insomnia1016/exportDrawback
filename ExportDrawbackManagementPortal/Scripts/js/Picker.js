var g_CurrentPickers = new Array();
document.attachEvent("onclick",
(function()//任意点击时关闭该控件	//ie6的情况可以由下面的切换焦点处理代替
{
    for(var i=0;i<g_CurrentPickers.length;i++)
    {
        if(event.srcElement != g_CurrentPickers[i].ctrlPop)
            g_CurrentPickers[i].Hide();
    }    
})
);
document.attachEvent("onkeyup",
(function()//按Esc键关闭，切换焦点关闭
{
    if (window.event.keyCode==27)
    {
        for(var i=0;i<g_CurrentPickers.length;i++)
        {
        //if(event.srcElement != g_CurrentPickers[i].ctrlPop)
            g_CurrentPickers[i].Hide();
        }
    }
    else if(document.activeElement)
    {
	    if(document.activeElement != this.ctrlPop)
	    {
            for(var i=0;i<g_CurrentPickers.length;i++)
            {
        if(event.srcElement != g_CurrentPickers[i].ctrlPop)
            g_CurrentPickers[i].Hide();
            }
	    }
	}  
})
);

var Picker = Class.create();
Picker.prototype=
{
    initialize: function(ctrlPanel,ctrlPop) 
    { 
        this.ctrlPanel = ctrlPanel;
        this.ctrlPop = ctrlPop;
    }, 
     
    Show: function() 
    {
        var ctrlPop = this.ctrlPop;
        if(ctrlPop.readOnly)
            return;
        var dads  = this.ctrlPanel.style;
        var th = ctrlPop;
        var ttop  = ctrlPop.offsetTop;     //TT控件的定位点高
        var thei  = ctrlPop.clientHeight;  //TT控件本身的高
        var tleft = ctrlPop.offsetLeft;    //TT控件的定位点宽
        var ttyp  = ctrlPop.type;          //TT控件的类型
        while (ctrlPop = ctrlPop.offsetParent){ttop+=ctrlPop.offsetTop; tleft+=ctrlPop.offsetLeft;}
        
        dads.top  = (ttyp=="image")? ttop+thei : ttop+thei+6;
        dads.left = tleft;
        this.ctrlPanel.style.display = '';
//        document.attachEvent("onkeyup",this.DocumentOnkeyup);
//        document.attachEvent("onclick",this.DocumentOnClick);
    },
    IsVisible:function()
    {
        return this.ctrlPanel.style.display != 'none'
    },
    Hide: function()
    {
        if(this.ctrlPanel)
            this.ctrlPanel.style.display = 'none';
//        document.detachEvent("onkeyup",this.DocumentOnkeyup);
//        document.detachEvent("onclick",this.DocumentOnClick);            
    },
    DocumentOnClick:function() //任意点击时关闭该控件	//ie6的情况可以由下面的切换焦点处理代替
    {
        for(var i=0;i<g_CurrentPickers.length;i++)
        {
            if(event.srcElement != g_CurrentPickers[i].ctrlPop)
                g_CurrentPickers[i].Hide();
        }
    },
    DocumentOnkeyup:function()	//按Esc键关闭，切换焦点关闭
    {
        if (window.event.keyCode==27)
        {
            for(var i=0;i<g_CurrentPickers.length;i++)
            {
            if(event.srcElement != g_CurrentPickers[i].ctrlPop)
                g_CurrentPickers[i].Hide();
            }
        }
        else if(document.activeElement)
        {
		    if(document.activeElement != this.ctrlPop)
		    {
                for(var i=0;i<g_CurrentPickers.length;i++)
                {
            if(event.srcElement != g_CurrentPickers[i].ctrlPop)
                g_CurrentPickers[i].Hide();
                }
		    }
		}     
    }
};