<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="simlitekkes.Login" %>

<!DOCTYPE html>

<html lang="id" xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <!-- HTML5 shim and Respond.js for IE8 support of HTML5 elements and media queries -->
    <!-- WARNING: Respond.js doesn't work if you view the page via file:// -->
    <!--[if lt IE 9]>
	<script src="https://oss.maxcdn.com/html5shiv/3.7.2/html5shiv.min.js"></script>
	<script src="https://oss.maxcdn.com/respond/1.4.2/respond.min.js"></script>
	<![endif]-->

    <!-- Required meta tags -->
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no">
    <meta name="description" content="Aplikasi Simlitabkes - Aplikasi Sistem Manajemen Penelitian dan Pengabdian Kepada Masyarakat Politeknik Kesehatan">
    <meta name="keywords" content="Simlitabkes, Penelitian, Pengabdian Kepada Masyarakat, Politeknik Kesehatan, Kementerian Kesehatan">
    <meta name="author" content="TIM TI - Pusdik SDM Kesehatan Kementerian Kesehatan">
    <title>SIMLITABKES - Login</title>
    <!-- App favicon -->
    <link rel="shortcut icon" href="assets/dist/img/favicon.png">
    <!--Global Styles(used by all pages)-->
    <link href="assets/plugins/bootstrap/css/bootstrap.min.css" rel="stylesheet">
    <link href="assets/plugins/toastr/toastr.css" rel="stylesheet">

    <!--Third party Styles(used by this page)-->

    <!--Start Your Custom Style Now-->
    <link href="assets/dist/css/style.css" rel="stylesheet">


    <!--Global script(used by all pages)-->
    <script src="assets/plugins/jQuery/jquery-3.4.1.min.js"></script>
    <script src="assets/plugins/bootstrap/js/bootstrap.min.js"></script>

    <!-- Third Party Scripts(used by this page)-->

    <!--Page Active Scripts(used by this page)-->
    <script src="assets/plugins/toastr/toastr.min.js"></script>

</head>
<body class="bg-white">
    <div class="d-flex align-items-center justify-content-center text-center h-100vh">
        <div class="form-wrapper m-auto">
            <div class="form-container my-4">
                <div class="register-logo text-center mb-2">
                    <img src="assets/dist/img/logo-kemenkes-small.png" alt="Kementerian Kesehatan" height="90">
                </div>
                <div class="panel">
                    <div class="panel-header text-center mb-3">
                        <h3 class="fs-24">Login SIMLITABKES</h3>
                        <p class="text-muted text-center mb-0">Silahkan gunakan akun yang telah anda peroleh.</p>
                    </div>
                    <form id="register_form" class="register-form" runat="server">
                        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
                        <div class="form-group">
                            <asp:TextBox ID="tbNamaUser" runat="server" placeholder="Nama User" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="form-group">
                            <asp:TextBox ID="tbPassword" runat="server" placeholder="Password" CssClass="form-control" TextMode="Password"></asp:TextBox>
                        </div>
                        <div class="form-row mb-2">
                            <div class="col-sm-3">
                                <img src="Helper/captchaImg.aspx" alt="Captcha" />
                            </div>
                            <div class="col-sm-9 pl-3 text-sm-right">
                                <asp:TextBox ID="txtCaptcha" runat="server" CssClass="form-control md-static" aria-describedby="captcha"></asp:TextBox>
                                <span class="fs-10 text-success">Hasil penjumlahan</span>
                            </div>
                        </div>

                        <asp:Button ID="btnLogin" runat="server" Text="Login" CssClass="btn btn-success btn-block" OnClick="btnLogin_Click" />
                    </form>
                </div>
                <div class="bottom-text text-center my-3">
                    <b>Sistem Manajemen Penelitian dan Pengabmas<br /> Politeknik Kesehatan</b><br />
                    <span class="text-muted">Kementerian Kesehatan Republik Indonesia<br />
                        2020</span>
                </div>
            </div>
        </div>
    </div>
    <!-- /.End of form wrapper -->
</body>
</html>
