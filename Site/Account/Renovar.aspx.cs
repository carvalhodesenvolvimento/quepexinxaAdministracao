using Gerbase.EncodeQueryString;
using System;
using System.Web.UI;

public partial class Account_Renovar : System.Web.UI.Page
{
    public int PromocaoID
    {
        get { return Convert.ToInt32(ViewState["PromocaoID"]); }
        set { ViewState["PromocaoID"] = value; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            //Verifica sessão ativa
            if (!Utilitarios.ValidarUsuarioSessao())
                this.Response.Redirect("../Default.aspx");
            else
            {
                this.Page.Form.DefaultFocus = this.txtDataFim.ClientID;
                this.Master.Cabecalho = false;
                this.Master.Menu = false;

                if (!IsPostBack)
                {
                    if (!string.IsNullOrEmpty(this.Request.QueryString["PID"]))
                    {
                        QueryString decodifica, decodificado;
                        decodifica = QueryString.FromCurrent();
                        decodificado = Encode.DecodeQueryString(decodifica, Encode.EncodeType.Hex);
                        this.PromocaoID = Convert.ToInt32(Server.HtmlDecode(decodificado["PID"]));
                    }
                    this.EventosBotao();
                }
            }
        }
        catch (ApplicationException ex)
        {
            Utilitarios.ExibirMensagem(this.Page, String.Format("{0} Detalhe do Erro: {1}", ex.Message, ex.InnerException.Message));
        }
        catch (NullReferenceException)
        {
            this.Response.Redirect("../Default.aspx");
        }
    }

    protected void btnSalvar_Click(object sender, EventArgs e)
    {
        try
        {
            Carvalho.QuePexinxa.CamadaAcessoDados.Promocao.Renovar(this.PromocaoID, Convert.ToDateTime(this.txtDataFim.Text), this.chkAtivo.Checked);
            Utilitarios.ExibirMensagem(this.Page, String.Format("{0}", "Registro gravado com Sucesso!"));
            this.DoPostBack();
        }
        catch (ApplicationException ex)
        {
            Utilitarios.ExibirMensagem(this.Page, String.Format("{0} Detalhe do Erro: {1}", ex.Message, ex.InnerException.Message));
        }
    }

    private void EventosBotao()
    {
        this.btnSalvar.Attributes.Add("onClick", "return Validar('" + this.txtDataFim.ClientID + "','" + this.lblMensagem.ClientID + "');");
    }

    private void DoPostBack()
    {
        ScriptManager.RegisterStartupScript(this.Page, typeof(Page), "Renovar", "opener.__doPostBack('gdvPromocao','');setTimeout(function () { window.close(); }, 500);", true);
    }
}