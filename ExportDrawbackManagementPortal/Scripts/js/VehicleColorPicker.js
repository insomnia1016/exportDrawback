var VehicleColorPicker = Class.create();
VehicleColorPicker.prototype = Object.extend(new ListPicker(), 
{
    initialize: function(ctrlPanel,ctrlPop,callServer) 
    { 
        this.ctrlPanel = ctrlPanel;
        this.ctrlPop = ctrlPop; 
        this.isCallback = true;
        this.CallServer = callServer;//?:callServer:new function(){alert('未实现!');};
    }, 
    ReceiveServerData:function(arg,context)
    {
        this.ctrlPanel.innerHTML = arg;
        this.ctrlPanel.setAttribute("hasdata","ok");
        if(this.PopCtrl && this.PopCtrl.value != '' && this.CheckNames())
        {
            this.Hide();
        }
        else
        {
            //this.Show();
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
            this.ClearCtrlValue();
            return false; 
        }               

        searchKey = searchKey.toUpperCase();
        var keys = new Array();
        for(var i=0; i<searchKey.length;i++)
        {//
            keys.push(searchKey.charAt(i));
        }
        var divs = this.ctrlPanel.getElementsByTagName("div");
        
        var codestr = '';
        var namestr = '';
        for(var k=0;k<keys.length;k++)
        {
            for(var i=0;i<divs.length;i++)
            {
                var divData = divs[i];
                var code = divData.getAttribute("itemcode");
                if(!code) continue;
                var name = divData.getAttribute("itemname");
                
//                var others = new Array();
//                for(var j=0;j<this.ctrlOtherNames.length;j++)
//                {
//                    others[j] = divData.getAttribute(this.ctrlOtherNames[j]);
//                }

                if(code == keys[k])
                {
                    namestr = namestr + name;
                    codestr = codestr + code;
                    break;
                    //this.SetCtrlValue(code,name,others);
                    //this.Hide();
                    //return true;
                }
            }
        }
        if(codestr == '')
        {
            this.ClearCtrlValue();
            return false;
        }
        else
        {
            this.SetCtrlValue(codestr,namestr,null);
            this.Hide();
        }
    }
   
});

