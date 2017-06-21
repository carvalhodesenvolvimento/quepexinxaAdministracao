using System;
using System.Web.UI;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using Gerbase.EncodeQueryString;

public partial class Account_PaginaPadrao : System.Web.UI.Page
{
    private enum TipoEstado
    {
        Novo,
        Edicao
    }

    public int ValorID
    {
        get { return Convert.ToInt32(ViewState["ValorID"]); }
        set { ViewState["ValorID"] = value; }
    }

    public string CampoID
    {
        get { return ViewState["CampoID"].ToString(); }
        set { ViewState["CampoID"] = value; }
    }

    public string Campo
    {
        get { return ViewState["Campo"].ToString(); }
        set { ViewState["Campo"] = value; }
    }

    public string NomePagina
    {
        get { return ViewState["NomePagina"].ToString(); }
        set { ViewState["NomePagina"] = value; }
    }

    public string ForeignKey
    {
        get { return ViewState["ForeignKey"].ToString(); }
        set { ViewState["ForeignKey"] = value; }
    }

    public string RetornoDoPostBack
    {
        get { return ViewState["RetornoDoPostBack"].ToString(); }
        set { ViewState["RetornoDoPostBack"] = value; }
    }

    public bool Retorno
    {
        get { return Convert.ToBoolean(ViewState["Retorno"]); }
        set { ViewState["Retorno"] = value; }
    }

    public List<Carvalho.QuePexinxa.CamadaEntidades.PaginaPadrao> ListaPaginaPadrao
    {
        get { return (List<Carvalho.QuePexinxa.CamadaEntidades.PaginaPadrao>)ViewState["ListaPaginaPadrao"]; }
        set { ViewState["ListaPaginaPadrao"] = value; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        QueryString decodifica, decodificado;
        string nomeValidacaoPagina = string.Empty;

        try
        {
            if (!Utilitarios.ValidarUsuarioSessao())
                this.Response.Redirect("../Default.aspx");
            else
            {
                decodifica = QueryString.FromCurrent();
                decodificado = Encode.DecodeQueryString(decodifica, Encode.EncodeType.Hex);

                this.NomePagina = Server.HtmlDecode(decodificado["NP"]).ToString();
                this.CampoID = Server.HtmlDecode(decodificado["CID"]).ToString();
                this.Campo = Server.HtmlDecode(decodificado["C"]).ToString();
                this.ForeignKey = Server.HtmlDecode(decodificado["FK"]).ToString();
                this.RetornoDoPostBack = Server.HtmlDecode(decodificado["PB"]).ToString();
                this.Retorno = Convert.ToBoolean(Server.HtmlDecode(decodificado["R"]));

                this.Page.Title = ".:: Que Pexinxa - Cadastrar " + this.NomePagina + " ::.";
                this.lblTituloLegendaFieldSet.Text = "Cadastrar " + this.NomePagina;
                this.lblNomeCampo.Text = this.NomePagina + ": ";
                this.Page.Form.DefaultFocus = this.txtCampo.ClientID;
                this.Page.Form.DefaultButton = this.btnSalvar.UniqueID;

                this.Master.Cabecalho = false;
                this.Master.Menu = false;

                if (!IsPostBack)
                {
                    this.EventosBotao(this.NomePagina);
                    this.PopulaGridView("[dbo].[Listar" + this.Campo + "]", true);
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
        finally
        {
            decodifica = null;
            decodificado = null;
        }
    }

    protected void btnSalvar_Click(object sender, EventArgs e)
    {
        Carvalho.QuePexinxa.CamadaEntidades.PaginaPadrao _paginaPadrao = new Carvalho.QuePexinxa.CamadaEntidades.PaginaPadrao();
        int _retorno;
        try
        {
            _paginaPadrao.ValorID = this.ValorID;
            _paginaPadrao.Valor = this.txtCampo.Text.Trim();
            _paginaPadrao.CampoID = this.CampoID;
            _paginaPadrao.Campo = this.Campo;
            _paginaPadrao.ForeignKey = this.ForeignKey;
            _paginaPadrao.Procedure = "[dbo].[Gravar" + this.Campo + "]";
            _retorno = Carvalho.QuePexinxa.CamadaAcessoDados.PaginaPadrao.Gravar(_paginaPadrao, QuePexinxa.UsuarioSessao.Conexao);

            if (_retorno.Equals(0))
            {
                this.PopulaGridView("[dbo].[Listar" + this.Campo + "]", true);
                this.ControlaEstado(TipoEstado.Novo);
                Utilitarios.ExibirMensagem(this.Page, this.lblMensagem.ClientID, String.Format("{0}", "Registro Inserido com Sucesso!"));
                this.DoPostBack();
            }
            else
                if (_retorno.Equals(2))
                    throw new Exception(this.txtCampo.Text + " já esta Cadastrado!");
                else
                    throw new Exception("Não Foi Possível alterar, pois o registro item esta associado!");
        }
        catch (ApplicationException ex)
        {
            Utilitarios.ExibirMensagem(this.Page, this.lblMensagem.ClientID, String.Format("{0} Detalhe do Erro: {1}", ex.Message, ex.InnerException.Message));
        }
        catch (Exception ex)
        {
            Utilitarios.ExibirMensagem(this.Page, this.lblMensagem.ClientID, String.Format("Ocorreu um erro: {0}", ex.Message));
        }
        finally
        {
            _paginaPadrao = null;
        }
    }

    protected void gdvLista_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            this.gdvLista.PageIndex = e.NewPageIndex;
            this.PopulaGridView("[dbo].[Listar" + this.Campo + "]", false);
        }
        catch (ApplicationException ex)
        {
            Utilitarios.ExibirMensagem(this.Page, this.lblMensagem.ClientID, String.Format("{0} Detalhe do Erro: {1}", ex.Message, ex.InnerException.Message));
        }
    }

    protected void gdvLista_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        Carvalho.QuePexinxa.CamadaEntidades.PaginaPadrao paginaPadrao = new Carvalho.QuePexinxa.CamadaEntidades.PaginaPadrao();
        try
        {
            paginaPadrao.ValorID = Convert.ToInt32(this.gdvLista.DataKeys[e.RowIndex].Value);
            paginaPadrao.CampoID = this.CampoID;
            paginaPadrao.Procedure = "[dbo].[Excluir" + this.Campo + "]";
            Carvalho.QuePexinxa.CamadaAcessoDados.PaginaPadrao.Excluir(paginaPadrao, QuePexinxa.UsuarioSessao.Conexao);
            this.PopulaGridView("[dbo].[Listar" + this.Campo + "]", true);
            this.ControlaEstado(TipoEstado.Novo);
            Utilitarios.ExibirMensagem(this.Page, this.lblMensagem.ClientID, String.Format("{0}", "Registro Excluído com Sucesso!"));
            this.DoPostBack();
        }
        catch (ApplicationException ex)
        {
            Utilitarios.ExibirMensagem(this.Page, this.lblMensagem.ClientID, String.Format("{0} Detalhe do Erro: {1}", ex.Message, ex.InnerException.Message));
        }
        finally
        {
            paginaPadrao = null;
        }
    }

    protected void gdvLista_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        Carvalho.QuePexinxa.CamadaEntidades.PaginaPadrao paginaPadrao = new Carvalho.QuePexinxa.CamadaEntidades.PaginaPadrao();
        try
        {
            paginaPadrao.ValorID = Convert.ToInt32(this.gdvLista.DataKeys[e.RowIndex].Value);
            paginaPadrao.CampoID = this.CampoID;
            paginaPadrao.Procedure = "[dbo].[Selecionar" + this.Campo + "]";
            paginaPadrao = Carvalho.QuePexinxa.CamadaAcessoDados.PaginaPadrao.Selecionar(paginaPadrao, QuePexinxa.UsuarioSessao.Conexao);

            this.ValorID = Convert.ToInt32(paginaPadrao.ValorID);
            this.txtCampo.Text = paginaPadrao.Valor;
            this.ControlaEstado(TipoEstado.Edicao);
        }
        catch (ApplicationException ex)
        {
            Utilitarios.ExibirMensagem(this.Page, this.lblMensagem.ClientID, String.Format("{0} Detalhe do Erro: {1}", ex.Message, ex.InnerException.Message));
        }
        finally
        {
            paginaPadrao = null;
        }
    }

