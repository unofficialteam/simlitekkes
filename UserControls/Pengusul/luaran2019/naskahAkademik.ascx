<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="naskahAkademik.ascx.cs" Inherits="simlitekkes.UserControls.Pengusul.luaran2019.naskahAkademik" %>

<div class="row">
    <div class="col-lg-4 text-right">
        Produk
    </div>
    <div class="col-lg-8 text-left">
        <asp:DropDownList runat="server" ID="ddlJenisLuaran" CssClass="form-control" Width="80%">
            <asp:ListItem Text="--Pilih--" Value="0"></asp:ListItem>
            <asp:ListItem Text="Policy brief, rekomendasi kebijakan, atau model kebijakan strategis" Value="1"></asp:ListItem>
        </asp:DropDownList>
    </div>
</div>
<br />
<div class="row">
    <div class="col-lg-4 text-right">
        Status
    </div>
    <div class="col-lg-8 text-left">
        <asp:DropDownList runat="server" ID="ddlStatus" CssClass="form-control" Width="80%">
            <asp:ListItem Text="--Pilih--" Value="0"></asp:ListItem>
            <asp:ListItem Text="Tersedia" Value="1"></asp:ListItem>
        </asp:DropDownList>
    </div>
</div>
<br />
<div class="row">
    <div class="col-lg-4 text-right">
        Bukti Luaran
    </div>
    <div class="col-lg-8 text-left">
        <ol>
            <asp:Repeater ID="rptrBuktiLuaran" runat="server">
                <ItemTemplate>
                    <li><%# Container.DataItem.ToString() %></li>
                </ItemTemplate>
            </asp:Repeater>
        </ol>
    </div>
</div>
<br />
<div class="row">
    <div class="col-lg-4 text-right">
    </div>
    <div class="col-lg-8 text-left">
        <asp:LinkButton runat="server" ID="lbSimpan" CssClass="btn btn-success btn-sm" Text="Simpan"
            OnClick="lbSimpan_Click" Width="100"></asp:LinkButton>&nbsp;&nbsp;
        <asp:LinkButton runat="server" ID="lbBatal" CssClass="btn btn-danger btn-sm" Text="Batal"
            OnClick="lbBatal_Click" Width="100"></asp:LinkButton>
    </div>
</div>
<br />
