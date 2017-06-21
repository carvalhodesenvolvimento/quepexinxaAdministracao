<%@ Page Language="C#" MaintainScrollPositionOnPostback="true" MasterPageFile="~/Account/Administracao.master" AutoEventWireup="true" CodeFile="Promocao.aspx.cs" Inherits="Account_Promocao" Title=".:: Que Pexinxa - Promoção ::." %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ MasterType TypeName="Account_Administracao" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <script type="text/javascript">
        function Validar(produtoID, dataInicio, dataFim, valorAntigo, valorAtual, mensagemID) {
            try {
                if (ValorCampo(produtoID) == "")
                    return ExibirMensagem(produtoID, mensagemID, "É necessário selecionar o Produto!");
                else
                    if (ValorCampo(dataInicio) == "")
                        return ExibirMensagem(dataInicio, mensagemID, "É necessário preencher o campo Data Início!");
                    else
                        if (ValorCampo(dataFim) == "")
                            return ExibirMensagem(dataFim, mensagemID, "É necessário preencher o campo Data Fim!");
                        else
                            if (ValorCampo(valorAntigo) == "")
                                return ExibirMensagem(valorAntigo, mensagemID, "É necessário preencher o campo Valor Antigo!");
                            else
                                if (ValorCampo(valorAtual) == "")
                                    return ExibirMensagem(valorAtual, mensagemID, "É necessário preencher o campo Valor Atual!");
                                else
                                {
                                    if (ValorCampo(dataInicio) != "" && ValorCampo(dataFim) != "") {
                                        var dataHoje = new Date().format("dd/MM/yyyy");
                                        dataHoje = parseFloat(dataHoje.split("/")[2] + "" + dataHoje.split("/")[1] + "" + dataHoje.split("/")[0]);
                                        var Inicio = parseFloat(document.getElementById(dataInicio).value.split("/")[2] + "" + document.getElementById(dataInicio).value.split("/")[1] + "" + document.getElementById(dataInicio).value.split("/")[0]);
                                        var Fim = parseFloat(document.getElementById(dataFim).value.split("/")[2] + "" + document.getElementById(dataFim).value.split("/")[1] + "" + document.getElementById(dataFim).value.split("/")[0]);

                                        if (Inicio > Fim)
                                            return ExibirMensagem(dataInicio, mensagemID, "A Data de início não pode ser maior que a data Fim!!!");
                                        else
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

        function GetScrollerPosition() {
            try {
                var scrollX, scrollY;
                if (document.all) {
                    if (!document.documentElement.scrollLeft)
                        scrollX = document.body.scrollLeft;
                    else
                        scrollX = document.documentElement.scrollLeft;

                    if (!document.documentElement.scrollTop)
                        scrollY = document.body.scrollTop;
                    else
                        scrollY = document.documentElement.scrollTop;
                }
                else {
                    scrollX = window.pageXOffset;
                    scrollY = window.pageYOffset;
                }
                document.getElementById("<%= xPos.ClientID %>").value = scrollX;
                document.getElementById("<%= yPos.ClientID %>").value = scrollY;
            }
            catch (_erro) {
                document.getElementById("ContentPlaceHolder1_lblMensagem").innerHTML = 'Ocorreu um Erro! ' + _erro.message;
                return false;
            }
            finally {
                setTimeout(function () { document.getElementById("ContentPlaceHolder1_lblMensagem").innerHTML = ''; }, 5000);
            }
        }

        function SetScrollerPosition() {
            try {
                if (document.getElementById("<%= xPos.ClientID %>").value != "") {
                    var x = document.getElementById("<%= xPos.ClientID %>").value;
                    var y = document.getElementById("<%= yPos.ClientID %>").value;
                    window.scrollTo(x, y);
                }
                document.getElementById("<%= xPos.ClientID %>").value = "0";
                document.getElementById("<%= yPos.ClientID %>").value = "0";
            }
            catch (_erro) {
                document.getElementById("ContentPlaceHolder1_lblMensagem").innerHTML = 'Ocorreu um Erro! ' + _erro.message;
                return false;
            }
            finally {
                setTimeout(function () { document.getElementById("ContentPlaceHolder1_lblMensagem").innerHTML = ''; }, 5000);
            }
        }
    </script>
        <style type="text/css">
        .bs-example{
    	    margin: 20px;
        }
    </style>
    <asp:HiddenField ID="xPos" runat="server" />
    <asp:HiddenField ID="yPos" runat="server" />
    <br />
    <fieldset>
        <legend>Produto</legend>
        <div class="container-fluid form-horizontal" style="padding-top:3px;">
            <h5 style="color: red"> 
                * Campos Obrigatórios
            </h5>
            <div class="form-group">
                <label class="alert alert-info control-label col-sm-1" style="margin-left:12px;text-align:left;padding-left:2px;padding-right:0px;padding-top:6px;padding-bottom:6px;" for="nome">Produto:</label>
                <div class="col-sm-8">
                    <asp:DropDownList ID="ddlProduto" runat="server" CssClass="form-control" DataValueField="ValorID" DataTextField="Valor" />
                </div>
                <span style="color:red; font-family:Arial;"> *</span>
            </div>
            <div class="form-group">
                <label class="alert alert-info control-label col-sm-1" style="margin-left:12px;text-align:left;padding-left:2px;padding-right:0px;padding-top:6px;padding-bottom:6px;" for="endereco">Data Início:</label>
                <div class="col-sm-2">
                    <asp:TextBox ID="txtDataInicio" runat="server" CssClass="form-control" style="text-align:center;"/>
                    <cc1:MaskedEditExtender ID="meetxtDataInicio" runat="server" MaskType="Date" TargetControlID="txtDataInicio" Mask="99/99/9999" /><br />
                    <cc1:MaskedEditValidator ID="mevtxtDataInicio" runat="server" ControlExtender="meetxtDataInicio" SetFocusOnError="true" Display="Dynamic" ControlToValidate="txtDataInicio" InvalidValueMessage="Data Inválida" />
                </div>
                <span style="color:red; font-family:Arial;"> *</span>
            </div>
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
                <label class="alert alert-info control-label col-sm-1" style="margin-left:12px;text-align:left;padding-left:2px;padding-right:0px;padding-top:6px;padding-bottom:6px;" for="endereco">Valor Antigo:</label>
                <div class="col-sm-2">
                    <asp:TextBox ID="txtValorAntigo" runat="server" CssClass="form-control" style="text-align:right;"/>
                    <cc1:MaskedEditExtender ID="meetxtValorAntigo" runat="server" MaskType="Number" TargetControlID="txtValorAntigo" Mask="9,999.99" CultureName="pt-BR" ClearMaskOnLostFocus="true" DisplayMoney="left" InputDirection="RightToLeft" />
                    <cc1:MaskedEditValidator ID="mevtxtValorAntigo" runat="server" ControlExtender="meetxtValorAntigo" SetFocusOnError="true" Display="Dynamic" ControlToValidate="txtValorAntigo" IsValidEmpty="true" InvalidValueMessage="Valor Inválido"/>
                </div>
                <span style="color:red; font-family:Arial;"> *</span>
            </div>
            <div class="form-group">
                <label class="alert alert-info control-label col-sm-1" style="margin-left:12px;text-align:left;padding-left:2px;padding-right:0px;padding-top:6px;padding-bottom:6px;" for="endereco">Valor Atual:</label>
                <div class="col-sm-2">
                    <asp:TextBox ID="txtValorAtual" runat="server" CssClass="form-control" style="text-align:right;"/>
                    <cc1:MaskedEditExtender ID="meetxttxtValorAtual" runat="server" MaskType="Number" TargetControlID="txtValorAtual" Mask="9,999.99" CultureName="pt-BR" ClearMaskOnLostFocus="true" DisplayMoney="left" InputDirection="RightToLeft" />
                    <cc1:MaskedEditValidator ID="mevtxttxtValorAtual" runat="server" ControlExtender="meetxttxtValorAtual" SetFocusOnError="true" Display="Dynamic" ControlToValidate="txtValorAtual" IsValidEmpty="true" InvalidValueMessage="Valor Inválido"/>
                </div>
                <span style="color:red; font-family:Arial;"> *</span>
            </div>
            <div class="form-group">        
                <div class="col-sm-offset-1 col-sm-10">
                    <asp:Button ID="btnSalvar" runat="server" CssClass="btn btn-primary"  Text="Salvar" OnClick="btnSalvar_Click"/>
                    <asp:Button ID="btnLimpar" runat="server" CssClass="btn btn-primary" Text="Limpar" OnClick="btnLimpar_Click"/>
                </div>
            </div>
            <div class="form-group"> 
                <div class="alert-danger">
                    <asp:Label ID="lblMensagem" runat="server" />
                </div>
            </div>
        </div>
        <fieldset>
            <legend>Promoção</legend>
            <asp:GridView ID="gdvPromocao" runat="server" CssClass="table table-bordered" AllowPaging="True" CellPadding="4" ForeColor="#333333" GridLines="None" AutoGenerateColumns="False" DataKeyNames="PromocaoID" Width="100%" EmptyDataText="Não há dados para serem exibidos." OnPageIndexChanging="gdvPromocao_PageIndexChanging" OnRowDataBound="gdvPromocao_RowDataBound" OnRowDeleting="gdvPromocao_RowDeleting" OnRowUpdating="gdvPromocao_RowUpdating" OnSelectedIndexChanging="gdvPromocao_SelectedIndexChanging">
                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                <AlternatingRowStyle BackColor="White" />
                <Columns>
                    <asp:TemplateField HeaderText="Editar" ShowHeader="False" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="50px">
                        <ItemTemplate>
                            <asp:ImageButton runat="server" ID="imgEditar" CommandName="Update" BorderStyle="None" CausesValidation="false" ImageUrl="../images/icones/editar.png" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Excluir" ShowHeader="False" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="50px">
                        <ItemTemplate>
                            <asp:ImageButton runat="server" ID="imgExcluir" BorderStyle="None" CommandName="Delete" CausesValidation="false" ImageUrl="../images/icones/excluir.png" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Homologar" ShowHeader="False" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="50px">
                        <ItemTemplate>
                            <asp:ImageButton runat="server" ID="imgHomologar" CommandName="Select" CssClass="img-responsive" BorderStyle="None" CausesValidation="false" ImageUrl="~/Images/icones/enviar.png" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="Lojista" HeaderText="Lojista" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" HeaderStyle-Width="200px" />
                    <asp:BoundField DataField="Produto" HeaderText="Produto" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                    <asp:BoundField DataField="DataInicio" HeaderText="Data Início" DataFormatString="{0: dd/MM/yyyy}" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="100px" />
                    <asp:BoundField DataField="DataFim" HeaderText="Data Fim" DataFormatString="{0: dd/MM/yyyy}" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="100px" />
                    <asp:BoundField DataField="ValorAntigo" HeaderText="Valor Antigo" HeaderStyle-HorizontalAlign="Center" DataFormatString="{0:c}" ItemStyle-HorizontalAlign="Right" HeaderStyle-Width="120px" />
                    <asp:BoundField DataField="ValorAtual" HeaderText="Valor Atual" HeaderStyle-HorizontalAlign="Center" DataFormatString="{0:c}" ItemStyle-HorizontalAlign="Right" HeaderStyle-Width="120px" />
                </Columns>
                <RowStyle BackColor="#EFF3FB" />
                <EditRowStyle BackColor="#2461BF" />
            </asp:GridView>
        </fieldset>
    </fieldset>
    <script type="text/javascript">
        SetScrollerPosition();
    </script>
</asp:Content>