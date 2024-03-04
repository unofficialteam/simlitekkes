<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="rab.ascx.cs" Inherits="simlitekkes.UserControls.Pengusul.pendanaan2021.rab" %>
<asp:ScriptManagerProxy ID="ScriptManagerProxy1" runat="server"></asp:ScriptManagerProxy>
<asp:UpdatePanel ID="upRAB" runat="server">
    <Triggers>
        <asp:PostBackTrigger ControlID="lbExportExcel" />
    </Triggers>
    <ContentTemplate>
        <style>
            .equal {
                display: flex;
                display: -webkit-flex;
                flex-wrap: wrap;
            }
        </style>

        <div class="row m-t-20">
            <div class="col-sm-12">
                <div class="card mt-5">
                    <div class="card-header">
                        <h6 class="fs-17 font-weight-600 mb-0">Rencana Anggaran Belanja</h6>
                    </div>
                    <div class="card-body p-2">
                        <div class="row equal mb-1" style="color: #fff; font-size: 14px; margin-left: 2px; margin-right: 2px;">
                            <div class="col-sm-8 p-1" style="background-color: #28a745; opacity: .80;">
                                <asp:Repeater ID="rptrSummary" runat="server">
                                    <HeaderTemplate>
                                        <ul>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <li>Usulan Dana Tahun <%# Eval("urutan_tahun") %> : 
                                                <strong>Rp. <%# decimal.Parse(Eval("dana_diajukan").ToString()).ToString("N0") %></strong>
                                        </li>
                                    </ItemTemplate>
                                    <FooterTemplate></ul></FooterTemplate>
                                </asp:Repeater>

                                <table style="text-align: left;">
                                    <tr>
                                        <td style="vertical-align: top; display: none;">
                                            <div>
                                                <%--Dana pertahun bidang fokus:--%>
                                                Dana per tahun
                                                <asp:Label ID="lblBidangFokusJudul" runat="server" Text=" bidang fokus"></asp:Label>:&nbsp;
                                            <asp:Label runat="server" ID="lblBidFokus"></asp:Label>
                                                &nbsp;<br />
                                                - Minimal: Rp.
                                            <asp:Label runat="server" ID="lblDanaMinimal" />
                                                &nbsp; - Maksimal: Rp.
                                            <asp:Label runat="server" ID="lblDanaMaksimal" />
                                            </div>
                                        </td>
                                    </tr>
                                </table>

                            </div>
                            <div class="col-sm-4 p-1 text-right" style="background-color: #28a745">
                                Total Dana Direncanakan : Rp.
                                    <asp:Label ID="lblTotalDanaDiajukan" runat="server" Font-Bold="true">0</asp:Label>
                            </div>
                        </div>
                        <div class="form-group row mt-3">
                            <label for="rblTahun" class="col-sm-2 col-form-label font-weight-bold" style="text-align: left">
                                Tahun Ke</label>
                            <div class="col-sm-10">
                                <asp:RadioButtonList ID="rblTahun" runat="server" CssClass="radio-button-list"
                                    AutoPostBack="true" RepeatDirection="Horizontal" RepeatLayout="Flow"
                                    OnSelectedIndexChanged="rblTahun_SelectedIndexChanged">
                                    <asp:ListItem Text="1" Value="1" Selected="True" />
                                    <asp:ListItem Text="2" Value="2" />
                                    <asp:ListItem Text="3" Value="3" />
                                </asp:RadioButtonList>
                            </div>
                        </div>
                        <div class="form-group row">
                            <label for="ddlKelompok" class="col-sm-2 col-form-label font-weight-bold" style="text-align: left">
                                Kelompok</label>
                            <div class="col-sm-4">
                                <asp:DropDownList ID="ddlKelompok" runat="server"
                                    AutoPostBack="true" CssClass="form-control"
                                    OnSelectedIndexChanged="ddlKelompok_SelectedIndexChanged">
                                    <asp:ListItem Text="- Pilih Kelompok -" Value="-1" Selected="True" />
                                </asp:DropDownList>
                            </div>
                            <div class="col text-right">
                                <asp:LinkButton ID="lbExportExcel" runat="server" CssClass="btn btn-info"
                                    data-toggle="tooltip" data-original-title="Unduh RAB Usulan Format Lama"
                                    OnClick="lbExportExcel_Click"><i class="fas fa-file-excel"></i>&nbsp;&nbsp;RAB Usulan</asp:LinkButton>
                            </div>
                        </div>

                        <hr />
                        <div class="row">
                            <div class="col-sm-12">
                                <table class="table table-hover">
                                    <thead>
                                        <tr>
                                            <th style="width: 200px">Komponen</th>
                                            <th>Item</th>
                                            <th style="width: 100px;">Satuan</th>
                                            <th style="width: 60px;">Volume</th>
                                            <th class="text-right" style="width: 140px;">Harga Satuan</th>
                                            <th class="text-right" style="width: 140px">Total</th>
                                            <th style="width: 100px"></th>
                                        </tr>
                                    </thead>
                                    <asp:ListView ID="lvRAB" runat="server"
                                        DataKeyNames="id_rab_item_belanja"
                                        OnItemEditing="lvRAB_ItemEditing"
                                        OnItemCanceling="lvRAB_ItemCanceling"
                                        OnItemDataBound="lvRAB_ItemDataBound"
                                        OnItemUpdating="lvRAB_ItemUpdating"
                                        OnItemDeleting="lvRAB_ItemDeleting">
                                        <LayoutTemplate>
                                            <tbody>
                                                <tr id="itemPlaceHolder" runat="server"></tr>
                                            </tbody>
                                        </LayoutTemplate>
                                        <ItemTemplate>
                                            <tr>
                                                <td><%# Eval("komponen_belanja") %></td>
                                                <td><%# Eval("nama_item") %></td>
                                                <td><%# Eval("satuan") %></td>
                                                <td><%# Eval("volume") %></td>
                                                <td style="text-align: right;"><%# decimal.Parse(Eval("harga_satuan").ToString()).ToString("N0") %></td>
                                                <td style="text-align: right;"><%# decimal.Parse(Eval("total_harga").ToString()).ToString("N0") %></td>
                                                <td>
                                                    <asp:LinkButton ID="lbEdit" runat="server" CssClass="btn btn-inverse-info btn-sm" CommandName="Edit">
                                                            <i class="fa fa-pencil" aria-hidden="true"></i>
                                                    </asp:LinkButton>&nbsp;
                                                        <asp:LinkButton ID="lbHapus" runat="server" CssClass="btn btn-inverse-danger btn-sm" CommandName="Delete">
                                                            <i class="fa fa-trash" aria-hidden="true"></i>
                                                        </asp:LinkButton>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <tr>
                                                <td>
                                                    <asp:DropDownList ID="ddlKomponenEdit" runat="server" CssClass="form-control">
                                                        <asp:ListItem Text="-- Pilih Komponen --" Value="-1" Selected="True"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="tbItemEdit" runat="server" CssClass="form-control" Width="100%"
                                                        Text='<%# Eval("nama_item") %>'>
                                                    </asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="tbSatuanEdit" runat="server" CssClass="form-control" Width="100%"
                                                        Text='<%# Eval("satuan") %>'></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="tbVolumeEdit" runat="server" CssClass="form-control qty-edit" Width="100%"
                                                        Text='<%# Eval("volume") %>'
                                                        onkeypress="return CheckNumber(event)"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="tbHargaSatuanEdit" runat="server" CssClass="form-control harga-edit" Width="100%"
                                                        Text='<%# decimal.Parse(Eval("harga_satuan").ToString()).ToString("N0").Replace(".","").Replace(",","") %>'
                                                        onkeypress="return CheckNumber(event)"></asp:TextBox>
                                                </td>
                                                <td style="text-align: right;">
                                                    <label class="subtotal-edit font-weight-bold"><%# decimal.Parse(Eval("total_harga").ToString()).ToString("N0") %></label>
                                                </td>
                                                <td>
                                                    <asp:LinkButton ID="lbSimpan" runat="server" CssClass="btn btn-inverse-success btn-sm"
                                                        CommandName="Update">
                                                            <i class="fa fa-check" aria-hidden="true"></i>
                                                    </asp:LinkButton>&nbsp;
                                                        <asp:LinkButton ID="lbBatal" runat="server" CssClass="btn btn-inverse-warning btn-sm"
                                                            CommandName="Cancel">
                                                            <i class="fa fa-times" aria-hidden="true"></i>
                                                        </asp:LinkButton>
                                                </td>
                                            </tr>
                                        </EditItemTemplate>
                                        <EmptyDataTemplate>
                                            <tr>
                                                <td colspan="7" style="text-align: center">
                                                    <div class="p-4">
                                                        <h6 class="text-info font-weight-bold">Belum ada data...</h6>
                                                    </div>
                                                </td>
                                            </tr>
                                        </EmptyDataTemplate>
                                    </asp:ListView>
                                    <tfoot>
                                        <tr class="table-active">
                                            <td>
                                                <asp:DropDownList ID="ddlKomponen" runat="server" CssClass="form-control">
                                                    <asp:ListItem Text="-- Pilih Komponen --" Value="-1" Selected="True"></asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="tbItem" runat="server" CssClass="form-control" Width="100%"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="tbSatuan" runat="server" CssClass="form-control" Width="100%"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="tbVolume" runat="server" CssClass="form-control qty" Width="100%"
                                                    onkeypress="return CheckNumber(event)"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="tbHargaSatuan" runat="server" CssClass="form-control harga" Width="100%"
                                                    onkeypress="return CheckNumber(event)"></asp:TextBox>
                                            </td>
                                            <td style="text-align: right; vertical-align:middle;">
                                                <label class="subtotal font-weight-bold fs-14">0</label>
                                            </td>
                                            <td>
                                                <asp:LinkButton ID="lbSimpanItem" runat="server" CssClass="btn btn-success"
                                                    OnClick="lbSimpanItem_Click">Tambah</asp:LinkButton>
                                            </td>
                                        </tr>
                                    </tfoot>
                                </table>
                            </div>
                        </div>
                        <hr />
                        <div class="row">
                            <div class="offset-sm-9 col-sm-3 text-right">
                                <%--<label class="p-l-15 p-r-15 p-t-5 p-b-5 font-weight-bold grandtotal" style="display: inline-block; color: white; background-color: #323232;">
                                    Rp.&nbsp;<asp:Label ID="lblTotalBiaya" runat="server" Text="0"></asp:Label>
                                </label>--%>
                                <h5>
                                    <span class="badge badge-dark">Rp.&nbsp;<asp:Label ID="lblTotalBiaya" runat="server" Text="0"></asp:Label>
                                    </span>
                                </h5>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <script src="assets/plugins/jquery.rowspanizer/jquery.rowspanizer.js"></script>

        <script>                
            function InitJS() {

                // Initialize Auto Calculate

                $('.qty').on('change', function () {

                    var qty = $(".qty").val();
                    var price = $(".harga").val();
                    var subTotal = parseInt(qty, 10) * parseFloat(price);

                    if (!isNaN(subTotal)) {
                        $(".subtotal").html(formatRupiah(subTotal));
                    }
                });

                $('.harga').on('change', function () {

                    var qty = $(".qty").val();
                    var price = $(".harga").val();
                    var subTotal = parseInt(qty, 10) * parseFloat(price);

                    if (!isNaN(subTotal)) {
                        $(".subtotal").html(formatRupiah(subTotal));
                    }
                });

                $('.qty-edit').on('change', function () {

                    var qty = $(".qty-edit").val();
                    var price = $(".harga-edit").val();
                    var subTotal = parseInt(qty, 10) * parseFloat(price);

                    if (!isNaN(subTotal)) {
                        $(".subtotal-edit").html(formatRupiah(subTotal));
                    }
                });

                $('.harga-edit').on('change', function () {

                    var qty = $(".qty-edit").val();
                    var price = $(".harga-edit").val();
                    var subTotal = parseInt(qty, 10) * parseFloat(price);

                    if (!isNaN(subTotal)) {
                        $(".subtotal-edit").html(formatRupiah(subTotal));
                    }
                });

                $("table").rowspanizer({
                    columns: [0]
                });
            };

            Sys.Application.add_load(InitJS);

            function CheckNumber(e) {
                var charCode = (e.which) ? e.which : event.keyCode
                if (charCode > 31 && (charCode < 48 || charCode > 57))
                    return false;

                return true;
            };

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
            };
        </script>
    </ContentTemplate>
</asp:UpdatePanel>
