using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Generic;

public partial class Account_Promocao : System.Web.UI.Page
{
    private enum TipoEstado
    {
        Novo
    }

    public int PromocaoID
    {
        get { return Convert.ToInt32(ViewState["PromocaoID"]); }
        set { ViewState["PromocaoID"] = value; }
    }

    public List<Carvalho.QuePexinxa.CamadaEntidades.Promocao> ListaPromocao
    {
        get { return (List<Carvalho.QuePexinxa.CamadaEntidades.Promocao>)ViewState["ListaPromocao"]; }
        set { ViewState["ListaPromocao"] = value; }
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
                    this.Page.Form.DefaultFocus = this.ddlProduto.ClientID;
                    this.PopulaDropDownList();
                    this.EventosBotao();
                    this.PopulaGridView(true);
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
        Carvalho.QuePexinxa.CamadaEntidades.Promocao promocao = new Carvalho.QuePexinxa.CamadaEntidades.Promocao();
        try
        {
            promocao.PromocaoID = this.PromocaoID;
            promocao.ProdutoID = Convert.ToInt32(this.ddlProduto.SelectedValue);
            promocao.DataInicio = Convert.ToDateTime(this.txtDataInicio.Text);
            promocao.DataFim = Convert.ToDateTime(this.txtDataFim.Text);
            promocao.ValorAntigo = Convert.ToDouble(this.txtValorAntigo.Text);
            promocao.ValorAtual = Convert.ToDouble(this.txtValorAtual.Text);
            promocao.UsuarioID = QuePexinxa.UsuarioSessao.UsuarioID;
            Carvalho.QuePexinxa.CamadaAcessoDados.Promocao.Gravar(promocao);
            this.PopulaGridView(true);
            this.ControlaEstado(TipoEstado.Novo);
            Utilitarios.ExibirMensagem(this.Page, this.lblMensagem.ClientID, String.Format("{0}", "Registro Gravado com Sucesso!"));
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

    protected void gdvPromocao_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            this.gdvPromocao.PageIndex = e.NewPageIndex;
            this.PopulaGridView(false);
        }
        catch (ApplicationException ex)
        {
            Utilitarios.ExibirMensagem(this.Page, this.lblMensagem.ClientID, String.Format("{0}: {1}", ex.Message, ex.InnerException.Message));
        }
    }

