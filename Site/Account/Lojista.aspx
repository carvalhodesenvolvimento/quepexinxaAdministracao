<%@ Page Language="C#" MaintainScrollPositionOnPostback="true" MasterPageFile="~/Account/Administracao.master" AutoEventWireup="true" CodeFile="Lojista.aspx.cs" Inherits="Account_Lojista" Title=".:: Que Pexinxa - Lojista ::." %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ MasterType TypeName="Account_Administracao" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <script type="text/javascript">
        function Validar(nomeID, enderecoID, dddID, telefoneID) {
            try {
                if (ValorCampo(nomeID) == "")
                    return ExibirMensagem(nomeID, 'ContentPlaceHolder1_lblMensagem', "É necessário preencher o campo Nome!");
                else
                    if (ValorCampo(enderecoID) == "")
                        return ExibirMensagem(enderecoID, 'ContentPlaceHolder1_lblMensagem', "É necessário preencher o campo Endereço!");
                    else
                        if (ValorCampo(dddID) == "")
                            return ExibirMensagem(dddID, 'ContentPlaceHolder1_lblMensagem', "É necessário preencher o campo DDD!");
                        else
                            if (ValorCampo(telefoneID) == "")
                                return ExibirMensagem(telefoneID, 'ContentPlaceHolder1_lblMensagem', "É necessário preencher o campo Telefone!");
                return true;
            }
            catch (_erro) {
                document.getElementById("ContentPlaceHolder1_lblMensagem").innerHTML = 'Ocorreu um erro! ' + _erro;
                return false;
            }
            finally {
                setTimeout(function () { document.getElementById("ContentPlaceHolder1_lblMensagem").innerHTML = ''; }, 5000);
            }
        }

        function ValidarCategoria(categoriaID) {
            try {
                if (ValorCampo(categoriaID) == "")
                    return ExibirMensagem(categoriaID, 'ContentPlaceHolder1_lblCategoria', "É necessário selecionar a Categoria!");
                return true;
            }
            catch (_erro) {
                document.getElementById("ContentPlaceHolder1_lblCategoria").innerHTML = 'Ocorreu um erro! ' + _erro;
                return false;
            }
            finally {
                setTimeout(function () { document.getElementById("ContentPlaceHolder1_lblCategoria").innerHTML = ''; }, 5000);
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
        <legend>Lojista</legend>
        <div class="container-fluid form-horizontal" style="padding-top:3px;">
            <h5 style="color: red"> 
                * Campos Obrigatórios
            </h5>
            <div class="form-group">
                <label class="alert alert-info control-label col-sm-1" style="margin-left:12px;text-align:left;padding-left:2px;padding-right:0px;padding-top:6px;padding-bottom:6px;" for="nome">Nome:</label>
                <div class="col-sm-8">
                    <asp:TextBox ID="txtNome" runat="server" CssClass="form-control" MaxLength="100" AutoComplete="off" />
                </div>
                <span style="color:red; font-family:Arial;"> *</span>
            </div>
            <div class="form-group">
                <label class="alert alert-info control-label col-sm-1" style="margin-left:12px;text-align:left;padding-left:2px;padding-right:0px;padding-top:6px;padding-bottom:6px;" for="endereco">Endereço:</label>
                <div class="col-sm-8">
                    <asp:TextBox ID="txtEndereco" runat="server" CssClass="form-control" MaxLength="255" AutoComplete="off" />
                </div>
                <span style="color:red; font-family:Arial;"> *</span>
            </div>
            <div class="form-group">
                <label class="alert alert-info control-label col-sm-1" style="margin-left:12px;text-align:left;padding-left:2px;padding-right:0px;padding-top:6px;padding-bottom:6px;" for="ddd">DDD:</label>
                <div class="col-sm-1">
                    <asp:TextBox ID="txtDDD" runat="server" MaxLength="2" CssClass="form-control" ToolTip="DDD" AutoComplete="off" />
                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" Enabled="True" FilterType="Numbers" TargetControlID="txtDDD"/>
                </div>
                <span style="color:red; font-family:Arial;"> *</span>
            </div>
            <div class="form-group">
                <label class="alert alert-info control-label col-sm-1" style="margin-left:12px;text-align:left;padding-left:2px;padding-right:0px;padding-top:6px;padding-bottom:6px;" for="telefone">Telefone:</label>
                <div class="col-sm-3">
                    <asp:TextBox ID="txtTelefone" runat="server" MaxLength="9" CssClass="form-control" ToolTip="Telefone" AutoComplete="off" />
                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" Enabled="True" FilterType="Numbers" TargetControlID="txtTelefone"/>
                </div>
                <span style="color:red; font-family:Arial;"> *</span>
            </div>
            <fieldset>
                <legend>Categorias</legend>
                <h5 style="color: red"> 
                    * Campos Obrigatórios
                </h5>
                <div class="form-group">
                    <label class="alert alert-info control-label col-sm-1" style="margin-left:12px;text-align:left;padding-left:2px;padding-top:6px;padding-bottom:6px;" for="Categoria">Categoria:</label>
                    <div class="col-sm-3">
                        <asp:DropDownList ID="ddlCategoria" runat="server" CssClass="form-control" DataValueField="ValorID" DataTextField="Valor" />
                    </div>
                    <span style="color:red; font-family:Arial;"> *</span>
                    <asp:ImageButton ID="imgBtnCategoria" runat="server" BorderStyle="None" ImageAlign="AbsMiddle" ImageUrl="~/images/abreJanela.gif" ToolTip="Cadastrar Categoria" />
                </div>
                <div class="form-group">        
                    <div class="col-sm-offset-1 col-sm-10">
                        <asp:Button ID="btnAdicionarCategoria" runat="server" CssClass="btn btn-primary" Text="Adicionar" OnClick="btnAdicionarCategoria_Click"/>
                        <asp:Button ID="btnLimparCategoria" runat="server" CssClass="btn btn-primary" Text="Limpar" OnClick="btnLimparCategoria_Click" />
                    </div>
                </div>
                <div class="form-group"> 
                    <div class="alert-danger">
                        <asp:Label ID="lblCategoria" runat="server" />
                    </div>
                </div>
                <fieldset>
                    <legend>Categorias Cadastradas</legend>
                    <div class="bs-example">
                        <div class="table-responsive">
                            <asp:GridView ID="gdvCategorias" runat="server" CssClass="table table-bordered table-condensed" AllowPaging="True" CellPadding="4" ForeColor="#333333" GridLines="None" AutoGenerateColumns="False" DataKeyNames="CategoriaID" Width="100%" EmptyDataText="Não há dados para serem exibidos." OnRowDataBound="gdvCategorias_RowDataBound" OnRowDeleting="gdvCategorias_RowDeleting" >
                                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                <AlternatingRowStyle BackColor="White" />
                                <Columns>
                                    <asp:TemplateField HeaderText="Excluir" ShowHeader="False" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="50px">
                                        <ItemTemplate>
                                            <asp:ImageButton runat="server" ID="imgExcluir" BorderStyle="None" CommandName="Delete" CausesValidation="false" ImageUrl="../images/icones/excluir.png" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="Categoria" HeaderText="Categoria" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                                </Columns>
                                <RowStyle BackColor="#EFF3FB" />
                                <EditRowStyle BackColor="#2461BF" />
                            </asp:GridView>
                        </div>
                    </div>
                </fieldset>
            </fieldset><br />
            <div class="form-group">        
                <div class="col-sm-offset-1 col-sm-10">
                    <asp:Button ID="btnSalvar" runat="server" CssClass="btn btn-primary"  Text="Salvar" OnClick="btnSalvar_Click"/>
                    <asp:Button ID="btnLimpar" runat="server" CssClass="btn btn-primary" Text="Limpar" OnClick="btnLimpar_Click"/>
                    <asp:Button ID="btnVoltar" runat="server" CssClass="btn btn-primary" Text="Voltar" OnClientClick="window.location.href = 'Principal.aspx'"/>
                </div>
            </div>
            <div class="form-group"> 
                <div class="alert-danger">
                    <asp:Label ID="lblMensagem" runat="server" />
                </div>
            </div>
        </div>
        <fieldset>
            <legend>Lojistas</legend>
                <asp:GridView ID="gdvLojistas" runat="server" CssClass="table table-bordered" DataKeyNames="LojistaID" AutoGenerateColumns="False" EmptyDataText="Não há dados para serem exibidos." OnRowDataBound="gdvLojistas_RowDataBound" OnRowDeleting="gdvLojistas_RowDeleting" OnRowUpdating="gdvLojistas_RowUpdating">
                    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                    <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                    <AlternatingRowStyle BackColor="White" />
                    <Columns>
                        <asp:TemplateField HeaderText="Editar" ShowHeader="False" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="50px">
                            <ItemTemplate>
                                <asp:ImageButton runat="server" ID="imgEditar" CommandName="Update" CssClass="img-responsive" BorderStyle="None" CausesValidation="false" ImageUrl="../images/icones/editar.png" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Excluir" ShowHeader="False" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="50px">
                            <ItemTemplate>
                                <asp:ImageButton runat="server" ID="imgExcluir" CommandName="Delete" CssClass="img-responsive" BorderStyle="None" CausesValidation="false" ImageUrl="../images/icones/excluir.png" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="Nome" HeaderText="Lojista" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                        <asp:BoundField DataField="Quantidade" HeaderText="Categorias" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="100px" />
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