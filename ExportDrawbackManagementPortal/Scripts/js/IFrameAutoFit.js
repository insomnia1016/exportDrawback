    function  iframeAutoFit()    //author:  meizz(梅花雪  20041225)  
    {  
       if(self!=top  &&  window.name=="contentFrame")  //这个  name  对应的是  iframe  的  name  
       {  
           var  iframe  =  parent.document.getElementById(window.name);  
           if(iframe)  
           {  
               iframe.parentNode.style.height  =  iframe.style.height;  
               iframe.style.height  =  parent.window.screen.height<679?"377px":(parent.window.screen.height - 23 - 156-77);  
               var  h  =  document.body.scrollHeight;  
               var  minimalHeight  =  parseInt((window.screen.width*11)/16,  10)  -  280;  
               h  =  h<minimalHeight  ?  minimalHeight  :  h;  
               if(window.navigator.appName  ==  "Microsoft  Internet  Explorer" &&  iframe.frameBorder=="1")
               {
                    h  +=  4;
               }
               //alert(parent.window.screen.height);
               //alert(h);
               //alert(iframe.style.height);
               //if(h > iframe.style.height)
               iframe.parentNode.style.height  =  iframe.style.height  =  h;  
           }  
           //else  alert("Iframe's  id  unequal  to  iframe's  name!");  
       }  
    }  
   if(document.attachEvent)  window.attachEvent("onload",  iframeAutoFit);  
   else  window.addEventListener('load',  iframeAutoFit,  false);  

