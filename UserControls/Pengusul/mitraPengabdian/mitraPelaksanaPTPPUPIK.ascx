<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="mitraPelaksanaPTPPUPIK.ascx.cs" Inherits="simlitekkes.UserControls.Pengusul.mitraPengabdian.mitraPelaksanaPTPPUPIK" %>

<div class="card">
    <div class="card-header">
        <h5 class="card-header-text" style="color: red;">Mitra Pelaksana Pengabdian</h5>
    </div>
    <div class="card-block">
        <div class="view-info">
            <div class="form-group row">
                <label class="col-xs-2 col-form-label form-control-label">Nama Pimpinan</label>
                <div class="col-sm-6 col-xs-12">
                    <asp:textbox id="tbNamaPimpinan" runat="server" cssclass="form-control"></asp:textbox>
                </div>
            </div>
            <div class="form-group row">
                <label class="col-xs-2 col-form-label form-control-label">Jabatan</label>
                <div class="col-sm-4 col-xs-12">
                    <asp:textbox id="tbJabatan" runat="server" cssclass="form-control"></asp:textbox>
                </div>
            </div>
            <div class="form-group row">
                <label class="col-xs-2 col-form-label form-control-label">Alamat</label>
                <div class="col-sm-10 col-xs-12">
                    <asp:textbox id="tbAlamatInstitusi" runat="server" cssclass="form-control" placeholder="Isikan alamat surat institusi mitra" textmode="MultiLine" rows="3"></asp:textbox>
                </div>
            </div>
            <div class="form-group row" style="padding-left: 15px;">
                <h5 class="card-header-text" style="color: red;">KONTRIBUSI PENDANAAN</h5>
                <i style="background-color:yellow; font-size:x-small; color:red;">Wajib Ada (minimal 30.000.000/Tahun)</i>
            </div>
            <div class="form-group row">
                <asp:label id="lblPendanaanThn1" runat="server" cssclass="col-xs-1 col-form-label form-control-label" text="Tahun 1"></asp:label>
                <div class="col-sm-2">
                    <asp:textbox id="tbPendanaanThn1" runat="server" cssclass="form-control uang1" clientidmode="Static"></asp:textbox>
                </div>
                <asp:label id="lblPendanaanThn2" runat="server" cssclass="col-xs-1 col-form-label form-control-label" text="Tahun 2"></asp:label>
                <div class="col-sm-2">
                    <asp:textbox id="tbPendanaanThn2" runat="server" cssclass="form-control uang2" clientidmode="Static"></asp:textbox>
                </div>
                <asp:label id="lblPendanaanThn3" runat="server" cssclass="col-xs-1 col-form-label form-control-label" text="Tahun 3"></asp:label>
                <div class="col-sm-2">
                    <asp:textbox id="tbPendanaanThn3" runat="server" cssclass="form-control uang3" clientidmode="Static"></asp:textbox>
                </div>
            </div>
        </div>
    </div>
</div>
