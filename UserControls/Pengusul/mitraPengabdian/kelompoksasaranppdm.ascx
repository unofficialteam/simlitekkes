<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="kelompoksasaranppdm.ascx.cs" Inherits="simlitekkes.UserControls.Pengusul.mitraPengabdian.kelompoksasaranppdm" %>
<div class="card">
    <%--    <div class="card-header">
        <h5 class="card-header-text" style="color: red;">Mitra Sasaran</h5>
    </div>--%>
    <div class="card-body">
        <div class="row">
            <div class="col-md-4">
                <div class="form-group">
                    <label>Tipe Mitra</label>
                    <asp:DropDownList ID="ddlTipeMitra1" runat="server" CssClass="form-control"
                        DataValueField="id_tipe_mitra" DataTextField="tipe_mitra">
                    </asp:DropDownList>
                </div>
            </div>
            <div class="col-md-4">
                <div class="form-group">
                    <label>Jenis Mitra</label>
                    <asp:DropDownList ID="ddlJenisMitra1" runat="server" CssClass="form-control"
                        DataValueField="id_jenis_mitra" DataTextField="jenis_mitra">
                        <asp:ListItem Text="Produktif Ekonomi/Wirausahawan" Value="1" Selected="True"></asp:ListItem>
                    </asp:DropDownList>
                </div>
            </div>
            <div class="col-md-4">
                <div class="form-group">
                    <label>Nama Kelompok Mitra</label>
                    <asp:TextBox ID="tbNamaMitra1" runat="server" CssClass="form-control" placeholder="Nama Mitra"></asp:TextBox>
                </div>
            </div>
            <div class="col-md-4">
                <div class="form-group">
                    <label>Nama Pimpinan Mitra</label>
                    <asp:TextBox ID="tbNamaPimpinan1" runat="server" CssClass="form-control" placeholder="Nama Pimpinan"></asp:TextBox>
                </div>
            </div>
            <div class="col-md-4">
                <div class="form-group">
                    <label>Alamat Kelompok Mitra</label>
                    <asp:TextBox ID="tbAlamatMitra1" runat="server" CssClass="form-control"
                        placeholder="Alamat" TextMode="MultiLine"></asp:TextBox>
                </div>
            </div>
            <div class="col-md-4">
                <div class="form-group">
                    <label>Bidang Pengembangan Mitra</label>
                    <asp:TextBox ID="tbBidangUsahaMitra1" runat="server" CssClass="form-control" placeholder="Bidang Usaha"></asp:TextBox>
                </div>
            </div>
        </div>
    </div>
</div>
<hr>
<div class="card mb-4" style="display: none;">
    <div class="card-body">
        <div class="row">
            <div class="col-md-4">
                <div class="form-group">
                    <label>Tipe Mitra</label>
                    <asp:DropDownList ID="ddlTipeMitra2" runat="server" CssClass="form-control"
                        DataValueField="id_tipe_mitra" DataTextField="tipe_mitra">
                    </asp:DropDownList>
                </div>
            </div>
            <div class="col-md-4">
                <div class="form-group">
                    <label>Jenis Mitra</label>
                    <asp:DropDownList ID="ddlJenisMitra2" runat="server" CssClass="form-control"
                        DataValueField="id_jenis_mitra" DataTextField="jenis_mitra">
                        <asp:ListItem Text="Produktif Ekonomi/Wirausahawan" Value="1" Selected="True"></asp:ListItem>
                    </asp:DropDownList>
                </div>
            </div>
            <div class="col-md-4">
                <div class="form-group">
                    <label>Nama Kelompok Mitra 2</label>
                    <asp:TextBox ID="tbNamaMitra2" runat="server" CssClass="form-control" placeholder="Nama Mitra"></asp:TextBox>
                </div>
            </div>
            <div class="col-md-4">
                <div class="form-group">
                    <label>Nama Pimpinan Mitra 2</label>
                    <asp:TextBox ID="tbNamaPimpinan2" runat="server" CssClass="form-control" placeholder="Nama Pimpinan"></asp:TextBox>
                </div>
            </div>
            <div class="col-md-4">
                <div class="form-group">
                    <label>Alamat Kelompok Mitra 2</label>
                    <asp:TextBox ID="tbAlamatMitra2" runat="server" CssClass="form-control"
                        placeholder="Alamat" TextMode="MultiLine"></asp:TextBox>
                </div>
            </div>
            <div class="col-md-4">
                <div class="form-group">
                    <label>Bidang Pengembangan Mitra 2</label>
                    <asp:TextBox ID="tbBidangUsahaMitra2" runat="server" CssClass="form-control" placeholder="Bidang Usaha"></asp:TextBox>
                </div>
            </div>
        </div>
    </div>
</div>
<asp:HiddenField ID="urutan_tahun" runat="server" />
