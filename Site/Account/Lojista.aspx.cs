using System;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using Gerbase.EncodeQueryString;
using System.Web.UI;

public partial class Account_Lojista : System.Web.UI.Page
{
    private enum TipoEstado
    {
        Novo,
        Categoria
    }

    public int LojistaID
    {
        get { return Convert.ToInt32(ViewState["LojistaID"]); }
        set { ViewState["LojistaID"] = value; }
    }

    public List<Carvalho.QuePexinxa.CamadaEntidades.Lojistas> ListaLojista
    {
        get { return (List<Carvalho.QuePexinxa.CamadaEntidades.Lojistas>)ViewState["ListaLojista"]; }
        set { ViewState["ListaLojista"] = value; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!Utilitarios.ValidarUsuarioSessao())
                this.Response.Redirect("../Default.aspx");
            else
            {
                if (!QuePexinxa.UsuarioSessao.NivelUsuarioID.Equals(1))
                {
                    this.Response.Redirect("Produto.aspx");
                }
                else
                {
                    this.RetornoPostBack();
                    if (!IsPostBack)
                    {
                        this.Page.Form.DefaultFocus = this.txtNome.ClientID;
                        this.PopulaDropDownList();
                        this.EventosBotao();
                        this.PopulaGridView(true);

                        if (!string.IsNullOrEmpty(this.Request.QueryString["LID"]))
                        {
                            QueryString decodifica, decodificado;
                            decodifica = QueryString.FromCurrent();
                            decodificado = Encode.DecodeQueryString(decodifica, Encode.EncodeType.Hex);
                            this.LojistaID = Convert.ToInt32(Server.HtmlDecode(decodificado["LID"]));

                            this.PopularFormulario();

                            decodifica = null;
                            decodificado = null;
                        }
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

#region Categoria
    public List<Carvalho.QuePexinxa.CamadaEntidades.LojistaXCategoria> ListaLojistaXCategoria
    {
        get { return (List<Carvalho.QuePexinxa.CamadaEntidades.LojistaXCategoria>)ViewState["ListaLojistaXCategoria"]; }
        set { ViewState["ListaLojistaXCategoria"] = value; }
    }

    public bool ListaLojistaXCategoriaModificada
    {
        get { return Convert.ToBoolean(ViewState["LCM"]); }
        set { ViewState["LCM"] = value; }
    }

    protected void btnAdicionarCategoria_Click(object sender, EventArgs e)
    {
        Carvalho.QuePexinxa.CamadaEntidades.LojistaXCategoria logista = new Carvalho.QuePexinxa.CamadaEntidades.LojistaXCategoria();
        List<Carvalho.QuePexinxa.CamadaEntidades.LojistaXCategoria> listalojista = new List<Carvalho.QuePexinxa.CamadaEntidades.LojistaXCategoria>();
        try
        {
            //add item a coleção
            if (this.ListaLojistaXCategoria != null && !this.ListaLojistaXCategoria.Count.Equals(0))
            {
                for (int i = 0; i < this.ListaLojistaXCategoria.Count; i++)
                {
                    if (this.ListaLojistaXCategoria[i].CategoriaID.Equals(Convert.ToInt32(this.ddlCategoria.SelectedValue)))
                    {
                        throw new Exception("Categoria já foi Inserido");
                    }
                }

                listalojista = this.ListaLojistaXCategoria;
            }

            logista.CategoriaID = Convert.ToInt32(this.ddlCategoria.SelectedValue);
            logista.Categoria = this.ddlCategoria.SelectedItem.Text;

            listalojista.Add(logista);
            this.ListaLojistaXCategoria = listalojista;

            this.ListaLojistaXCategoriaModificada = true;
            this.PopulaGridViewCategorias();
            this.ControlaEstado(TipoEstado.Categoria);
            Utilitarios.ExibirMensagem(this.Page, this.lblCategoria.ClientID, "Material Inserido com Sucesso!!!");
        }
        catch (ApplicationException ex)
        {
            Utilitarios.ExibirMensagem(this.Page, this.lblCategoria.ClientID, String.Format("{0} Detalhe do Erro: {1}", ex.Message, ex.InnerException.Message));
        }
        catch (Exception ex)
        {
            Utilitarios.ExibirMensagem(this.Page, this.lblCategoria.ClientID, String.Format("Detalhe do Erro: {0}", ex.Message));
        }
    }

    protected void btnLimparCategoria_Click(object sender, EventArgs e)
    {
        try
        {
            this.ControlaEstado(TipoEstado.Categoria);
        }
        catch (ApplicationException ex)
        {
            Utilitarios.ExibirMensagem(this.Page, this.lblCategoria.ClientID, String.Format("{0} Detalhe do Erro: {1}", ex.Message, ex.InnerException.Message));
        }
    }

    protected void gdvCategorias_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            for (int i = 0; i < this.ListaLojistaXCategoria.Count; i++)
            {
                if (this.ListaLojistaXCategoria[i].CategoriaID.Equals(Convert.ToInt32(this.gdvCategorias.DataKeys[e.RowIndex].Value)))
                {
                    this.ListaLojistaXCategoria.RemoveAt(i);
                    break;
                }
            }

            this.ListaLojistaXCategoriaModificada = true;
            this.PopulaGridViewCategorias();
            this.ControlaEstado(TipoEstado.Categoria);
            Utilitarios.ExibirMensagem(this.Page, this.lblCategoria.ClientID, "Categoria Excluída com Sucesso!!!");
        }
        catch (ApplicationException ex)
        {
            Utilitarios.ExibirMensagem(this.Page, this.lblCategoria.ClientID, String.Format("{0} Detalhe do Erro: {1}", ex.Message, ex.InnerException.Message));
        }
    }

    protected void gdvCategorias_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                ((ImageButton)e.Row.FindControl("imgExcluir")).Attributes.Add("onclick", "javascript:return confirm('Confirma a exclusão do Item?')");
                e.Row.Attributes.Add("onMouseOver", "mouseOver(this,true)");
                e.Row.Attributes.Add("onMouseOut", "mouseOut(this)");
            }
        }
        catch (ApplicationException ex)
        {
            Utilitarios.ExibirMensagem(this.Page, this.lblCategoria.ClientID, String.Format("{0} Detalhe do Erro: {1}", ex.Message, ex.InnerException.Message));
        }
    }

    private void PopulaGridViewCategorias()
    {
        this.gdvCategorias.DataSource = this.ListaLojistaXCategoria;
        this.gdvCategorias.DataBind();
    }
