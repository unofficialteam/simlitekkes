<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="rabRevisi.ascx.cs" Inherits="simlitekkes.UserControls.Pengusul.PerbaikanProposal.rabRevisi" %>
<style>
    .equal {
        display: flex;
        display: -webkit-flex;
        flex-wrap: wrap;
    }
</style>
<%--<asp:UpdatePanel ID="upRAB" runat="server">--%>
<div class="container-fluid">
    <div class="row">
        <div class="col-sm-12">
            <div class="card">
                <div class="card-block">
                    <triggers>
                        <asp:PostBackTrigger ControlID="lbExportExcel" />
                    </triggers>
                    <contenttemplate>
                        <div class="panel panel-default m-t-20">
                            <div class="panel-heading bg-default txt-white">
                                Rencana Anggaran Belanja
                            </div>
                            <div class="panel-body p-15">
                                <div class="row equal m-b-25" style="color: #fff; font-size: 14px; margin-left: 2px; margin-right: 2px;">
                                    <div class="col-sm-8 p-15" style="background-color: #2196F3">
                                        <asp:Repeater ID="rptrSummary" runat="server">
                                            <HeaderTemplate>
                                                <ul>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <li>Dana Tahun <%# Eval("urutan_tahun") %> : Disetujui
                                                <strong>Rp. <%# decimal.Parse(Eval("dana_disetujui").ToString()).ToString("N0") %></strong> | 
                                                Direncanakan <strong>Rp. <%# decimal.Parse(Eval("dana_diajukan").ToString()).ToString("N0") %></strong>
                                                </li>
                                            </ItemTemplate>
                                            <FooterTemplate></ul></FooterTemplate>
                                        </asp:Repeater>
                                    </div>
                                    <div class="col-sm-4 p-15 text-right" style="background-color: #415DFE">
                                        Total Dana Disetujui : Rp.
                            <asp:Label ID="lblTotalDanaDisetujui" runat="server" Font-Bold="true">0</asp:Label><br />
                                        Total Dana Direncanakan : Rp.
                            <asp:Label ID="lblTotalDanaDiajukan" runat="server" Font-Bold="true">0</asp:Label>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-10">
                                        <div class="form-group row">
                                            <label for="example-text-input" class="col-xs-2 col-form-label form-control-label">
                                                Tahun Ke</label>
                                            <div class="col-sm-10">
                                                <asp:RadioButtonList ID="rblTahun" runat="server" CssClass="radio-button-list"
                                                    AutoPostBack="true" RepeatDirection="Horizontal" RepeatLayout="Flow"
                                                    OnSelectedIndexChanged="rblTahun_SelectedIndexChanged">
                                                    <%--<asp:ListItem Text="1" Value="1" Selected="True" />
                                            <asp:ListItem Text="2" Value="2" />
                                            <asp:ListItem Text="3" Value="3" />--%>
                                                </asp:RadioButtonList>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-2 text-right">
                                        <asp:LinkButton ID="lbExportExcel" runat="server" CssClass="btn btn-success"
                                            data-toggle="tooltip" data-original-title="Unduh RAB Usulan Format Lama"
                                            OnClick="lbExportExcel_Click"><i class="fa fa-file-excel-o"></i>&nbsp;&nbsp;RAB Usulan</asp:LinkButton>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-10">
                                        <div class="form-group row">
                                            <label for="example-text-input" class="col-xs-2 col-form-label form-control-label">
                                                Kelompok</label>
                                            <div class="col-sm-6">
                                                <asp:DropDownList ID="ddlKelompok" runat="server"
                                                    AutoPostBack="true" CssClass="form-control"
                                                    OnSelectedIndexChanged="ddlKelompok_SelectedIndexChanged">
                                                    <asp:ListItem Text="- Pilih Kelompok -" Value="-1" Selected="True" />
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-2 text-right">
                                    </div>
                                </div>
                                <hr />
                                <div class="row">
                                    <div class="col-sm-12">
                                        <asp:GridView ID="gvRAB" runat="server" AutoGenerateColumns="false"
                                            GridLines="None" ShowHeaderWhenEmpty="true" CssClass="table table-hover"
                                            DataKeyNames="id_rab_komponen_belanja,id_rab_komponen_belanja_revisi">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Item">
                                                    <ItemTemplate>
                                                        <a href="#" data-toggle="tooltip" style="color: #08c; text-decoration-style: dotted;"
                                                            title="" data-original-title='<%# Eval("keterangan") %>'>
                                                            <%# Eval("komponen_belanja")%>
                                                        </a>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Satuan">
                                                    <ItemTemplate>
                                                        <%# Eval("satuan")%>
                                                    </ItemTemplate>
                                                    <HeaderStyle Width="100px" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Volume">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="tbVolume" runat="server" CssClass="form-control qty" Width="100%"
                                                            Text='<%# Eval("volume") %>' onkeypress="return CheckNumber(event)"></asp:TextBox>
                                                    </ItemTemplate>
                                                    <HeaderStyle Width="60px" HorizontalAlign="Center" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Harga Satuan">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="tbBiaya" runat="server" CssClass="form-control harga" Width="100%"
                                                            Text='<%# decimal.Parse(Eval("harga_satuan").ToString()).ToString("N0").Replace(".","") %>'
                                                            onkeypress="return CheckNumber(event)"></asp:TextBox>
                                                    </ItemTemplate>
                                                    <HeaderStyle Width="200px" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Total">
                                                    <ItemTemplate>
                                                        <label class="subtotal font-weight-bold"><%# decimal.Parse(Eval("total").ToString()).ToString("N0") %></label>
                                                    </ItemTemplate>
                                                    <HeaderStyle Width="180px" HorizontalAlign="Right" />
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                            </Columns>
                                            <EmptyDataTemplate>
                                                <div class="col-sm-12">
                                                    <p class="text-primary">Data tidak ditemukan.</p>
                                                </div>
                                            </EmptyDataTemplate>
                                        </asp:GridView>
                                    </div>
                                </div>
                                <hr />
                                <div class="row">
                                    <div class="offset-sm-10 col-sm-2 text-right">
                                        <label class="p-l-15 p-r-15 p-t-5 p-b-5 font-weight-bold grandtotal" style="display: inline-block; color: white; background-color: #323232;">
                                            Rp.&nbsp;<asp:Label ID="lblTotalBiaya" runat="server" Text="0"></asp:Label>
                                        </label>
                                    </div>
                                </div>
                            </div>
                            <div class="panel-footer text-center">
                                <asp:LinkButton ID="lbSimpan" runat="server" CssClass="btn btn-primary"
                                    OnClick="lbSimpan_OnClick">Simpan</asp:LinkButton>
                            </div>
                        </div>
                    </contenttemplate>
                </div>
            </div>
        </div>
    </div>

    <script>
        function InitJS() {

            // Initialize Auto Calculate 
            //$('.pnm, .price, .subtot, .grdtot').prop('readonly', true);
            //var $tblrows = $("#tblProducts tbody tr");
            var $tblrows = $(<%= "\"#" + gvRAB.ClientID + " tr" + "\"" %>);

            $tblrows.each(function (index) {
                var $tblrow = $(this);

                $tblrow.find('.qty').on('change', function () {

                    var qty = $tblrow.find(".qty").val();
                    var price = $tblrow.find(".harga").val();
                    var subTotal = parseInt(qty, 10) * parseFloat(price);

                    if (!isNaN(subTotal)) {

                        $tblrow.find(".subtotal").html(formatRupiah(subTotal));
                        //$tblrow.find(".subtotal").val(subTotal.toFixed(2));

                        getGrandTotal();
                    }
                });

                $tblrow.find('.harga').on('change', function () {

                    var qty = $tblrow.find(".qty").val();
                    var price = $tblrow.find(".harga").val();
                    var subTotal = parseInt(qty, 10) * parseFloat(price);

                    if (!isNaN(subTotal)) {

                        $tblrow.find(".subtotal").html(formatRupiah(subTotal));

                        getGrandTotal();
                    }
                });
            });

            // Initialize Tooltip
            $('[data-toggle="tooltip"]').tooltip();
        };

        Sys.Application.add_load(InitJS);

        function getGrandTotal() {
            var grandTotal = 0;

            $(".subtotal").each(function () {
                var stval = parseFloat($(this).text().replace(/\./g, ""));
                grandTotal += isNaN(stval) ? 0 : stval;
            });

            $('.grandtotal').html('Rp. ' + formatRupiah(grandTotal));
        };

        function CheckNumber(e) {
            var charCode = (e.which) ? e.which : event.keyCode
            if (charCode > 31 && (charCode < 48 || charCode > 57))
                return false;

            return true;
        }

        /* Fungsi formatRupiah */
        function formatRupiah(angka) {
            var number_string = angka.toString(),
                sisa = number_string.length % 3,
                rupiah = number_string.substr(0, sisa),
                ribuan = number_string.substr(sisa).match(/\d{3}/g);

            if (ribuan) {
                separator = sisa ? '.' : '';
                rupiah += separator + ribuan.join('.');
            }

            return rupiah;
        }
    </script>
    </contenttemplate>
</div>
<%--</asp:UpdatePanel>--%>
