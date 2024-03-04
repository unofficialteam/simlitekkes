<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="riwayatPenelitian.ascx.cs" Inherits="simlitekkes.UserControls.Pengusul.riwayatPenelitian" %>
<div class="card">
    <div class="card-header">
        <h5 class="card-header-text">RIWAYAT PENELITIAN</h5>
    </div>
    <div class="card-block">
        <div class="view-info">
            <div class="row">
                <div class="col-lg-12">
                    <div class="general-info">
                        <div class="row">
                            <div class="col-sm-12 table-responsive">
                                <asp:ListView ID="lvRiwayatPenelitian" runat="server">
                                    <LayoutTemplate>
                                        <table class="table table-hover">
                                            <tbody>
                                                <tr>
                                                    <td style="width: 30px; text-align: center; padding: 0;"></td>
                                                    <td style="padding: 0;"></td>
                                                </tr>
                                                <tr id="itemPlaceHolder" runat="server"></tr>
                                            </tbody>
                                        </table>
                                    </LayoutTemplate>
                                    <ItemTemplate>
                                        <tr>
                                            <td><%# Container.DataItemIndex + 1 %></td>
                                            <td>
                                                <span style="color: forestgreen"><b><%# Eval("judul") %></b></span><br />
                                                <b>Tahun: </b><%# Eval("thn_pelaksanaan_kegiatan") %> |
                                                <b>Peran: </b><%# Eval("peran_personil") %> |
                                                <b>Sumber Dana: </b><%# Eval("sumber_dana") %><br />
                                                <span class="text-primary"><%# Eval("nama_skema") %></span>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                    <EmptyDataTemplate>
                                        <tr>
                                            <td colspan="2">
                                                <div class="p-4">
                                                    <div class="alert alert-info" role="alert">
                                                        Belum ada data Penelitian sumber dana Kemenkes...
                                                    </div>
                                                </div>
                                            </td>
                                        </tr>
                                    </EmptyDataTemplate>
                                </asp:ListView>
                            </div>
                        </div>
                        <!-- end of row -->
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>
