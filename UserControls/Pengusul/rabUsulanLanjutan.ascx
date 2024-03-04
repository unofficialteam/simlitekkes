<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="rabUsulanLanjutan.ascx.cs" Inherits="simlitekkes.UserControls.Pengusul.rabUsulanLanjutan" %>
<asp:ScriptManagerProxy ID="ScriptManagerProxy1" runat="server"></asp:ScriptManagerProxy>
<asp:UpdatePanel ID="upRAB" runat="server">
    <Triggers>
        <asp:PostBackTrigger ControlID="btnUnggah" />
    </Triggers>
    <ContentTemplate>
        <style>
            .accordion-title > a:before {
                float: left !important;
                font-family: FontAwesome;
                content: "\f068";
                padding-right: 8px;
            }

            .accordion-title > a.collapsed:before {
                float: left !important;
                content: "\f067";
            }
        </style>

        <div class="row m-t-20">
            <div class="col-sm-12">
                <div class="panel panel-default">
                    <div class="panel-heading bg-default txt-white">
                        Unggah RAB Usulan Kegiatan
                    </div>
                    <div class="panel-body p-15">
                        <div class="row p-t-20 m-b-20">
                            <div class="col-sm-6">
                                <b>Unggah RAB</b><br />
                                <asp:FileUpload ID="FileUpload1" runat="server" /><br />
                                <br />
                                <asp:Button ID="btnUnggah" runat="server" CssClass="btn btn-primary" Text="Unggah" OnClick="btnUnggah_Click" />
                            </div>
                            <div class="col-sm-6 text-right">
                                <b>Template File RAB Usulan</b><br />
                                <asp:HyperLink ID="hlUnduhTemplate" runat="server"
                                    CssClass="btn btn-success" NavigateUrl="~/dokumen/template/TEMPLATE RAB.xlsx">
                                <i class="fa fa-file-excel"></i>&nbsp;Template
                                </asp:HyperLink>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-sm-12">
                <div class="panel panel-default m-t-20">
                    <div class="panel-heading bg-default txt-white">
                        Rencana Anggaran Belanja
                    </div>
                    <div class="panel-body p-15">
                        <div class="row">
                            <div class="col-sm-12">
                                <div class="card-block accordion-block">
                                    <div id="accordion" role="tablist" aria-multiselectable="true">
                                        <%--<div class="accordion-panel">
                                            <div class="accordion-heading" role="tab" id="headingOne">
                                                <div class="card-title accordion-title">
                                                    <a class="accordion-msg scale_active collapsed" data-toggle="collapse" data-parent="#accordion" href="#collapseOne" aria-expanded="false" aria-controls="collapseOne">
                                                        <span style="padding-right: 74%; font-weight: bold;">Tahun 1</span>
                                                        <label class="p-l-15 p-r-15" style="display: inline-block; color: white; background-color: black;">
                                                            Rp.&nbsp;<asp:Label ID="lblTahun1" runat="server" Text="0"></asp:Label>
                                                        </label>
                                                    </a>
                                                </div>
                                            </div>
                                            <div id="collapseOne" class="panel-collapse collapse" role="tabpanel" aria-labelledby="headingOne" aria-expanded="false" style="height: 0px;">
                                                <div class="accordion-content accordion-desc">                                                   
                                                    <asp:ListView ID="lvRABTahun1" runat="server"
                                                        ItemPlaceholderID="itemPlaceHolder"
                                                        ItemType="simlitekkes.Models.Pengusul.ItemRencanaAnggaran">
                                                        <LayoutTemplate>
                                                            <table class="table table-hover">
                                                                <thead>
                                                                    <tr>
                                                                        <th>Jenis Pembelanjaan</th>
                                                                        <th>Item</th>
                                                                        <th>Satuan</th>
                                                                        <th class="text-center">Vol.</th>
                                                                        <th class="text-right">Biaya Satuan</th>
                                                                        <th class="text-right">Total</th>
                                                                    </tr>
                                                                </thead>
                                                                <tbody>
                                                                    <tr id="itemPlaceHolder" runat="server"></tr>
                                                                </tbody>
                                                            </table>
                                                        </LayoutTemplate>
                                                        <ItemTemplate>
                                                            <tr>
                                                                <td><%# Item.JenisPembelanjaan %></td>
                                                                <td><%# Item.Item %></td>
                                                                <td><%# Item.Satuan %></td>
                                                                <td class="text-center"><%# Item.Volume %></td>
                                                                <td class="text-right"><%# Item.Honor.ToString("N0") %></td>
                                                                <td class="text-right"><%# (Item.Volume * Item.Honor).ToString("N0") %></td>
                                                            </tr>
                                                        </ItemTemplate>
                                                    </asp:ListView>
                                                </div>
                                            </div>
                                        </div>--%>
                                        <div class="accordion-panel">
                                            <div class="accordion-heading" role="tab" id="headingTwo">
                                                <div class="card-title accordion-title">
                                                    <a class="accordion-msg scale_active collapsed" data-toggle="collapse" data-parent="#accordion" href="#collapseTwo" aria-expanded="false" aria-controls="collapseTwo">
                                                        <span style="padding-right: 74%; font-weight: bold;">Tahun 2</span>
                                                        <label class="p-l-15 p-r-15" style="display: inline-block; color: white; background-color: black;">
                                                            Rp.&nbsp;<asp:Label ID="lblTahun2" runat="server" Text="0"></asp:Label>
                                                        </label>
                                                    </a>
                                                </div>
                                            </div>
                                            <div id="collapseTwo" class="panel-collapse collapse" role="tabpanel" aria-labelledby="headingTwo" aria-expanded="false" style="height: 0px;">
                                                <div class="accordion-content accordion-desc">
                                                    <%--<asp:GridView ID="gvRABTahun2" runat="server" AutoGenerateColumns="true"></asp:GridView>--%>
                                                    <asp:ListView ID="lvRABTahun2" runat="server"
                                                        ItemPlaceholderID="itemPlaceHolder"
                                                        ItemType="simlitekkes.Models.Pengusul.ItemRencanaAnggaran">
                                                        <LayoutTemplate>
                                                            <table class="table table-hover">
                                                                <thead>
                                                                    <tr>
                                                                        <th>Jenis Pembelanjaan</th>
                                                                        <th>Item</th>
                                                                        <th>Satuan</th>
                                                                        <th class="text-center">Vol.</th>
                                                                        <th class="text-right">Biaya Satuan</th>
                                                                        <th class="text-right">Total</th>
                                                                    </tr>
                                                                </thead>
                                                                <tbody>
                                                                    <tr id="itemPlaceHolder" runat="server"></tr>
                                                                </tbody>
                                                            </table>
                                                        </LayoutTemplate>
                                                        <ItemTemplate>
                                                            <tr>
                                                                <td><%# Item.JenisPembelanjaan %></td>
                                                                <td><%# Item.Item %></td>
                                                                <td><%# Item.Satuan %></td>
                                                                <td class="text-center"><%# Item.Volume %></td>
                                                                <td class="text-right"><%# Item.Honor.ToString("N0") %></td>
                                                                <td class="text-right"><%# (Item.Volume * Item.Honor).ToString("N0") %></td>
                                                            </tr>
                                                        </ItemTemplate>
                                                    </asp:ListView>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="accordion-panel">
                                            <div class=" accordion-heading" role="tab" id="headingThree">
                                                <div class="card-title accordion-title">
                                                    <a class="accordion-msg scale_active collapsed" data-toggle="collapse" data-parent="#accordion" href="#collapseThree" aria-expanded="false" aria-controls="collapseThree">
                                                        <span style="padding-right: 74%; font-weight: bold;">Tahun 3</span>
                                                        <label class="p-l-15 p-r-15" style="display: inline-block; color: white; background-color: black;">
                                                            Rp.&nbsp;<asp:Label ID="lblTahun3" runat="server" Text="0"></asp:Label>
                                                        </label>
                                                    </a>
                                                </div>
                                            </div>
                                            <div id="collapseThree" class="panel-collapse collapse" role="tabpanel" aria-labelledby="headingThree" aria-expanded="false">
                                                <div class="accordion-content accordion-desc">
                                                    <%--<asp:GridView ID="gvRABTahun3" runat="server" AutoGenerateColumns="true"></asp:GridView>--%>
                                                    <asp:ListView ID="lvRABTahun3" runat="server"
                                                        ItemPlaceholderID="itemPlaceHolder"
                                                        ItemType="simlitekkes.Models.Pengusul.ItemRencanaAnggaran">
                                                        <LayoutTemplate>
                                                            <table class="table table-hover">
                                                                <thead>
                                                                    <tr>
                                                                        <th>Jenis Pembelanjaan</th>
                                                                        <th>Item</th>
                                                                        <th>Satuan</th>
                                                                        <th class="text-center">Vol.</th>
                                                                        <th class="text-right">Biaya Satuan</th>
                                                                        <th class="text-right">Total</th>
                                                                    </tr>
                                                                </thead>
                                                                <tbody>
                                                                    <tr id="itemPlaceHolder" runat="server"></tr>
                                                                </tbody>
                                                            </table>
                                                        </LayoutTemplate>
                                                        <ItemTemplate>
                                                            <tr>
                                                                <td><%# Item.JenisPembelanjaan %></td>
                                                                <td><%# Item.Item %></td>
                                                                <td><%# Item.Satuan %></td>
                                                                <td class="text-center"><%# Item.Volume %></td>
                                                                <td class="text-right"><%# Item.Honor.ToString("N0") %></td>
                                                                <td class="text-right"><%# (Item.Volume * Item.Honor).ToString("N0") %></td>
                                                            </tr>
                                                        </ItemTemplate>
                                                    </asp:ListView>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <hr />
                        <div class="row">
                            <div class="col-sm-6">
                                Dana per tahun <asp:Label ID="lblBidangFokusJudul" runat="server" Text="- bidang fokus"></asp:Label> :
                            <asp:Label ID="lblBidangFokus" runat="server"></asp:Label><br />
                                - minimal : Rp.
                            <asp:Label ID="lblDanaMinimal" runat="server"></asp:Label><br />
                                - maksimal : Rp.
                            <asp:Label ID="lblDanaMaksimal" runat="server"></asp:Label>
                            </div>
                            <div class="col-sm-6 text-right">
                                <label class="p-l-15 p-r-15" style="display: inline-block; color: white; background-color: black; font-weight:bold;">
                                    TOTAL USULAN DANA Rp.&nbsp;<asp:Label ID="lblTotalDana" runat="server" Text="0"></asp:Label>
                                </label>
                            </div>
                        </div>
                    </div>

                    <%--<div class="row m-t-10 m-b-20">
                    <div class="col-sm-12 text-center">
                            <asp:LinkButton ID="lbSimpan" runat="server" CssClass="btn btn-primary"
                                OnClick="lbSimpan_Click">Simpan</asp:LinkButton>
                        </div>
                </div>--%>
                </div>
                <%--<div class="panel-footer text-center">
            <asp:LinkButton ID="lbSimpan" runat="server" CssClass="btn btn-primary">Simpan</asp:LinkButton>
        </div>--%>
            </div>
        </div>
        <script src="assets/plugins/jquery.rowspanizer/jquery.rowspanizer.js"></script>
        <script>
            $("table").rowspanizer({
                columns: [0]
            });
        </script>
    </ContentTemplate>
</asp:UpdatePanel>
