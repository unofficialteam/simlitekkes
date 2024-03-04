<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="persyaratanUmumReviewerPPM.ascx.cs" Inherits="simlitekkes.UserControls.Pengusul.pendaftaranReviewer.persyaratanUmumReviewerPPM" %>
<asp:ScriptManagerProxy ID="smpPersyaratanWajibPpm" runat="server"></asp:ScriptManagerProxy>

<div class="row">
    <div class="col-sm-12 p-0">
        <div class="main-header">
        </div>
    </div>
</div>

<asp:MultiView ID="mvPersyaratanWajibPpm" runat="server" ActiveViewIndex="0">
    <asp:View ID="vPersyaratanWajibPpm" runat="server">
        <section class="panels-wells">
            <%--<div class="col-md-12">
                <div class="card">
            <div class="card-block">--%>
            <div class="row">
                <div class="col-lg-12">
                    <div class="panel panel-default">
                        <div class="panel-heading bg-default txt-white">
                            Persyaratan Wajib Pendaftaran Reviewer Nasional (PPM)
                        </div>
                        <div class="panel-body">




                            <div class="panel panel-default" style="padding: 10px;">
                                <div class="panel-body">
                                    <div class="row">
                                        <asp:Label ID="lblNamaInstitusi" runat="server" ForeColor="Blue" Font-Bold="true" Text=""></asp:Label>&nbsp;
                                                            <asp:Label ID="Label1" runat="server" ForeColor="Green" Font-Bold="true" Text="Program Studi"></asp:Label>&nbsp;
                                                            <asp:Label ID="lblNamaProdi" runat="server" ForeColor="Green" Font-Bold="true" Text=""></asp:Label>
                                    </div>
                                    <div class="row">
                                        Pendidikan:&nbsp;<asp:Label ID="lblJenjangPendidikan" runat="server" ForeColor="Green" Font-Bold="true" Text=""></asp:Label>&nbsp;
                                                            <asp:Label ID="Label2" runat="server" ForeColor="Green" Font-Bold="true" Text="-"></asp:Label>&nbsp;
                                                            <asp:Label ID="lblJabatanFungsional" runat="server" ForeColor="Green" Font-Bold="true" Text=""></asp:Label>&nbsp;
                                                            <label class="text-danger" runat="server" id="infoJenjangPendidikan" visible="false" style="font-weight: bold;">Minimal S3-Lektor atau S2-Lektor Kepala</label>
                                    </div>
                                    <div class="row">
                                        Status:&nbsp;<asp:Label ID="lblStstusAktif" runat="server" ForeColor="Green" Font-Bold="true" Text=""></asp:Label><br />
                                        <label class="text-danger" runat="server" id="infoEligibilitasDosen" visible="false">
                                            <asp:Label runat="server" ID="lblInfoEligibilitasDosen" Text="" CssClass="text-danger" Font-Bold="true"></asp:Label>
                                        </label>
                                    </div>
                                    <div class="row">
                                        h-index:&nbsp;<asp:Label ID="lblHindex" runat="server" ForeColor="Green" Font-Bold="true" Text="0"></asp:Label>&nbsp;                            
                                        <label class="text-danger" runat="server" id="infoHindex" style="font-weight: bold;"></label>
                                    </div>
                                </div>
                            </div>



                            <asp:Panel runat="server" ID="pnlDaftarRevBaru" Visible="true">

                                <div class="panel panel-default" style="padding: 10px;">
                                    <div class="panel-body">
                                        <div class="row">
                                            <asp:Label ID="Label3" runat="server" ForeColor="Blue" Font-Bold="true" Text="Pengalaman PPM Pendanaan RistekDikti"></asp:Label>
                                        </div>
                                        <div class="row">
                                            - Sebagai ketua PPM mono tahun:&nbsp;<asp:Label ID="lblJmlKetuaMono" runat="server" CssClass="badge badge-inverse-primary" Font-Bold="true" Text="0"></asp:Label>
                                            <br>
                                            - Sebagai ketua PPM multi tahun:&nbsp;<asp:Label ID="lblJmlKetuaMulti" runat="server" CssClass="badge badge-inverse-primary" Font-Bold="true" Text="0"></asp:Label>
                                            <br>
                                            - Sebagai anggota PPM mono tahun:&nbsp;<asp:Label ID="lblJmlAnggotaMono" runat="server" CssClass="badge badge-inverse-primary" Font-Bold="true" Text="0"></asp:Label>
                                            <br>
                                            - Sebagai anggota PPM multi tahun:&nbsp;<asp:Label ID="lblJmlAnggotaMulti" runat="server" CssClass="badge badge-inverse-primary" Font-Bold="true" Text="0"></asp:Label>
                                            <br>
                                            <br>

                                            <span style="color: blue;">Syarat pendaftar reviewer mono tahun: 
                                                <br>
                                                &nbsp;&nbsp;- Jumlah usulan didanai mono tahun minimal 2 kali sebagai ketua, atau 
                                                <br>
                                                &nbsp;&nbsp;- Jumlah usulan didanai mono tahun 1 kali sebagai ketua dan minimal 1 kali sebagai anggota (mono/multi tahun)                                                
                                            </span>
                                            <br />
                                            <span style="color: blue;">Syarat pendaftar reviewer multi tahun dan/atau mono tahun: 
                                                <br>
                                                &nbsp;&nbsp;- Jumlah usulan didanai multi tahun minimal 2 kali sebagai ketua, atau 
                                                <br>
                                                &nbsp;&nbsp;- Jumlah usulan didanai multi tahun 1 kali sebagai ketua dan minimal 1 kali sebagai ketua mono tahun, atau 
                                                <br>
                                                &nbsp;&nbsp;- Jumlah usulan didanai multi tahun 1 kali sebagai ketua dan minimal 1 kali sebagai anggota (mono/multi tahun)

                                            </span>
                                        </div>
                                    </div>
                                </div>

                                <div class="panel panel-default" style="padding: 10px;">
                                    <div class="panel-body">
                                        <div class="row">
                                            <asp:Label ID="Label4" runat="server" ForeColor="Blue" Font-Bold="true" Text="Artikel jurnal internasional"></asp:Label>
                                        </div>
                                        <div class="row">
                                            <div class="col-lg-12">
                                                <span style="word-break: break-word;">- Sebagai penulis pertama/korespondensi:&nbsp;</span><asp:Label ID="lblJmlPenulisPertama" runat="server" CssClass="badge badge-inverse-primary" Font-Bold="true" Text="0"></asp:Label>&nbsp;Artikel&nbsp;<br />
                                                <label runat="server" style="color: blue;" id="infoJmlArtikelJurnal">(jika ada)</label>
                                                <span style="display: none;">
                                                    <br />
                                                    -­ Sebagai penulis anggota:&nbsp;<asp:Label ID="lblJmlPenulisAnggota" runat="server" CssClass="badge badge-inverse-primary" Font-Bold="true" Text="0"></asp:Label>&nbsp;Artikel
                                                </span>

                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="panel panel-default">
                                    <div class="panel-body">
                                        <div class="row">
                                            <div class="col-lg-12">
                                                <%--<span class="text-primary font-weight-bold" style="word-break: break-word;">Kegiatan Seminar ilmiah Internasional/Nasional</span>--%>
                                                <asp:Label ID="Label5" runat="server" Style="word-break: break-word;" ForeColor="Blue" Font-Bold="true" Text="Kegiatan Seminar ilmiah Internasional/Nasional"></asp:Label>&nbsp;
                                                
                                                    <asp:Label ID="lblJmlSeminar" runat="server" CssClass="badge badge-inverse-primary" Font-Bold="true" Text="0"></asp:Label>&nbsp;
                                                
                                                    <%--<asp:LinkButton runat="server" ID="lbEditSeminar" CssClass="btn btn-info" OnClick="lbEditSeminar_Click" Font-Italic="true" Visible="false">
                                                            <i class="fa fa-pencil"></i>&nbsp;&nbsp;Edit
                                                    </asp:LinkButton>--%>
                                                <br />
                                                <label style="color: blue;" runat="server" id="infoJmlSeminar">(jika ada)</label>

                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="panel panel-default" style="padding: 10px;">
                                    <div class="panel-body">
                                        <div class="row">
                                            <asp:Label ID="Label6" runat="server" ForeColor="Blue" Font-Bold="true" Text="Jumlah buku ajar"></asp:Label>&nbsp;
                                                            <asp:Label ID="lblJmlBukuAjar" runat="server" CssClass="badge badge-inverse-primary" Font-Bold="true" Text="0"></asp:Label>&nbsp;
                                                            <label style="color: blue;" runat="server" id="infoJmlBuku">(jika ada)</label>
                                        </div>
                                    </div>
                                </div>
                                <div class="panel panel-default" style="padding: 10px;">
                                    <div class="panel-body">
                                        <div class="row">
                                            <asp:Label ID="Label7" runat="server" ForeColor="Blue" Font-Bold="true" Text="Jumlah kekayaan intelektual"></asp:Label>&nbsp;
                                                            <asp:Label ID="lblJmlHki" runat="server" CssClass="badge badge-inverse-primary" Font-Bold="true" Text="0"></asp:Label>&nbsp;
                                                            <label style="color: blue;" runat="server" id="infoJmlHki">(jika ada)</label>
                                        </div>
                                    </div>
                                </div>
                                <div class="panel panel-default" style="padding: 10px;">
                                    <div class="panel-body">
                                        <div class="row">
                                            <span>
                                                <asp:Label ID="Label8" runat="server" ForeColor="Blue" Font-Bold="true" Text="Prestasi dalam kegiatan seminar hasil PPM "></asp:Label>&nbsp;<span style="color: blue;">(jika ada)</span>
                                                &nbsp;<asp:LinkButton runat="server" ID="lbEditPresentasiNPoster" CssClass="btn btn-info" OnClick="lbEditPresentasiNPoster_Click" Font-Italic="true" Visible="true">
                                                            <i class="fa fa-pencil"></i>&nbsp;&nbsp;Edit
                                                </asp:LinkButton></span>
                                        </div>
                                        <div class="row">
                                            - Penyaji presentasi terbaik:&nbsp;<asp:Label ID="lblJmlPenyajiTerbaik" runat="server" CssClass="badge badge-inverse-primary" Font-Bold="true" Text="0"></asp:Label><br />
                                            -­ Penyaji poster terbaik:&nbsp;<asp:Label ID="lblJmlPosterTerbaik" runat="server" CssClass="badge badge-inverse-primary" Font-Bold="true" Text="0"></asp:Label>
                                        </div>
                                    </div>
                                </div>


                            </asp:Panel>



                        </div>
                    </div>
                </div>
            </div>
            <%-- </div>
                </div>
            </div>--%>
        </section>
    </asp:View>
</asp:MultiView>