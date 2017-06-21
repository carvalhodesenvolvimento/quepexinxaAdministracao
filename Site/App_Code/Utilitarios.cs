using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using Gerbase.EncodeQueryString;

public static class Utilitarios
{
    public static void PopulaDropDownList(DropDownList dropdown, List<Carvalho.QuePexinxa.CamadaEntidades.PaginaPadrao> listaPaginaPadrao)
    {
        dropdown.DataSource = listaPaginaPadrao;
        dropdown.DataBind();
        dropdown.Items.Insert(0, "");
        dropdown.SelectedIndex = 0;
    }

    public static void ExibirMensagem(Page pagina, string _mensagem)
    {
        ScriptManager.RegisterStartupScript(pagina, typeof(Page), "mensagem", "document.getElementById('ContentPlaceHolder1_lblMensagem').innerHTML = '" + _mensagem.Replace("'", "").Replace("\r", "").Replace("\n", "") + "'; setTimeout(function () {document.getElementById('ContentPlaceHolder1_lblMensagem').innerHTML = '';}, 10000);", true);
    }

    public static void ExibirMensagem(Page pagina, string _objetoID, string _mensagem)
    {
        ScriptManager.RegisterStartupScript(pagina, typeof(Page), "mensagem", "document.getElementById('" + _objetoID + "').innerHTML = '" + _mensagem.Replace("'", "").Replace("\r", "").Replace("\n", "") + "'; setTimeout(function () {document.getElementById('" + _objetoID + "').innerHTML = '';}, 10000);", true);
    }

    public static bool ValidarUsuarioSessao()
    {
        if (!string.IsNullOrEmpty(System.Web.HttpContext.Current.Session["UsuarioSessaoQuePexinxa"].ToString()))
            return true;
        return false;
    }

    public static string RetornaParametrosPaginaPadrao(string retorno, string nomePagina, string campoID, string campo, string foreignKey, string retornoDoPostBack)
    {
        QueryString codifica, codificado;
        codifica = new QueryString();
        codifica.Add("R", retorno);
        codifica.Add("NP", nomePagina);
        codifica.Add("CID", campoID);
        codifica.Add("C", campo);
        codifica.Add("FK", foreignKey);
        codifica.Add("PB", retornoDoPostBack);
        codificado = Encode.EncodeQueryString(codifica, Encode.EncodeType.Hex);
        return codificado.ToString();
    }

    public static int? ConvertToInt(string valor)
    {
        int? valorTemp = null;
        if (!string.IsNullOrEmpty(valor))
            valorTemp = Convert.ToInt32(valor);
        return valorTemp;
    }

    public static DateTime? ConvertToDateTime(string valor)
    {
        DateTime? valorTemp = null;
        if (!string.IsNullOrEmpty(valor))
            valorTemp = Convert.ToDateTime(valor);
        return valorTemp;
    }

    public static bool? ConvertToBoolean(string valor)
    {
        bool? valorTemp = null;
        if (!string.IsNullOrEmpty(valor))
            valorTemp = Convert.ToBoolean(valor);
        return valorTemp;
    }

    public static string ConvertToString(string valor)
    {
        string valorTemp = null;
        if (!string.IsNullOrEmpty(valor))
            valorTemp = valor.ToString();
        return valorTemp;
    }
}