<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Default" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
    <head runat="server">
        <title>.:: Que Pexinxa - Login ::.</title>
        <meta charset="utf-8">
        <meta name="viewport" content="width=device-width, initial-scale=1">
        <link rel="icon" type="image/png" sizes="16x16" href="Images/Imagem1.png">
        <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css">
        <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.1.1/jquery.min.js"></script>
        <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js"></script>
        <script type="text/javascript">
            function Validar(nomeID, senhaID) {
                if (document.getElementById(nomeID).value == '') {
                    document.getElementById('lblMensagem').innerHTML = 'É necessário preencher o campo Nome!';
                    document.getElementById(nomeID).focus();
                    return false;
                }
                else {
                    if (document.getElementById(senhaID).value == '') {
                        document.getElementById('lblMensagem').innerHTML = 'É necessário preencher o campo Senha!';
                        document.getElementById(senhaID).focus();
                        return false;
                    }
                }
                return true;
            }
        </script>
        <style>
            .navbar {
                margin-bottom: 0;
                background-color: Highlight;
                z-index: 9999;
                border: 0;
                font-size: 12px !important;
                line-height: 1.42857143 !important;
                letter-spacing: 4px;
                border-radius: 0;
            }
            #aguarde
            {
	            color: white;
	            float: right;
                font-weight: bold;
            }
            .data
            {
	            color: White;
                font-weight: bold;
	            float: right;
            }
        </style>
    </head>
    <body style="background-repeat:no-repeat; background-position:center; background-attachment:fixed;">
        <div class="container-fluid" style="padding-top:3px;">
            <form id="form1" runat="server" class="form-horizontal">
                <asp:ScriptManager ID="ScriptManager1" runat="server" ></asp:ScriptManager>
                <asp:UpdatePanel ID="UpdatePanel1" runat="server" >
                    <ContentTemplate>
                        <nav class="navbar">
                            <div class="container-fluid">
                                <div id="aguarde">
                                    <asp:UpdateProgress runat="server" ID="ProgressIndicator" DisplayAfter="0" >
                                        <ProgressTemplate><img src="images/spinner.gif" width="16" height="16" alt="Aguarde" />Aguarde...&nbsp;</ProgressTemplate>
                                    </asp:UpdateProgress>
		                        </div>
                            </div>
		                    <asp:Label ID="lblData" runat="server" CssClass="data label" />
                        </nav>
                        <img src="Images/Imagem3.png" class="img-responsive" alt="Logo">
                        <h5>
                            Área de Administração
                        </h5>
                        <div class="form-group">
                            <label class="alert alert-info control-label col-sm-1" style="margin-left:12px;text-align:left;padding-left:2px;padding-top:6px;padding-bottom:6px;" for="matricula">Nome:</label>
                            <div class="col-sm-3">
                                <asp:TextBox ID="txtNome" runat="server" class="form-control" MaxLength="50" placeholder="Nome" autocomplete="off"/>
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="alert alert-info control-label col-sm-1" style="margin-left:12px;text-align:left;padding-left:2px;padding-top:6px;padding-bottom:6px;" for="senha">Senha:</label>
                            <div class="col-sm-3">
                                <asp:TextBox ID="txtSenha" runat="server" class="form-control" TextMode="Password" MaxLength="32" placeholder="Entre Senha" autocomplete="off"/>
                            </div>
                        </div>
                        <div class="form-group">        
                            <div class="col-sm-offset-1 col-sm-10">
                                <asp:Button ID="btnOK" runat="server" Text="Entrar" CssClass="btn btn-primary" OnClick="btnOK_Click" />
                            </div>
                        </div>
                        <div class="form-group"> 
                            <div class="alert-danger">
                                <asp:Label ID="lblMensagem" runat="server" />
                            </div>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </form>
        </div>
    </body>
</html>