using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using Gerbase.EncodeQueryString;

public partial class Account_Principal : System.Web.UI.Page
{
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
                Response.Redirect("../Default.aspx?S=53696D");
            else
            {
                this.RetornoPostBack();
                if (!IsPostBack)
                {
                    this.PopulaGridView(true);
                }
            }
        }
        catch (ApplicationException ex)
        {
            Utilitarios.ExibirMensagem(this.Page, this.lblMensagem.ClientID, String.Format("{0} Detalhe do Erro: {1}", ex.Message, ex.InnerException.Message));
        }
        catch (System.Web.HttpException ex)
        {
            Utilitarios.ExibirMensagem(this.Page, this.lblMensagem.ClientID, String.Format("Detalhe do Erro: {0}", ex.Message));
        }
        catch (NullReferenceException)
        {
            this.Response.Redirect("../Default.aspx?S=53696D");
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

    protected void gdvPromocao_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes.Add("onMouseOver", "mouseOver(this,true)");
                e.Row.Attributes.Add("onMouseOut", "mouseOut(this)");

                QueryString codifica, codificado;
                codifica = new QueryString();
                codifica.Add("PID", this.gdvPromocao.DataKeys[e.Row.RowIndex].Value.ToString());
                codificado = Encode.EncodeQueryString(codifica, Encode.EncodeType.Hex);
                ((ImageButton)e.Row.FindControl("imgRenovar")).Attributes.Add("onclick", "abrirPopup2('Renovar.aspx" + codificado + "', 'Renovar', 1000, 400);");

                if (Convert.ToBoolean(((Carvalho.QuePexinxa.CamadaEntidades.Promocao)(e.Row.DataItem)).Ativo))
                    ((System.Web.UI.WebControls.TableRow)(e.Row)).Cells[7].Text = "SIM";
                else
                    ((System.Web.UI.WebControls.TableRow)(e.Row)).Cells[7].Text = "NÃO";
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
            this.ListaPromocao = Carvalho.QuePexinxa.CamadaAcessoDados.Promocao.ListarHomologada(QuePexinxa.UsuarioSessao.LojistaID);

        this.gdvPromocao.DataSource = this.ListaPromocao;
        this.gdvPromocao.DataBind();
    }

    private void RetornoPostBack()
    {
        if (!string.IsNullOrEmpty(this.Request.Form["__EVENTTARGET"]))
            if (this.Request.Form["__EVENTTARGET"].Equals("gdvPromocao"))
                this.PopulaGridView(true);
    }
}