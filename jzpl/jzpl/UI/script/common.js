
function myf(id){
	return document.getElementById(id);
}
function FloatAdd(arg1,arg2)
{
    var r1,r2,m;
    try{r1=arg1.toString().split(".")[1].length;}catch(e){r1=0;}
    try{r2=arg2.toString().split(".")[1].length;}catch(e){r2=0;}
    m=Math.pow(10,Math.max(r1,r2));
    return Math.round(arg1*m+arg2*m)/m;
}
function FloatSub(arg1,arg2)
{
    return FloatAdd(arg1,-arg2);
}
function checkValue(obj,errMsg)
{
    if(obj.value=='')
    {
        alert(errMsg);
        obj.focus();
        return false;
    }
    return true;
}
var oldColor;

function SetNewColor(oSrc)
{       
    oldColor=oSrc.style.backgroundColor;
    oSrc.style.backgroundColor="#EBECEB";    
}
function SetOldColor(oSrc)
{    
    oSrc.style.backgroundColor=oldColor;
}
function SetSelectedColor(oSrc)
{    
    objParent = oSrc.parentNode.parentNode;
    for(i=1;i<objParent.childNodes.length;i++)
    {        
        objParent.childNodes[i].style.backgroundColor="#FFFFFF";
        objParent.childNodes[i].style.color="#000000";
    }
    oSrc.parentNode.style.backgroundColor="#00275E";
    oSrc.parentNode.style.color="#FFFFFF";
}

function checkNum(obj)
{
    var reg = /^\d+(\.\d+)?$/;
    if(typeof(obj)=="object")
    { 
        if(obj.value==null||obj.value=="")
        {
            return false;
        }
        if(!reg.test(obj.value))
        {  
            return false;
        } 
    }
    if(typeof(obj)=="string")
    {
        if(!reg.test(obj))
        {                       
            return false;
        } 
    }  
    return true;            
}

function show(obj,x,y)
{
    obj.style.display="block";
    obj.style.left=x;
    obj.style.top=y;
}
function hidden(obj)
{
    if(typeof obj == "object")
    {
        obj.style.display="none";
    }
    else
    {
        if(typeof obj == "string")
        {
            document.getElementById(obj).style.display="none";
        }
        else
        {
            alert("脚本错误！");
        }
    }    
}
/*判断文本是否为空
objid：判断对象的ID
return true：为空
return false：不为空*/

function checkTextIsNull(objid){    
text_=myf(objid).value;
if(text_.replace(/\s/g,"")=="") return true;
return false;
}

/*判断文本是否为数值
objid：判断对象的ID
return true：无符号浮点数
return false：非无符号浮点数
*/   
function checkIsNumber(objid){
obj=document.getElementById(objid);
var patrn=/^(([0-9]+\.[0-9]*[1-9][0-9]*)|([0-9]*[1-9][0-9]*\.[0-9]+)|([0-9]*[1-9][0-9]*))$/; 
if (!patrn.exec(obj.value)) return false ;
return true ;    
}
function SetGVBoxHeight(boxid,gvid)
{
    if(!myf(boxid)||!myf(gvid)) return;
    myf(boxid).style.height=myf(gvid).offsetHeight + 20 + 'px';
}
function ShowMD(MDName,para)
{
    var obj;
    if(MDName=="pkgLov")
    {
        obj = window.showModalDialog("pkg_lov.aspx",window,"status:no;dialogWidth:670px;dialogHeight:580px");
        return obj;
    }
    if(MDName=="pkgPartLov")
    {
        obj=window.showModalDialog("pkg_part_lov.aspx?dbbh="+para+"",window,"status:no;dialogWidth:670px;dialogHeight:620px");
        return obj;
    }
    if(MDName=="pkgLocLov")
    {
        obj=window.showModalDialog("pkg_loc_lov.aspx?company="+para[0]+"&areaid="+para[1]+"",window,"status:no;dialogWidth:550px;dialogHeight:550px");
        return obj;
    }
    if(MDName="pkgPartLov1")
    {
        obj=window.showModalDialog("pkg_part_lov1.aspx",window,"status:no;dialogWidth:670px;dialogHeight:620px");
        return obj;
    }
    
}
/*
求表列合计
paras:
    tableId:表（GridView控件）ID
    colNum:要合计的列的位置第一列就为1
    getValueType: 直接取单元格的值：0 取文本框的值：1 
return:
    返回合计
*/
function TableColumnTotalSalc(tableId,colNum,getValueType){
contain_ = myf(tableId);
rows_ = contain_.rows;
sum_ = 0;
for(i=1;i<rows_.length;i++){
if(getValueType==0){
value_ = rows_[i].cells[colNum-1].innerText;
}
if(getValueType==1){
value_ = rows_[i].cells[colNum-1].childNodes[0].value;
}
if(getValueType==2){
value_ = rows_[i].cells[colNum-1].childNodes[0].innerText;
}
sum_ += parseFloat(value_==""?0:value_);
}
return sum_;

}
