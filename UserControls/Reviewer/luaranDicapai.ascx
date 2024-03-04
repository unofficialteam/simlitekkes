<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="luaranDicapai.ascx.cs" Inherits="simlitekkes.UserControls.Reviewer.luaranDicapai" %>
<div class="row" >
    <div class="col-lg-12">
        <div>
            <asp:Label runat="server" ID="lblJenisLuaran" ForeColor="Blue" Font-Bold="true"></asp:Label>
        </div>
    </div>
</div>


<div class="row" style="padding: 10px;">
    <div class="col-lg-12">
        <div>
            Target luaran:
            <asp:Label runat="server" ID="lblTarget" ForeColor="Green" Font-Bold="true"></asp:Label>
            &nbsp;|&nbsp;
            Capaian saat ini:
            <asp:Label runat="server" ID="lblCapaian" ForeColor="Green" Font-Bold="true" Text="-"></asp:Label>
        </div>
    </div>
</div>

<div class="row">
    <div class="col-lg-12">
        <div>
            <asp:Label runat="server" ID="lblInfoLuaran" ForeColor="Black"  Text="-"></asp:Label>
        </div>
    </div>
</div>
<div style="min-height: 20px;"></div>
<div class="row" style="padding: 10px;">
    <div class="col-lg-3">
        <asp:Panel runat="server" ID="pnlDok1" Visible="false">
            <table>
                <tr>
                    <td style="vertical-align: middle;">
                        <asp:LinkButton runat="server" ID="lbUnduhPdf1" ForeColor="Gray" OnClick="lbUnduhPdf1_Click">
                            <i class="hvr-buzz-out far fa-file-pdf" style="font-size: 50px; "></i>
                        </asp:LinkButton>
                    </td>
                    <td style="vertical-align: middle; word-wrap: break-word; word-break: break-all;">
                        <asp:Label runat="server" ID="lblJnsDokBuktiLuaran1" ForeColor="Black" ></asp:Label>
                    </td>
                </tr>
            </table>
        </asp:Panel>
    </div>
    <div class="col-lg-3">
        <asp:Panel runat="server" ID="pnlDok2" Visible="false">
            <table>
                <tr>
                    <td style="vertical-align: middle;">
                        <asp:LinkButton runat="server" ID="lbUnduhPdf2" ForeColor="Gray" OnClick="lbUnduhPdf2_Click">
                            <i class="hvr-buzz-out far fa-file-pdf" style="font-size: 50px; "></i>
                        </asp:LinkButton>
                    </td>
                    <td style="vertical-align: middle; word-wrap: break-word; word-break: break-all;">
                        <asp:Label runat="server" ID="lblJnsDokBuktiLuaran2" ForeColor="Black"></asp:Label>
                    </td>
                </tr>
            </table>
        </asp:Panel>
    </div>
    <div class="col-lg-3">
        <asp:Panel runat="server" ID="pnlDok3" Visible="false">
            <table>
                <tr>
                    <td style="vertical-align: middle;">
                        <asp:LinkButton runat="server" ID="lbUnduhPdf3" ForeColor="Gray" OnClick="lbUnduhPdf3_Click">
                            <i class="hvr-buzz-out far fa-file-pdf" style="font-size: 50px; "></i>
                        </asp:LinkButton>
                    </td>
                    <td style="vertical-align: middle;">
                        <asp:Label runat="server" ID="lblJnsDokBuktiLuaran3" ForeColor="Black"></asp:Label>
                    </td>
                </tr>
            </table>
        </asp:Panel>
    </div>
    <div class="col-lg-3">
        <asp:Panel runat="server" ID="pnlDok4" Visible="false">
            <table>
                <tr>
                    <td style="vertical-align: middle;">
                        <asp:LinkButton runat="server" ID="lbUnduhPdf4" ForeColor="Gray" OnClick="lbUnduhPdf4_Click">
                            <i class="hvr-buzz-out far fa-file-pdf" style="font-size: 50px; "></i>
                        </asp:LinkButton>
                    </td>
                    <td style="vertical-align: middle;">
                        <asp:Label runat="server" ID="lblJnsDokBuktiLuaran4" ForeColor="Black"></asp:Label>
                    </td>
                </tr>
            </table>
        </asp:Panel>
    </div>
    <br />
    <%--<div class="col-lg-4">
    </div>--%>
</div>