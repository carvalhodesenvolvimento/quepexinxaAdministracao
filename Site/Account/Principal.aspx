<%@ Page Language="C#" MasterPageFile="~/Account/Administracao.master" AutoEventWireup="true" CodeFile="Principal.aspx.cs" Inherits="Account_Principal" Title=".:: Que Pexinxa - Principal ::." %>
<%@ MasterType TypeName="Account_Administracao" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <style type="text/css">
        .bs-example{
    	    margin: 20px;
        }
    </style>
    <script type="text/javascript">
        function Redirect(_parametros) {
            window.location.href = "Lojista.aspx" + _parametros;
        }
    </script>
    <br />
    <fieldset>
        <legend>
            Promoções
        </legend>
        <div class="form-group"> 
            <div class="alert-danger">
                <asp:Label ID="lblMensagem" runat="server" />
            </div>
        </div>
        <div class="bs-example">
            <div class="table-responsive">
                <asp:GridView ID="gdvPromocao" runat="server" CssClass="table table-bordered" AllowPaging="True" CellPadding="4" ForeColor="#333333" GridLines="None" AutoGenerateColumns="False" DataKeyNames="PromocaoID" Width="100%" EmptyDataText="Não há dados para serem exibidos." OnPageIndexChanging="gdvPromocao_PageIndexChanging" OnRowDataBound="gdvPromocao_RowDataBound">
                    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                    <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                    <AlternatingRowStyle BackColor="White" />
                    <Columns>
                        <asp:TemplateField HeaderText="Renovar" ShowHeader="False" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="50px">
                            <ItemTemplate>
                                <asp:ImageButton runat="server" ID="imgRenovar" CssClass="img-responsive" BorderStyle="None" CausesValidation="false" ImageUrl="~/Images/icones/atualizar.JPG" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="Lojista" HeaderText="Lojista" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" HeaderStyle-Width="200px" />
                        <asp:BoundField DataField="Produto" HeaderText="Produto" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                        <asp:BoundField DataField="DataInicio" HeaderText="Data Início" DataFormatString="{0: dd/MM/yyyy}" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="100px" />
                        <asp:BoundField DataField="DataFim" HeaderText="Data Fim" DataFormatString="{0: dd/MM/yyyy}" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="100px" />
                        <asp:BoundField DataField="ValorAntigo" HeaderText="Valor Antigo" HeaderStyle-HorizontalAlign="Center" DataFormatString="{0:c}" ItemStyle-HorizontalAlign="Right" HeaderStyle-Width="120px" />
                        <asp:BoundField DataField="ValorAtual" HeaderText="Valor Atual" HeaderStyle-HorizontalAlign="Center" DataFormatString="{0:c}" ItemStyle-HorizontalAlign="Right" HeaderStyle-Width="120px" />
                        <asp:BoundField DataField="Ativo" HeaderText="Ativo" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="60px" />
                    </Columns>
                    <RowStyle BackColor="#EFF3FB" />
                    <EditRowStyle BackColor="#2461BF" />
                </asp:GridView>
            </div>
        </div>
    </fieldset>
</asp:Content>