#endregion

    protected void btnSalvar_Click(object sender, EventArgs e)
    {
        Carvalho.QuePexinxa.CamadaEntidades.Lojistas release = new Carvalho.QuePexinxa.CamadaEntidades.Lojistas();
        try
        {
            if (this.ListaLojistaXCategoria == null || this.ListaLojistaXCategoria.Count.Equals(0))
                throw new Exception("É Necessário Inserir a(s) Categoria(s)!");
            else {
                release.LojistaID = this.LojistaID;
                release.Nome = this.txtNome.Text;
                release.Endereco = this.txtEndereco.Text;
                release.DDD = this.txtDDD.Text;
                release.Telefone = this.txtTelefone.Text;
                release.ListaLojistaXCategoria = this.ListaLojistaXCategoria;
                Carvalho.QuePexinxa.CamadaAcessoDados.Lojistas.Gravar(release, this.ListaLojistaXCategoriaModificada);
                this.PopulaGridView(true);
                this.ControlaEstado(TipoEstado.Novo);
                Utilitarios.ExibirMensagem(this.Page, this.lblMensagem.ClientID, String.Format("{0}", "Registro Gravado com Sucesso!"));
            }
        }
        catch (ApplicationException ex)
        {
            Utilitarios.ExibirMensagem(this.Page, this.lblMensagem.ClientID, String.Format("{0} Detalhe do Erro: {1}", ex.Message, "Existem Registros associados a este Lojista!"));
        }
        catch (Exception ex)
        {
            Utilitarios.ExibirMensagem(this.Page, this.lblMensagem.ClientID, String.Format("Detalhe do Erro: {0}", ex.Message));
        }
        finally
        {
            release = null;
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

    protected void gdvLojistas_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            this.gdvLojistas.PageIndex = e.NewPageIndex;
            this.PopulaGridView(false);
        }
        catch (ApplicationException ex)
        {
            Utilitarios.ExibirMensagem(this.Page, this.lblMensagem.ClientID, String.Format("{0}: {1}", ex.Message, ex.InnerException.Message));
        }
    }

    protected void gdvLojistas_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            Carvalho.QuePexinxa.CamadaAcessoDados.Lojistas.Excluir(Convert.ToInt32(this.gdvLojistas.DataKeys[e.RowIndex].Value), QuePexinxa.UsuarioSessao.Conexao);
            this.PopulaGridView(true);
            Utilitarios.ExibirMensagem(this.Page, this.lblMensagem.ClientID, String.Format("{0}", "Lojista Excluído com Sucesso!"));
        }
        catch (ApplicationException ex)
        {
            if (((System.Data.SqlClient.SqlException)(((System.Exception)(ex)).InnerException)).Number.Equals(547))
                Utilitarios.ExibirMensagem(this.Page, this.lblMensagem.ClientID, String.Format("{0} Detalhe do Erro: {1}", ex.Message, "Existem Promoções associadas a este Lojista"));
            else
                Utilitarios.ExibirMensagem(this.Page, this.lblMensagem.ClientID, String.Format("{0} Detalhe do Erro: {1}", ex.Message, ex.InnerException.Message));
        }
    }

    protected void gdvLojistas_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        try
        {
            this.LojistaID = Convert.ToInt32(this.gdvLojistas.DataKeys[e.RowIndex].Value);
            this.PopularFormulario();
        }
        catch (ApplicationException ex)
        {
            Utilitarios.ExibirMensagem(this.Page, this.lblMensagem.ClientID, String.Format("{0}: {1}", ex.Message, ex.InnerException.Message));
        }
    }

    protected void gdvLojistas_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes.Add("onMouseOver", "mouseOver(this,true)");
                e.Row.Attributes.Add("onMouseOut", "mouseOut(this)");
                ((ImageButton)e.Row.FindControl("imgExcluir")).Attributes.Add("onclick", "javascript:return confirm('Confirma a exclusão do Item?')");
            }
        }
        catch (ApplicationException ex)
        {
            Utilitarios.ExibirMensagem(this.Page, this.lblMensagem.ClientID, String.Format("{0}: {1}", ex.Message, ex.InnerException.Message));
        }
    }

    private void PopulaGridView(bool _reload)
    {
        if (_reload)
            this.ListaLojista = Carvalho.QuePexinxa.CamadaAcessoDados.Lojistas.Listar(QuePexinxa.UsuarioSessao.Conexao);

        this.gdvLojistas.DataSource = this.ListaLojista;
        this.gdvLojistas.DataBind();
    }

    private void PopularFormulario()
    {
        Carvalho.QuePexinxa.CamadaEntidades.Lojistas lojista = new Carvalho.QuePexinxa.CamadaEntidades.Lojistas();
        lojista.LojistaID = this.LojistaID;
        lojista = Carvalho.QuePexinxa.CamadaAcessoDados.Lojistas.Selecionar(lojista, QuePexinxa.UsuarioSessao.Conexao);
        this.txtNome.Text = lojista.Nome;
        this.txtEndereco.Text = lojista.Endereco;
        this.txtDDD.Text = lojista.DDD;
        this.txtTelefone.Text = lojista.Telefone;
        this.ListaLojistaXCategoria = lojista.ListaLojistaXCategoria;
        this.PopulaGridViewCategorias();
    }

    private void PopulaDropDownList()
    {
        Carvalho.QuePexinxa.CamadaEntidades.PaginaPadrao _paginaPadrao = new Carvalho.QuePexinxa.CamadaEntidades.PaginaPadrao();

        _paginaPadrao.Procedure = "[dbo].[ListarCategoria]";
        Utilitarios.PopulaDropDownList(this.ddlCategoria, Carvalho.QuePexinxa.CamadaAcessoDados.PaginaPadrao.Listar(_paginaPadrao, QuePexinxa.UsuarioSessao.Conexao));
    }

    private void RetornoPostBack()
    {
        if (!string.IsNullOrEmpty(this.Request.Form["__EVENTTARGET"]))
        {
            Carvalho.QuePexinxa.CamadaEntidades.PaginaPadrao _paginaPadrao = new Carvalho.QuePexinxa.CamadaEntidades.PaginaPadrao();
            switch (this.Request.Form["__EVENTTARGET"].ToString())
            {
                case "ddlCategoria":
                    _paginaPadrao.Procedure = "[dbo].[ListarCategoria]";
                    Utilitarios.PopulaDropDownList(this.ddlCategoria, Carvalho.QuePexinxa.CamadaAcessoDados.PaginaPadrao.Listar(_paginaPadrao, QuePexinxa.UsuarioSessao.Conexao));
                    break;
            }
        }
    }

    private void EventosBotao()
    {
        this.imgBtnCategoria.Attributes.Add("onClick", "abrirPopup2('PaginaPadrao.aspx" + Utilitarios.RetornaParametrosPaginaPadrao("true", "Categoria", "CategoriaID", "Categoria", "", "ddlCategoria") + "', 'PaginaPadrao', 450, 450);GetScrollerPosition();");
        this.btnAdicionarCategoria.Attributes.Add("onClick", "return ValidarCategoria('" + this.ddlCategoria.ClientID + "');GetScrollerPosition();");
        this.btnSalvar.Attributes.Add("onClick", "return Validar('" + this.txtNome.ClientID + "','" + this.txtEndereco.ClientID + "','" + this.txtDDD.ClientID + "','" + this.txtTelefone.ClientID + "');GetScrollerPosition();");
    }
    
    private void ControlaEstado(TipoEstado estado)
    {
        switch (estado)
        {
            case TipoEstado.Novo:
                //Lojista
                this.LojistaID = 0;
                foreach (Control item in this.Controls)
                {
                    //Se o contorle for um TextBox...
                    if (item is TextBox)
                    {
                        ((TextBox)(item)).Text = String.Empty;
                    }
                }

                //this.txtNome.Text = string.Empty;
                //this.txtEndereco.Text = string.Empty;
                //this.txtDDD.Text = string.Empty;
                //this.txtTelefone.Text = string.Empty;
                //Categoria
                this.ListaLojistaXCategoriaModificada = false;
                this.ListaLojistaXCategoria = null;
                this.PopulaGridViewCategorias();
                break;

            case TipoEstado.Categoria:
                this.ddlCategoria.ClearSelection();
                break;
        }
    }
}