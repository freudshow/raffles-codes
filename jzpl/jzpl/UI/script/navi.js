var timeout	= 500;
var closetimer	= 0;
var ddmenuitem	= 0;

// open hidden layer
function mopen(id)
{	
	// cancel close timer
	mcancelclosetime();

	// close old layer
	//--zyj--//if(ddmenuitem) ddmenuitem.style.visibility = 'hidden';
	if(ddmenuitem) mclose();

	// get new layer and show it
	ddmenuitem = document.getElementById(id);
	
	ddmenuitem.style.visibility = 'visible';
	//document.getElementById('jzpl_page').style.visibility='hidden';
	if(isie6()) document.getElementById('jzpl_page').style.display='none';
	stopBubble(event);
}
// close showed layer
function mclose()
{
	if(ddmenuitem) 
	{ddmenuitem.style.visibility = 'hidden';
	//document.getElementById('jzpl_page').style.visibility='visible';
	if(isie6()) document.getElementById('jzpl_page').style.display='block';}
}

// go close timer
function mclosetime()
{
	closetimer = window.setTimeout(mclose, timeout);
}

// cancel close timer
function mcancelclosetime()
{
	if(closetimer)
	{
		window.clearTimeout(closetimer);
		closetimer = null;
	}
}

// close layer when click-out
document.onclick = mclose; 
function isie6()
{
    var browser=navigator.appName 
    var b_version=navigator.appVersion 
    var version=b_version.split(";"); 
    var trim_Version=version[1].replace(/[ ]/g,"");  
    if(browser=="Microsoft Internet Explorer" && trim_Version=="MSIE6.0") 
    { 
        return true; 
    } 
    return false;
}
function stopBubble(e)
{
    if(e&&e.stopPropagation)
    {
        e.stopPropagation();
    }
    else
    {
        window.event.cancelBubble=true;
    }
}