<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="pengirimanUserPasswordOperator.ascx.cs" Inherits="simlitekkes.UserControls.OperatorPenelitianPusdik.pengirimanUserPasswordOperator" %>


<div class="row">

<div class="col-md-12">
    <div class="card mb-4">
        <div class="card-header">
            <div class="d-flex justify-content-between align-items-center">
                <div>
                    <h5 style="color: darkred">Pengiriman User Password Operator</h5>
                </div>
                <div class="text-right">
                </div>
            </div>
        </div>
        <div class="card-body">
            <asp:GridView ID="gvPengirimanPassword" runat="server" GridLines="None" ShowHeaderWhenEmpty="True"
                DataKeyNames="id_institusi,surel,nama_user,pswd"
                AutoGenerateColumns="False" Width="100%"
                OnRowUpdating="gvPengirimanPassword_RowUpdating">
                <Columns>
                    <asp:TemplateField HeaderText="No">
                        <ItemTemplate>
                            <%# Container.DataItemIndex + 1 %>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" Width="30" />
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Perguruan Tinggi">
                        <ItemTemplate>
                            <%# Eval("kd_perguruan_tinggi") %> - <%# Eval("nama_institusi") %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Email">
                        <ItemTemplate>
                            <%# Eval("surel") %><br />
                            Jml dikirim: <span style="background-color: lightgreen; padding: 2px; font-weight: bold;"><%# Eval("jml_pengiriman") %></span>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <%--<asp:TemplateField HeaderText="">
                        <ItemTemplate>
                            Jml dikirim: <%# Eval("jml_pengiriman") %>
                        </ItemTemplate>
                    </asp:TemplateField>--%>
                    <asp:TemplateField HeaderText="">
                        <ItemTemplate>
                            <asp:LinkButton runat="server" ID="lbKirimEmail" CssClass="btn btn-success" CommandName="update" 
                                CommandArgument="<%# Container.DataItemIndex %>" ToolTip="Kirim user dan password operator PT Penelitian">
                                   <i class="fa fa-paper-plane"></i>
                            </asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <EmptyDataTemplate>
                    <div class="col-sm-12">
                        <p class="text-primary">Data tidak ditemukan</p>
                    </div>
                </EmptyDataTemplate>
            </asp:GridView>
        </div>
    </div>
</div>
    
</div>