<%@ Page Language="C#" AutoEventWireup="true" Codebehind="jp_pkg_issue_a.aspx.cs"
    Inherits="jzpl.UI.Package.jp_pkg_issue_a" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>大包物资下发</title>
    <link href="../../UI/CSS/common.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="../../UI/script/common.js"></script>

    <script type="text/javascript">
    window.onload=function(){
    objs = myf("GVData").getElementsByTagName("input");
    for(i=0;i<objs.length;i++)
    {
    if(objs[i].type=="text")
    {
    objs[i].readOnly=true;
    }
    }
    }
    function IssueQtyLnk_Click(OnHandQty,ShowSelectedQtyLalbeId)
    {
    try{ReleaseQty_  = parseFloat(myf('LblReleasedQty').innerText);      }catch(e){ReleaseQty_=0;}//申请下达数量
    try{TotalQty_    = parseFloat(myf("LblTotalQty").innerText);  if(isNaN(TotalQty_)){throw "err";}       }catch(e){TotalQty_=0;} //已选择总数   
    try{SelectedQty_ = parseFloat(myf(ShowSelectedQtyLalbeId).value); if(isNaN(SelectedQty_)){throw "err";} }catch(e){SelectedQty_=0;}//已在本位置上选择要下发的数量
    try{ReqIssueQty_    = parseFloat(myf("LblIssuedQty").innerText); if(isNaN(ReqIssueQty_)){throw "err";} }catch(e){ReqIssueQty_=0;}//申请已下发的数量
    OnHandQty_ = parseFloat(OnHandQty);//当前现有量
    if(OnHandQty_==0||isNaN(OnHandQty_)){return false;}
    RemainQty_ = FloatAdd(FloatSub(FloatSub(ReleaseQty_,ReqIssueQty_),TotalQty_),SelectedQty_);//下达数量-申请已下发-已选择总数+选择行已选择要下发的数量

//    if(RemainQty_<=0)
//    {
//    alert("操作失败，下发数量已到最大值。");
//    return false;
//    }
    //alert(ReleaseQty_);
    //alert(ReqIssueQty_);
    var MinQty_;
    if(RemainQty_>OnHandQty_)
    {
    MinQty_=OnHandQty_;
    }
    else
    {
    MinQty_=RemainQty_;
    }     

    //弹出输入框，已下达数量和库存现有量的小者为默认值，返回下发数量
    var IssueQty_ = prompt("请输入下发数量：",MinQty_);    
    if(IssueQty_==null) return false;
    var reg = /^\d+(\.\d+)?$/; 
    if(IssueQty_==undefined||IssueQty_==""||!reg.test(IssueQty_))
    {
        alert("操作失败，请输入有效数值。");
        return false;
    }   
    IssueQty_ =  parseFloat(IssueQty_);

    if(IssueQty_>MinQty_)
    {
    alert("操作失败，下发数量已超出可下发的最大数量。")
    return false;
    }


    //判断下发数量不能大于库存现有量
    //判断下发数量不能超过下达数量
    //累计总数量，不能超过下达数量
    //显示下发数量

    //显示累计数量
    myf(ShowSelectedQtyLalbeId).value = IssueQty_;
    //$('LblTotalQty').innerText = (Math.round(TotalQty_*100000) - Math.round(SelectedQty_*100000) + Math.round(IssueQty_*100000))/100000;
    
    myf('LblTotalQty').innerText = FloatAdd(FloatSub(TotalQty_,SelectedQty_),IssueQty_);
     return false;
    }    
    </script>

    <base target="_self" />
