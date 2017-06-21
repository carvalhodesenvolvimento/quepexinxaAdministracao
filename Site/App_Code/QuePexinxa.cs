using System.Web;

public static class QuePexinxa
{
    public static Carvalho.QuePexinxa.CamadaEntidades.UsuarioSessao UsuarioSessao
    {
        get { return (HttpContext.Current.Session["UsuarioSessaoQuePexinxa"] != null ? (Carvalho.QuePexinxa.CamadaEntidades.UsuarioSessao)HttpContext.Current.Session["UsuarioSessaoQuePexinxa"] : null); }
        set { HttpContext.Current.Session["UsuarioSessaoQuePexinxa"] = value; }
    }
}