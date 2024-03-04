<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="laporanKemajuan.ascx.cs" Inherits="simlitekkes.UserControls.Pengusul.laporanKemajuan" %>
<%@ Register Src="~/UserControls/Pengusul/luaranLapKemajuan/publikasi.ascx" TagName="ktLuaranPublikasi" TagPrefix="uc" %>
<%@ Register Src="~/UserControls/Pengusul/luaranLapKemajuan/konferensi.ascx" TagName="ktKonferensi" TagPrefix="uc" %>
<%@ Register Src="~/UserControls/Pengusul/luaranLapKemajuan/keyNoteSpeaker.ascx" TagName="ktKeyNoteSpeaker" TagPrefix="uc" %>
<%@ Register Src="~/UserControls/Pengusul/luaranLapKemajuan/visitingLecturer.ascx" TagName="ktVisitingLecturer" TagPrefix="uc" %>
<%@ Register Src="~/UserControls/Pengusul/luaranLapKemajuan/bookChapter.ascx" TagName="ktBookChapter" TagPrefix="uc" %>
<%@ Register Src="~/UserControls/Pengusul/luaranLapKemajuan/bukuAjar.ascx" TagName="ktBukuAjar" TagPrefix="uc" %>
<%@ Register Src="~/UserControls/Pengusul/luaranLapKemajuan/bukuHasilPenelitian.ascx" TagName="ktBukuHasilPenelitian" TagPrefix="uc" %>
<%@ Register Src="~/UserControls/Pengusul/luaranLapKemajuan/desain.ascx" TagName="ktDesain" TagPrefix="uc" %>
<%@ Register Src="~/UserControls/Pengusul/luaranLapKemajuan/desainProduk.ascx" TagName="ktDesainProduk" TagPrefix="uc" %>
<%@ Register Src="~/UserControls/Pengusul/luaranLapKemajuan/desainProdukIndustri.ascx" TagName="ktDesainProdukIndustri" TagPrefix="uc" %>
<%@ Register Src="~/UserControls/Pengusul/luaranLapKemajuan/dokPurwarupaLikeIndustri.ascx" TagName="ktDokPurwarupa" TagPrefix="uc" %>
<%@ Register Src="~/UserControls/Pengusul/luaranLapKemajuan/dokBisnisPlan.ascx" TagName="ktDokBisnisPlan" TagPrefix="uc" %>
<%@ Register Src="~/UserControls/Pengusul/luaranLapKemajuan/dokHasilUjiCobaDiLingkungan.ascx" TagName="ktDokHasilUjiCobaLingkungan" TagPrefix="uc" %>
<%@ Register Src="~/UserControls/Pengusul/luaranLapKemajuan/dokPengujianPurwarupa.ascx" TagName="ktDokPengujianPurwarupa" TagPrefix="uc" %>
<%--<%@ Register src="luaranLapKemajuan/pVT.ascx" tagname="ktpvt" tagprefix="uc" %>--%>
<%@ Register Src="~/UserControls/Pengusul/luaranLapKemajuan/pVT.ascx" TagName="ktPVT" TagPrefix="uc" %>
<%@ Register Src="~/UserControls/Pengusul/report/pdfLaporanKemajuanKontrol.ascx" TagName="ktPdfLaporanKemajuanKontrol" TagPrefix="uc" %>
<%@ Register Src="~/UserControls/Pengusul/luaranLapKemajuan/hakCipta.ascx" TagName="ktHakCipta" TagPrefix="uc" %>
<%@ Register Src="~/UserControls/Pengusul/luaranLapKemajuan/tTG.ascx" TagName="ktTTG" TagPrefix="uc" %>
<%@ Register Src="~/UserControls/Pengusul/luaranLapKemajuan/rekayasaSosial.ascx" TagName="ktRS" TagPrefix="uc" %>
<%@ Register Src="~/UserControls/Pengusul/luaranLapKemajuan/paten.ascx" TagName="ktPaten" TagPrefix="uc" %>
<%@ Register Src="~/UserControls/Pengusul/luaranLapKemajuan/strategi.ascx" TagName="ktStrategi" TagPrefix="uc" %>
<%@ Register Src="~/UserControls/Pengusul/luaranLapKemajuan/sistem.ascx" TagName="ktSistem" TagPrefix="uc" %>
<%@ Register Src="~/UserControls/Pengusul/luaranLapKemajuan/produk.ascx" TagName="ktProduk" TagPrefix="uc" %>
<%@ Register Src="~/UserControls/Pengusul/luaranLapKemajuan/kebijakan.ascx" TagName="ktKebijakan" TagPrefix="uc" %>
<%@ Register Src="~/UserControls/Pengusul/luaranLapKemajuan/purwarupaPrototipe.ascx" TagName="ktPurwarupaPrototipe" TagPrefix="uc" %>
<%@ Register Src="~/UserControls/Pengusul/luaranLapKemajuan/metode.ascx" TagName="ktMetode" TagPrefix="uc" %>
<%@ Register Src="~/UserControls/Pengusul/luaranLapKemajuan/karyaSeni.ascx" TagName="ktKaryaSeni" TagPrefix="uc" %>
<%@ Register Src="~/UserControls/Pengusul/luaranLapKemajuan/purwarupaLaikIndustri.ascx" TagName="ktPurwarupaLaikIndustri" TagPrefix="uc" %>
<%@ Register Src="~/UserControls/Pengusul/luaranLapKemajuan/model.ascx" TagName="ktModel" TagPrefix="uc" %>
<%@ Register Src="~/UserControls/Pengusul/luaranLapKemajuan/naskahAkademik.ascx" TagName="ktNaskahAkademik" TagPrefix="uc" %>
<%@ Register Src="~/UserControls/Pengusul/luaranLapKemajuan/merekDagang.ascx" TagName="ktmerekDagang" TagPrefix="uc" %>
<%@ Register Src="~/UserControls/Pengusul/luaranLapKemajuan/indikasiGeografis.ascx" TagName="ktindikasiGeografis" TagPrefix="uc" %>
<%@ Register Src="~/UserControls/Pengusul/luaranLapKemajuan/LuaranLain.ascx" TagName="ktluaranLain" TagPrefix="uc" %>

