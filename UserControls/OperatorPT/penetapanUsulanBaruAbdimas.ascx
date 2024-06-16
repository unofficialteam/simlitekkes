<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="penetapanUsulanBaruAbdimas.ascx.cs" Inherits="simlitekkes.UserControls.OperatorPT.penetapanUsulanBaruAbdimas" %>
<%@ Register Src="~/Helper/controlPaging.ascx" TagName="controlPaging" TagPrefix="asc" %>

<style>
    .table a {
        color: #415dfe;
    }

        .table a:hover {
            color: #8d9efe;
        }

    .table th {
        vertical-align: middle;
    }

    .custom_cell {
        font-weight: bold;
        color: #3e86f1;
    }
</style>

<asp:MultiView runat="server" ID="mvMain">
    <asp:View runat="server" ID="vRekapSkema">
        <div class="container-fluid">
            <div class="row">
                <div class="col-md-12" style="margin-top: 0px; color: #313233e3;">
                    <h4>Penetapan Usulan Baru</h4>
                </div>
                <div class="col-md-7">
                    <div class="row">
                        <div class="col-md-6">
                            <asp:RadioButtonList ID="rblProgramHibah" runat="server" RepeatDirection="Horizontal"
                                CssClass="radio-button-list" AutoPostBack="true"
                                OnSelectedIndexChanged="rblProgramHibah_SelectedIndexChanged">
                                <asp:ListItem Text="Abdimas Perguruan Tinggi" Value="7" Selected="True" />
                                <asp:ListItem Text="Abdimas Unggulan Nasional" Value="3" />
                            </asp:RadioButtonList>
                        </div>
                        <div class="col-md-6">
                            <div class="form-inline p-t-5 p-b-0 float-right">
                                <div class="form-group">
                                    <label class="form-control-label">Tahapan Kegiatan&nbsp;&nbsp;</label>
                                    <asp:DropDownList OnSelectedIndexChanged="ddlTahapan_SelectedIndexChanged" ID="ddlTahapan" runat="server" CssClass="form-control input-sm" AutoPostBack="True">
                                        <asp:ListItem Text="--Pilih--" Value="00" Selected="True" />
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-md-5">
                    <div class="form-inline p-t-5 float-right">
                        <div class="form-group">
                            <label class="form-control-label">Tahun Usulan&nbsp;&nbsp;</label>
                            <asp:DropDownList OnSelectedIndexChanged="ddlTahunUsulan_SelectedIndexChanged" ID="ddlThnUsulan" runat="server" AutoPostBack="True"
                                CssClass="form-control input-sm">
                            </asp:DropDownList>
                            &nbsp;&nbsp;
                        Pelaksanaan&nbsp;
                        <asp:DropDownList OnSelectedIndexChanged="ddlTahunPelaksanaan_SelectedIndexChanged" ID="ddlThnPelaksanaan" runat="server" AutoPostBack="True"
                            CssClass="form-control input-sm">
                        </asp:DropDownList>
                        </div>
                    </div>
                </div>
                <div class="col-md-12">
                    <div class="row mt-4">
                        <div class="col-md-2">
                            <div class="col-sm-12 p-r-10" style="background-color: #8b8b8b; height: 100px;">
                                <div class="side-box text-right" style="line-height: 1;">
                                    <i class="fas fa-file-text-o" style="color: white;"></i>
                                </div>
                                <div class="m-t-40 text-right">
                                    <asp:Label runat="server" Font-Size="28" Font-Bold="true" ID="lblTotalJmlUsulanRekapSkema" Text="0" ForeColor="White"></asp:Label>
                                </div>
                            </div>
                            <div class="col-sm-12 p-l-10 p-t-5" style="background-color: #323232; color: white; height: 50px; line-height: 1.2; padding-top: 5px;">
                                Total Judul Kegiatan
                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="col-sm-12 p-r-10" style="background-color: #45a7c7; height: 100px;">
                                <div class="side-box text-right" style="line-height: 1;">
                                    <i class="fa fa-envelope-o" style="color: white;"></i>
                                </div>
                                <div class="m-t-40 text-right">
                                    <asp:Label runat="server" Font-Size="28" Font-Bold="true" ID="lblTotalJmlTdkLolosRekapSkema" Text="0" ForeColor="White"></asp:Label>
                                </div>
                            </div>
                            <div class="col-sm-12 p-l-10 p-t-5" style="background-color: #1587a7; color: white; height: 50px; line-height: 1.2; padding-top: 5px;">
                                Tidak lolos
                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="col-sm-12 p-r-10" style="background-color: #00a65a; height: 100px;">
                                <div class="side-box text-right" style="line-height: 1;">
                                    <i class="fas fa-file-text-o" style="color: white;"></i>
                                </div>
                                <div class="m-t-40 text-right">
                                    <asp:Label runat="server" Font-Size="28" Font-Bold="true" ID="lblTotalJmlLolosRekapSkema" Text="0" ForeColor="White"></asp:Label>
                                </div>
                            </div>
                            <div class="col-sm-12 p-l-10 p-t-5" style="background-color: #00863a; color: white; height: 50px; line-height: 1.2; padding-top: 5px;">
                                Lolos
                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="col-sm-12 p-r-10" style="background-color: #ed6b59; height: 100px;">
                                <div class="side-box text-right" style="line-height: 1;">
                                    <i class="fa fa-envelope-o" style="color: white;"></i>
                                </div>
                                <div class="m-t-40 text-right">
                                    <asp:Label runat="server" Font-Size="28" Font-Bold="true" ID="lblTotalJmlBlmDitetapkanRekapSkema" Text="0" ForeColor="White"></asp:Label>
                                </div>
                            </div>
                            <div class="col-sm-12 p-l-10 p-t-5" style="background-color: #dd4b39; color: white; height: 50px; line-height: 1.2; padding-top: 5px;">
                                Belum Ditetapkan
                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="col-sm-12 p-r-10" style="background-color: #ffac32; height: 100px;">
                                <div class="side-box text-right" style="line-height: 1;">
                                    <i class="fa fa-envelope-o" style="color: white;"></i>
                                </div>
                                <div class="m-t-40 text-right">
                                    <asp:Label runat="server" Font-Size="Large" Font-Bold="true" ID="lblTotalJmlDanaRekapSkema" Text="0" ForeColor="White"></asp:Label>
                                </div>
                            </div>
                            <div class="col-sm-12 p-l-10 p-t-5" style="background-color: #f39c12; color: white; height: 50px; line-height: 1.2; padding-top: 5px;">
                                Dana
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-md-12 mt-3">
                    <div class="table-responsive">
                        <asp:GridView ID="gvRekapSkema" runat="server" GridLines="None"
                            CssClass="table table-striped table-hover table-bordered"
                            DataKeyNames="id_skema, nama_skema"
                            ShowHeader="true" ShowHeaderWhenEmpty="true" AutoGenerateColumns="False"
                            OnRowCommand="gvRekapSkema_RowCommand">
                            <Columns>
                                <asp:TemplateField HeaderText="No." HeaderStyle-CssClass="text-center" HeaderStyle-Width="5%" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                                    <ItemTemplate>
                                        <asp:Label ID="lblNoRekapSkema" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Nama Skema" HeaderStyle-Width="55%" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Middle">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lbNamaSkemaRekapSkema" runat="server" Visible="true" CommandName="detailRekapSkema"
                                            Text='<%# Bind("nama_skema") %>' CommandArgument='<%# Container.DataItemIndex %>'
                                            CssClass="custom_cell" ToolTip="Tampilkan Detail"></asp:LinkButton>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Jml. Judul" HeaderStyle-CssClass="text-right" HeaderStyle-Width="10%" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                                    <ItemTemplate>
                                        <asp:Label ID="lblJmlJudulRekapSkema" runat="server" Text='<%# Bind("jml_usulan") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Tidak lolos" HeaderStyle-CssClass="text-right" HeaderStyle-Width="10%" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                                    <ItemTemplate>
                                        <asp:Label ID="lblJmlTdkLolosRekapSkema" runat="server" Text='<%# Bind("jml_tdk_lolos") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Lolos" HeaderStyle-CssClass="text-right" HeaderStyle-Width="10%" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                                    <ItemTemplate>
                                        <asp:Label ID="lblJmlLolosRekapSkema" runat="server" Text='<%# Bind("jml_lolos") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Belum Ditetapkan" HeaderStyle-CssClass="text-right" HeaderStyle-Width="10%" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                                    <ItemTemplate>
                                        <asp:Label ID="lblJmlBlmDitetapkanRekapSkema" runat="server" Text='<%# Bind("jml_blm_ditetapkan") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Dana" HeaderStyle-CssClass="text-right" HeaderStyle-Width="10%" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                                    <ItemTemplate>
                                        <asp:Label ID="lblJmlDanaRekapSkema" runat="server" Text='<%# Convert.ToDecimal((Eval("jml_dana")) == System.DBNull.Value ? 0 : Eval("jml_dana")).ToString("N0", new System.Globalization.CultureInfo("id-ID"))%>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:TemplateField>
                            </Columns>
                            <EmptyDataTemplate>
                                <strong>DATA TIDAK DITEMUKAN</strong>
                            </EmptyDataTemplate>
                            <EmptyDataRowStyle CssClass="text-center" HorizontalAlign="Center" />
                        </asp:GridView>
                    </div>
                </div>
            </div>
        </div>
    </asp:View>
    <asp:View runat="server" ID="vPenetapan">
        <div class="container-fluid">
            <div class="row">
                <div class="col-sm-12" style="margin-top: 0px; color: #313233e3;">
                    <h4>Penetapan Usulan Baru</h4>
                </div>
                <div class="col-md-8">
                    Program Kegiatan:        
                <asp:Label ID="lblProgramHibahPenetapan" runat="server" Font-Bold="true"></asp:Label>
                    | Skema:       
                <asp:Label Font-Bold="true" ID="lblNamaSkemaPenetapan" runat="server"></asp:Label><br />
                    Tahapan:       
                <asp:Label runat="server" ID="lblTahapanPenetapan" Font-Bold="true"></asp:Label>
                </div>
                <div class="col-md-4 text-right">
                    Tahun Usulan:        
                <asp:Label runat="server" ID="lblThnUsulanPenetapan" Font-Bold="true" Text=""></asp:Label>
                    | Pelaksanaan:        
                <asp:Label runat="server" ID="lblThnPelaksanaanPenetapan" Text="" Font-Bold="true"></asp:Label><br />
                    <asp:LinkButton ID="lbKembaliPenetapan" runat="server" CssClass="btn btn-info"
                        OnClick="lbKembaliPenetapan_Click">Kembali</asp:LinkButton>
                </div>
                <div class="col-md-12 mt-3 table-responsive">
                    <asp:RadioButtonList ID="rblStsPenetapan" runat="server" RepeatDirection="Horizontal"
                        CssClass="radio-button-list" AutoPostBack="true"
                        OnSelectedIndexChanged="rblStsPenetapan_SelectedIndexChanged">
                        <asp:ListItem Text="Semua" Value="" Selected="True" />
                        <asp:ListItem Text="Lolos" Value="1" />
                        <asp:ListItem Text="Tdk. Lolos" Value="0" />
                        <asp:ListItem Text="Blm. Ditetapkan" Value="9" />
                    </asp:RadioButtonList>
                </div>
                <div class="col-md-12 mt-3">
                    <div class="row">
                        <div class="col-md-7">
                            <div class="form-group">
                                <div class="input-group mb-3">
                                    <div class="input-group-prepend">
                                        <span class="input-group-text">Jml. Judul :&nbsp;
                                        <asp:Label ID="lblJmlJudulPenetapan" runat="server" Font-Bold="false" Text=""></asp:Label>
                                            &nbsp;&nbsp;<b>|</b>&nbsp;&nbsp;Cari</span>
                                    </div>
                                    <asp:TextBox runat="server" ID="tbPencarianPenetapan" CssClass="form-control" placeholder="Nama Peneliti"></asp:TextBox>
                                    <div class="input-group-append">
                                        <asp:LinkButton runat="server" ID="lbPencarianPenetapan" CssClass="btn btn-outline-info" OnClick="lbPencarianPenetapan_Click"><i class="fas fa-search"></i></asp:LinkButton>
                                    </div>
                                    &nbsp;&nbsp;
                                <asp:LinkButton ID="lbSimpanPermanen" runat="server"
                                    CssClass="btn btn-success mr-2" ToolTip="Simpan Permanen"
                                    OnClick="lbSimpanPermanen_Click">
                                    <i class="fas fa-unlock mr-2"></i> Simpan Permanen
                                </asp:LinkButton>
                                    <asp:Label ID="lblSimpanPermanen" runat="server" Visible="false"
                                        CssClass="btn btn-danger mr-2" ToolTip="Simpan Permanen">
                                    <i class="fas fa-lock mr-2"></i> Simpan Permanen
                                    </asp:Label>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-5">
                            <div class="form-inline" style="color: #7cab3f; float: right">
                                Jml. Baris:&nbsp;        
                            <label for="ddlJmlBarisPenetapan"></label>
                                <asp:DropDownList ID="ddlJmlBarisPenetapan"
                                    OnSelectedIndexChanged="ddlJmlBarisPenetapan_SelectedIndexChanged" runat="server" AutoPostBack="True"
                                    CssClass="form-control input-sm">
                                    <asp:ListItem Text="10" Value="10" Selected="True" />
                                    <asp:ListItem Text="50" Value="50" />
                                    <asp:ListItem Text="100" Value="100" />
                                    <asp:ListItem Text="Semua" Value="0" />
                                </asp:DropDownList>
                                &nbsp;&nbsp;
                            <asp:LinkButton runat="server" ID="lbExcelPenetapan"
                                ToolTip="Export Excel" ForeColor="Green" OnClick="lbExcelPenetapan_Click">        
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
                                            <th style="width: 350px"><b>Usulan</b></th>
                                            <th style="width: 350px"><b>Hasil Penilaian</b></th>
                                            <th><b>Rerata</b></th>
                                            <th><b>Status Penetapan</b></th>
                                            <th class="text-center" style="width: 145px"></th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <asp:ListView ID="lvPenetapan" runat="server"
                                            DataKeyNames="id_usulan_kegiatan, id_transaksi_kegiatan, thn_usulan_kegiatan, 
                                        thn_pelaksanaan_kegiatan, id_skema, id_personal, id_institusi, kd_klaster, 
                                        kd_tahapan_kegiatan, rerata_nilai, rerata_rek_dana, id_penetapan_pemenang, 
                                        kd_sts_penetapan_pemenang, dana_disetujui, kd_sts_permanen, kd_sts_akhir_kategori_tahapan"
                                            OnItemEditing="lvPenetapan_ItemEditing"
                                            OnItemCanceling="lvPenetapan_ItemCanceling"
                                            OnItemUpdating="lvPenetapan_ItemUpdating"
                                            OnItemDataBound="lvPenetapan_ItemDataBound">
                                            <ItemTemplate>
                                                <tr>
                                                    <td class="text-center">
                                                        <asp:Label ID="lblNoPenetapan" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblNidn" runat="server" Text='<%# Eval("nidn")%>'></asp:Label>
                                                        - <b>
                                                            <asp:Label ID="lblNama" runat="server" Text='<%# Eval("nama")%>'></asp:Label>
                                                        </b>
                                                        <br />
                                                        <span style="font-style: italic; color: darkblue;">
                                                            <asp:Label ID="lblJudul" runat="server" Text='<%# Eval("judul") %>'></asp:Label>
                                                        </span>
                                                    </td>
                                                    <td>Reviewer 1:&nbsp;
                                                    <%# Eval("nama_rev_1")%><br />
                                                        <span style="font-style: italic; color: Green;">Nilai:
                                                        <%# Eval("nilai_rev_1")%>&nbsp;-&nbsp; Rekomendasi dana:
                                                        <%# Convert.ToDecimal((Eval("rek_dana_rev_1")) == System.DBNull.Value ? 0 : Eval("rek_dana_rev_1")).ToString("N0", new System.Globalization.CultureInfo("id-ID"))%>
                                                        </span>
                                                        <br />
                                                        Reviewer 2:&nbsp;
                                                    <%# Eval("nama_rev_2")%><br />
                                                        <span style="font-style: italic; color: Green;">Nilai:
                                                        <%# Eval("nilai_rev_2")%>&nbsp;-&nbsp; Rekomendasi dana:
                                                        <%# Convert.ToDecimal((Eval("rek_dana_rev_2")) == System.DBNull.Value ? 0 : Eval("rek_dana_rev_2")).ToString("N0", new System.Globalization.CultureInfo("id-ID"))%>
                                                        </span>
                                                    </td>
                                                    <td>Nilai: 
                                                    <span style="font-weight: bold; color: Green;">
                                                        <asp:Label ID="lblRerataNilai" runat="server" Text='<%# Eval("rerata_nilai", "{0:F2}")%>'></asp:Label>
                                                    </span>
                                                        <br />
                                                        Dana: 
                                                    <span style="font-weight: bold; color: Purple;">
                                                        <asp:Label ID="lblRerataRekomendasiDana" runat="server" Text='<%# Convert.ToDecimal((Eval("rerata_rek_dana")) == System.DBNull.Value ? 0 : Eval("rerata_rek_dana")).ToString("N0", new System.Globalization.CultureInfo("id-ID"))%>'></asp:Label>
                                                    </span>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblStsPenetepan" runat="server" Font-Size="Medium" Text='<%# Eval("sts_penetapan_pemenang")%>'
                                                            CssClass='<%# (Eval("kd_sts_penetapan_pemenang").ToString() == "1") ? 
                                                        "badge badge-success" : "badge badge-danger" %>'></asp:Label>
                                                        <br />
                                                        <asp:Label ID="lblDanaDisetujui" runat="server" Font-Bold="true" Font-Size="Medium"
                                                            Visible='<%# (Eval("kd_sts_penetapan_pemenang").ToString() == "1") ? true : false %>'
                                                            Text='<%# Convert.ToDecimal((Eval("dana_disetujui")) == System.DBNull.Value ? 0 : Eval("dana_disetujui")).ToString("N0", new System.Globalization.CultureInfo("id-ID"))%>'></asp:Label>
                                                    </td>
                                                    <td class="text-center">
                                                        <asp:LinkButton ID="lbEdit" runat="server" CommandName="Edit"
                                                            CommandArgument="<%# Container.DataItemIndex %>"
                                                            CssClass="btn btn-outline-primary btn-sm"
                                                            Visible='<%# (Eval("kd_sts_permanen").ToString() == "1") || rblProgramHibah.SelectedValue == "3"  ? false : true %>'>
                                                        <i class="fas fa-pencil-alt"></i>
                                                        </asp:LinkButton>

                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="lblNoPenetapan" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblNidn" runat="server" Text='<%# Eval("nidn")%>'></asp:Label>
                                                        - <b>
                                                            <asp:Label ID="lblNama" runat="server" Text='<%# Eval("nama")%>'></asp:Label>
                                                        </b>
                                                        <br />
                                                        <span style="font-style: italic; color: darkblue;">
                                                            <asp:Label ID="lblJudul" runat="server" Text='<%# Eval("judul") %>'></asp:Label>
                                                        </span>
                                                    </td>
                                                    <td>Reviewer 1:&nbsp;
                                                    <%# Eval("nama_rev_1")%><br />
                                                        <span style="font-style: italic; color: Green;">Nilai:
                                                        <%# Eval("nilai_rev_1")%>&nbsp;-&nbsp; Rekomendasi dana:
                                                        <asp:Label ID="lblRekDanaRev1" runat="server" Font-Bold="true" Font-Size="Medium"
                                                            Text='<%# Convert.ToDecimal((Eval("rek_dana_rev_1")) == System.DBNull.Value ? 0 : Eval("rek_dana_rev_1")).ToString("N0", new System.Globalization.CultureInfo("id-ID"))%>'
                                                            Visible='<%# (Eval("kd_sts_akhir_kategori_tahapan").ToString() == "1") ? true : false %>'>
                                                        </asp:Label>
                                                        </span>
                                                        <br />
                                                        Reviewer 2:
                                                    <%# Eval("nama_rev_2")%><br />
                                                        <span style="font-style: italic; color: Green;">Nilai:
                                                        <%# Eval("nilai_rev_2")%>&nbsp;-&nbsp; Rekomendasi dana:
                                                        <asp:Label ID="lblRekDanaRev2" runat="server" Font-Bold="true" Font-Size="Medium"
                                                            Text='<%# Convert.ToDecimal((Eval("rek_dana_rev_2")) == System.DBNull.Value ? 0 : Eval("rek_dana_rev_2")).ToString("N0", new System.Globalization.CultureInfo("id-ID"))%>'
                                                            Visible='<%# (Eval("kd_sts_akhir_kategori_tahapan").ToString() == "1") ? true : false %>'>
                                                        </asp:Label>
                                                        </span>
                                                    </td>
                                                    <td>Nilai: 
                                                    <span style="font-weight: bold; color: Green;">
                                                        <%# Eval("rerata_nilai", "{0:F2}")%>
                                                    </span>
                                                        <br />
                                                        Dana: 
                                                    <span style="font-weight: bold; color: Purple;">
                                                        <%# Convert.ToDecimal((Eval("rerata_rek_dana")) == System.DBNull.Value ? 0 : Eval("rerata_rek_dana")).ToString("N0", new System.Globalization.CultureInfo("id-ID"))%>
                                                    </span>
                                                    </td>
                                                    <td class="text-center">
                                                        <asp:DropDownList ID="ddlStsPenetapan" runat="server" CssClass="form-control">
                                                        </asp:DropDownList>
                                                        <asp:TextBox ID="tbDanaDisetujui" runat="server" CssClass="form-control"
                                                            Text='<%# (Eval("kd_sts_penetapan_pemenang").ToString() == "1") ? 
                                                            Convert.ToDecimal((Eval("dana_disetujui")) == System.DBNull.Value ? 0 : Eval("dana_disetujui")).ToString("N0", new System.Globalization.CultureInfo("id-ID")) :
                                                            Convert.ToDecimal((Eval("rerata_rek_dana")) == System.DBNull.Value ? 0 : Eval("rerata_rek_dana")).ToString("N0", new System.Globalization.CultureInfo("id-ID")) %>' />
                                                    </td>
                                                    <td class="text-center" style="padding-right: 1rem; padding-left: 1rem;">
                                                        <asp:LinkButton ID="lbUpdatePenetapan" runat="server" CommandName="Update"
                                                            CommandArgument="<%# Container.DataItemIndex %>"
                                                            CssClass="btn btn-outline-success btn-sm">
                                                        <i class="fas fa-check"></i>
                                                        </asp:LinkButton>
                                                        <asp:LinkButton ID="lbCancelPenetapan" runat="server" CommandName="Cancel"
                                                            CommandArgument="<%# Container.DataItemIndex %>"
                                                            CssClass="btn btn-outline-warning btn-sm">
                                                        <i class="fas fa-times"></i>
                                                        </asp:LinkButton>
                                                    </td>
                                                </tr>
                                            </EditItemTemplate>
                                            <EmptyDataTemplate>
                                                <tr>
                                                    <td colspan="6" style="text-align: center" class="alert alert-info text-center" role="alert">
                                                        <h5>DATA TIDAK DITEMUKAN</h5>
                                                    </td>
                                                </tr>
                                            </EmptyDataTemplate>
                                        </asp:ListView>
                                    </tbody>
                                </table>
                            </div>
                            <asc:controlPaging runat="server" ID="pagingPenetapan" OnPageChanging="pagingPenetapan_PageChanging" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </asp:View>


</asp:MultiView>