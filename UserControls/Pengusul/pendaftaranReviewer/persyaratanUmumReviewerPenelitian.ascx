﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="persyaratanUmumReviewerPenelitian.ascx.cs" Inherits="simlitekkes.UserControls.Pengusul.pendaftaranReviewer.persyaratanUmumReviewerPenelitian" %>
<asp:ScriptManagerProxy ID="smpPersyaratanWajibPpm" runat="server"></asp:ScriptManagerProxy>

<div class="row">
<%--    <div class="col-sm-12 p-0">
        <div class="main-header">
        </div>
    </div>--%>
</div>

<asp:MultiView ID="mvPersyaratanWajibPenelitian" runat="server" ActiveViewIndex="0">
    <asp:View ID="vPersyaratanWajibPenelitian" runat="server">
        <section class="panels-wells">
            <%--<div class="col-md-12">
                <div class="card">
                    <div class="card-block">--%>
            <div class="row">
                <div class="col-lg-12">
                    <div class="panel panel-default">
                        <div class="panel-heading bg-default txt-white">
                            Persyaratan Wajib Pendaftaran Reviewer Nasional (Penelitian)
                        </div>
                        <div class="panel-body">
                            <div class="panel panel-default" style="padding: 10px;">
                                <div class="panel-body">
                                    <div class="row">
                                        <asp:Label ID="lblNamaInstitusi" runat="server" ForeColor="Blue" Font-Bold="true" Text=""></asp:Label>&nbsp;        
                                                    <asp:Label ID="Label1" runat="server" ForeColor="Green" Font-Bold="true" Text="Program Studi"></asp:Label>&nbsp;        
                                                    <asp:Label ID="lblNamaProdi" runat="server" ForeColor="Green" Font-Bold="true" Text=""></asp:Label>&nbsp;-&nbsp;
                                                    <asp:Label ID="lblKategoriProdi" runat="server" ForeColor="Violet" Font-Bold="true" Text=""></asp:Label>
                                    </div>
                                    <div class="row">
                                        Pendidikan:&nbsp;<asp:Label ID="lblJenjangPendidikan" runat="server" ForeColor="Green" Font-Bold="true" Text=""></asp:Label>&nbsp;
                                                    <asp:Label ID="Label2" runat="server" ForeColor="Green" Font-Bold="true" Text="-"></asp:Label>&nbsp;
                                                    <asp:Label ID="lblJabatanFungsional" runat="server" ForeColor="Green" Font-Bold="true" Text=""></asp:Label>&nbsp;
                                                    <label class="label bg-warning" runat="server" id="infoJenjangPendidikan" visible="false">Minimal S3-Lektor atau S2-Lektor Kepala</label>
                                    </div>
                                    <div class="row">
                                        Status:&nbsp;<asp:Label ID="lblStstusAktif" runat="server" ForeColor="Green" Font-Bold="true" Text=""></asp:Label>
                                        <label class="label bg-warning" runat="server" id="infoEligibilitasDosen" visible="false">
                                            <asp:Label runat="server" ID="lblInfoEligibilitasDosen" Text=""></asp:Label>
                                        </label>
                                    </div>
                                    <div class="row">
                                        h-index:&nbsp;<asp:Label ID="lblHindex" runat="server" ForeColor="Green" Font-Bold="true" Text="0"></asp:Label>&nbsp;                
                                        <label class="text-danger" runat="server" id="infoHindex" visible="false">H-index tidak memenuhi</label>
                                        <%--<label class="text-danger" runat="server" id="infoHindex" visible="false">H-index minimal 5 (Sain-Teknologi), H-index minimal 2 (Sosial-Humaniora), H-index minimal 0 (Seni)</label>--%>
                                    </div>
                                </div>
                            </div>
                            <div class="panel panel-default" style="padding: 10px;">
                                <div class="panel-body">
                                    <div class="row">
                                        <asp:Label ID="Label3" runat="server" ForeColor="Blue" Font-Bold="true" Text="Pengalaman sebagai ketua di penelitian pendanaan RistekDikti"></asp:Label>&nbsp;
                                                    <asp:Label ID="lblJmlPenelitian" runat="server" CssClass="badge badge-inverse-primary" Font-Bold="true" Text="0"></asp:Label>&nbsp;kali (judul)
                                                    <br />
                                        <label class="label bg-warning" runat="server" id="infoJmlPenelitian" visible="false">Minimal 3 kali sebagai ketua pada penelitian kompetitif nasional</label>
                                    </div>
                                </div>
                            </div>
                            <div class="panel panel-default" style="padding: 10px;">
                                <h5><b>LUARAN KEGIATAN PENELITIAN</b></h5>
                                <div class="panel-body">
                                    <div class="row">
                                        <asp:Label ID="Label4" runat="server" ForeColor="Blue" Font-Bold="true" Text="Artikel jurnal internasional"></asp:Label>
                                    </div>
                                    <div class="row">
                                        - Sebagai penulis pertama / korespondensi: <asp:Label ID="lblJmlArtikelPenulisPertama" runat="server" CssClass="badge badge-inverse-primary" Font-Bold="true" Text="0"></asp:Label> Artikel
                                                    <br />
                                        - Sebagai penulis anggota: <asp:Label ID="lblJmlArtikelPenulisAnggota" runat="server" CssClass="badge badge-inverse-primary" Font-Bold="true" Text="0"></asp:Label> Artikel
                                    </div>
                                    <br />
                                    <div class="row">
                                        <asp:Label ID="Label5" runat="server" ForeColor="Blue" Font-Bold="true" Text="Jumlah paten produk iptek (granted)"></asp:Label>&nbsp;
                                                    <asp:Label ID="lblJmlPatenGranted" runat="server" CssClass="badge badge-inverse-primary" Font-Bold="true" Text="0"></asp:Label>
                                    </div>
                                    <div class="row">
                                        <asp:Label ID="Label8" runat="server" ForeColor="Blue" Font-Bold="true" Text="Jumlah paten produk iptek (terdaftar)"></asp:Label>&nbsp;
                                                    <asp:Label ID="lblJmlPatenTerdaftar" runat="server" CssClass="badge badge-inverse-primary" Font-Bold="true" Text="0"></asp:Label>
                                    </div>
                                    <br />
                                    <div class="row">
                                        <asp:Label ID="Label6" runat="server" ForeColor="Blue" Font-Bold="true" Text="Karya seni monumental "></asp:Label>&nbsp;
                                                    <asp:Label ID="lblJmlKaryaSeni" runat="server" CssClass="badge badge-inverse-primary" Font-Bold="true" Text="0"></asp:Label>&nbsp;
                                                    <%--<asp:LinkButton runat="server" ID="lbEditKaryaSeni" class="label bg-primary" OnClick="lbEditKaryaSeni_Click">Edit</asp:LinkButton>--%>

                                        <asp:LinkButton runat="server" ID="lbEditKaryaSeni" CssClass="btn btn-info" OnClick="lbEditKaryaSeni_Click" Font-Italic="true">
                                            <i class="fa fa-pencil"></i>&nbsp;&nbsp;Edit
                                        </asp:LinkButton>
                                        <br />
                                        <span style="color: blue;">Jumlah luaran kegiatan penelitian:</span> <asp:Label runat="server" ID="lblJmlLuaranKegPenelitian" Text="0" CssClass="badge badge-inverse-primary" Font-Bold="true" ></asp:Label> <br />
                                        <label style="color: blue;" runat="server" id="infoJmlLuaran">
                                            Syarat luaran Kegiatan Penelitian:<br />
                                            &nbsp;&nbsp;- Minimal <b>3</b>  artikel Jurnal Internasional sbg penulis utama/korespondensi untuk bidang science tech <span  style="color: maroon;">atau</span>
                                            <br />
                                            &nbsp;&nbsp;- Minimal <b>2</b> artikel Jurnal Internasional sbg penulis utama/korespondensi untuk bidang soshum <span style="color: maroon;">atau</span><br />
                                            &nbsp;&nbsp;- Minimal <b>1</b> buah produk ipteksosbud yang dilindungi paten (granted) <span style="color: maroon;">atau</span><br />
                                            &nbsp;&nbsp;- Khusus untuk bidang seni, memiliki karya seni monumental yaitu karya seni yang pernah dipamerkan/dipentaskan dalam pertunjukan/mendapat rekognisi/mendapat penghargaan berskala nasional atau internasional

                                        </label>

                                        <%--<label class="label bg-warning" runat="server" id="infoJmlLuaran" visible="false">
                                            - Minimal <b>3</b>  artikel Jurnal Internasional sbg penulis utama / korespondensi untuk bidang science tech <b>atau</b>
                                            <br />
                                            - Minimal <b>2</b> artikel Jurnal Internasional sbg penulis utama / korespondensi untuk bidang soshum <b>atau</b><br />
                                            - Minimal <b>1</b> buah produk ipteksosbud yang dilindungi paten (granted) <b>atau</b><br />
                                            - Khusus untuk bidang seni, memiliki karya seni monumental yaitu karya seni yang pernah dipamerkan/dipentaskan dalam pertunjukan/mendapat rekognisi/mendapat penghargaan berskala nasional atau internasional
                                        </label>--%>
                                    </div>
                                </div>
                            </div>
                            <div class="panel panel-default" style="padding: 10px;">
                                <div class="panel-body">
                                    <div class="row">
                                        <asp:Label ID="Label7" runat="server" ForeColor="Blue" Font-Bold="true" Style="word-break: break-word;" Text="Artikel prosiding di seminar ilmiah Internasional"></asp:Label>
                                    </div>
                                    <div class="row">
                                        - Sebagai penulis pertama / korespondensi: <asp:Label ID="lblJmlProsidingPenulisKetua" runat="server" CssClass="badge badge-inverse-primary" Font-Bold="true" Text="0"></asp:Label>&nbsp;Artikel
                                                    <br />
                                        - Sebagai penulis anggota: <asp:Label ID="lblJmlProsidingAnggota" runat="server" CssClass="badge badge-inverse-primary" Font-Bold="true" Text="0"></asp:Label>&nbsp;Artikel
                                                    <br />
                                        <label class="label bg-warning" runat="server" id="infoJmlProsiding" visible="false">Minimal 1 kali sebagai pemakalah dalam seminar ilmiah internasional</label>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <%--</div>
                </div>
            </div>--%>
        </section>
    </asp:View>
</asp:MultiView>