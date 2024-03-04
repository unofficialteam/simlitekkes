<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="luaranWajibPtDanPp.ascx.cs" Inherits="simlitekkes.UserControls.Pengusul.luaran2019.luaranWajibPtDanPp" %>

<div class="form-inline">
    <div class="col-lg-2 text-right">
        Jenis Luaran
    </div>
    <div class="col-lg-5 text-left">
        <asp:DropDownList runat="server" ID="ddlJenisLuaran" CssClass="form-control" Width="80%" AutoPostBack="true"
            DataTextField="nama_jenis_luaran" DataValueField="id_jenis_luaran"
            OnSelectedIndexChanged="ddlJenisLuaran_SelectedIndexChanged" AppendDataBoundItems="true">
        </asp:DropDownList>
    </div>
    <div class="col-lg-1 text-right">
        Status
    </div>
    <div class="col-lg-4 text-left">
        <asp:DropDownList runat="server" ID="ddlTargetStatusLuaran" CssClass="form-control" Width="80%" AppendDataBoundItems="true"
            DataTextField="nama_target_capaian_luaran" DataValueField="id_target_capaian_luaran" AutoPostBack="true" OnSelectedIndexChanged="ddlJenisLuaran_SelectedIndexChanged">
        </asp:DropDownList>
    </div>
</div>
<br />
<div class="row">
    <div class="row">
        <div class="col-lg-12">
            <table class="table">
                <asp:ListView ID="lvLuaranWajib" runat="server"
                    DataKeyNames="id_luaran_dijanjikan,tahun_ke,is_edit,volume"
                    OnItemDataBound="lvLuaranWajib_ItemDataBound">
                    <ItemTemplate>
                        <tr>
                            <td style="width: 100px;"><b>Tahun <%# Eval("tahun_ke") %></b></td>
                            <td>
                                <asp:Label ID="lblSubstansi" runat="server" Font-Bold="true"
                                    Visible='<%# Eval("is_edit").ToString() == "1" ? true : false %>'>Substansi :</asp:Label>
                                <asp:TextBox ID="tbSubstansi" runat="server" CssClass="form-control" Width="90%"
                                    Visible='<%# Eval("is_edit").ToString() == "1" ? true : false %>'
                                    placeholder="Substansi Luaran" Text='<%# Eval("keterangan") %>' />
                                <br />
                                <b>Bukti Luaran :</b><br />
                                <%# Eval("bukti_luaran") %>
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:ListView>
            </table>
        </div>
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
            OnClick="lbBatal_Click" Width="100" Visible="false"></asp:LinkButton>
    </div>
</div>
<br />
