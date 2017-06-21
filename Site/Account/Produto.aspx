<%@ Page Language="C#" MaintainScrollPositionOnPostback="true" MasterPageFile="~/Account/Administracao.master" AutoEventWireup="true" CodeFile="Produto.aspx.cs" Inherits="Account_Produto" Title=".:: Que Pexinxa - Produto ::." %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ MasterType TypeName="Account_Administracao" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <script type="text/javascript">
        function Validar(categoriaID, descricaoID, mensagemID) {
            try {
                if (ValorCampo(categoriaID) == "")
                    return ExibirMensagem(categoriaID, mensagemID, "É necessário selecionar a Categoria!");
                else
                    if (ValorCampo(descricaoID) == "")
                        return ExibirMensagem(descricaoID, mensagemID, "É necessário preencher o campo Descrição!");
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
                <label class="alert alert-info control-label col-sm-1" style="margin-left:12px;text-align:left;padding-left:2px;padding-right:0px;padding-top:6px;padding-bottom:6px;" for="nome">Categoria:</label>
                <div class="col-sm-8">
                    <asp:DropDownList ID="ddlCategoria" runat="server" CssClass="form-control" DataValueField="ValorID" DataTextField="Valor" />
                </div>
                <span style="color:red; font-family:Arial;"> *</span>
            </div>
            <div class="form-group">
                <label class="alert alert-info control-label col-sm-1" style="margin-left:12px;text-align:left;padding-left:2px;padding-right:0px;padding-top:6px;padding-bottom:6px;" for="endereco">Descrição:</label>
                <div class="col-sm-8">
                    <asp:TextBox ID="txtDescricao" runat="server" CssClass="form-control" MaxLength="255" AutoComplete="off" />
                </div>
                <span style="color:red; font-family:Arial;"> *</span>
            </div>
            <div class="form-group">
                <div class="qsf-demo-canvas qsf-demo-canvas-overview">
                    Selecione a Foto para Carregar                             <asp:FileUpload ID="FileUpload1" runat="server" />
                </div>
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
            <legend>Produtos</legend>
            <asp:GridView ID="gdvProdutos" runat="server" CssClass="table table-bordered" AllowPaging="True" CellPadding="4" ForeColor="#333333" GridLines="None" AutoGenerateColumns="False" DataKeyNames="ProdutoID, Imagem" Width="100%" EmptyDataText="Não há dados para serem exibidos." OnPageIndexChanging="gdvProdutos_PageIndexChanging" OnRowDataBound="gdvProdutos_RowDataBound" OnRowDeleting="gdvProdutos_RowDeleting" OnRowUpdating="gdvProdutos_RowUpdating">
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
                    <asp:BoundField DataField="Categoria" HeaderText="Categoria" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" HeaderStyle-Width="300px" />
                    <asp:BoundField DataField="Descricao" HeaderText="Descrição" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                    <asp:TemplateField HeaderText="Imagem" ShowHeader="False" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="70px">
                        <ItemTemplate>
                            <asp:ImageButton runat="server" ID="imgImagem" BorderStyle="None" CausesValidation="false" Width="25" Height="25" HeaderStyle-Width="100px" />
                        </ItemTemplate>
                    </asp:TemplateField>
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