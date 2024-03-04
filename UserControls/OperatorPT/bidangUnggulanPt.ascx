<%@ Control Language="C#" CodeBehind="bidangUnggulanPt.ascx.cs" Inherits="simlitekkes.UserControls.OperatorPT.bidangUnggulanPt" %>

<asp:ScriptManagerProxy ID="ScriptManagerProxy1" runat="server"></asp:ScriptManagerProxy>
<asp:UpdatePanel ID="upBidangUnggulanPt" runat="server" UpdateMode="Always">
    <ContentTemplate>
        <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="upBidangUnggulanPt">
            <ProgressTemplate>
                <div class="modal_loading">
                    <div class="center">
                        <i class="fa fa-refresh fa-spin fa-5x"></i>
                    </div>
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>

        <section class="content-header">
            <h1>
                <asp:Label ID="lblJudulPage" runat="server" Text="Bidang Unggulan PT"></asp:Label></h1>
            <div style="float: right; background: transparent; margin-top: 0; margin-bottom: 0; font-size: 12px; position: absolute; top: 15px; right: 10px; border-radius: 2px; padding: 0 5px;">
                <asp:LinkButton ID="lbTambahBidang" runat="server" CssClass="btn btn-success btn-sm" Visible="false" OnClick="lbTambahBidang_Click">Tambah Bidang Unggulan</asp:LinkButton>
            </div>
        </section>

        <section class="content">
            <div>
                <asp:Label ID="lblError" runat="server" ForeColor="Red"></asp:Label>
                <asp:MultiView ID="mvBidangUnggulanPt" runat="server" ActiveViewIndex="0">
                    <asp:View ID="vDaftarBidangUnggulanPt" runat="server">
                        <div class="box">
                            <div class="box-body">
                                <div class="row">
                                    <div class="col-sm-12">
                                        <asp:GridView ID="gvDaftarBidangUnggulanPt" runat="server" GridLines="None" CssClass="table table-striped table-hover"
                                            DataKeyNames="id_bidang_unggulan_perguruan_tinggi, bidang_unggulan_perguruan_tinggi, tahun_penetapan, kode_status_aktif"
                                            ShowHeader="true" ShowHeaderWhenEmpty="true" AutoGenerateColumns="False"
                                            OnRowDataBound="gvDaftarBidangUnggulanPt_RowDataBound" OnRowUpdating="gvDaftarBidangUnggulanPt_RowUpdating">
                                            <Columns>
                                                <asp:TemplateField HeaderText="No.">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbnNomor" runat="server" Text="<%# Container.DataItemIndex + 1 %>"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle Width="30px" />
                                                    <ItemStyle Width="30px" HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Bidang Unggulan">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblBidangUnggulan" runat="server" Text='<%# Bind("bidang_unggulan_perguruan_tinggi") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Tahun Penetapan">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblThnPenetapan" runat="server" Text='<%# Bind("tahun_penetapan") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Sts Aktif">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblStsAktif" runat="server" Text='<%# Bind("kode_status_aktif") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Update">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lbEditBidang" runat="server" CommandName="Update" Visible="false" CssClass="btn btn-primary" ToolTip="Edit Data">
                                                            <i class="fa fa-edit"></i>
                                                        </asp:LinkButton>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                </asp:TemplateField>
                                            </Columns>
                                            <EmptyDataTemplate>
                                                <div style="min-height: 100px; margin: 0 auto;">
                                                    <strong>DATA TIDAK ADA</strong>
                                                </div>
                                            </EmptyDataTemplate>
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>
                    </asp:View>
                </asp:MultiView>
                <div class="modal fade" id="modalInsupBidang" role="dialog" aria-labelledby="myModalInsupBidang"
                    aria-hidden="true" data-backdrop="false" style="background-color: rgba(0, 0, 0, 0.5);">
                    <div class="modal-dialog" role="document">
                        <div class="modal-content">
                            <div class="modal-header">
                                <h4 style="text-align: center" class="modal-title" id="myModalDetailUsulan">Bidang Unggulan PT
                                </h4>
                                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                    <span aria-hidden="true">×</span>
                                </button>
                            </div>
                            <div class="modal-body">
                                <div class="form-horizontal">
                                    <div class="box-body">
                                        <div class="form-group">
                                            <label for="tbBidangUnggulanPt" class="col-sm-4 control-label">Bidang Unggulan</label>
                                            <div class="col-sm-5">
                                                <asp:TextBox ID="tbBidangUnggulanPt" runat="server" CssClass="form-control input-sm"
                                                    placeholder="Bidang Unggulan"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label for="tbThnPenetapan" class="col-sm-4 control-label">Tahun</label>
                                            <div class="col-sm-5">
                                                <%--<asp:TextBox ID="tbThnPenetapan" runat="server" CssClass="form-control input-sm"
                                                    placeholder="Tahun Penetapan"></asp:TextBox>--%>
                                                <asp:DropDownList ID="ddlThnPenetapan" runat="server" CssClass="form-control input-sm">                                                    
                                                </asp:DropDownList>

                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label for="rblStsAktif" class="col-sm-4 control-label">Sts Aktif</label>
                                            <div class="col-sm-5">
                                                <asp:RadioButtonList runat="server" ID="rblStsAktif" RepeatDirection="Horizontal">
                                                    <asp:ListItem Text="&nbsp;Aktif" Value="1" Selected="True"></asp:ListItem>
                                                    <asp:ListItem Text="&nbsp;Tidak aktif" Value="0"></asp:ListItem>
                                                </asp:RadioButtonList>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="modal-footer">
                                <button type="button" class="btn btn-default" data-dismiss="modal">Batal</button>
                                <asp:LinkButton ID="lbInsupBidangUnggulan" runat="server" CssClass="btn btn-info pull-right"
                                    OnClick="lbInsupBidangUnggulan_Click" OnClientClick="$('#modalInsupBidang').modal('hide');">Simpan</asp:LinkButton>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </section>
    </ContentTemplate>
</asp:UpdatePanel>
