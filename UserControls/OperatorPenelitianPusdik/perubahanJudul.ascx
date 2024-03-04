<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="perubahanJudul.ascx.cs" Inherits="simlitekkes.UserControls.OperatorPenelitianPusdik.perubahanJudul" %>
<%@ Register Src="~/Helper/controlPaging.ascx" TagName="controlPaging" TagPrefix="asc" %>

<div class="container-fluid">
    <div class="row">
        <div class="col-sm-12" style="margin-top: 0px; color: #313233e3;">
            <h4>Daftar Perubahan Judul</h4>
        </div>
        <div class="col-md-12">
            <div class="form-inline p-t-5 float-left">
                <div class="form-group">
                    Tahun Pendanaan&nbsp;
                        <asp:DropDownList ID="ddlTahunPelaksanaan" runat="server" AutoPostBack="True"
                            OnSelectedIndexChanged="ddlTahunPelaksanaan_SelectedIndexChanged" CssClass="form-control input-sm">
                        </asp:DropDownList>
                </div>
            </div>
        </div>
        <div class="col-md-12 mt-3">
            <div class="row">
                <div class="col-md-7">
                    <div class="form-group">
                        <div class="input-group mb-3">
                            <div class="input-group-prepend">
                                <span class="input-group-text">Jml. Judul :&nbsp;
                                            <asp:Label ID="lblJmlJudulPerubahan" runat="server" Font-Bold="false" Text=""></asp:Label>
                                    &nbsp;&nbsp;<b>|</b>&nbsp;&nbsp;Cari</span>
                            </div>
                            <asp:TextBox runat="server" ID="tbPencarianNama" CssClass="form-control" placeholder="Nama Ketua"></asp:TextBox>
                            <div class="input-group-append">
                                <asp:LinkButton runat="server" ID="lbCariNamaKetua" CssClass="btn btn-outline-info"
                                    OnClick="lbCariNamaKetua_Click"><i class="fas fa-search"></i></asp:LinkButton>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-md-5">
                    <div class="form-inline" style="color: #7cab3f; float: right">
                        Jml. Baris:&nbsp;
                        <label for="ddlJmlBaris"></label>
                        <asp:DropDownList ID="ddlJmlBaris"
                            OnSelectedIndexChanged="ddlJmlBaris_SelectedIndexChanged" runat="server" AutoPostBack="True"
                            CssClass="form-control input-sm">
                            <asp:ListItem Text="10" Value="10" Selected="True" />
                            <asp:ListItem Text="50" Value="50" />
                            <asp:ListItem Text="100" Value="100" />
                            <asp:ListItem Text="Semua" Value="0" />
                        </asp:DropDownList>
                        &nbsp;&nbsp;
                                <asp:LinkButton runat="server" ID="lbExcelPerubahanJudul"
                                    ToolTip="Export Excel" ForeColor="Green" OnClick="lbExcelPerubahanJudul_Click">        
                                    <i class="far fa-file-excel fa-2x"></i>
                                </asp:LinkButton>
                    </div>
                </div>
                <div class="col-md-12">
                    <div class="table-responsive">
                        <table class="table table-hover table-striped mb-4">
                            <thead>
                                <tr>
                                    <th class="text-center" style="width: 40px"><b>No.</b></th>
                                    <th><b>Pengusul</b></th>
                                    <th><b>Skema</b></th>
                                    <th><b>Perubahan</b></th>
                                    <th><b>Catatan</b></th>
                                </tr>
                            </thead>
                            <tbody>
                                <asp:ListView ID="lvPerubahanJudul" runat="server"
                                    DataKeyNames="id_perubahan_judul"
                                    OnItemDataBound="lvPerubahanJudul_ItemDataBound">
                                    <ItemTemplate>
                                        <tr>
                                            <td class="text-center">
                                                <asp:Label ID="lblNo" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblNidn" runat="server" Text='<%# Eval("nidn")%>'></asp:Label>
                                                - <b>
                                                    <asp:Label ID="lblNama" runat="server" Text='<%# Eval("nama")%>'></asp:Label>
                                                </b>
                                                -            
                                                <asp:Label ID="lblNamaInstitusi" runat="server" Text='<%# Eval("nama_institusi")%>'></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblNamaSkema" runat="server" Text='<%# Eval("nama_skema")%>'></asp:Label>
                                            </td>
                                            <td>
                                                
                                                <span style="font-style: italic; color: darkblue;">
                                                    Lama: <asp:Label ID="lblJudulLama" runat="server" Text='<%# Eval("judul_lama") %>'></asp:Label>
                                                </span><br />
                                                
                                                <span style="font-style: italic; color:green;">
                                                    Baru: <asp:Label ID="lblJudulBaru" runat="server" Text='<%# Eval("judul_baru") %>'></asp:Label>
                                                </span>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblCatatan" runat="server" Text='<%# Eval("catatan")%>'></asp:Label>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                    <EmptyDataTemplate>
                                        <tr>
                                            <td colspan="5" style="text-align: center" class="alert alert-info text-center" role="alert">
                                                <h5>DATA TIDAK DITEMUKAN</h5>
                                            </td>
                                        </tr>
                                    </EmptyDataTemplate>
                                </asp:ListView>
                            </tbody>
                        </table>
                    </div>
                    <asc:controlPaging runat="server" ID="pagingPerubahanJudul" OnPageChanging="pagingPerubahanJudul_PageChanging" />
                </div>
            </div>
        </div>
    </div>
</div>
