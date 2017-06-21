using System;
using System.Web;
using Gerbase.EncodeQueryString;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Web.Security;

public partial class Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            this.Page.Form.DefaultFocus = this.txtNome.ClientID;
            this.Page.Form.DefaultButton = this.btnOK.UniqueID;
            this.lblData.Text = DateTime.Today.ToLongDateString();

            if (!IsPostBack)
            {
                this.EventosBotao();
                if (!string.IsNullOrEmpty(this.Request.QueryString["S"]))
                {
                    QueryString decodifica, decodificado;
                    decodifica = QueryString.FromCurrent();
                    decodificado = Encode.DecodeQueryString(decodifica, Encode.EncodeType.Hex);

                    if (Server.HtmlDecode(decodificado["S"]).Equals("Sim"))
                    {
                        //Destroi o objeto
                        QuePexinxa.UsuarioSessao = null;

                        //Destroi a Sessão
                        System.Web.HttpContext.Current.Session.Abandon();//["UsuarioSessao"] = null;
                    }
                    decodifica = null;
                    decodificado = null;
                }
            }
        }
        catch (ApplicationException ex)
        {
            Utilitarios.ExibirMensagem(this.Page, this.lblMensagem.ClientID, String.Format("{0} Detalhe do Erro: {1}", ex.Message, ex.InnerException.Message));
        }
    }

    protected void btnOK_Click(object sender, EventArgs e)
    {
        Carvalho.QuePexinxa.CamadaEntidades.Usuario usuario = new Carvalho.QuePexinxa.CamadaEntidades.Usuario();
        Carvalho.QuePexinxa.CamadaEntidades.UsuarioSessao usuarioSessao = new Carvalho.QuePexinxa.CamadaEntidades.UsuarioSessao();
        try
        {
            //Alimenta Usuario
            usuario.Nome = this.txtNome.Text;
            usuario.Senha = FormsAuthentication.HashPasswordForStoringInConfigFile(txtSenha.Text, "MD5");
            usuarioSessao = Carvalho.QuePexinxa.CamadaAcessoDados.Usuario.Selecionar(usuario, DatabaseFactory.CreateDatabase());

            if (!usuarioSessao.UsuarioID.Equals(0))
            {
                //Alimenta as variáveis com os dados do usuário
                usuarioSessao.SessionID = System.Web.HttpContext.Current.Session.SessionID;
                usuarioSessao.IP = HttpContext.Current.Request.UserHostAddress;

                //Atribue o objeto usuarioSessao a uma variável do tipo Session da aplicação
                QuePexinxa.UsuarioSessao = usuarioSessao;

                //Redireciona o usuário para a próxima página
                this.Response.Redirect("Account/Principal.aspx");
            }
            else
            {
                Utilitarios.ExibirMensagem(this.Page, this.lblMensagem.ClientID, String.Format("{0}", "Usuário não Encontrado!"));
            }
        }
        catch (ApplicationException ex)
        {
            Utilitarios.ExibirMensagem(this.Page, this.lblMensagem.ClientID, String.Format("{0} Detalhe do Erro: {1}", ex.Message, ex.InnerException.Message));
        }
        catch (System.IO.FileNotFoundException ex)
        {
            Utilitarios.ExibirMensagem(this.Page, this.lblMensagem.ClientID, String.Format("Detalhe do Erro: {0}", ex.Message));
        }
        finally
        {
            usuario = null;
            usuarioSessao = null;
        }
    }

    private void EventosBotao()
    {
        this.btnOK.Attributes.Add("onClick", "return Validar('" + this.txtNome.ClientID + "','" + this.txtSenha.ClientID + "');");
    }
}