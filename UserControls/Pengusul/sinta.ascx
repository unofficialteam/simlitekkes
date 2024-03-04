<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="sinta.ascx.cs" Inherits="simlitekkes.UserControls.Pengusul.sinta" %>


<style type="text/css">
    .auto-style1 {
        width: 191px;
    }

    .auto-style2 {
        width: 7px;
    }
</style>
<asp:ScriptManagerProxy ID="smpSinta" runat="server"></asp:ScriptManagerProxy>
<asp:UpdatePanel ID="upSinta" runat="server">
    <ContentTemplate>
        <asp:MultiView ID="mvSinta" runat="server" ActiveViewIndex="0">
            <asp:View ID="vDaftar" runat="server">
                <div class="card">
                    <div class="card-header">
                        <div class="d-flex justify-content-between align-items-center">
                            <div>
                                <h6 class="fs-17 font-weight-600 mb-0">SINTA |
                            <asp:Label ID="lblSintaId" runat="server" Text="-"></asp:Label>
                                </h6>
                            </div>
                            <div class="text-right">
                                <div class="actions">
                                    <asp:LinkButton ID="lbEdit" runat="server" OnClick="lbEdit_click"
                                        class="btn btn-success" Text="Edit">                                
                                    </asp:LinkButton>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="card-body">
                        <div class="row" style="padding-bottom: 10px;">
                            <div class="col-11">
                                <div class="flex-fill ml-3 mb-2">
                                    <span style="font-size: medium;">Sinta Skor: </span>
                                    <asp:Label ID="lblSintaSkor" runat="server" CssClass="font-weight-bold" Font-Size="Large" Text="-"></asp:Label>
                                </div>
                            </div>
                            <div class="col-1 text-right">
                                <asp:LinkButton ID="lbSinkronisasi" runat="server" OnClick="lbSinkronisasi_Click"
                                    class="btn btn-success btn-circle">
                                <i class="fas fa-sync-alt"></i>
                                </asp:LinkButton>
                            </div>
                        </div>
                        <div class="row table-responsive">
                            <div class="col-12 row">
                                <div class="col-6">
                                    <div class="flex-fill ml-3 mb-2">
                                        <asp:Label runat="server" ID="lblJudulScopus" Text="Scopus" Font-Bold="true" Font-Size="Large"></asp:Label>
                                        | 
                                ID
                                <asp:Label ID="lblScopusid" runat="server" CssClass="font-weight-bold" Font-Size="Small" Text="" ForeColor="#009900"></asp:Label>
                                    </div>
                                    <div class="flex-fill ml-3 mb-2">
                                        H-Index: 
                                <asp:Label ID="lblScopushindex" runat="server" CssClass="font-weight-bold" Font-Size="Small" Text="" ForeColor="#009900"></asp:Label>
                                    </div>
                                    <div class="flex-fill ml-3 mb-2">
                                        Articles: 
                                <asp:Label ID="lblScopusArticle" runat="server" CssClass="font-weight-bold" Font-Size="Small" Text="" ForeColor="#009900"></asp:Label>
                                    </div>
                                    <div class="flex-fill ml-3 mb-2">
                                        Citation: 
                                <asp:Label ID="lblScopusCitation" runat="server" CssClass="font-weight-bold" Font-Size="Small" Text="" ForeColor="#009900"></asp:Label>
                                    </div>
                                </div>
                                <div class="col-6">
                                    <asp:Label runat="server" ID="lblJudulGoogleScholar" Text="Google Scholar" Font-Bold="true" Font-Size="Large"></asp:Label>
                                    |
                            ID
                            <asp:Label ID="lblGooglescholarid" runat="server" CssClass="font-weight-bold" Font-Size="Small" Text="" ForeColor="#009900"></asp:Label>
                                    <div class="flex-fill ml-3 mb-2">
                                        H-Index: 
                                <asp:Label ID="lblGooglehindex" runat="server" CssClass="font-weight-bold" Font-Size="Small" Text="" ForeColor="#009900"></asp:Label>
                                    </div>
                                    <div class="flex-fill ml-3 mb-2">
                                        Articles: 
                                <asp:Label ID="lblGoogleArticle" runat="server" CssClass="font-weight-bold" Font-Size="Small" Text="" ForeColor="#009900"></asp:Label>
                                    </div>
                                    <div class="flex-fill ml-3 mb-2">
                                        Citation:
                                <asp:Label ID="LblGoogleCitation" runat="server" CssClass="font-weight-bold" Font-Size="Small" Text="" ForeColor="#009900"></asp:Label>
                                    </div>
                                    <div class="flex-fill ml-3 mb-2">
                                        Google I10: 
                                <asp:Label ID="lblGoogleI10" runat="server" CssClass="font-weight-bold" Font-Size="Small" Text="" ForeColor="#009900"></asp:Label>
                                    </div>
                                </div>
                                <asp:Panel runat="server" ID="pnlRank" Visible="false">
                                    <table>
                                        <tr>
                                            <th scope="row" class="auto-style1">Rank In National</th>
                                            <td class="auto-style2">
                                                <asp:Label ID="lblNatRank" runat="server" ForeColor="Green" Text="-"></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <th scope="row" class="auto-style1">Rank in Affiliation</th>
                                            <td class="auto-style2">
                                                <asp:Label ID="lblAffRank" runat="server" ForeColor="Green" Text="-"></asp:Label></td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </div>
                        </div>
                    </div>
                </div>
            </asp:View>
            <asp:View ID="vEdit" runat="server">
                <div class="card">
                    <div class="card-header">
                        <h5 class="card-header-text">Sinta</h5>
                    </div>
                    <div class="card-block">
                        <div class="row">
                            <div class="col-lg-12 col-xl-6">
                                <table class="table ml-2">
                                    <tbody>
                                        <tr>
                                            <th scope="row" class="auto-style1">Sinta ID</th>
                                            <td>
                                                <div class="col-sm-12">
                                                    <div class="input-group">
                                                        <asp:TextBox ID="tbIdSinta" runat="server" class="form-control" placeholder="Id SINTA"></asp:TextBox>
                                                        <div class="input-group-append">
                                                            <asp:LinkButton ID="lbCek" runat="server" Text="Cek" OnClick="lbCek_Click" class="btn btn-primary"></asp:LinkButton>
                                                        </div>
                                                    </div>
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <th scope="row" class="auto-style1">Nama Author</th>
                                            <td>
                                                <asp:Label ID="lblNamaAuthor" runat="server" ForeColor="Green" Text="-"></asp:Label></td>
                                        </tr>
                                        <%--<tr>
                                            <th scope="row" class="auto-style1">Sinta ID</th>
                                            <td>
                                                <asp:Label ID="lblSintaId2" runat="server" ForeColor="Green" Text="-"></asp:Label></td>
                                        </tr>--%>
                                        <tr>
                                            <th scope="row" class="auto-style1">Sinta Skor</th>
                                            <td>
                                                <asp:Label ID="lblSintaSkor2" runat="server" ForeColor="Green" Text="-"></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <th scope="row" class="auto-style1">Rank In National</th>
                                            <td>
                                                <asp:Label ID="lblNatRank2" runat="server" ForeColor="Green" Text="-"></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <th scope="row" class="auto-style1">Rank in Affiliation</th>
                                            <td>
                                                <asp:Label ID="lblAffRank2" runat="server" ForeColor="Green" Text="-"></asp:Label>
                                        </tr>

                                    </tbody>
                                </table>
                            </div>

                        </div>

                        <div class="row mt-3">
                            <div class="col-lg-6">
                                <table class="table">
                                    <thead>
                                        <tr>
                                            <th colspan="2"><span class="label label-primary" style="font-size: large">Scopus</span></th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr>
                                            <td>Scopus Id</td>
                                            <td>
                                                <asp:Label ID="lblScopusid2" runat="server" ForeColor="Green" Text=""></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td>H-Index</td>
                                            <td>
                                                <asp:Label ID="lblScopushindex2" runat="server" ForeColor="Green" Text=""></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td>Articles</td>
                                            <td>
                                                <asp:Label ID="lblScopusArticle2" runat="server" ForeColor="Green" Text=""></asp:Label></td>

                                        </tr>
                                        <tr>
                                            <td>Citation</td>
                                            <td>
                                                <asp:Label ID="lblScopusCitation2" runat="server" ForeColor="Green" Text=""></asp:Label></td>

                                        </tr>
                                    </tbody>
                                </table>
                            </div>
                            <div class="col-lg-6">
                                <table class="table">
                                    <thead>
                                        <tr>
                                            <th colspan="2">
                                                <span class="label label-primary" style="font-size: large">Google Scholar</span>
                                            </th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr>
                                            <td>Google ID</td>
                                            <td>
                                                <asp:Label ID="lblGooglescholarid2" runat="server" ForeColor="Green" Text=""></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td>H-Index</td>
                                            <td>
                                                <asp:Label ID="lblGooglehindex2" runat="server" ForeColor="Green" Text=""></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td>Articles</td>
                                            <td>
                                                <asp:Label ID="lblGoogleArticle2" runat="server" ForeColor="Green" Text=""></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td>Citation</td>
                                            <td>
                                                <asp:Label ID="LblGoogleCitation2" runat="server" ForeColor="Green" Text=""></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td>Google I10</td>
                                            <td>
                                                <asp:Label ID="lblGoogleI102" runat="server" ForeColor="Green" Text=""></asp:Label></td>
                                        </tr>
                                    </tbody>
                                </table>
                            </div>
                            <table class="table m-0">
                                <tbody>
                                </tbody>
                            </table>
                            <table class="table m-0">
                                <tbody>
                                </tbody>
                            </table>
                        </div>

                        <div class="row mt-4 mb-4">
                            <div class="col-sm-12 text-center">
                                <asp:LinkButton ID="lbSimpan" runat="server" Text="Simpan" class="btn btn-primary" OnClick="lbSimpan_click"></asp:LinkButton>
                                <asp:LinkButton ID="lbBatal" runat="server" Text="Batal" class="btn btn-secondary" OnClick="lbBatal_click"></asp:LinkButton>
                            </div>
                        </div>
                    </div>
                </div>
            </asp:View>
        </asp:MultiView>
    </ContentTemplate>
</asp:UpdatePanel>

<div class="modal modal-success fade" id="myModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel"
    aria-hidden="true" data-backdrop="false" style="background-color: rgba(0, 0, 0, 0.5);">
    <div class="modal-dialog" role="document">
        <div class="modal-content">
            <div class="modal-header">
                <h5 class="modal-title" id="myModalHapus">Konfirmasi</h5>
                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                    <span aria-hidden="true">&times;</span>
                </button>
            </div>
            <div class="modal-body">
                Anda yakin akan menyimpan id sinta ini untuk identitas SINTA anda&nbsp; 
                <asp:Label runat="server" ID="lblHapus" Text="..." ForeColor="Green"></asp:Label>
                ?
            </div>
            <div class="modal-footer">
                <asp:LinkButton ID="lbHapus" runat="server" CssClass="btn btn-primary" OnClick="lbSimpanSinta_Click" OnClientClick="$('#myModal').modal('hide');">Simpan</asp:LinkButton>
                <button type="button" class="btn btn-secondary" data-dismiss="modal">Batal</button>

            </div>
        </div>
    </div>
</div>
