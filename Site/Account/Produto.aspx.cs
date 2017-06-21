using System;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using Gerbase.EncodeQueryString;
using System.IO;

public partial class Account_Produto : System.Web.UI.Page
{
    private enum TipoEstado
    {
        Novo
    }

    public int ProdutoID
    {
        get { return Convert.ToInt32(ViewState["ProdutoID"]); }
        set { ViewState["ProdutoID"] = value; }
    }

    public List<Carvalho.QuePexinxa.CamadaEntidades.Produtos> ListaProdutos
    {
        get { return (List<Carvalho.QuePexinxa.CamadaEntidades.Produtos>)ViewState["ListaProdutos"]; }
        set { ViewState["ListaProdutos"] = value; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!Utilitarios.ValidarUsuarioSessao())
                this.Response.Redirect("../Default.aspx");
            else
            {
                if (!IsPostBack)
                {
                    this.Page.Form.DefaultFocus = this.ddlCategoria.ClientID;
                    this.PopulaDropDownList();
                    this.EventosBotao();
                    this.PopulaGridView(true);

                    if (!string.IsNullOrEmpty(this.Request.QueryString["PID"]))
                    {
                        QueryString decodifica, decodificado;
                        decodifica = QueryString.FromCurrent();
                        decodificado = Encode.DecodeQueryString(decodifica, Encode.EncodeType.Hex);
                        this.ProdutoID = Convert.ToInt32(Server.HtmlDecode(decodificado["PID"]));

                        this.PopularFormulario();

                        decodifica = null;
                        decodificado = null;
                    }
                }
            }
        }
        catch (ApplicationException ex)
        {
            Utilitarios.ExibirMensagem(this.Page, this.lblMensagem.ClientID, String.Format("{0} Detalhe do Erro: {1}", ex.Message, ex.InnerException.Message));
        }
        catch (NullReferenceException)
        {
            this.Response.Redirect("../Default.aspx");
        }
    }

    protected void btnSalvar_Click(object sender, EventArgs e)
    {
        Carvalho.QuePexinxa.CamadaEntidades.Produtos produto = new Carvalho.QuePexinxa.CamadaEntidades.Produtos();
        try
        {/*
            if (this.ProdutoID.Equals(0) && this.RadAsyncUpload1.UploadedFiles.Count.Equals(0))
            {
                throw new Exception("É Necessário selecionar uma Imagem!");
            }
            else
            {*/
                produto.ProdutoID = this.ProdutoID;
                produto.LojistaID = QuePexinxa.UsuarioSessao.LojistaID;
                produto.CategoriaID = Convert.ToInt32(this.ddlCategoria.SelectedValue);
                produto.Descricao = this.txtDescricao.Text;
                /*
                if (!this.RadAsyncUpload1.UploadedFiles.Count.Equals(0))
                {
                    produto.Imagem = this.RadAsyncUpload1.UploadedFiles[0].FileName;
                    produto.Extensao = this.RadAsyncUpload1.UploadedFiles[0].GetExtension();
                }*/

                produto.UsuarioID = QuePexinxa.UsuarioSessao.UsuarioID;
                this.ProdutoID = Carvalho.QuePexinxa.CamadaAcessoDados.Produtos.Gravar(produto);
                /*
                if (this.ProdutoID > 0 && !this.RadAsyncUpload1.UploadedFiles.Count.Equals(0))
                    this.RadAsyncUpload1.UploadedFiles[0].SaveAs(Server.MapPath("Anexos/") + (this.ProdutoID + produto.Extensao), true);
                else*/
                    if (this.ProdutoID == -2)
                        throw new Exception("Produto já esta Cadastrado!");
                    else
                        if (this.ProdutoID == -1)
                            throw new Exception("Não Foi Possível alterar o Produto, pois o registro item esta associado!");

                this.PopulaGridView(true);
                this.ControlaEstado(TipoEstado.Novo);
                Utilitarios.ExibirMensagem(this.Page, this.lblMensagem.ClientID, String.Format("{0}", "Registro Gravado com Sucesso!"));
            //}
        }
        catch (ApplicationException ex)
        {
            Utilitarios.ExibirMensagem(this.Page, this.lblMensagem.ClientID, String.Format("{0} Detalhe do Erro: {1}", ex.Message, ex.InnerException.Message));
        }
        catch (Exception ex)
        {
            Utilitarios.ExibirMensagem(this.Page, this.lblMensagem.ClientID, String.Format("Detalhe do Erro: {0}", ex.Message));
        }
    }

    protected void btnLimpar_Click(object sender, EventArgs e)
    {
        try
        {
            this.ControlaEstado(TipoEstado.Novo);
        }
        catch (ApplicationException ex)
        {
            Utilitarios.ExibirMensagem(this.Page, this.lblMensagem.ClientID, String.Format("{0} Detalhe do Erro: {1}", ex.Message, ex.InnerException.Message));
        }
    }

    protected void gdvProdutos_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            this.gdvProdutos.PageIndex = e.NewPageIndex;
            this.PopulaGridView(false);
        }
        catch (ApplicationException ex)
        {
            Utilitarios.ExibirMensagem(this.Page, this.lblMensagem.ClientID, String.Format("{0}: {1}", ex.Message, ex.InnerException.Message));
        }
    }

    protected void gdvProdutos_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            Carvalho.QuePexinxa.CamadaAcessoDados.Produtos.Excluir(Convert.ToInt32(this.gdvProdutos.DataKeys[e.RowIndex].Values[0]));
            new FileInfo(Server.MapPath("Anexos/" + this.gdvProdutos.DataKeys[e.RowIndex].Values[1])).Delete();
            this.PopulaGridView(true);
            this.ControlaEstado(TipoEstado.Novo);
            Utilitarios.ExibirMensagem(this.Page, this.lblMensagem.ClientID, String.Format("{0}", "Registro Excluído com Sucesso!"));
        }
        catch (ApplicationException ex)
        {
            Utilitarios.ExibirMensagem(this.Page, this.lblMensagem.ClientID, String.Format("{0}: {1}", ex.Message, ex.InnerException.Message));
        }
    }

    protected void gdvProdutos_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        try
        {
            this.ProdutoID = Convert.ToInt32(this.gdvProdutos.DataKeys[e.RowIndex].Values[0]);
            this.PopularFormulario();
        }
        catch (ApplicationException ex)
        {
            Utilitarios.ExibirMensagem(this.Page, this.lblMensagem.ClientID, String.Format("{0}: {1}", ex.Message, ex.InnerException.Message));
        }
    }

    protected void gdvProdutos_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes.Add("onMouseOver", "mouseOver(this,true)");
                e.Row.Attributes.Add("onMouseOut", "mouseOut(this)");
                ((ImageButton)e.Row.FindControl("imgExcluir")).Attributes.Add("onclick", "javascript:return confirm('Confirma a exclusão do Item?')");

                ((ImageButton)e.Row.FindControl("imgImagem")).ImageUrl = "Anexos/" + this.gdvProdutos.DataKeys[e.Row.RowIndex].Values[1];
            }
        }
        catch (ApplicationException ex)
        {
            Utilitarios.ExibirMensagem(this.Page, this.lblMensagem.ClientID, String.Format("{0}: {1}", ex.Message, ex.InnerException.Message));
        }
    }

    private void PopularFormulario()
    {
        Carvalho.QuePexinxa.CamadaEntidades.Produtos produto = new Carvalho.QuePexinxa.CamadaEntidades.Produtos();
        produto.ProdutoID = this.ProdutoID;
        produto = Carvalho.QuePexinxa.CamadaAcessoDados.Produtos.Selecionar(produto);
        this.ProdutoID = produto.ProdutoID;
        this.ddlCategoria.SelectedValue = produto.CategoriaID.ToString();
        this.txtDescricao.Text = produto.Descricao;
    }

    private void PopulaDropDownList()
    {
        Carvalho.QuePexinxa.CamadaEntidades.PaginaPadrao paginaPadrao = new Carvalho.QuePexinxa.CamadaEntidades.PaginaPadrao();
        paginaPadrao.Procedure = "[dbo].[ListarCategoriaPorLojista]";
        paginaPadrao.ForeignKey = QuePexinxa.UsuarioSessao.LojistaID.ToString();
        Utilitarios.PopulaDropDownList(this.ddlCategoria, Carvalho.QuePexinxa.CamadaAcessoDados.PaginaPadrao.Listar(paginaPadrao, QuePexinxa.UsuarioSessao.Conexao));
    }

    private void EventosBotao()
    {
        this.btnSalvar.Attributes.Add("onClick", "return Validar('" + this.ddlCategoria.ClientID + "','" + this.txtDescricao.ClientID + "','" + this.lblMensagem.ClientID + "');GetScrollerPosition();");
    }

    private void PopulaGridView(bool _reload)
    {
        if (_reload)
            this.ListaProdutos = Carvalho.QuePexinxa.CamadaAcessoDados.Produtos.Listar(QuePexinxa.UsuarioSessao.LojistaID);

        this.gdvProdutos.DataSource = this.ListaProdutos;
        this.gdvProdutos.DataBind();
    }

    private void ControlaEstado(TipoEstado estado)
    {
        switch (estado)
        {
            case TipoEstado.Novo:
                this.ProdutoID = 0;
                this.ddlCategoria.ClearSelection();
                this.txtDescricao.Text = string.Empty;
                break;
        }
    }
}