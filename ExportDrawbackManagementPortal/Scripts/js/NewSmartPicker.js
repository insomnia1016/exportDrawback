var SmartPicker2 = Class.create();
SmartPicker2.prototype = Object.extend(new Picker(), 
{
    initialize: function(keyName,dataKeyName,callServer,codeLen,searchAttrs,showAttrs,dataAttrs) 
    { 
        try
        {
        this.dataKeyName = dataKeyName;
        this.keyName = keyName;
        this.ctrlPanel = $(keyName + "_Panel");
        this.dataPanle = $(keyName + "_Data");
        this.codeLen = codeLen?codeLen:1;
        this.ctrlPop = null; 
        this.isCallback = true;
        this.CallServer = callServer;//?:callServer:new function(){alert('未实现!');};
        this.searchAttrs =  searchAttrs?searchAttrs:new Array();
        this.showAttrs = showAttrs?showAttrs:this.searchAttrs
        this.dataAttrs =dataAttrs?dataAttrs:this.searchAttrs;
        this.xml = GetXmlDocument();
        this.xml.setProperty("SelectionLanguage","XPath");
        }
        catch(e)
        {
        alert(e.message);
        }
    }, 
    ReInitCtrl:function(ctrlPop,dataCtrls)
    {
        this.ctrlPop = ctrlPop; 
        this.dataCtrls = dataCtrls?dataCtrls:new Array();
        this.LoadData();
    },
   
    LoadData:function()
    {
        var hasdata = this.ctrlPanel.getAttribute("hasdata");
        if(hasdata == null || hasdata == '')
        {
            var arg = GetPickerData("Smart#" + this.dataKeyName);
            if(arg != null)
            {
                this.xml.loadXML(arg);
                this.ctrlPanel.setAttribute("hasdata","ok");
            }
            else if(typeof(this.CallServer) == "function")
            {
                this.CallServer('GetXml',this.ctrlPop);
                this.ctrlPanel.setAttribute("hasdata","wait");
            }
            
        } 
    },
    OnKeyUp:function()
    {
        switch(event.keyCode)
        {
            case 40://Down
                this.LikeFilterItems(30);
                this.Show();
                break;
            case 38://Up
                if(this.IsVisible())
                {
                this.AutoMatching();
                this.Hide();
                }
                break;
            case 123://F12
                this.AutoMatching();
                break;
            default:
                if(this.IsVisible())
                {
                    this.LikeFilterItems(30);
                }
                break;                           
        }
    },
    ReceiveServerData:function(arg,context)
    {
        if(arg == '')
        {
            status = "数据加载错误"; 
            this.ctrlPanel.setAttribute("hasdata","");
            return;
         }
        this.xml.loadXML(arg);
        SetPickerData("Smart#" + this.dataKeyName,arg);
        this.ctrlPanel.setAttribute("hasdata","ok");
        //this.ctrlPanel.innerHTML = arg;
        if(this.ctrlPop && this.ctrlPop.value != '' && this.CheckNames())
        {
            this.Hide();
        }
        else
        {

        }
    },
    OnDoubleClick:function()
    {
        this.LikeFilterItems(30);
        this.Show();
    },
    ClearCtrlValue:function()
    {
        //this.ctrlPop.title = '无效'; 
        for(var i=0;i<this.dataCtrls.length;i++)
        {
            if(this.dataCtrls[i] && this.dataCtrls[i] != this.ctrlPop)
                this.dataCtrls[i].value = '';
        }
    },
    SetCtrlValue:function(item)
    {
        for(var i=0;i<this.dataAttrs.length;i++)
        {
            var itemValue = item.getAttribute(this.dataAttrs[i]);
            if(this.dataCtrls[i])
                this.dataCtrls[i].value = itemValue;
        }
    },
    /*
    *匹配地一条符合条件的数据
    */
    AutoMatching:function()
    {
        var nodes = this.EqFilterItems();;
        if(nodes.length==0)
        {
            this.Show();
            return false;
        }
        this.SetCtrlValue(nodes[0])
        this.Hide();
        return true;
    }
    ,
    //
    CheckNames:function()
    {   
        var nodes = this.LikeFilterItems(30);
        if(nodes.length == 1)
        {
            this.SetCtrlValue(nodes[0])
            return true;
        }        
        this.ClearCtrlValue();
        return false;
    },
    EqFilterItems:function(searchKey)
    {
        if(!searchKey)
        {
            searchKey = ''
            if(this.ctrlPop) 
                searchKey = this.ctrlPop.value.toUpperCase();
            if(searchKey == "")
            {
                this.ClearCtrlValue();
                return new Array();
            }
        }
        this.LoadData();
        if(this.ctrlPanel.getAttribute("hasdata") !="ok")
        {
            return new Array();
        } 
 
        var exp = "Root/Items/Item[@ItemCode='" + searchKey + "' ";
        for(var i=0;i<this.searchAttrs.length;i++)
        {
            exp = exp + " or @" + this.searchAttrs[i] + "='" + searchKey + "'";
        }
        exp = exp + "]" ;
        var nodes = this.xml.selectNodes(exp);
        return nodes;
    }
    ,
    /*
    *按指定的查询条件和显示数量，过滤和显示数据项
    */
    LikeFilterItems:function(maxShowCount,searchKey)
    {
        if(!searchKey)
        {
            searchKey = ''
            if(this.ctrlPop) 
                searchKey = this.ctrlPop.value.toUpperCase();
            if(searchKey == "")
            {
                this.ClearCtrlValue();
            }
        }
        this.LoadData();
        if(this.ctrlPanel.getAttribute("hasdata") !="ok")
        {
            return new Array();
        } 
 
        var exp = "Root/Items/Item[starts-with(@ItemCode,'" + searchKey + "')  ";
        for(var i=0;i<this.searchAttrs.length;i++)
        {
            exp = exp + " or starts-with(@" + this.searchAttrs[i] + ",'" + searchKey + "')";
        }
        exp = exp + "]" ;
        var nodes = this.xml.selectNodes(exp);

        var divs = this.dataPanle.childNodes;
        for(var i=divs.length-1;i>=0;i--)
        {
            this.dataPanle.removeChild(divs[i])
        }
        if(nodes.length == 0)
        {
            item = document.createElement("div");
            item.innerHTML = "请减少过滤条件，没有符合条件的记录。"
            this.dataPanle.appendChild(item); 
        }
        for(var i=0;i<nodes.length;i++)
        {
            var code = nodes[i].getAttribute("ItemCode");
            var name = nodes[i].getAttribute("ItemName");
            var item = document.createElement("div");
            var otherAttrsString = "";
            var itemOtherHtml = "";
            for(var j=0;j<this.dataAttrs.length;j++)
            {
                otherAttrsString = otherAttrsString + " " + this.dataAttrs[j] + "='" + nodes[i].getAttribute(this.dataAttrs[j]) + "'";
            }
            
            for(var j=0;j<this.showAttrs.length;j++)
            {
                if(j>0)
                    itemOtherHtml = itemOtherHtml + "<span style='margin-left:5px;'> | " + nodes[i].getAttribute(this.showAttrs[j]) + "</span>";
                else
                    itemOtherHtml = itemOtherHtml + "<span> " + nodes[i].getAttribute(this.showAttrs[j]) + "</span>";              
            }
            
            var itemHtml = "<div style='background: #fff;cursor: hand;' onclick='" 
                         + this.keyName 
                         + ".ItemOnClick(this)' class='GridItemMouseOut' onmouseover=\"this.className = 'GridItemMouseOver'\" onmouseout=\"this.className = 'GridItemMouseOut'\" "
                         + "ItemCode='" + code + "' " + otherAttrsString +  ">"
                         //+ "<span style=''>" + GetFixLenStringWithPostfix(code,this.codeLen,'_') + "</span>" 
                         + itemOtherHtml 
                         + "</div>"
            item.innerHTML = itemHtml;

            this.dataPanle.appendChild(item); 
            
            if(maxShowCount != 0 && i>=maxShowCount)
            {
                item = document.createElement("div");
                //item.innerHTML = "<div style='background: #fff;cursor: hand;' onclick='" 
                               + this.keyName + ".ShowAllItem();' class='GridItemMouseOut' onmouseover=\"this.className = 'GridItemMouseOver'\" onmouseout=\"this.className = 'GridItemMouseOut'\" >More...<div>";               
                //item.innerHTML = "<div style='background: #fff;' class='GridItemMouseOut' onclick='alert(\"请增加过滤条件。\");' onmouseover=\"this.className = 'GridItemMouseOver'\" onmouseout=\"this.className = 'GridItemMouseOut'\" >More...<div>";
                item.innerHTML = "<div style='background: #fff;'  onclick='return false;' >More...<div>";
                item.title = "请增加过滤条件，符合条件记录数：" + nodes.length;
                this.dataPanle.appendChild(item); 
                break;
            }
        }
        return nodes;
    },
    ShowAllItem:function()
    {
    
    }
    ,
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
    ItemOnClick:function(item)
    {
        if(item.getAttribute("itemcode"))
        {
            this.SetCtrlValue(item);
            this.Hide();
        }
    }
});