<%@ Page Title="" Language="C#" MasterPageFile="~/Account/Administracao.master" AutoEventWireup="true" CodeFile="PaginaPadrao.aspx.cs" Inherits="Account_PaginaPadrao" %>
<%@ MasterType TypeName="Account_Administracao" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <link href="../Content/suporte.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" href="../Content/padrao.css" type="text/css" />
    <script type="text/javascript" src="../Scripts/Funcoes.js"></script>
    <fieldset>
        <legend>
            <asp:Label ID="lblTituloLegendaFieldSet" runat="server"/>
        </legend>
        <div class="container-fluid form-horizontal" style="padding-top:3px;">
            <h5 style="color: red"> 
                * Campos Obrigatórios
            </h5>
            <div class="form-group">
                <label class="alert alert-info control-label col-sm-1" style="margin-left:12px;text-align:left;padding-left:2px;padding-top:6px;padding-bottom:6px;" for="matricula"><asp:Label ID="lblNomeCampo" runat="server"/></label>
                <div class="col-sm-2">
                    <asp:TextBox ID="txtCampo" runat="server" class="form-control" MaxLength="50" placeholder="Nome" autocomplete="off"/>
                </div>
            </div>
            <div class="form-group">        
                <div class="col-sm-offset-1 col-sm-10">
                    <asp:Button ID="btnSalvar" runat="server" Text="Salvar" CssClass="btn btn-primary" OnClick="btnSalvar_Click"/><br />
                </div>
            </div>
            <div class="form-group"> 
                <div class="alert-danger">
                    <asp:Label ID="lblMensagem" runat="server" />
                </div>
            </div>
            <div class="form-group">
                <asp:GridView ID="gdvLista" runat="server" AllowPaging="True" CellPadding="4" ForeColor="#333333" GridLines="None" AutoGenerateColumns="False" DataKeyNames="ValorID" Width="100%" EmptyDataText="Não há dados para serem exibidos." OnPageIndexChanging="gdvLista_PageIndexChanging" OnRowDataBound="gdvLista_RowDataBound" OnRowDeleting="gdvLista_RowDeleting" PageSize="5" OnRowUpdating="gdvLista_RowUpdating">
                    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                    <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                    <AlternatingRowStyle BackColor="White" />
                    <Columns>
                        <asp:TemplateField HeaderText="Editar" ShowHeader="False" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="50px">
                            <ItemTemplate>
                                <asp:ImageButton runat="server" ID="imgEditar" BorderStyle="None" CommandName="Update" CausesValidation="false" ImageUrl="../images/icones/editar.png" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Excluir" ShowHeader="False" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="50px">
                            <ItemTemplate>
                                <asp:ImageButton runat="server" ID="imgExcluir" BorderStyle="None" CommandName="Delete" CausesValidation="false" ImageUrl="../images/icones/excluir.png" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="Valor" HeaderText="" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                    </Columns>
                    <RowStyle BackColor="#EFF3FB" />
                    <EditRowStyle BackColor="#2461BF" />
                </asp:GridView>

            </div>
        </div>
    </fieldset>
</asp:Content>