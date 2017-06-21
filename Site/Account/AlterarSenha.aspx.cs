using System;
using System.Web.Security;

public partial class Account_AlterarSenha : System.Web.UI.Page
{
    private enum TipoEstado
    {
        Novo
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!Utilitarios.ValidarUsuarioSessao())
            {
                this.Response.Redirect("../Default.aspx?S=53696D");
            }
            else
            {
                this.Page.Form.DefaultFocus = this.txtSenhaAntiga.ClientID;
                this.Page.Form.DefaultButton = this.btnSalvar.UniqueID;
                if (!IsPostBack)
                {
                    this.EventosBotao();
                    this.lblUsuario.Text = QuePexinxa.UsuarioSessao.Nome;
                }
            }
        }
        catch (ApplicationException ex)
        {
            Utilitarios.ExibirMensagem(this.Page, String.Format("{0} Detalhe do Erro: {1}", ex.Message, ex.InnerException.Message));
        }
        catch (NullReferenceException)
        {
            this.Response.Redirect("../Default.aspx?S=53696D");
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
            Utilitarios.ExibirMensagem(this.Page, String.Format("{0} Detalhe do Erro: {1}", ex.Message, ex.InnerException.Message));
        }
    }

    protected void btnSalvar_Click(object sender, EventArgs e)
    {
        Carvalho.QuePexinxa.CamadaEntidades.Usuario usuario = new Carvalho.QuePexinxa.CamadaEntidades.Usuario();
        int retorno;
        try
        {
            usuario.Senha = FormsAuthentication.HashPasswordForStoringInConfigFile(this.txtSenhaAntiga.Text, "MD5");
            usuario.SenhaNova = FormsAuthentication.HashPasswordForStoringInConfigFile(this.txtNovaSenha.Text, "MD5");
            usuario.UsuarioID = QuePexinxa.UsuarioSessao.UsuarioID;
            retorno = Carvalho.QuePexinxa.CamadaAcessoDados.Usuario.AlterarSenha(usuario, QuePexinxa.UsuarioSessao.Conexao);

            if (retorno.Equals(0))
            {
                this.ControlaEstado(TipoEstado.Novo);
                Utilitarios.ExibirMensagem(this.Page, String.Format("{0}", "Senha Alterada com Sucesso!"));
            }
            else
            {
                throw new Exception("A Senha Antiga não confere!");
            }
        }
        catch (ApplicationException ex)
        {
            Utilitarios.ExibirMensagem(this.Page, String.Format("{0} Detalhe do Erro: {1}", ex.Message, ex.InnerException.Message));
        }
        catch (Exception ex)
        {
            Utilitarios.ExibirMensagem(this.Page, String.Format("Ocorreu um erro. Detalhe do Erro: {0}", ex.Message));
        }
        finally
        {
            usuario = null;
        }
    }

    private void EventosBotao()
    {
        this.btnSalvar.Attributes.Add("onClick", "return Validar('" + this.txtSenhaAntiga.ClientID + "','" + this.txtNovaSenha.ClientID + "','" + this.txtRedigiteSenha.ClientID + "');");
    }

    private void ControlaEstado(TipoEstado estado)
    {
        switch (estado)
        {
            case TipoEstado.Novo:
                this.txtSenhaAntiga.Text = string.Empty;
                this.txtNovaSenha.Text = string.Empty;
                this.txtRedigiteSenha.Text = string.Empty;
                break;
        }
    }
}