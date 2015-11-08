Option Explicit
Dim fso, fso2
Dim fls, fls2, fl, sfd
Dim date_str As String
Dim var As String
Dim sp As Variant
Dim tuhao As String
Dim banbenhao As String
Dim deviser_IC As String
Dim bili As String
Dim g As Integer
Dim u As Integer
Dim list()
Dim dwgpath As String
Dim jpgPath As String
Dim FilterType(0) As Integer
Dim FilterData(0) As Variant

Private Sub cmdInsert_Click()
UserForm1.Hide
Set fso = CreateObject("scripting.filesystemobject")

Dim w As Integer
w = g
ThisDrawing.ActiveLayer = ThisDrawing.Layers.Item("0")

Dim i, j As Integer
Dim nameNo As String

Dim scalefactor As Double
Dim rotationAngle As Double
Dim imageName As String
Dim rasterObj As AcadRasterImage
Dim n As Integer
Dim strname As String
Dim str As String
Dim st As String
Dim fileobj As File

Dim blockRefObj As AcadBlockReference
Dim insertionPoint(0 To 2) As Double
Dim insertionPnt As Variant
Dim distance_H As Variant
Dim distance_W As Variant
Dim namepath As String
Dim nextpoint As Variant

If tbScale.Text = "" Then
   MsgBox "请输入比例值！"
   UserForm1.Show
Else

st = "\\172.16.7.55\sign$\dwg\"    '所在文件夹
 
Dim ta2 As String
Dim tb2 As String
Dim re2 As Integer
For re2 = 1 To g + 1
    ta2 = Mid(ListBox1.list(re2 - 1), 1, 7)
    tb2 = st + "\" + ta2 & ".dwg"
    If fso2.FileExists(tb2) Then
      ' MsgBox "文件存在！"
    Else
       MsgBox "未找到 " & ListBox1.list(re2 - 1) & " 插入签名图片，请联系软件开发组！"
       UserForm1.Show
       Exit Sub
    End If
Next re2

ThisDrawing.SendCommand "UCS" & vbCr & "W" & vbCr & vbCr

insertionPnt = ThisDrawing.Utility.GetPoint(, "Enter a insert point: ")
distance_H = ThisDrawing.Utility.GetDistance(insertionPnt, "Enter a height point: ")

nextpoint = ThisDrawing.Utility.GetPoint(insertionPnt, "Enter the Width point: ")
distance_W = Abs(insertionPnt(0) - nextpoint(0))
scalefactor = 1 * CDbl(distance_H) / 17  '如果不填满框格,10可以改的小些.
rotationAngle = 0

