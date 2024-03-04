<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RevCatatanHarian.aspx.cs" Inherits="simlitekkes.RevCatatanHarian" %>
<%@ Register Src="~/UserControls/Pengusul/catatanHarian.ascx" TagName="ktCatatanHarian" TagPrefix="uc" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no">
    <title></title>
    <link rel="shortcut icon" href="assets/dist/img/favicon.png">

    <!--Global Styles(used by all pages)-->
    <link href="assets/plugins/bootstrap/css/bootstrap.min.css" rel="stylesheet">
    <link href="assets/plugins/toastr/toastr.css" rel="stylesheet">
    <link href="assets/plugins/metisMenu/metisMenu.min.css" rel="stylesheet">
    <link href="assets/plugins/fontawesome/css/all.min.css" rel="stylesheet">
    <link href="assets/plugins/typicons/src/typicons.min.css" rel="stylesheet">
    <link href="assets/plugins/select2/dist/css/select2.min.css" rel="stylesheet">
    <link href="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-datepicker/1.9.0/css/bootstrap-datepicker.min.css" rel="stylesheet">
    <link href="assets/plugins/themify-icons/themify-icons.min.css" rel="stylesheet">

    <!--Third party Styles(used by this page)-->
    <link href="assets/plugins/datatables/dataTables.bootstrap4.min.css" rel="stylesheet">
    <!--Start Your Custom Style Now-->
    <link href="assets/dist/css/style.css" rel="stylesheet">
    <link href="assets/dist/css/custom.css" rel="stylesheet" />


    <!--Global script(used by all pages)-->
    <script src="assets/plugins/jQuery/jquery-3.4.1.min.js"></script>
    <script src="assets/plugins/bootstrap/js/bootstrap.min.js"></script>
    <script src="assets/dist/js/popper.min.js"></script>
    <script src="assets/plugins/metisMenu/metisMenu.min.js"></script>
    <script src="assets/plugins/perfect-scrollbar/dist/perfect-scrollbar.min.js"></script>

    <!-- Third Party Scripts(used by this page)-->
    <script src="assets/plugins/chartJs/Chart.min.js"></script>
    <script src="assets/plugins/sparkline/sparkline.min.js"></script>
    <script src="assets/plugins/datatables/dataTables.min.js"></script>
    <script src="assets/plugins/datatables/dataTables.bootstrap4.min.js"></script>

    <!--Page Active Scripts(used by this page)-->
    <script src="assets/plugins/toastr/toastr.min.js"></script>
    <script src="assets/plugins/select2/dist/js/select2.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-datepicker/1.9.0/js/bootstrap-datepicker.min.js"></script>

    <!--Page Scripts(used by all page)-->
    <script src="assets/dist/js/sidebar.js"></script>
    <script src="assets/plugins/autonumeric/autoNumeric.min.js"></script>
</head>
<body>
    <form id="form1" runat="server" style="margin: 5px;">
            <asp:ScriptManager ID="smMain" runat="server"></asp:ScriptManager>
        <div>
            <uc:ktCatatanHarian runat="server" ID="ktCatatanHarian" />
        </div>
    </form>
</body>
</html>
