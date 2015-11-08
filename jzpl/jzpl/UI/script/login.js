
function checkForm()
{
    var loginForm = document.login;    
    
    if(checkValue(loginForm.userid,'请输入用户名。') ==false) return;
    if(checkValue(loginForm.pwd,'请输入你的密码。')==false) return;
    loginForm.submit();
}
function autoSend()
{
	if (event.keyCode ==13)
	{
		checkForm();
	}		
}		

function formInit()
{
    var loginFrom = document.login;
    loginFrom.userid.focus();
}