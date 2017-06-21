using System;

public partial class Account_Administracao : System.Web.UI.MasterPage
{
    public bool Cabecalho
    {
        set { this.cabecalho.Visible = value; }
    }

    public bool Menu
    {
        set { this.menu.Visible = value; }
    }

    public string Usuario
    {
        set { this.lblUsuario.Text = value; }
    }

    public string Data
    {
        set { this.lblData.Text = value; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                this.Usuario = "Usuário: " + QuePexinxa.UsuarioSessao.Nome +" - Loja: " + QuePexinxa.UsuarioSessao.Lojista;
                this.Data = DateTime.Today.ToLongDateString();

                if (QuePexinxa.UsuarioSessao.NivelUsuarioID.Equals(2))
                {
                    //this.home.Visible = false;
                    this.lojista.Visible = false;
                }
            }
        }
        catch (NullReferenceException)
        {
            this.Response.Redirect("Default.aspx");
        }
    }
}