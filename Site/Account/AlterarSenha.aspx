<%@ Page Language="C#" MasterPageFile="~/Account/Administracao.master" AutoEventWireup="true" CodeFile="AlterarSenha.aspx.cs" Inherits="Account_AlterarSenha" Title=".:: Que Pexinxa - Alterar Senha ::." %>
<%@ MasterType TypeName="Account_Administracao" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <script type="text/javascript">
        function Validar(senhaAntigaID, senhaNovaID, redigiteSenhaNovaID)
        {
            if (document.getElementById(senhaAntigaID).value == '') {
                ExibirMensagem('É necessário preencher o campo Senha Antiga!');
                document.getElementById(senhaAntigaID).focus();
                return false;
            }

            if (document.getElementById(senhaNovaID).value == '') {
                ExibirMensagem('É necessário preencher o campo Nova Senha!');
                document.getElementById(senhaNovaID).focus();
                return false;
            }

            if (document.getElementById(redigiteSenhaNovaID).value == '') {
                ExibirMensagem('É necessário preencher o campo Redigite a Senha!');
                document.getElementById(redigiteSenhaNovaID).focus();
                return false;
            }

            if (document.getElementById(redigiteSenhaNovaID).value != document.getElementById(senhaNovaID).value) {
                ExibirMensagem('Senhas não confere!');
                document.getElementById(redigiteSenhaNovaID).focus();
                return false;
            }

            if (document.getElementById(senhaNovaID).value.length < 6) {
                ExibirMensagem('São necessário no mínimo 6 caracteres para a nova senha!');
                document.getElementById(senhaNovaID).focus();
                return false;
            }
            return true;
        }
    </script>
    <br />
    <fieldset>
        <legend>Alterar Senha</legend>
        <div class="container-fluid form-horizontal" style="padding-top:3px;">
            <h5 style="color: red"> 
                * Campos Obrigatórios
            </h5>
            <div class="form-group">
                <label class="alert alert-info control-label col-sm-1" style="margin-left:12px;text-align:left;padding-left:2px;padding-top:6px;padding-bottom:6px;" for="matricula">Usuário:</label>
                <div class="col-sm-3">
                    <asp:Label ID="lblUsuario" runat="server" class="control-label"/>
                </div>
            </div>
            <div class="form-group">
                <label class="alert alert-info control-label col-sm-1" style="margin-left:12px;text-align:left;padding-left:2px;padding-right:0px;padding-top:6px;padding-bottom:6px;" for="senha">Senha Antiga:</label>
                <div class="col-sm-3">
                    <asp:TextBox ID="txtSenhaAntiga" runat="server" class="form-control" TextMode="Password" MaxLength="32" placeholder="Entre Senha Antiga"/>
                </div>
                <span style="color:red; font-family:Arial;"> *</span>
            </div>
            <div class="form-group">
                <label class="alert alert-info control-label col-sm-1" style="margin-left:12px;text-align:left;padding-left:2px;padding-top:6px;padding-bottom:6px;" for="senha">Nova Senha:</label>
                <div class="col-sm-3">
                    <asp:TextBox ID="txtNovaSenha" runat="server" class="form-control" TextMode="Password" MaxLength="32" placeholder="Entre Nova Senha"/>
                </div>
                <span style="color:red; font-family:Arial"> *</span>
            </div>
            <div class="form-group">
                <label class="alert alert-info control-label col-sm-1" style="margin-left:12px;text-align:left;padding-left:2px;padding-top:6px;padding-bottom:6px;" for="senha">Nova Senha:</label>
                <div class="col-sm-3">
                    <asp:TextBox ID="txtRedigiteSenha" runat="server" class="form-control" TextMode="Password" MaxLength="32" placeholder="Redigite Senha"/>
                </div>
                <span style="color:red; font-family:Arial"> *</span>
            </div>
            <div class="form-group">        
                <div class="col-sm-offset-1 col-sm-10">
                    <asp:Button ID="btnSalvar" runat="server" Text="Salvar" CssClass="btn btn-primary" OnClick="btnSalvar_Click"/>
                    <asp:Button ID="btnLimpar" runat="server" Text="Limpar" CssClass="btn btn-primary" OnClick="btnLimpar_Click"/>
                </div>
            </div>
            <div class="form-group"> 
                <div class="alert-danger">
                    <asp:Label ID="lblMensagem" runat="server" />
                </div>
            </div>
        </div>
     </fieldset>
</asp:Content>