    protected void gdvLista_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType.Equals(DataControlRowType.Header))
                e.Row.Cells[2].Text = this.NomePagina;
            else
                if (e.Row.RowType.Equals(DataControlRowType.DataRow))
                {
                    ((ImageButton)e.Row.FindControl("imgExcluir")).Attributes.Add("onclick", "javascript:return confirm('Confirma a exclusão do Item?')");
                    e.Row.Attributes.Add("onMouseOver", "mouseOver(this,true)");
                    e.Row.Attributes.Add("onMouseOut", "mouseOut(this)");
                }
        }
        catch (ApplicationException ex)
        {
            Utilitarios.ExibirMensagem(this.Page, this.lblMensagem.ClientID, String.Format("{0} Detalhe do Erro: {1}", ex.Message, ex.InnerException.Message));
        }
    }

    private void PopulaGridView(string _procedure, bool _reload)
    {
        if (_reload)
        {
            Carvalho.QuePexinxa.CamadaEntidades.PaginaPadrao paginaPadrao = new Carvalho.QuePexinxa.CamadaEntidades.PaginaPadrao();
            paginaPadrao.CampoID = this.CampoID;
            paginaPadrao.Campo = this.Campo;
            paginaPadrao.ForeignKey = this.ForeignKey;
            paginaPadrao.Procedure = _procedure;
            this.ListaPaginaPadrao = Carvalho.QuePexinxa.CamadaAcessoDados.PaginaPadrao.Listar(paginaPadrao, QuePexinxa.UsuarioSessao.Conexao);
        }

        this.gdvLista.DataSource = this.ListaPaginaPadrao;
        this.gdvLista.DataBind();
    }

    private void DoPostBack()
    {
        if (this.Retorno)
            ScriptManager.RegisterStartupScript(this.Page, typeof(Page), "PaginaPadrao", "opener.__doPostBack('" + this.RetornoDoPostBack + "',''); setTimeout(function () { window.close(); }, 500);", true);
    }

    private void EventosBotao(string nomeCampo)
    {
        this.btnSalvar.Attributes.Add("onClick", "return ValidarFormulario('" + this.txtCampo.ClientID + "','" + this.lblMensagem.ClientID + "','" + nomeCampo + "');");
    }

    private void ControlaEstado(TipoEstado estado)
    {
        switch (estado)
        {
            case TipoEstado.Novo:
                this.ValorID = 0;
                this.txtCampo.Text = string.Empty;
                this.btnSalvar.Text = "Salvar";
                break;

            case TipoEstado.Edicao:
                this.btnSalvar.Text = "Atualizar";
                break;
        }
    }
}