var SmartPicker = Class.create();
SmartPicker.prototype = Object.extend(new Picker(), 
{
    initialize: function(keyName,callServer,codeLen,attrs,dataAttrs) 
    { 
        try
        {
        this.keyName = keyName;
        this.ctrlPanel = $(keyName + "_Panel");
        this.dataPanle = $(keyName + "_Data");
        this.codeLen = codeLen?codeLen:1;
        this.ctrlPop = null; 
        this.isCallback = true;
        this.CallServer = callServer;//?:callServer:new function(){alert('未实现!');};
        this.attrs =  attrs?attrs:new Array();
        this.dataAttrs =dataAttrs?dataAttrs:new Array();
        this.xml = GetXmlDocument();
        this.xml.setProperty("SelectionLanguage","XPath");
        }
        catch(e)
        {
        alert(e.message);
        }
    }, 
    ReInitCtrl:function(ctrlPop,ctrlCode,ctrlName,dataCtrls)
    {
        this.ctrlPop = ctrlPop; 
        this.ctrlCode = ctrlCode;
        this.ctrlName = ctrlName;
        this.dataCtrls = dataCtrls?dataCtrls:new Array();
        this.LoadData();
    },
   
    LoadData:function()
    {
        var hasdata = this.ctrlPanel.getAttribute("hasdata");
        if(hasdata == null)
        {
            var arg = GetPickerData("Smart#" + this.keyName);
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
        this.xml.loadXML(arg);
        this.ctrlPanel.setAttribute("hasdata","ok");
        SetPickerData("Smart#" + this.keyName,arg);
        //this.ctrlPanel.innerHTML = arg;
        if(this.PopCtrl && this.PopCtrl.value != '' && this.CheckNames())
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
        this.ctrlName.title = '无效'; 
        this.ctrlCode.value = '';
        if(this.ctrlName != this.ctrlPop)
            this.ctrlName.value = '';
    },
    SetCtrlValue:function(values)
    {
        this.ctrlCode.value = values["ItemCode"];
        this.ctrlName.value = values["ItemName"];
        
        this.ctrlName.title = values["ItemCode"] + ' ' + values["ItemName"];
        for(var i=0;i<this.attrs.length;i++)
        {
            if(values[this.attrs[i]])
                this.ctrlName.title = this.ctrlName.title + ' ' + this.attrs[i];
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
        var values = new Object();
        values["ItemCode"] = nodes[0].getAttribute("ItemCode");
        values["ItemName"] = nodes[0].getAttribute("ItemName");
        this.SetCtrlValue(values)
        this.Hide();
        return true;
    }
    ,
    //
    CheckNames:function()
    {   
        var nodes = this.LikeFilterItems(20);
        if(nodes.length == 1)
        {
            var values = new Object();
            values["ItemCode"] = nodes[0].getAttribute("ItemCode");
            values["ItemName"] = nodes[0].getAttribute("ItemName");
            this.SetCtrlValue(values)
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
 
        var exp = "Root/Items/Item[@ItemCode='" + searchKey + "' or @ItemName='" + searchKey + "'";
        for(var i=0;i<this.attrs.length;i++)
        {
            exp = exp + " or @" + this.attrs[i] + "='" + searchKey + "'";
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
 
        var exp = "Root/Items/Item[starts-with(@ItemCode,'" + searchKey + "') or starts-with(@ItemName,'" + searchKey + "')";
        for(var i=0;i<this.attrs.length;i++)
        {
            exp = exp + " or starts-with(@" + this.attrs[i] + ",'" + searchKey + "')";
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
            for(var j=0;j<this.attrs.length;j++)
            {
                itemOtherHtml = itemOtherHtml + "<span margin-left:5px;> | " + nodes[i].getAttribute(this.attrs[j]) + "</span>";
                otherAttrsString = otherAttrsString + " " + this.attrs[j] + "='" + nodes[i].getAttribute(this.attrs[j]) + " '";
            }
            var itemHtml = "<div style='background: #fff;cursor: hand;' onclick='" 
                         + this.keyName 
                         + ".ItemOnClick(this)' class='GridItemMouseOut' onmouseover=\"this.className = 'GridItemMouseOver'\" onmouseout=\"this.className = 'GridItemMouseOut'\" "
                         + "ItemCode='" + code + "' ItemName='" + name + "'" + otherAttrsString +  ">"
                         + "<span style=''>" + GetFixLenStringWithPostfix(code,this.codeLen,'_') + "</span>" + itemOtherHtml 
                         + "<span style='margin-left:5px;'> | " + name + "</span></div>"
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
            var values = new Object();
            values["ItemCode"] = item.getAttribute("ItemCode");
            values["ItemName"] = item.getAttribute("ItemName");
            this.SetCtrlValue(values)
    //        for(var j=0;j<this.ctrlOtherNames.length;j++)
    //        {
    //            others[j] = item.getAttribute(this.ctrlOtherNames[j]);
    //        }
            this.Hide();
        }
    }

    
});
