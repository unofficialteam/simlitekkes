<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="beranda.ascx.cs" Inherits="simlitekkes.UserControls.OperatorPenelitianPusdik.beranda" %>
<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>
<asp:UpdatePanel runat="server">
    <ContentTemplate>
        <div class="row">
            <asp:MultiView runat="server" ID="mvBeranda" ActiveViewIndex="0">
                <asp:View ID="vChart" runat="server">
                    <div class="col-md-12">
                        <div class="card">
                            <div class="card-header bg-info text-white">
                                <h6>Rekap Tahapan Pengajuan Usulan</h6>
                            </div>
                            <div class="card-body">
                                <div class="row">
                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <label>Tahun Usulan</label>
                                            <asp:DropDownList runat="server" ID="ddlTahunUsulan" CssClass="form-control select2" OnSelectedIndexChanged="ddlFilterTahunUsulan" AutoPostBack="true">
                                                <asp:ListItem Value="2021">2021</asp:ListItem>
                                                <asp:ListItem Value="2022">2022</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <label>Tahun Pelaksanaan</label>
                                            <asp:DropDownList runat="server" ID="ddlTahunPelaksanaan" CssClass="form-control select2" OnSelectedIndexChanged="ddlFilterTahunPelaksanaan" AutoPostBack="true">
                                                <asp:ListItem Value="2022">2022</asp:ListItem>
                                                <asp:ListItem Value="2023">2023</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="table-responsive" style="overflow: auto">
                                            <asp:Chart ID="Chart1" runat="server" CssClass="table table-bordered table-condensed" Width="1000" >
                                                <Titles>
                                                    <asp:Title ShadowOffset="3" Name="Items" />
                                                </Titles>
                                                <Legends>
                                                    <asp:Legend Alignment="Center" Docking="Top" IsTextAutoFit="False" Name="Default"
                                                        LegendStyle="Row" />
                                                </Legends>
                                                <ChartAreas>
                                                    <asp:ChartArea Name="ChartArea1"></asp:ChartArea>
                                                </ChartAreas>
                                            </asp:Chart>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </asp:View>
            </asp:MultiView>
        </div>
    </ContentTemplate>
</asp:UpdatePanel>

