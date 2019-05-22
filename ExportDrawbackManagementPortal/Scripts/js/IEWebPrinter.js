var IEWebPrinter = Class.create();
IEWebPrinter.prototype=
{
    initialize: function(wb,setting) 
    { 
        this.RegKeys = new Array("header","footer","margion_left","margion_top","margion_right","margion_bottom");
        this.RegKeyPath = "HKEY_CURRENT_USER\\Software\\Microsoft\\Internet Explorer\\PageSetup\\";
        if(wb == null)
        {
            alert("浏览器对象未能初始化错误！");
            return;
        }
        this.WebBrowser = wb;
        this.CurrentSetting = new Object();
        if(setting == null)
        {
            setting = new Object();
            setting.header = "";
            setting.footer = "";
            setting.margion_left = "0.0";
            setting.margion_top = "0.0";
            setting.margion_right = "0.0";
            setting.margion_bottom = "0.0";
        }
        
        this.CurrentSetting.header = setting.header;
        this.CurrentSetting.footer = setting.footer;
        this.CurrentSetting.margion_left = setting.margion_left;
        this.CurrentSetting.margion_top = setting.margion_top;
        this.CurrentSetting.margion_right = setting.margion_right;
        this.CurrentSetting.margion_bottom = setting.margion_bottom;
        for(var tmpKey in this.RegKeys)
        {
            this.CurrentSetting[tmpKey] = setting[tmpKey];
        }        
        this.OriginalSetting = this.ReadSettingFromReg();
        
    }, 
     
    WriteSettingToReg: function(setting) 
    {
        if(setting == null)
            setting = CurrentSetting;
        try
        {
            var regWsh = CreateObject("WScript.Shell");
        }
        catch(e){alert("error"); return;}
        for(var tmpKey in this.RegKeys)
        {
            regWsh.RegWrite(regKeyPath + tmpKey,setting[tmpKey]);
        }       
    },
    ReadSettingFromReg:function()
    {
        var setting = new Object();
        
        try
        {
            var regWsh = CreateObject("WScript.Shell");
        }
        catch(e){alert("error"); return;}
        for(var tmpKey in this.RegKeys)
        {
            setting[tmpKey] = regWsh.RegRead(this.RegKeyPath + tmpKey);
        }  
    }
};
