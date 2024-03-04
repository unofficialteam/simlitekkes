<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="naskahAkademik.ascx.cs" Inherits="simlitekkes.UserControls.Pengusul.luaranLapKemajuan.naskahAkademik" %>

<style type="text/css">
    .auto-style1 {
        font-weight: bold;
    }
    .auto-style2 {
        color: #008000;
        font-weight: bold;
    }
</style>

<div class="row" style="padding: 10px;">
    <div class="col-lg-12">
        <div class="auto-style2">
            Naskah Akademik (Policy&nbsp;Brief,&nbsp;Rekomendasi&nbsp;Kebijakan,&nbsp;atau&nbsp;Model&nbsp;Kebijakan&nbsp;Strategis)</div>
        <div style="padding: 10px;">
            <div class="row" style="margin-top: 10px;">
                <div class="col-lg-2">
                    Status naskah akademik</div>
                <div class="col-lg-4">
                    <asp:DropDownList runat="server" ID="ddlStatusKebijakan" CssClass="form-control"
                        OnSelectedIndexChanged="ddlStatusKebijakan_SelectedIndexChanged" AutoPostBack="true"
                        DataTextField="nama_target_jenis_luaran" DataValueField="id_target_jenis_luaran" ClientIDMode="Static">
                    </asp:DropDownList>
                </div>
            </div>
            <div class="row" style="margin-top: 10px;">
                <div class="col-lg-2">
                    Jenis naskah akademik
                </div>
                <div class="col-lg-4">
                    <asp:DropDownList runat="server" ID="ddlJenisNaskahKebijakan" CssClass="form-control"
                        DataTextField="naskah_akademik" DataValueField="id_jenis_naskah_akademik" ClientIDMode="Static"
                        AppendDataBoundItems="true">
                    </asp:DropDownList>
                </div>
            </div>
            <div class="row" style="margin-top: 10px;">
                <div class="col-lg-2">
                    <asp:Label runat="server" ID="lblNamaLembaga" Text="Institusi/Lembaga yang akan menerima naskah"></asp:Label>
                </div>
                <div class="col-lg-10">
                    <asp:TextBox runat="server" ID="tbNamaLembaga" CssClass="form-control" Text=""></asp:TextBox>
                </div>
            </div>


            <div class="row">
                <div class="col-lg-6">
                    <hr>
                </div>
                <div class="col-lg-6">
                </div>
            </div>

            <div>Unggah Dokumen</div>

            <table style="margin-top: 10px;">
                <tr>
                    <td>
                        <asp:LinkButton runat="server" ID="lbUnduhPdfDok1" Text="" ForeColor="Red" OnClick="lbUnduhPdfDok1_Click">
                            <i class="far fa-file-pdf" style="font-size: 50px; "></i>
                        </asp:LinkButton>
                    </td>
                    <td>
                        <div style="margin-left: 10px;">
                            <asp:Label runat="server" ID="lblNamaJenisDokumen1" Text=""></asp:Label><br>
                            <asp:FileUpload runat="server" ID="fileUpload1" CssClass="form-control" />
                        </div>
                    </td>
                </tr>
            </table>
            <div>
                <span style="font-size: 14px; padding: 10px; color: red; font-style: italic;">(Ukuran File Maksimal
                    <asp:Label runat="server" ID="lblUkuranMaksDok1"></asp:Label>
                    MB dengan format PDF)
                </span>
            </div>

            

            


            <div style="margin: 10px;">
                <hr>
            </div>

            <div class="row" style="margin-top: 10px;">
                <div class="col-lg-12 text-right">
                    <asp:LinkButton runat="server" ID="lbSimpan" CssClass="btn btn-success" OnClick="lbSimpan_Click">				
                        <i class="fa fa-save""></i>&nbsp;&nbsp;Simpan
                    </asp:LinkButton>
                </div>
            </div>

        </div>
    </div>
</div>
