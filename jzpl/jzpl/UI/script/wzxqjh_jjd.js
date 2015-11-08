
function SetPSQty(num,id)
{    
    obj_ = document.getElementById(id);
    if(obj_.value=="")
    {
    obj_.value=num;
    }
}
function CheckNum(id)
{
    obj_ = document.getElementById(id);
    var reg = /^\d+(\.\d+)?$/;  
    if(obj_.value!=""&&!reg.test(obj_.value))
    {
        alert("请输入数值！");            
        obj_.focus();   
    }
}
function CallServerForGetProdSiteList()
{
    recDate = document.getElementById("TxtRecieveDate").value;
    if(recDate=="") return;
    url = "ajaxHandler.aspx?mode=getpordplace&date="+escape(recDate);
    xmlHttp.open("GET",url,true);
    xmlHttp.onreadystatechange = SetProdSiteList;
    xmlHttp.send(null);
}
function SetProdSiteList()
{
    if(xmlHttp.readyState == 4)
    {         
        ClearSelectOptions("DdlProdSite");
        XmlProdSite =  xmlHttp.responseXML;
        objSelect = document.getElementById("DdlProdSite");        
        records = XmlProdSite.getElementsByTagName("recordSet");
        for(i=0;i<records.length;i++)
        {            
            value_ = records[i].getElementsByTagName("PLACE_ID")[0].text;            
            text_ = records[i].getElementsByTagName("SITE_DES")[0].text;
            option_ = new Option(text_,value_);
            objSelect.options.add(option_);
        }
        CallServerForGetRecivers();
    }
}
function ClearSelectOptions(name)
{
    obj =document.all(name);
    i = obj.length;
    while(i>0) obj.options[--i] = null;
}
function CallServerForGetRecivers()
{    
    recDate = document.getElementById("TxtRecieveDate").value;
    prodSite= document.all("DdlProdSite").value;
    url="ajaxHandler.aspx?mode=getreceiverlist&date="+escape(recDate)+"&prodsite="+escape(prodSite);
    xmlHttp.open("GET",url,true);    
    xmlHttp.onreadystatechange = SetRecievers;
    xmlHttp.send(null);
}
function SetRecievers()
{
    if(xmlHttp.readyState == 4)
    {
        objSelect = document.getElementById("DdlReciever");
        ClearSelectOptions("DdlReciever");
        recievers = xmlHttp.responseText.split(';');
        for(i=0;i<recievers.length;i++){
        option_ = new Option(recievers[i],recievers[i]);
        objSelect.options.add(option_);     
        }
        CallServerForGetLocations();   
    }
}
function CallServerForGetLocations()
{
    recDate = document.getElementById("TxtRecieveDate").value;
    prodSite= document.all("DdlProdSite").value;
    reciever = document.all("DdlReciever").value;
    url=url="ajaxHandler.aspx?mode=getlocationlist&date="+escape(recDate)+"&prodsite="+escape(prodSite)+"&reciever="+escape(reciever);
    xmlHttp.open("GET",url,true);
    xmlHttp.onreadystatechange=SetLoacations;
    xmlHttp.send(null);
}
function SetLoacations()
{
    if(xmlHttp.readyState == 4)
    {
        objSelect = document.all("DdlLocation");
        ClearSelectOptions("DdlLocation");
        locations = xmlHttp.responseText.split(';');
        for(i =0;i<locations.length;i++)
        {
            option_ = new Option(locations[i],locations[i]);
            objSelect.options.add(option_);
        }
    }
}
function checkInput2()
{
    popudiv = document.getElementById("out_popu");
    jjd_count = parseInt(document.getElementById("TxtJjdCount").value);
    tbl_ = document.getElementById("GVData");   
    if(!tbl_) return false;   
    rows_ = tbl_.getElementsByTagName("tr");
    for(i=0;i<rows_.length;i++)
    {
        input_ = rows_[i].getElementsByTagName("input");    
        if(input_.length>0&&input_[0].checked)
        {
            show(popudiv,event.clientX+40,event.clientY+30);  
            document.getElementById("BtnInsertJjd").disabled = "disabled";  
            return false;
        }
    }
    hidden(popudiv);
    alert("无效打印！");
    return false;
}

function SetSelectedColor(oSrc)
{    
    lines = oSrc.parentNode.childNodes;
    for(i=1;i<lines.length;i++)
    {
        lines[i].style.backgroundColor="#F7F6F3";
    }
    oSrc.style.backgroundColor="#EBECEB";
}

function SetSelectedJjdNoToLableText(oSrc)
{
    document.getElementById("TxtJjdShow").innerText = oSrc.innerText;
    document.getElementById("BtnInsertJjd").disabled="";
}