''''''''''''''''''''''签名及日期显示
'MsgBox "高度= " & distance_H
'MsgBox "长度= " & distance_W
Dim datetext As AcadText
Dim insertionpntdate(2) As Double
Dim height As Double

insertionpntdate(0) = insertionPnt(0) + distance_W + 5 * tbScale.Text
insertionpntdate(1) = insertionPnt(1) + 2 * tbScale.Text
insertionpntdate(2) = insertionPnt(2)
insertionPnt(0) = insertionPnt(0) + (distance_W - 15 * tbScale.Text) / 2   '12.5->
height = distance_H / 3

Dim r As Integer
If w > 0 Then
For i = 0 To w
    namepath = st & "\" & list(i) & ".dwg"
    Set blockRefObj = ThisDrawing.ModelSpace.InsertBlock(insertionPnt, namepath, 1 * scalefactor, 1 * scalefactor, 1#, 0) '5+  在Ｘ方向上放大
    Set datetext = ThisDrawing.ModelSpace.AddText(date_str, insertionpntdate, height)
    insertionPnt(1) = insertionPnt(1) - distance_H     '修改图片上下间距 0.7 ->
    insertionpntdate(1) = insertionpntdate(1) - distance_H
    ThisDrawing.SendCommand "dr" & vbCr & "object" & vbCr & "(handent " & Chr(34) & blockRefObj.Handle & Chr(34) & ")" & vbCr & vbCr & vbCr
Next i
End If
End If
UserForm1.Hide
End Sub

Private Sub cmdExit_Click()
End
End Sub

Private Sub cmdPreview_Click()
Dim url, xmlhttp, dom, node, xmlDOC, SoapRequest
UserForm1.Hide
ListBox1.Clear

Set fso2 = CreateObject("scripting.filesystemobject")
Image1.Picture = LoadPicture
Image2.Picture = LoadPicture
Image3.Picture = LoadPicture
Image4.Picture = LoadPicture
Image5.Picture = LoadPicture

'date_str = Date
'date_str = Now
Dim date1 As String
date1 = Now
Dim pc As Variant
pc = Split(date1, " ")
date_str = pc(0)

tuhao = tbDrawNumb.Text
banbenhao = tbVersionNumb.Text
deviser_IC = tbIcNumb.Text
bili = tbScale.Text

If deviser_IC = "Y00" Or deviser_IC = "" Then
   MsgBox "请设计员输入IC卡号！"
   UserForm1.Show
Else
   If Len(deviser_IC) <> 7 Then
      MsgBox "设计员输入IC卡号有误,请重新输入！"
      UserForm1.Show
   Else

If tuhao = "" And banbenhao = "" Then
   MsgBox "请输入图号和版本号！"
   UserForm1.Show
ElseIf tuhao = "" And banbenhao <> "" Then
   MsgBox "请输入图号！"
   UserForm1.Show
ElseIf tuhao <> "" And banbenhao = "" Then
   MsgBox "请输入版本号！"
   UserForm1.Show
ElseIf tuhao <> "" And banbenhao <> "" Then
    
    SoapRequest = "<?xml version=" & Chr(34) & "1.0" & Chr(34) & " encoding=" & Chr(34) & "utf-8" & Chr(34) & "?>" & _
    "<soap:Envelope xmlns:xsi=" & Chr(34) & "http://www.w3.org/2001/XMLSchema-instance" & Chr(34) & " " & _
    "xmlns:xsd=" & Chr(34) & "http://www.w3.org/2001/XMLSchema" & Chr(34) & " " & _
    "xmlns:soap=" & Chr(34) & "http://schemas.xmlsoap.org/soap/envelope/" & Chr(34) & ">" & _
    "<soap:Body>" & _
    "<GetApproveTemplate xmlns=" & Chr(34) & "http://tempuri.org/" & Chr(34) & ">" & _
    "<drawnICNo>" & deviser_IC & "</drawnICNo>" & _
    "<drawingNo>" & tuhao & "</drawingNo>" & _
    "<rev>" & banbenhao & "</rev>" & _
    "</GetApproveTemplate>" & _
    "</soap:Body>" & _
    "</soap:Envelope>"
    url = "http://172.16.7.55/services/drawing.asmx?op=GetApproveTemplate"
    Set xmlDOC = CreateObject("MSXML.DOMDocument")
    xmlDOC.loadXML (SoapRequest)
'    MsgBox SoapRequest

    Set xmlhttp = CreateObject("Msxml2.XMLHTTP")
    xmlhttp.Open "POST", url, False
    xmlhttp.setRequestHeader "Content-Type", "text/xml;charset=utf-8"
    xmlhttp.setRequestHeader "SOAPAction", "http://tempuri.org/GetApproveTemplate"
    xmlhttp.setRequestHeader "Content-Length", Len(SoapRequest)
    xmlhttp.Send (xmlDOC)

    If xmlhttp.Status = 200 Then
        xmlDOC.Load (xmlhttp.responseXML)
'        MsgBox "执行结果为：" & xmlDOC.getElementsByTagName("GetApproveTemplateResult")(0).Text
    Else
        MsgBox "failed"
    End If

var = xmlDOC.getElementsByTagName("GetApproveTemplateResult")(0).Text

If var = "NoDrawing" Then
   MsgBox "在系统里没有找到该版图纸！"
ElseIf var = "NoApproveTemplate" Then
   MsgBox "系统里存在该版图纸，但无审核模板，请联系您主管登陆到ECDMS中修改此纸的审批模版！"
Else
sp = Split(var, ";")

g = UBound(sp)
ReDim list(g)
For u = 0 To g
   list(u) = Mid(sp(u), 1, 7)
Next

For u = 0 To g
   ListBox1.AddItem sp(u)
Next u

If Mid(var, 9, 1) = ";" Then
   MsgBox "设计员的卡号在系统中没有找到，请检查输入是否有误！"
   UserForm1.Show
Else

'Dim jpgPath As String
jpgPath = "\\172.16.7.55\sign$\jpg\"

Dim z As Integer
z = g + 1

Dim ta As String
Dim tb As String
Dim re As Integer
For re = 1 To z
    ta = Mid(ListBox1.list(re - 1), 1, 7)
    tb = jpgPath + ta & ".jpg"
    If fso2.FileExists(tb) Then
      ' MsgBox "文件存在！"
    Else
       MsgBox "未找到 " & ListBox1.list(re - 1) & " 的预览签名图片，请联系软件开发组！"
       UserForm1.Show
       Exit Sub
    End If
Next re

If z >= 2 Then
   If z = 3 Then
      Image3.Picture = LoadPicture(jpgPath & Mid(ListBox1.list(2), 1, 7) + ".jpg")
   ElseIf z = 4 Then
      Image3.Picture = LoadPicture(jpgPath & Mid(ListBox1.list(2), 1, 7) + ".jpg")
      Image4.Picture = LoadPicture(jpgPath & Mid(ListBox1.list(3), 1, 7) + ".jpg")
   ElseIf z = 5 Then
      Image3.Picture = LoadPicture(jpgPath & Mid(ListBox1.list(2), 1, 7) + ".jpg")
      Image4.Picture = LoadPicture(jpgPath & Mid(ListBox1.list(3), 1, 7) + ".jpg")
      Image5.Picture = LoadPicture(jpgPath & Mid(ListBox1.list(4), 1, 7) + ".jpg")
   End If

   If fso2.FileExists(jpgPath + Mid(ListBox1.list(0), 1, 7) & ".jpg") Then
      Image1.Picture = LoadPicture(jpgPath & Mid(ListBox1.list(0), 1, 7) + ".jpg")
   End If
   Image2.Picture = LoadPicture(jpgPath & Mid(ListBox1.list(1), 1, 7) + ".jpg")
Else
   MsgBox "审核人信息有误，请联系您主管登陆到ECDMS中修改此纸的审批模版"
End If
End If
End If
End If
End If
End If
cmdInsert.Enabled = True
UserForm1.Show
End Sub

Private Sub UserForm_Click()

End Sub


Sub AcadStartup()
    On Error Resume Next
    Dim currMenuGroup As AcadMenuGroup
    Set currMenuGroup = ThisDrawing.Application.MenuGroups.Item(1)

    ' 创建新工具栏
    Dim newToolbar As AcadToolbar
    Set newToolbar = currMenuGroup.Toolbars.Add("YCRO")  '电子签名&&图纸模版
    Dim submacro As String

    Dim newMenu As AcadPopupMenu
    Set newMenu = currMenuGroup.Menus.Add("YCRO")  '电子签名&&图纸模版
    
    Dim newbutton As AcadToolbarItem
    Dim subMenuItem As AcadPopupMenuItem
    Dim menuseparator As AcadPopupMenuItem

    submacro = "-vbarun userfor" + Chr(32)
    Set subMenuItem = newMenu.AddMenuItem(newMenu.count + 1, "电子签名", submacro)
    Set newbutton = newToolbar.AddToolbarButton(newToolbar.count + 1, "电子签名", "插入签名", submacro)
    newbutton.SetBitmaps "C:\icon\签字笔.bmp", "C:\icon\签字笔.bmp"
    
    submacro = "-vbarun addtemplate" + Chr(32)
    Set subMenuItem = newMenu.AddMenuItem(newMenu.count + 1, "图纸模版", submacro)
    Set newbutton = newToolbar.AddToolbarButton(newToolbar.count + 1, "图纸模版", "图纸模版", submacro)
    newbutton.SetBitmaps "C:\icon\图纸模版.bmp", "C:\icon\图纸模版.bmp"
    
    
    ' 显示工具栏
    newToolbar.Visible = True
    newMenu.InsertInMenuBar (ThisDrawing.Application.MenuBar.count + 1)

    '' 将工具栏固定在屏幕的左边。
    newToolbar.Dock acToolbarDockLeft

End Sub