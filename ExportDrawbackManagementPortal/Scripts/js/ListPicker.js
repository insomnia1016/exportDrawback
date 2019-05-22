var ListPicker = Class.create();
ListPicker.prototype = Object.extend(new Picker(), 
{
    initialize: function(keyName,ctrlPanel,ctrlPop,callServer,isForceCheck) 
    { 
        this.keyName = keyName;
        this.ctrlPanel = ctrlPanel;
        this.ctrlPop = ctrlPop; 
        this.isCallback = true;
        this.CallServer = callServer;//?:callServer:new function(){alert('未实现!');};
        this.isForceCheck = isForceCheck;
    }, 
    ReInitCtrl:function(ctrlPop,ctrlCode,ctrlName,ctrlOthers,ctrlOtherNames)
    {
        this.ctrlPop = ctrlPop; 
        this.ctrlCode = ctrlCode;
        this.ctrlName = ctrlName;
        this.ctrlOthers = ctrlOthers?ctrlOthers:new Array();
        this.ctrlOtherNames = ctrlOtherNames?ctrlOtherNames:new Array();
        this.LoadData();
    },
   
    LoadData:function()
    {
        var hasdata = this.ctrlPanel.getAttribute("hasdata");
        if(hasdata == null)
        {
            var arg = GetPickerData("ListPicker#" + this.keyName);
            if(arg != null)
            {
                this.ctrlPanel.innerHTML = arg;
                this.ctrlPanel.setAttribute("hasdata","ok");
            }
            else if(typeof(this.CallServer) == "function")
            {
                this.CallServer('GetListHtml',this.ctrlPop);
                this.ctrlPanel.setAttribute("hasdata","wait");
            }
            
//            if(this.PopCtrl && this.PopCtrl.value != '')
//            {
//                if(CheckNames())
//                {
//                    Hide();
//                }
//            }
        } 
    },
    ReceiveServerData:function(arg,context)
    {
        this.ctrlPanel.innerHTML = arg;
        this.ctrlPanel.setAttribute("hasdata","ok");
        SetPickerData("ListPicker#" + this.keyName,arg);
        if(this.PopCtrl && this.PopCtrl.value != '' && this.CheckNames())
        {
            this.Hide();
        }
        else
        {
            //this.Show();
        }
    },
    ClearCtrlValue:function()
    {
        this.ctrlPop.title = '无效'; 
        this.ctrlCode.value = '';
        if(this.ctrlName != this.ctrlPop)
            this.ctrlName.value = '';
        for(var ctrl in this.ctrlOthers)
        {
            ctrl.value = '';
        }
    },
    SetCtrlValue:function(code,name,others)
    {
        this.ctrlCode.value = code;
        this.ctrlName.value = name;
        this.ctrlName.title = code + ' ' + name;
        for(var i=0;i<this.ctrlOtherNames.length;i++)
        {
            if(this.ctrlOthers[i])
                this.ctrlOthers[i].value = others[i];
            this.ctrlName.title = this.ctrlName.title + ' ' + others[i];
        }
    },
    CheckNames:function()
    {     
        this.LoadData();
         var searchKey = ''
        if(this.ctrlPop) 
            searchKey = this.ctrlPop.value;
        if(searchKey == "")
        {
            if(this.isForceCheck)
                this.ClearCtrlValue();
            return false; 
        }
        
        searchKey = searchKey.toUpperCase();
        var divs = this.ctrlPanel.getElementsByTagName("div");
        for(var i=0;i<divs.length;i++)
        { 
            var divData = divs[i];
            var code = divData.getAttribute("itemcode");
            if(!code) continue;
            var name = divData.getAttribute("itemname");
            var others = new Array();
            for(var j=0;j<this.ctrlOtherNames.length;j++)
            {
                others[j] = divData.getAttribute(this.ctrlOtherNames[j]);
            }

            if(code == searchKey || name == searchKey || KeyIndexOfArray(searchKey,others) > -1 )
            {
                this.SetCtrlValue(code,name,others);
                this.Hide();
                return true;
            }
        }
        if(this.isForceCheck)
            this.ClearCtrlValue();
        return false;
    },
    
    CheckNamesOnKeyEnter:function()
    {
        if(event.keyCode == 13)
        {
            if(!this.CheckNames())
            {
                this.Show();
            }
            else
            {
                this.Hide();
            }
            return false;
        }
    },
    OnKeyUp:function()
    {
        switch(event.keyCode)
        {
            case 40://Down
                this.Show();
                break;
            case 38://Up
                this.Hide();
                this.CheckNames();
                break;
            case 123://F12
                this.CheckNames();
                break;
            default:
                break;                           
        }
    },
    ItemOnClick:function(item)
    {
        var code = item.getAttribute("itemcode");
        if(!code) return;
        
        var name = item.getAttribute("itemname");
        var others = new Array();
        for(var j=0;j<this.ctrlOtherNames.length;j++)
        {
            others[j] = item.getAttribute(this.ctrlOtherNames[j]);
        }
        this.SetCtrlValue(code,name,others);
        this.Hide();
    }

    
});