<asp:MultiView runat="server" ID="mvMain">
    <asp:View runat="server" ID="vDaftarLapKemajuan">
        <div class="box-body">
            <div class="card">
                <div class="card-block">
                    <div class="row">
                        <div class="col-sm-12 pull-left">
                            <asp:Label runat="server" ID="lblJudulForm" Text="LAPORAN KEMAJUAN" Font-Bold="true"
                                Font-Size="Larger"></asp:Label>
                        </div>
                        <div class="col-sm-12">
                            <fieldset class="form-group">
                                <div class="col-sm-12 pull-left">
                                    <div class="form-inline pull-left">
                                        Tahun Pelaksanaan&nbsp;
                                        <asp:DropDownList ID="ddlThnPelaksanaan" runat="server" AutoPostBack="True"
                                            CssClass="form-control input-sm"
                                            OnSelectedIndexChanged="ddlThnPelaksanaan_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </fieldset>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-sm-12">
                            <div class="form-group">
                                <div class="col-sm-12 table-responsive">
                                    <asp:GridView ID="gvLapKemajuan" runat="server" GridLines="None"
                                        CssClass="table table-striped table-hover"
                                        ShowHeader="true" ShowHeaderWhenEmpty="true" AutoGenerateColumns="False"
                                        DataKeyNames="id_usulan_kegiatan, id_transaksi_kegiatan, nama_skema, judul, 
                                        urutan_thn_usulan_kegiatan, lama_kegiatan, ringkasan, keyword, id_skema, 
                                        kd_sts_pelaksanaan, kd_tahapan_kegiatan, level_tkt_target, id_kategori_riset, 
                                        is_jadwal_dibuka"
                                        OnRowUpdating="gvLapKemajuan_RowUpdating"
                                        OnRowCommand="gvLapKemajuan_RowCommand" OnRowDataBound="gvLapKemajuan_RowDataBound">
                                        <Columns>
                                            <asp:TemplateField HeaderText="No.">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblNo" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle Width="30px" />
                                                <ItemStyle Width="30px" HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Program">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblProgHibah" runat="server" Text='<%# Bind("program_hibah") %>'></asp:Label><br />
                                                    <asp:Label ID="lblNamaSkema" runat="server" ForeColor="Green" Text='<%# Bind("nama_skema") %>'></asp:Label><br />
                                                    Tahun ke:&nbsp;
                                                    <asp:Label ID="lblUrutanThn" runat="server" Font-Bold="true" Text='<%# Bind("urutan_thn_usulan_kegiatan") %>'></asp:Label>
                                                    &nbsp;dari&nbsp;
                                                    <asp:Label ID="lblDurasi" runat="server" Font-Bold="true" Text='<%# Bind("lama_kegiatan") %>'></asp:Label>
                                                    &nbsp;tahun
                                                </ItemTemplate>
                                                <ItemStyle Width="300px" HorizontalAlign="Left" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Judul">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblJudul" runat="server" Text='<%# Bind("judul") %>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="">
                                                <ItemTemplate>
                                                    <asp:LinkButton runat="server" ID="lbUnduhLaporanKemajuan" CommandName="unduhLaporanKemajuan"
                                                        CssClass="far fa-file-pdf" ForeColor="Gray" Font-Size="30px" Font-Bold="true"
                                                        CommandArgument="<%# Container.DataItemIndex %>">
                                                    </asp:LinkButton>
                                                </ItemTemplate>
                                                <ItemStyle Width="50px" HorizontalAlign="Left" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lbEdit" runat="server" CommandName="Update" CssClass="btn btn-primary btn-sm"
                                                        CommandArgument="<%# Container.DataItemIndex %>" Font-Size="Medium" ToolTip="Edit"
                                                        Visible='<%# (Eval("is_jadwal_dibuka").ToString() == "1") ? true : false %>'>
                                                        <i class="fas fa-edit"></i>
                                                    </asp:LinkButton>
                                                </ItemTemplate>
                                                <ItemStyle Width="50px" HorizontalAlign="Left" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                        </Columns>
                                        <EmptyDataTemplate>
                                            <div style="min-height: 100px; margin: 0 auto;">
                                                <strong>DATA TIDAK DITEMUKAN</strong>
                                            </div>
                                        </EmptyDataTemplate>
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </asp:View>
    <asp:View runat="server" ID="vIsianLapKemajuan">
        <div class="box-body">
            <div class="card">
                <div class="card-block">
                    <fieldset class="form-group">
                        <div class="row">
                            <div class="col-sm-12 pull-left">
                                <asp:Label runat="server" ID="lblJudulHeader" Text="LAPORAN KEMAJUAN" Font-Bold="true"
                                    Font-Size="Larger"></asp:Label>
                            </div>
                        </div>
                    </fieldset>
                    <fieldset class="form-group">
                        <div class="row">
                            <div class="col-sm-12">
                                <asp:Label runat="server" Font-Bold="true" ID="lblSkemaIsian" Text=""></asp:Label>
                                &nbsp;<b>| Tahun Pelaksanaan</b>&nbsp;
                        <asp:Label runat="server" Font-Bold="true" ID="lblThnPelaksanaanIsian" Text=""></asp:Label>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-12">
                                Tahun ke:&nbsp;
                            <asp:Label runat="server" Font-Bold="true" ID="lblTahunKeIsian" Text=""></asp:Label>
                                &nbsp;dari&nbsp;
                            <asp:Label runat="server" Font-Bold="true" ID="lblDurasiIsian" Text=""></asp:Label>
                                &nbsp;tahun
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-10 pull-left" style="text-align: left;">
                                <asp:Label runat="server" ForeColor="Green" ID="lblJudulIsian" Text=""></asp:Label>
                            </div>
                            <div class="col-sm-2 pull-right" style="text-align: right;">
                                <asp:LinkButton runat="server" ID="lbKembaliIsian"
                                    class="btn btn-primary waves-effect waves-light" OnClick="lbKembaliIsian_Click">
                                            <span class="m-l-10">Kembali</span>
                                </asp:LinkButton>
                            </div>
                        </div>
                    </fieldset>

                    <asp:UpdatePanel runat="server" ID="upLapKemajuan">
                        <Triggers>
                            <asp:PostBackTrigger ControlID="gvLuaranWajib" />
                            <asp:PostBackTrigger ControlID="gvLuaranTambahan" />
                        </Triggers>
                        <ContentTemplate>

                            <fieldset class="form-group">
                                <div class="row">
                                    <div class="col-sm-12">
                                        <div class="accordion" id="accordionExample">
                                            <div class="card">
                                                <div class="card-header" id="headingOne">
                                                    <h2 class="mb-0">
                                                        <button class="btn btn-link" type="button" data-toggle="collapse" data-target="#collapseOne" aria-expanded="false" aria-controls="collapseOne">
                                                            Ringkasan
                                                        </button>
                                                    </h2>
                                                </div>

                                                <div id="collapseOne" class="collapse" aria-labelledby="headingOne" data-parent="#accordionExample">
                                                    <div class="card-body">
                                                        <fieldset class="form-group">
                                                            <div class="row">
                                                                <p>
                                                                    Tuliskan secara ringkas latar belakang penelitian, tujuan dan tahapan metode penelitian, luaran yang ditargetkan, uraian TKT penelitian dan hasil penelitian yang diperoleh sesuai dengan tahun pelaksanaan penelitian.
                                                                </p>
                                                                <asp:TextBox runat="server" ID="tbRingkasan" CssClass="form-control max-textarea" TextMode="MultiLine" Rows="10" Width="100%" Text=""></asp:TextBox>
                                                            </div>
                                                        </fieldset>
                                                        <div class="row text-right">
                                                            <asp:LinkButton runat="server" ID="lbSimpanRingkasan"
                                                                CssClass="btn btn-success waves-effect waves-light" Text="Simpan"
                                                                OnClick="lbSimpanRingkasan_Click"></asp:LinkButton>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="card">
                                                <div class="card-header" id="headingTwo">
                                                    <h2 class="mb-0">
                                                        <button class="btn btn-link collapsed" type="button" data-toggle="collapse" data-target="#collapseTwo" aria-expanded="false" aria-controls="collapseTwo">
                                                            Keyword
                                                        </button>
                                                    </h2>
                                                </div>
                                                <div id="collapseTwo" class="collapse" aria-labelledby="headingTwo" data-parent="#accordionExample">
                                                    <div class="card-body">
                                                        <fieldset class="form-group">
                                                            <div class="row">
                                                                <p>
                                                                    Maksimal 5 kata kunci. Gunakan tanda baca titik koma (;) sebagai pemisah
                                                                </p>
                                                                <asp:TextBox runat="server" ID="tbKeyword" CssClass="form-control max-textarea" TextMode="MultiLine" Rows="1" Width="100%" Text=""></asp:TextBox>
                                                            </div>
                                                        </fieldset>
                                                        <div class="row text-right">
                                                            <asp:LinkButton runat="server" ID="lbSimpanKeyword"
                                                                CssClass="btn btn-success waves-effect waves-light" Text="Simpan"
                                                                OnClick="lbSimpanKeyword_Click"></asp:LinkButton>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="card">
                                                <div class="card-header" id="headingThree">
                                                    <h2 class="mb-0">
                                                        <button class="btn btn-link collapsed" type="button" data-toggle="collapse" data-target="#collapseThree" aria-expanded="false" aria-controls="collapseThree">
                                                            Substansi Laporan
                                                        </button>
                                                    </h2>
                                                </div>
                                                <div id="collapseThree" class="collapse" aria-labelledby="headingThree" data-parent="#accordionExample">
                                                    <div class="card-body">
                                                        <p>
                                                            <asp:Label runat="server" ID="lblInfo1FormUnggah" Text="Unggah dokumen substansi laporan kemajuan dalam format PDF sesuai dengan template yang disediakan"></asp:Label>
                                                        </p>
                                                        <div class="row">
                                                            <div class="col-lg-7">
                                                                <table>
                                                                    <tr>
                                                                        <td>
                                                                            <asp:LinkButton runat="server" ID="lbUnduhTemplateDok2" Text="" Font-Bold="true" OnClick="lbUnduhTemplateDok_Click">
                                                                                <i class="fa fa-file-word-o" style="font-size: 60px; color: blue;"></i>
                                                                            </asp:LinkButton>
                                                                        </td>
                                                                        <td style="padding-left: 5px;">
                                                                            <%-- <div><b>Unduh Template</b></div>--%>
                                                                            <asp:LinkButton runat="server" ID="lbUnduhTemplateDok" Text="Unduh Template" Font-Bold="true" OnClick="lbUnduhTemplateDok_Click"></asp:LinkButton>

                                                                        </td>
                                                                        <td style="padding-left: 10px;">

                                                                            <asp:LinkButton runat="server" ID="lbUnduhPdfDok" Text="" ForeColor="Gray" OnClick="lbUnduhPdfDok_Click">
										                                                <i class="far fa-file-pdf" style="font-size: 50px; "></i>
                                                                            </asp:LinkButton>
                                                                        </td>


                                                                        <td>
                                                                            <b>
                                                                                <asp:Label runat="server" ID="lblInfo2FormUnggah" Text="dokumen substansi laporan kemajuan"></asp:Label>
                                                                                <span>&nbsp;<asp:Label runat="server" ID="lblStsUnggah" Text="(Belum diunggah)" ForeColor="Red"></asp:Label>
                                                                                </span>
                                                                            </b>
                                                                            <%--<div>Tgl. Unggah:&nbsp;<asp:Label runat="server" ID="lblTglUnggah" Text="-"></asp:Label></div>
                                        <div>Ukuran file:&nbsp;<asp:Label runat="server" ID="lblUkuranFile" Text="-"></asp:Label></div>--%>

                                                                            <div>
                                                                                <div style="padding: 10px;">
                                                                                    <div>
                                                                                        <div class="input-group input-group-button input-group-primary">
                                                                                            <asp:FileUpload runat="server" ID="fileUpload1" CssClass="form-control" />
                                                                                            <span class="input-group-btn">
                                                                                                <asp:LinkButton runat="server" ID="lbUnggahDokumen" CssClass="btn btn-info"
                                                                                                    OnClick="lbUnggahDokumen_Click">
                                                                        <i class="fa fa-cloud-upload">&nbsp;Unggah</i></asp:LinkButton>
                                                                                            </span>
                                                                                        </div>
                                                                                    </div>
                                                                                    <div>
                                                                                        <asp:Label runat="server" ID="lblInfo" Text="" ForeColor="Red" Font-Bold="true"> </asp:Label>
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                            <div>
                                                                                <span style="font-size: 14px; padding: 10px; color: red; font-style: italic;">Ukuran File Maksimal 5 MB dengan format PDF
                                                                                </span>
                                                                            </div>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </div>
                                                            <div class="col-lg-5">
                                                                <asp:Label runat="server" ID="lblErrorInfo" Text="" ForeColor="Red"></asp:Label>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="card">
                                                <div class="card-header" id="headingFour">
                                                    <h2 class="mb-0">
                                                        <button class="btn btn-link collapsed" type="button" data-toggle="collapse" data-target="#collapseFour" aria-expanded="false" aria-controls="collapseFour">
                                                            Luaran Wajib 
                                                        </button>
                                                    </h2>
                                                </div>
                                                <div id="collapseFour" class="collapse" aria-labelledby="headingFour" data-parent="#accordionExample">
                                                    <div class="card-body">
                                                        <div class="row">
                                                            <div class="col-sm-12">
                                                                <div class="form-group">
                                                                    <div class="col-sm-12 table-responsive">
                                                                        <asp:GridView ID="gvLuaranWajib" runat="server" GridLines="None" Enabled="true"
                                                                            CssClass="table table-striped table-hover"
                                                                            ShowHeader="false" ShowHeaderWhenEmpty="true" AutoGenerateColumns="False"
                                                                            DataKeyNames="id_usulan_kegiatan,id_jenis_luaran, id_luaran_dijanjikan, id_target_capaian_luaran,arr_kd_sts_unggah,arr_id_dokumen_bukti_luaran,jml_dokumen"
                                                                            OnRowUpdating="gvLuaranWajib_RowUpdating"
                                                                            OnRowCommand="gvLuaranWajib_RowCommand" OnRowDataBound="gvLuaranWajib_RowDataBound">
                                                                            <Columns>
                                                                                <asp:TemplateField HeaderText="No.">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblNo" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                    <HeaderStyle Width="30px" />
                                                                                    <ItemStyle Width="30px" HorizontalAlign="Center" VerticalAlign="Top" />
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblJenisLuaran" runat="server" Text='<%# Bind("nama_jenis_luaran") %>'></asp:Label><br />
                                                                                        <asp:Label ID="lblTargetLuaran" runat="server" ForeColor="Green" Text='<%# Bind("nama_target_capaian_luaran") %>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                    <ItemStyle Width="300px" HorizontalAlign="Left" VerticalAlign="Top" />
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="">
                                                                                    <ItemTemplate>
                                                                                        <asp:LinkButton runat="server" ID="lbUnduhLuaran" CommandName="UnduhLuaran"
                                                                                            CssClass="far fa-file-pdf" ForeColor="Red" Font-Size="30px" Font-Bold="true"
                                                                                            CommandArgument="<%# Container.DataItemIndex %>" Visible="true">
                                                                                        </asp:LinkButton>
                                                                                    </ItemTemplate>
                                                                                    <ItemStyle Width="50px" HorizontalAlign="Left" VerticalAlign="Top" />
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="">
                                                                                    <ItemTemplate>
                                                                                        <asp:LinkButton ID="lbEdit" runat="server" CommandName="Update" CssClass="btn btn-primary btn-sm"
                                                                                            CommandArgument="<%# Container.DataItemIndex %>" Font-Size="Medium" ToolTip="Edit">
                                                                                    <i class="fas fa-edit"></i>
                                                                                        </asp:LinkButton>
                                                                                        <asp:Label runat="server" ID="lblStsIsian" CssClass="label label-default" Text="Belum" Visible="false"></asp:Label>
                                                                                    </ItemTemplate>
                                                                                    <ItemStyle Width="50px" HorizontalAlign="Left" VerticalAlign="Top" />
                                                                                </asp:TemplateField>
                                                                            </Columns>
                                                                            <EmptyDataTemplate>
                                                                                <div style="min-height: 100px; margin: 0 auto;">
                                                                                    <strong>DATA TIDAK DITEMUKAN</strong>
                                                                                </div>
                                                                            </EmptyDataTemplate>
                                                                        </asp:GridView>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="card">
                                                <div class="card-header" id="headingFive">
                                                    <h2 class="mb-0">
                                                        <button class="btn btn-link collapsed" type="button" data-toggle="collapse" data-target="#collapseFive" aria-expanded="false" aria-controls="collapseFive">
                                                            Luaran Tambahan
                                                        </button>
                                                    </h2>
                                                </div>
                                                <div id="collapseFive" class="collapse" aria-labelledby="headingFive" data-parent="#accordionExample">
                                                    <div class="card-body">
                                                        <div class="row">
                                                            <div class="col-sm-12">
                                                                <div class="form-group">
                                                                    <div class="col-sm-12 table-responsive">
                                                                        <asp:GridView ID="gvLuaranTambahan" runat="server" GridLines="None" Enabled="true"
                                                                            CssClass="table table-striped table-hover"
                                                                            ShowHeader="false" ShowHeaderWhenEmpty="true" AutoGenerateColumns="False"
                                                                            DataKeyNames="id_usulan_kegiatan,id_jenis_luaran, id_luaran_dijanjikan, id_target_capaian_luaran,arr_kd_sts_unggah,arr_id_dokumen_bukti_luaran,jml_dokumen"
                                                                            OnRowUpdating="gvLuaranTambahan_RowUpdating"
                                                                            OnRowCommand="gvLuaranTambahan_RowCommand" OnRowDataBound="gvLuaranTambahan_RowDataBound">
                                                                            <Columns>
                                                                                <asp:TemplateField HeaderText="No.">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblNo" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                    <HeaderStyle Width="30px" />
                                                                                    <ItemStyle Width="30px" HorizontalAlign="Center" VerticalAlign="Top" />
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblJenisLuaran" runat="server" Text='<%# Bind("nama_jenis_luaran") %>'></asp:Label><br />
                                                                                        <asp:Label ID="lblTargetLuaran" runat="server" ForeColor="Green" Text='<%# Bind("nama_target_capaian_luaran") %>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                    <ItemStyle Width="300px" HorizontalAlign="Left" VerticalAlign="Top" />
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="">
                                                                                    <ItemTemplate>
                                                                                        <asp:LinkButton runat="server" ID="lbUnduhLuaran" CommandName="UnduhLuaran" Visible="true"
                                                                                            CssClass="far fa-file-pdf" ForeColor="Red" Font-Size="30px" Font-Bold="true"
                                                                                            CommandArgument="<%# Container.DataItemIndex %>">
                                                                                        </asp:LinkButton>
                                                                                    </ItemTemplate>
                                                                                    <ItemStyle Width="50px" HorizontalAlign="Left" VerticalAlign="Top" />
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="">
                                                                                    <ItemTemplate>
                                                                                        <asp:LinkButton ID="lbEdit" runat="server" CommandName="Update" CssClass="btn btn-primary btn-sm"
                                                                                            CommandArgument="<%# Container.DataItemIndex %>" Font-Size="Medium" ToolTip="Edit">
                                                                                    <i class="fas fa-edit"></i>
                                                                                        </asp:LinkButton>
                                                                                        <asp:Label runat="server" ID="lblStsIsian" CssClass="label label-default" Text="Belum" Visible="false"></asp:Label>
                                                                                    </ItemTemplate>
                                                                                    <ItemStyle Width="50px" HorizontalAlign="Left" VerticalAlign="Top" />
                                                                                </asp:TemplateField>
                                                                            </Columns>
                                                                            <EmptyDataTemplate>
                                                                                <div style="min-height: 100px; margin: 0 auto;">
                                                                                    <strong>DATA TIDAK DITEMUKAN</strong>
                                                                                </div>
                                                                            </EmptyDataTemplate>
                                                                        </asp:GridView>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="card">
                                                <div class="card-header" id="headingSix">
                                                    <h2 class="mb-0">
                                                        <button class="btn btn-link collapsed" type="button" data-toggle="collapse" data-target="#collapseSix" aria-expanded="false" aria-controls="collapseSix">
                                                            Realisasi Keterlibatan/Kontribusi Mitra
                                                        </button>
                                                    </h2>
                                                </div>
                                                <div id="collapseSix" class="collapse" aria-labelledby="headingSix" data-parent="#accordionExample">
                                                    <div class="card-body">
                                                        <asp:Panel runat="server" ID="pnlDokumenMitra" Visible="false">
                                                            <div class="row">
                                                                <div class="col-lg-7">
                                                                    <table>
                                                                        <tr>
                                                                            <td style="padding-left: 10px;">
                                                                                <asp:LinkButton runat="server" ID="lbUnduhPdfMitra" Text="" ForeColor="Gray" OnClick="lbUnduhPdfMitra_Click">
										                                                <i class="far fa-file-pdf" style="font-size: 50px; "></i>
                                                                                </asp:LinkButton>
                                                                            </td>

                                                                            <td>
                                                                                <b>
                                                                                    <asp:Label runat="server" ID="Label2" Text="dokumen realisasi keterlibatan/kontribusi mitra"></asp:Label>
                                                                                    <span>&nbsp;<asp:Label runat="server" ID="lblStsUnggahMitra" Text="(Belum diunggah)" ForeColor="Red"></asp:Label>
                                                                                    </span>
                                                                                </b>

                                                                                <div>
                                                                                    <div style="padding: 10px;">
                                                                                        <div>
                                                                                            <div class="input-group input-group-button input-group-primary">
                                                                                                <asp:FileUpload runat="server" ID="fileUploadMitra" CssClass="form-control" />
                                                                                                <span class="input-group-btn">
                                                                                                    <asp:LinkButton runat="server" ID="lbUnggahDokumenMitra" CssClass="btn btn-info"
                                                                                                        OnClick="lbUnggahDokumenMitra_Click">
                                                                                                    <i class="fas fa-cloud-upload">&nbsp;Unggah</i>
                                                                                                    </asp:LinkButton>
                                                                                                </span>
                                                                                            </div>
                                                                                        </div>
                                                                                        <div>
                                                                                            <asp:Label runat="server" ID="Label4" Text="" ForeColor="Red" Font-Bold="true"> </asp:Label>
                                                                                        </div>
                                                                                    </div>
                                                                                </div>
                                                                                <div>
                                                                                    <span style="font-size: 14px; padding: 10px; color: red; font-style: italic;">Ukuran File Maksimal 2 MB dengan format PDF
                                                                                    </span>
                                                                                </div>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </div>
                                                                <div class="col-lg-5">
                                                                    <asp:Label runat="server" ID="Label5" Text="" ForeColor="Red"></asp:Label>
                                                                </div>
                                                            </div>
                                                        </asp:Panel>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                        <%--<div class="card">
                                            <div class="card-block accordion-block">
                                                <div class="color-accordion" id="sclae-accordion">
                                                    <a class="accordion-msg"></a>
                                                    <div class="accordion-desc">
                                                    </div>
                                                    <a class="accordion-msg"></a>
                                                    <div class="accordion-desc">
                                                    </div>
                                                    <a class="accordion-msg"></a>
                                                    <div class="accordion-desc">
                                                    </div>
                                                    <a class="accordion-msg">                                                        
                                                    </a>
                                                    <div class="accordion-desc">
                                                    </div>
                                                    <a class="accordion-msg"></a>
                                                    <div class="accordion-desc">
                                                    </div>
                                                    <a class="accordion-msg"></a>
                                                    <div class="accordion-desc">
                                                    </div>
                                                </div>
                                            </div>
                                        </div>--%>
                                    </div>
                                </div>
                            </fieldset>

                            <div class="modal fade " id="modalLuaran" tabindex="-1"
                                aria-labelledby="modalTitle" aria-hidden="true" data-backdrop="false" style="background-color: rgba(0, 0, 0, 0.5);">
                                <div class="modal-dialog modal-lg">
                                    <div class="modal-content">
                                        <div class="modal-header">
										
										<h4 class="modal-title" id="modalTitle">
                                                        <asp:Label runat="server" ID="lblModalTitle" Text="Luaran Wajib" ForeColor="Blue" Font-Bold="true"></asp:Label>
                                        </h4>
										
										<button type="button" class="close" data-dismiss="modal" aria-label="Close">
											<span aria-hidden="true" style="color: red;">&times;</span>
										</button>
										
                                        </div>
                                        <div class="modal-body">
                                            <asp:MultiView runat="server" ID="mvModal">
                                                <asp:View runat="server" ID="vPublikasi">
                                                    <uc:ktLuaranPublikasi runat="server" ID="ktLuaranPublikasi"></uc:ktLuaranPublikasi>
                                                </asp:View>
                                                <asp:View runat="server" ID="vProsiding">
                                                    <uc:ktKonferensi runat="server" ID="ktKonferensi"></uc:ktKonferensi>
                                                </asp:View>
                                                <asp:View runat="server" ID="vKeyNoteSpeaker">
                                                    <uc:ktKeyNoteSpeaker runat="server" ID="ktKeyNoteSpeaker"></uc:ktKeyNoteSpeaker>
                                                </asp:View>
                                                <asp:View runat="server" ID="vVisitingLecturer">
                                                    <uc:ktVisitingLecturer runat="server" ID="ktVisitingLecturer"></uc:ktVisitingLecturer>
                                                </asp:View>
                                                <asp:View runat="server" ID="vBookChapter">
                                                    <uc:ktBookChapter runat="server" ID="ktBookChapter"></uc:ktBookChapter>
                                                </asp:View>
                                                <asp:View runat="server" ID="vBukuHasilPenelitian">
                                                    <uc:ktBukuHasilPenelitian runat="server" ID="ktBukuHasilPenelitian"></uc:ktBukuHasilPenelitian>
                                                </asp:View>
                                                <asp:View runat="server" ID="vBukuAjar">
                                                    <uc:ktBukuAjar runat="server" ID="ktBukuAjar"></uc:ktBukuAjar>
                                                </asp:View>
                                                <asp:View runat="server" ID="vDesain">
                                                    <uc:ktDesain runat="server" ID="ktDesain"></uc:ktDesain>
                                                </asp:View>
                                                <asp:View runat="server" ID="vDesainProduk">
                                                    <uc:ktDesainProduk runat="server" ID="ktDesainProduk"></uc:ktDesainProduk>
                                                </asp:View>
                                                <asp:View runat="server" ID="vDesainProdukIndustri">
                                                    <uc:ktDesainProdukIndustri runat="server" ID="ktDesainProdukIndustri"></uc:ktDesainProdukIndustri>
                                                </asp:View>
                                                <asp:View runat="server" ID="vDokPurwarupa">
                                                    <uc:ktDokPurwarupa runat="server" ID="ktDokPurwarupa"></uc:ktDokPurwarupa>
                                                </asp:View>
                                                <asp:View runat="server" ID="vDokBisnisPlan">
                                                    <uc:ktDokBisnisPlan runat="server" ID="ktDokBisnisPlan"></uc:ktDokBisnisPlan>
                                                </asp:View>
                                                <asp:View runat="server" ID="vDokHasilUjiCobaLingkingan">
                                                    <uc:ktDokHasilUjiCobaLingkungan runat="server" ID="ktDokHasilUjiCobaLingkungan"></uc:ktDokHasilUjiCobaLingkungan>
                                                </asp:View>
                                                <asp:View runat="server" ID="vDokPengujianPurwarupa">
                                                    <uc:ktDokPengujianPurwarupa runat="server" ID="ktDokPengujianPurwarupa"></uc:ktDokPengujianPurwarupa>
                                                </asp:View>
                                                <asp:View runat="server" ID="vPVT">
                                                    <uc:ktPVT runat="server" ID="ktPVT"></uc:ktPVT>
                                                </asp:View>
                                                <asp:View runat="server" ID="vHakCipta">
                                                    <uc:ktHakCipta runat="server" ID="ktHakCipta"></uc:ktHakCipta>
                                                </asp:View>
                                                <asp:View runat="server" ID="vTTG">
                                                    <uc:ktTTG runat="server" ID="ktTTG"></uc:ktTTG>
                                                </asp:View>
                                                <asp:View runat="server" ID="vRS">
                                                    <uc:ktRS runat="server" ID="ktRS"></uc:ktRS>
                                                </asp:View>
                                                <asp:View runat="server" ID="vPaten">
                                                    <uc:ktPaten runat="server" ID="ktPaten"></uc:ktPaten>
                                                </asp:View>
                                                <asp:View runat="server" ID="vStrategi">
                                                    <uc:ktStrategi runat="server" ID="ktStrategi"></uc:ktStrategi>
                                                </asp:View>
                                                <asp:View runat="server" ID="vSistem">
                                                    <uc:ktSistem runat="server" ID="ktSistem"></uc:ktSistem>
                                                </asp:View>
                                                <asp:View runat="server" ID="vProduk">
                                                    <uc:ktProduk runat="server" ID="ktProduk"></uc:ktProduk>
                                                </asp:View>
                                                <asp:View runat="server" ID="vKebijakan">
                                                    <uc:ktKebijakan runat="server" ID="ktKebijakan"></uc:ktKebijakan>
                                                </asp:View>
                                                <asp:View runat="server" ID="vPurwarupaPrototipe">
                                                    <uc:ktPurwarupaPrototipe runat="server" ID="ktPurwarupaPrototipe"></uc:ktPurwarupaPrototipe>
                                                </asp:View>
                                                <asp:View runat="server" ID="vMetode">
                                                    <uc:ktMetode runat="server" ID="ktMetode"></uc:ktMetode>
                                                </asp:View>
                                                <asp:View runat="server" ID="vKaryaSeni">
                                                    <uc:ktKaryaSeni runat="server" ID="ktKaryaSeni"></uc:ktKaryaSeni>
                                                </asp:View>
                                                <asp:View runat="server" ID="vPurwarupaLaikIndustri">
                                                    <uc:ktPurwarupaLaikIndustri runat="server" ID="ktPurwarupaLaikIndustri"></uc:ktPurwarupaLaikIndustri>
                                                </asp:View>
                                                <asp:View runat="server" ID="vModel">
                                                    <uc:ktModel runat="server" ID="ktModel"></uc:ktModel>
                                                </asp:View>
                                                <asp:View runat="server" ID="vNaskahAkademik">
                                                    <uc:ktNaskahAkademik runat="server" ID="ktNaskahAkademik"></uc:ktNaskahAkademik>
                                                </asp:View>
                                                <asp:View runat="server" ID="vMerekDagang">
                                                    <uc:ktmerekDagang runat="server" ID="ktmerekDagang"></uc:ktmerekDagang>
                                                </asp:View>
                                                <asp:View runat="server" ID="vIndikasiGeografis">
                                                    <uc:ktindikasiGeografis runat="server" ID="ktindikasiGeografis"></uc:ktindikasiGeografis>
                                                </asp:View>
                                                <asp:View runat="server" ID="vLuaranLain">
                                                    <uc:ktluaranLain runat="server" ID="ktluaranLain"></uc:ktluaranLain>
                                                </asp:View>
                                            </asp:MultiView>
                                        </div>
                                        <%--<div class="modal-footer">
                                            <button type="button" class="btn btn-secondary" data-dismiss="modal">Batal</button>
                                        </div>--%>
                                    </div>
                                </div>
                            </div>

                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
    </asp:View>
</asp:MultiView>

<uc:ktPdfLaporanKemajuanKontrol runat="server" ID="ktPdfLaporanKemajuanKontrol" Visible="false" />
