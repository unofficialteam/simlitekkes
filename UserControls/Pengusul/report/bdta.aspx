<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="bdta.aspx.cs" Inherits="simlitekkes.UserControls.Pengusul.report.bdta" %>

<%@ Register Src="~/UserControls/Pengusul/report/biodataAnggota.ascx" TagName="pdfBioData" TagPrefix="uc" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            
        <uc:pdfBioData runat="server" ID="pdfBioData" ></uc:pdfBioData>
        </div>
    </form>
</body>
</html>
