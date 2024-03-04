<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="jadwalKegiatanperTahapan.ascx.cs" Inherits="simlitekkes.UserControls.Admin.jadwalKegiatanperTahapan" %>
<%@ Register Src="~/Helper/controlPaging.ascx" TagPrefix="asp" TagName="controlPaging" %>
<asp:ScriptManagerProxy ID="ScriptManagerProxy1" runat="server">
</asp:ScriptManagerProxy>

<asp:UpdatePanel ID="UPDaftarEligibilitasInstitusi" runat="server" UpdateMode="Conditional">

    <%--<Triggers>
                <asp:PostBackTrigger ControlID="" />
    </Triggers>--%>

    <ContentTemplate>

        <%--        <section class="content-header">
            <h1>Jadwal Kegiatan Per Tahapan</h1>
            <div style="float: right; background: transparent; margin-top: 0; margin-bottom: 0; font-size: 12px; position: absolute; top: 15px; right: 10px; border-radius: 2px; padding: 0 5px;">
            </div>
        </section>--%>
        <div class="card">
            <div class="card-header bg-info text-white">
                <h6>Jadwal Kegiatan Per Tahapan</h6>
            </div>
            <!-- Header content -->
            <section class="content-header">
                <div class="row" style="margin-bottom: 5px;">
                    <%--<div class="col-sm-12">
                        <div class="form-inline">
                            <div class="form-group" style="padding-right: 10px;">
                                <label for="lblJenisProgram" class="sr-only">Jenis Program Kegiatan</label>
                                <p class="form-control-static">Kegiatan&nbsp;</p>

                            </div>
                            <div class="form-group">
                                <label for="lblThnUsulan" class="sr-only">Thn Usulan Kegiatan</label>
                                <p class="form-control-static">Thn Usulan&nbsp;</p>

                            </div>
                            <div class="form-group">
                                <label for="lblThnPelaksanaan" class="sr-only">Thn Pelaksanaan Kegiatan</label>
                                <p class="form-control-static">Pelaksanaan&nbsp;</p>

                            </div>
                        </div>
                    </div>
                    <div class="col-sm-12">
                        <div class="form-inline">
                            <div class="form-group">
                                <label for="lblTahapan" class="sr-only">Tahapan</label>
                                <p class="form-control-static">Tahapan&nbsp;</p>

                            </div>
                        </div>
                    </div>--%>
                    <div class="col-sm-12">
                        <div class="form-inline">
                            <div class="form-group">
                                <%--<asp:Label ID="lblError" runat="server" Text="Label"></asp:Label>--%>
                            </div>
                        </div>
                    </div>
                </div>
            </section>

            <!-- Main content -->
            <section class="content">
                <div style="min-height: 500px;">
                    <asp:MultiView ID="MultiViewJadwalKegiatan" runat="server">
                        <asp:View ID="ViewDaftarJadwalKegiatan" runat="server">
                            <div class="box">
                                <div class="box-header">
                                    <%--<h3 class="box-title">Jadwal Tahapan Kegiatan</h3>--%>
                                    <%--<div class="box-tools">                                 
                                </div>--%>
                                    <!-- /.box-tools -->

                                    <!--awal -->
                                    <div class="card-body row">
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label for="ddlJmlBarisKomponenBelanja">Jml Baris:</label>
                                                <asp:DropDownList ID="ddlJmlBaris" runat="server" AutoPostBack="True"
                                                    CssClass="form-control input-sm"
                                                    OnSelectedIndexChanged="ddlJmlBaris_SelectedIndexChanged">
                                                    <asp:ListItem Text="10" Value="10" Selected="True" />
                                                    <asp:ListItem Text="50" Value="50" />
                                                    <asp:ListItem Text="100" Value="100" />
                                                    <asp:ListItem Text="Semua" Value="0" />
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-md-4">
                                            <label>Jenis Kegiatan</label>
                                            <asp:DropDownList ID="ddlJenisKegiatan" runat="server" AutoPostBack="True"
                                                CssClass="form-control input-sm"
                                                OnSelectedIndexChanged="ddlJenisKegiatan_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-md-4">
                                            <label>Tahapan</label>

                                            <asp:DropDownList ID="ddlTahapan" runat="server" AutoPostBack="True"
                                                CssClass="form-control input-sm"
                                                OnSelectedIndexChanged="ddlTahapan_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </div>

                                        <%--</div>--%>
                                        <!-- akhir -->


                                        <!--awal -->
                                        <%--<div class="card-body row">--%>
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label for="ddlJmlBarisKomponenBelanja">Tahun Usulan</label>
                                                <asp:DropDownList ID="ddlThnUsulan" runat="server" AutoPostBack="True"
                                                    CssClass="form-control input-sm"
                                                    OnSelectedIndexChanged="ddlThnUsulan_SelectedIndexChanged">
                                                    <asp:ListItem>2019</asp:ListItem>
                                                    <asp:ListItem>2020</asp:ListItem>
                                                    <asp:ListItem>2021</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-md-4">
                                            <label>Tahun Pelaksanaan</label>
                                            <asp:DropDownList ID="ddlThnPelaksanaan" runat="server" AutoPostBack="True"
                                                CssClass="form-control input-sm"
                                                OnSelectedIndexChanged="ddlThnPelaksanaan_SelectedIndexChanged">
                                                <asp:ListItem>2020</asp:ListItem>
                                                <asp:ListItem>2021</asp:ListItem>
                                                <asp:ListItem>2022</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-md-4">
                                        </div>

                                    </div>
                                    <!--akhir   -->


                                    <div class="row" style="margin-bottom: 5px;">
                                        <%--                                        <div class="col-sm-6">
                                            <div class="form-inline">
                                                <div class="form-group">
                                                    <label class="sr-only">Baris</label>
                                                    <p class="form-control-static">Menampilkan&nbsp;</p>
                                                </div>
                                                <div class="form-group">
                                                    <label for="ddlJmlBaris" class="sr-only">JumlahBaris</label>

                                                </div>
                                                &nbsp;data
                                            </div>
                                        </div>--%>
                                        <div class="col-sm-4">
                                            <div class="form-inline" style="text-align: right;">
                                                <div class="form-group" style="padding-right: 10px;">
                                                    <asp:controlPaging runat="server" ID="PagingJadwalKegiatan" OnPageChanging="PagingJadwalKegiatan_PageChanging" />
                                                </div>
                                                <div class="form-group">
                                                    Total&nbsp;<asp:Label ID="lblJmlRecords" runat="server" Text="0"></asp:Label>&nbsp;Data
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-4"></div>
                                        <div class="col-sm-4">
                                            <div class="form-group pull-right" style="text-align: right; padding-top: 12px;">
                                                <asp:LinkButton ID="lbOpenMultiSkema" runat="server" Font-Underline="False" ForeColor="#ff6600" Font-Bold="true" OnClick="lbOpenMultiSkema_Click" ToolTip="Menambahkan jadwal secara bersama">Jadwal Baru</asp:LinkButton>&nbsp;|&nbsp;
                            <asp:LinkButton ID="lbOpenClearJadwal" runat="server" Font-Underline="False" ForeColor="#ff6600" Font-Bold="true" OnClick="lbOpenClearJadwal_Click" ToolTip="Menutup semua jadwal dalam halaman ini">Tutup Semua</asp:LinkButton>&nbsp;&nbsp;
                                            </div>

                                        </div>
                                    </div>

                                    <!-- /.box-header -->

                                    <div class="col-md-12">
                                        <div class="table-responsive">
                                            <asp:GridView ID="gvDaftarJadwalKegiatan" runat="server" GridLines="None" CssClass="table table-striped table-hover"
                                                DataKeyNames="id_skema,nama_skema,tgl_mulai,tgl_berakhir,id_konfig" ShowHeader="true" ShowHeaderWhenEmpty="true" AutoGenerateColumns="False"
                                                OnRowDataBound="gvDaftarJadwalKegiatan_RowDataBound" OnRowCommand="gvDaftarJadwalKegiatan_RowCommand">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="No.">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbnNomor" runat="server" Text='<%# Bind("no_baris") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle Width="30px" />
                                                        <ItemStyle Width="30px" HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Kategori Tahapan">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblKdKategoriTahapan" runat="server" Text='<%# Bind("nama_skema") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" Width="220" />
                                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Jadwal Kegiatan">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblTglMulai" runat="server" Text='<%# Eval("tgl_mulai", "{0:dd-MMM-yyyy}") %>'></asp:Label>&nbsp;-&nbsp;
                                                        <asp:Label ID="lblTglBerakhir" runat="server" Text='<%# Eval("tgl_berakhir", "{0:dd-MMM-yyyy}") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lbEdit" runat="server" CommandName="EditJadwal" CommandArgument='<%# Bind("id_konfig") %>' CssClass="btn btn-primary">
                                                            <i class="fa fa-edit"></i>&nbsp;Edit
                                                            </asp:LinkButton>&nbsp;
                                                        <asp:LinkButton ID="lbStatus" runat="server" CommandName="SetOff" CommandArgument='<%# Bind("id_konfig") %>' CssClass="btn btn-primary"><i class="fas fa-undo"></i></asp:LinkButton>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" Width="220" />
                                                        <ItemStyle HorizontalAlign="Center" Font-Bold="false" />
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
                                    <!-- /.box-body -->
                                </div>
                            <!-- /.box -->
                        </asp:View>
                    </asp:MultiView>
                </div>
            </section>

        </div>
        <div class="modal fade" id="singleJadwalModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel">
            <div class="modal-dialog" role="document">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title font-weight-600" id="dangerModalLabel">Edit Jadwal</h5>
                        <button type="button" class="close text-white" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">×</span>
                        </button>
                    </div>
                    <div class="modal-body">
                        <span style="color: black">Skema
                            <asp:Label ID="lblNamaSkema" runat="server" Text=""></asp:Label>: 
                            <asp:Label ID="lblTahapanKegiatan" runat="server" Text="" Font-Bold="true"></asp:Label><br />
                            Thn usulan
                            <asp:Label ID="lblThnUsulan" runat="server" Text="" Font-Bold="true"></asp:Label>, pelaksanaan
                            <asp:Label ID="lblThnPelaksanaan" runat="server" Text="" Font-Bold="true"></asp:Label>
                        </span>
                        <div class="row" style="padding-top: 10px;">
                            <div class="form-group">
                                <label for="lblTglMulai" class="col-sm-4 control-label">Mulai </label>
                                <div class="col-sm-8">
                                    <asp:TextBox runat="server" ID="tbTglMulai" placeholder="Tgl Mulai" CssClass="form-control pull-right" data-provide="datepicker"></asp:TextBox>
                                </div>
                                <span style="color: #227722; font-size: 11px;">*) MM/dd/yyyy</span>
                            </div>
                        </div>
                        <div class="row">
                            <div class="form-group">
                                <label for="lblTglBerakhir" class="col-sm-4 control-label">Berakhir </label>
                                <div class="col-sm-8">
                                    <asp:TextBox runat="server" ID="tbTglBerakhir" placeholder="Tgl Berakhir" CssClass="form-control pull-right" data-provide="datepicker"></asp:TextBox>
                                </div>
                                <span style="color: #227722; font-size: 11px;">*) MM/dd/yyyy</span>
                            </div>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-default" data-dismiss="modal">Batal</button>
                        <%--<button type="button" class="btn btn-primary">Save changes</button>--%>
                        <asp:LinkButton ID="lbSimpanJadwal" runat="server" CssClass="btn btn-info pull-right" OnClick="lbSimpanJadwal_Click" OnClientClick="$('#singleJadwalModal').modal('hide');">Simpan</asp:LinkButton>

                    </div>
                </div>
            </div>
        </div>

        <div class="modal fade" id="jadwalMultiSkemaModal" tabindex="-1" role="dialog" aria-labelledby="jadwalMultiSkemaModalLabel">
            <div class="modal-dialog" role="document">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title font-weight-600" id="dangerModalLabel1">Jadwal Baru</h5>
                        <button type="button" class="close text-white" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">×</span>
                        </button>
                    </div>
                    <div class="modal-body" style="padding-top: 0px;">
                        <%--                        <div class="row">--%>
                        <div class="form-group">
                            <section class="panel">
                                <div class="panel-body progress-panel" style="padding-top: 0px;">
                                    <div class="task-progress">
                                        <h1>
                                            <asp:Label ID="lblThnUsulanJMS" runat="server" Text="" Font-Bold="true"></asp:Label>
                                            / 
                                                <asp:Label ID="lblThnPelaksanaanJMS" runat="server" Text="" Font-Bold="true"></asp:Label>
                                        </h1>
                                    </div>
                                    <div class="task-option">
                                        <asp:Label ID="lblTahapanJMS" runat="server" Text="" Font-Bold="true" ForeColor="#ff6600"></asp:Label>
                                    </div>
                                    <asp:GridView ID="gvJadwalMultiSkema" runat="server" AutoGenerateColumns="False"
                                        DataKeyNames="id_skema"
                                        OnRowDataBound="gvJadwalMultiSkema_RowDataBound" CssClass="table table-hover personal-task" GridLines="None">
                                        <Columns>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="cbKdStatusAktif" runat="server" />
                                                </ItemTemplate>
                                                <HeaderTemplate>
                                                    <asp:CheckBox ID="chkboxSelectAll" runat="server" onclick="CheckAllEmp(this);" />
                                                </HeaderTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Skema Kegiatan">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblNamaSkema" runat="server" Text='<%# Bind("nama_skema") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                    <div class="row">
                                        <div class="form-group">
                                            <label for="lblTglMulai" class="col-sm-2 control-label">Jadwal </label>
                                            <div class="col-sm-8">
                                                <asp:TextBox runat="server" ID="tbMultiSkemaTglMulai" placeholder="Tgl Mulai" CssClass="form-control pull-right" data-provide="datepicker"></asp:TextBox>
                                                <asp:TextBox runat="server" ID="tbMultiSkematglBerakhir" placeholder="Tgl Berakhir" CssClass="form-control pull-right" data-provide="datepicker"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-sm-3">
                                            <span style="color: #227722; font-size: 11px;">*) MM/dd/yyyy</span>
                                        </div>
                                    </div>
                                    <script type="text/javascript" language="javascript">
                                        function CheckAllEmp(Checkbox) {
                                            var GridVwHeaderChckbox = document.getElementById("<%=gvJadwalMultiSkema.ClientID %>");
                                            for (i = 1; i < GridVwHeaderChckbox.rows.length; i++) {
                                                GridVwHeaderChckbox.rows[i].cells[0].getElementsByTagName("INPUT")[0].checked = Checkbox.checked;
                                            }
                                        }
                                    </script>
                                </div>
                            </section>
                        </div>
                        <%--                        </div>--%>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-default" data-dismiss="modal">Batal</button>
                        <%--<button type="button" class="btn btn-primary">Save changes</button>--%>
                        <asp:LinkButton ID="lbSimpanMultiSkema" runat="server" CssClass="btn btn-info pull-right" OnClick="lbSimpanMultiSkema_Click" OnClientClick="$('#jadwalMultiSkemaModal').modal('hide');">Simpan</asp:LinkButton>

                    </div>
                </div>
            </div>
        </div>

        <div class="modal fade" id="clearJadwalModal" tabindex="-1" role="dialog" aria-labelledby="clearJadwalModalLabel">
            <div class="modal-dialog" role="document">
                <div class="modal-content">
                    <h5 class="modal-title font-weight-600" id="dangerModalLabel2">Tutup Jadwal</h5>
                    <button type="button" class="close text-white" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">×</span>
                    </button>
                    <div class="modal-body" style="padding-top: 0px;">
                        <%--                        <div class="row">--%>
                        <div class="form-group">
                            <section class="panel">
                                <div class="panel-body progress-panel" style="padding-top: 0px;">
                                    <div class="task-progress">
                                        <h1>
                                            <asp:Label ID="lblThnUsulanClear" runat="server" Text="" Font-Bold="true"></asp:Label>
                                            / 
                                                <asp:Label ID="lblThnPelaksanaanClear" runat="server" Text="" Font-Bold="true"></asp:Label>
                                        </h1>
                                    </div>
                                    <div class="task-option">
                                        <asp:Label ID="lblTahapanClear" runat="server" Text="" Font-Bold="true" ForeColor="#ff6600"></asp:Label>
                                    </div>
                                    <asp:GridView ID="gvClearJadwalSkema" runat="server" AutoGenerateColumns="False"
                                        OnRowDataBound="gvClearJadwalSkema_RowDataBound"
                                        DataKeyNames="id_skema,id_konfig" CssClass="table table-hover personal-task" GridLines="None">
                                        <Columns>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="cbKdStatusAktif" runat="server" />
                                                </ItemTemplate>
                                                <HeaderTemplate>
                                                    <asp:CheckBox ID="chkboxSelectAll" runat="server" onclick="CheckAllEmpClear(this);" />
                                                </HeaderTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Skema Kegiatan">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblNamaSkema" runat="server" Text='<%# Bind("nama_skema") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Jadwal Kegiatan">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblTglMulai" runat="server" Text='<%# Eval("tgl_mulai", "{0:dd-MMM-yyyy}") %>'></asp:Label>&nbsp;-&nbsp;
                                                        <asp:Label ID="lblTglBerakhir" runat="server" Text='<%# Eval("tgl_berakhir", "{0:dd-MMM-yyyy}") %>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                    <script type="text/javascript" language="javascript">
                                        function CheckAllEmpClear(Checkbox) {
                                            var GridVwHeaderChckbox = document.getElementById("<%=gvClearJadwalSkema.ClientID %>");
                                            for (i = 1; i < GridVwHeaderChckbox.rows.length; i++) {
                                                GridVwHeaderChckbox.rows[i].cells[0].getElementsByTagName("INPUT")[0].checked = Checkbox.checked;
                                            }
                                        }
                                    </script>
                                </div>
                            </section>
                        </div>
                        <%--                        </div>--%>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-default" data-dismiss="modal">Batal</button>
                        <%--<button type="button" class="btn btn-primary">Save changes</button>--%>
                        <asp:LinkButton ID="lbClearJadwal" runat="server" CssClass="btn btn-info pull-right" OnClick="lbClearJadwal_Click" OnClientClick="$('#clearJadwalModal').modal('hide');">Tutup</asp:LinkButton>

                    </div>
                </div>
            </div>
        </div>

    </ContentTemplate>

</asp:UpdatePanel>