    protected void gdvPromocao_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            Carvalho.QuePexinxa.CamadaAcessoDados.Promocao.Excluir(Convert.ToInt32(this.gdvPromocao.DataKeys[e.RowIndex].Value));
            this.PopulaGridView(true);
            this.ControlaEstado(TipoEstado.Novo);
            Utilitarios.ExibirMensagem(this.Page, this.lblMensagem.ClientID, String.Format("{0}", "Registro Excluído com Sucesso!"));
        }
        catch (ApplicationException ex)
        {
            Utilitarios.ExibirMensagem(this.Page, this.lblMensagem.ClientID, String.Format("{0}: {1}", ex.Message, ex.InnerException.Message));
        }
    }

    protected void gdvPromocao_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        try
        {
            this.PromocaoID = Convert.ToInt32(this.gdvPromocao.DataKeys[e.RowIndex].Value);
            this.PopularFormulario();
        }
        catch (ApplicationException ex)
        {
            Utilitarios.ExibirMensagem(this.Page, this.lblMensagem.ClientID, String.Format("{0}: {1}", ex.Message, ex.InnerException.Message));
        }
    }

    protected void gdvPromocao_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes.Add("onMouseOver", "mouseOver(this,true)");
                e.Row.Attributes.Add("onMouseOut", "mouseOut(this)");
                ((ImageButton)e.Row.FindControl("imgExcluir")).Attributes.Add("onclick", "javascript:return confirm('Confirma a exclusão do Item?')");
                ((ImageButton)e.Row.FindControl("imgHomologar")).Attributes.Add("onclick", "javascript:return confirm('Confirma a Homologação da Promoção?')");
            }
        }
        catch (ApplicationException ex)
        {
            Utilitarios.ExibirMensagem(this.Page, this.lblMensagem.ClientID, String.Format("{0}: {1}", ex.Message, ex.InnerException.Message));
        }
    }

    protected void gdvPromocao_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        try
        {
            Carvalho.QuePexinxa.CamadaAcessoDados.Promocao.Homologar(Convert.ToInt32(this.gdvPromocao.DataKeys[e.NewSelectedIndex].Value));
            this.PopulaGridView(true);
            this.ControlaEstado(TipoEstado.Novo);
            Utilitarios.ExibirMensagem(this.Page, this.lblMensagem.ClientID, String.Format("{0}", "Material Homologado com Sucesso!"));
        }
        catch (ApplicationException ex)
        {
            Utilitarios.ExibirMensagem(this.Page, this.lblMensagem.ClientID, String.Format("{0}: {1}", ex.Message, ex.InnerException.Message));
        }
    }

    private void PopularFormulario()
    {
        Carvalho.QuePexinxa.CamadaEntidades.Promocao promocao = new Carvalho.QuePexinxa.CamadaEntidades.Promocao();
        promocao.PromocaoID = this.PromocaoID;
        promocao = Carvalho.QuePexinxa.CamadaAcessoDados.Promocao.Selecionar(promocao);
        this.PromocaoID = promocao.PromocaoID;
        this.ddlProduto.SelectedValue = promocao.ProdutoID.ToString();
        this.txtDataInicio.Text = promocao.DataInicio.ToString();
        this.txtDataFim.Text = promocao.DataFim.ToString();
        this.txtValorAntigo.Text = promocao.ValorAntigo.ToString("c2");
        this.txtValorAtual.Text = promocao.ValorAtual.ToString("c2");
    }

    private void PopulaDropDownList()
    {
        Carvalho.QuePexinxa.CamadaEntidades.PaginaPadrao paginaPadrao = new Carvalho.QuePexinxa.CamadaEntidades.PaginaPadrao();
        paginaPadrao.Procedure = "[dbo].[ListarTodosProdutosPorLojista]";
        paginaPadrao.ForeignKey = QuePexinxa.UsuarioSessao.LojistaID.ToString();
        Utilitarios.PopulaDropDownList(this.ddlProduto, Carvalho.QuePexinxa.CamadaAcessoDados.PaginaPadrao.Listar(paginaPadrao, QuePexinxa.UsuarioSessao.Conexao));
    }

    private void EventosBotao()
    {
        this.btnSalvar.Attributes.Add("onClick", "return Validar('" + this.ddlProduto.ClientID + "','" + this.txtDataInicio.ClientID + "','" + this.txtDataFim.ClientID + "','" + this.txtValorAntigo.ClientID + "','" + this.txtValorAtual.ClientID + "','" + this.lblMensagem.ClientID + "');GetScrollerPosition();");
    }

    private void PopulaGridView(bool _reload)
    {
        if (_reload)
            this.ListaPromocao = Carvalho.QuePexinxa.CamadaAcessoDados.Promocao.Listar(QuePexinxa.UsuarioSessao.LojistaID);

        this.gdvPromocao.DataSource = this.ListaPromocao;
        this.gdvPromocao.DataBind();
    }

    private void ControlaEstado(TipoEstado estado)
    {
        switch (estado)
        {
            case TipoEstado.Novo:
                this.PromocaoID = 0;
                //this.ddlProduto.ClearSelection();

                foreach (Control item in this.Controls)
                {
                    //Se o contorle for um DropDownList...
                    if (item is DropDownList)
                    {
                        ((DropDownList)(item)).ClearSelection();
                    }
                    else
                    {
                        //Se o contorle for um TextBox...
                        if (item is TextBox)
                        {
                            ((TextBox)(item)).Text = String.Empty;
                        }
                    }
                }
                //this.txtDataInicio.Text = string.Empty;
                //this.txtDataFim.Text = string.Empty;
                //this.txtValorAntigo.Text = string.Empty;
                //this.txtValorAtual.Text = string.Empty;
                break;
        }
    }
}