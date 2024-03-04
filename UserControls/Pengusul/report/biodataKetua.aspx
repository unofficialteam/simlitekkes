<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="biodataKetua.aspx.cs" Inherits="simlitekkes.UserControls.Pengusul.report.biodataKetua" %>

<%@ Register Src="~/UserControls/Pengusul/report/biodata.ascx" TagName="pdfBioData" TagPrefix="uc" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server" style="margin-left: 40px; ">
        <div>
        <uc:pdfBioData runat="server" ID="pdfBioDataKetua" ></uc:pdfBioData>
        </div>
    </form>
</body>
</html>
