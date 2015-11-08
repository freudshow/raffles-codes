function BoxScroll(gv,box)
{
    if(document.getElementById(gv))
    {
        document.getElementById(box).style.display = "block";
    }
    else
    {
        document.getElementById(box).style.display = "none";
    }
}