</head>
<body>
    <form id="form1" runat="server">
        <div style="margin: 10px">
        <div id="title">大包物资下发<hr /></div>
        
            <table>
                <tr>
                    <td style="width: 80px">
                        申请ID：</td>
                    <td>
                        <asp:Label ID="LblReqId" runat="server" Font-Bold="True"></asp:Label></td>
                    <td style="text-align:right; width: 80px;">
                       下达数量：</td>
                    <td>
                        <asp:Label ID="LblReleasedQty" runat="server" Font-Bold="True"></asp:Label></td>
                    <td style="width: 80px; text-align:right">
                        下发数量：</td>
                    <td >
                        <asp:Label ID="LblIssuedQty" runat="server" Font-Bold="True"></asp:Label></td>
                </tr>
            </table>
            <table>
                <tr>
                    <td style="width: 80px">
                        大包编号：</td>
                    <td>
                        <asp:Label ID="LblPackageNo" runat="server" Font-Bold="True"></asp:Label></td>
                </tr>
                <tr>
                    <td style="width: 80px">
                        大包名称：</td>
                    <td>
                        <asp:Label ID="LblPackageName" runat="server" Font-Bold="True"></asp:Label></td>
                </tr>
                <tr>
                    <td style="width: 80px">
                        零件编号：</td>
                    <td>
                        <asp:Label ID="LblPartNo" runat="server" Font-Bold="True"></asp:Label></td>
                    <tr>
                        <td style="width: 80px">
                            零件名称：</td>
                        <td>
                            <asp:Label ID="LblPartName" runat="server" Font-Bold="True"></asp:Label></td>
                        <tr>
                            <td style="width: 80px">
                                零件规格：</td>
                            <td>
                                <asp:Label ID="LblPartSpec" runat="server" Font-Bold="True"></asp:Label></td>
                        </tr>
            </table>
            <hr />
            <table>
                <tr>
                    <td style="width: 80px">
                        库位：
                    </td>
                    <td style="width: 401px">
                        <asp:TextBox ID="TxtLocation" runat="server" Width="300px"></asp:TextBox></td>
                    <td style="width: 80px; text-align: right">
                        <asp:Button ID="BtnResult" runat="server" CssClass="button_1" Text="筛选" OnClick="BtnResult_Click" /></td>
                </tr>
            </table>
            <hr />
            <div style="width: 580px; height: 200px; overflow: scroll; border: 1px solid;">
                <asp:GridView ID="GVData" runat="server" CssClass="gv" AutoGenerateColumns="False"
                    Width="1040px" OnRowDataBound="GVData_RowDataBound">
                    <EmptyDataTemplate>
                        <table style="width: 1040px;" class="gv" border="1" bordercolor="white" cellpadding="0"
                            cellspacing="0">
                            <tr>
                                <th style="width: 80px">
                                    合格
                                </th>
                                <th style="width: 80px">
                                    免检
                                </th>
                                <th style="width: 80px;">
                                    合格
                                </th>
                                <th style="width: 80px">
                                    免检
                                </th>
                                <th style="width: 80px;" rowspan="2">
                                    公司
                                </th>
                                <th style="width: 80px;" rowspan="2">
                                    区域ID</th>
                                <th style="width: 160px;" rowspan="2">
                                    区域描述</th>
                                <th style="width: 80px;" rowspan="2">
                                    位置ID</th>
                                <th style="width: 160px;" rowspan="2">
                                    位置描述</th>
                                <th style="width: 80px;" rowspan="2">
                                    到货ID</th>
                                <th style="width: 80px;" rowspan="2">
                                    到货日期
                                </th>
                            </tr>                           
                        </table>
                    </EmptyDataTemplate>
                    <Columns>
                        <asp:BoundField DataField="objid">
                            <HeaderStyle CssClass="hidden" />
                            <ItemStyle CssClass="hidden" />
                        </asp:BoundField>
                        <asp:BoundField DataField="rowversion">
                            <HeaderStyle CssClass="hidden" />
                            <ItemStyle CssClass="hidden" />
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="选定合格">
                            <HeaderStyle Width="80px" />
                            <ItemTemplate>
                                <asp:TextBox ID="GVLblOkQty" runat="server" BorderWidth="0px"
                                     Width="78px"></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="选定免检">
                        <HeaderStyle Width="80px" />
                            <ItemTemplate>
                            <asp:TextBox ID="GVLblNochkQty" runat="server" BorderWidth="0px"
                                     Width="78px"></asp:TextBox>
                                
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="可用合格">
                            <HeaderStyle Width="80px" />
                            <ItemTemplate>
                                <asp:LinkButton ID="GVLnkOkQty" runat="server"><%# Eval("part_ok_qty")%></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="可用免检">
                        <HeaderStyle Width="80px" />
                            <ItemTemplate>
                                <asp:LinkButton ID="GVLnkNochkQty" runat="server"><%# Eval("no_check_qty") %></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="company_id" HeaderText="公司">
                            <HeaderStyle Width="80px" />                            
                        </asp:BoundField>
                        <asp:BoundField DataField="area_id" HeaderText="区域ID">
                            <HeaderStyle Width="80px" />                            
                        </asp:BoundField>
                        <asp:BoundField DataField="area" HeaderText="区域描述">
                            <HeaderStyle Width="160px" />                            
                        </asp:BoundField>
                        <asp:BoundField DataField="location_id" HeaderText="位置ID">
                            <HeaderStyle Width="80px" />                            
                        </asp:BoundField>
                        <asp:BoundField DataField="location" HeaderText="位置描述">
                            <HeaderStyle Width="160px" />                            
                        </asp:BoundField>
                        <asp:BoundField DataField="arrival_id" HeaderText="到货ID">
                            <HeaderStyle Width="80px" />                            
                        </asp:BoundField>
                        <asp:BoundField DataField="arrived_date" HeaderText="到货日期">
                            <HeaderStyle Width="80px" />                            
                        </asp:BoundField>
                    
                    </Columns>
                </asp:GridView>                
            </div>
            <hr />
            <table>
                <tr>
                    <td style="width: 488px">
                        已选定数量：<asp:Label ID="LblTotalQty" runat="server"></asp:Label></td>
                    <td style="width: 80px; text-align: right">
                        <asp:Button ID="BtnSave" runat="server" CssClass="button_1" Text="保存" OnClick="BtnSave_Click" /></td>
                </tr>
            </table>
        </div>
        <asp:HiddenField ID="HiddenObjid" runat="server" />
        <asp:HiddenField ID="HiddenRowversion" runat="server" />
    </form>
</body>
</html>
