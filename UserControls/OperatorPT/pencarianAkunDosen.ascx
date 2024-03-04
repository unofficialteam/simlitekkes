<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="pencarianAkunDosen.ascx.cs" Inherits="simlitekkes.UserControls.OperatorPT.pencarianAkunDosen" %>

<div class="card">
    <div class="card-block">
        <div id="basic" class="col-lg-12 layout-spacing">
            <div class="statbox widget box box-shadow">
                <div class="widget-header">
                    <div class="row">
                        <div class="col-xl-12 col-md-12 col-sm-12 col-12">
                            <h4>
                                <asp:Label runat="server" ID="lblJudulHeader" Text="Pencarian Akun Dosen" Font-Bold="true"
                                    Font-Size="Larger"></asp:Label>
                            </h4>
                        </div>
                    </div>
                </div>
                <div class="widget-content widget-content-area">

                    <div class="row">
                        <div class="col-12 mx-auto">
                            <div class="form-inline" style="text-align: left;">
                                <div class="form-group">
                                    <asp:TextBox runat="server" ID="tbNIDN" placeholder="NIDN" CssClass="form-control"
                                        MaxLength="10" TextMode="Number"></asp:TextBox>
                                </div>
                                &nbsp;
                                <asp:LinkButton ID="lbCariNIDN" runat="server" CssClass="btn btn-success waves-effect waves-light" OnClick="lbCariNIDN_Click">Cari</asp:LinkButton>
                            </div>
                        </div>
                    </div>
                    <hr />
                    <div class="col-12 row">
                        <div class="col-12">
                            <div class="flex-fill ml-3 mb-2">
                                <h6 class="fs-15 text-muted mb-0">Nama</h6>
                                <asp:Label ID="lblNama" runat="server" Text="-" CssClass="font-weight-bold"></asp:Label>
                            </div>
                            <div class="flex-fill ml-3 mb-2">
                                <h6 class="fs-15 text-muted mb-0">Program Studi</h6>
                                <asp:Label ID="lblProdi" runat="server" Text="-" CssClass="font-weight-bold"></asp:Label>
                            </div>
                            <div class="flex-fill ml-3 mb-2">
                                <h6 class="fs-15 text-muted mb-0">Username</h6>
                                <asp:Label ID="lblUsername" runat="server" Text="-" CssClass="font-weight-bold"></asp:Label>
                            </div>
                            <div class="flex-fill ml-3 mb-2">
                                <h6 class="fs-15 text-muted mb-0">Password</h6>
                                <asp:Label ID="lblPassword" runat="server" Text="-" CssClass="font-weight-bold"></asp:Label>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>

