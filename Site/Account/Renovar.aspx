<%@ Page Title=".:: Renovar ::." Language="C#" MasterPageFile="~/Account/Administracao.master" AutoEventWireup="true" CodeFile="Renovar.aspx.cs" Inherits="Account_Renovar" %>
<%@ MasterType TypeName="Account_Administracao" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <script type="text/javascript">
        function Validar(dataFim, mensagemID) {
            try {
                    if (ValorCampo(dataFim) == "")
                        return ExibirMensagem(dataFim, mensagemID, "É necessário preencher o campo Data Fim!");
                    else {
                        if (ValorCampo(dataFim) != "") {
                            var dataHoje = new Date().format("dd/MM/yyyy");
                            dataHoje = parseFloat(dataHoje.split("/")[2] + "" + dataHoje.split("/")[1] + "" + dataHoje.split("/")[0]);
                            var Fim = parseFloat(document.getElementById(dataFim).value.split("/")[2] + "" + document.getElementById(dataFim).value.split("/")[1] + "" + document.getElementById(dataFim).value.split("/")[0]);

                            if (Fim < dataHoje)
                                return ExibirMensagem(dataFim, mensagemID, "A Data Fim não pode ser menor que a data de Hoje!!!");
                        }
                    }
                    return true;
            }
            catch (_erro) {
                document.getElementById(mensagemID).innerHTML = 'Ocorreu um erro! ' + _erro;
                return false;
            }
            finally {
                setTimeout(function () { document.getElementById(mensagemID).innerHTML = ''; }, 5000);
            }
        }
    </script>
        <style type="text/css">
        .bs-example{
    	    margin: 20px;
        }
    </style>
    <br />
    <fieldset>
        <legend>Renovar Promoção</legend>
        <div class="container-fluid form-horizontal" style="padding-top:3px;">
            <h5 style="color: red"> 
                * Campos Obrigatórios
            </h5>
            <div class="form-group">
                <label class="alert alert-info control-label col-sm-1" style="margin-left:12px;text-align:left;padding-left:2px;padding-right:0px;padding-top:6px;padding-bottom:6px;" for="endereco">Data Fim:</label>
                <div class="col-sm-2">
                    <asp:TextBox ID="txtDataFim" runat="server" CssClass="form-control" style="text-align:center;"/>
                    <cc1:MaskedEditExtender ID="meetxtDataFim" runat="server" MaskType="Date" TargetControlID="txtDataFim" Mask="99/99/9999" /><br />
                    <cc1:MaskedEditValidator ID="mevtxtDataFim" runat="server" ControlExtender="meetxtDataFim" SetFocusOnError="true" Display="Dynamic" ControlToValidate="txtDataFim" InvalidValueMessage="Data Inválida" />
                </div>
                <span style="color:red; font-family:Arial;"> *</span>
            </div>
            <div class="form-group">
                <label class="alert alert-info control-label col-sm-1" style="margin-left:12px;text-align:left;padding-left:2px;padding-right:0px;padding-top:6px;padding-bottom:6px;" for="endereco">Ativo:</label>
                <div class="col-sm-2">
                    <asp:CheckBox ID="chkAtivo" runat="server" Checked="true" />
                </div>
            </div>
            <div class="form-group">        
                <div class="col-sm-offset-1 col-sm-10">
                    <asp:Button ID="btnSalvar" runat="server" CssClass="btn btn-primary"  Text="Salvar" OnClick="btnSalvar_Click"/>
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