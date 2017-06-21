using System;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace Carvalho.QuePexinxa.CamadaEntidades
{
    [Serializable]
    public class UsuarioSessao
    {
        public int UsuarioID { get; set; }

        public int NivelUsuarioID { get; set; }

        public int LojistaID { get; set; }

        public string Lojista { get; set; }

        public string Nome { get; set; }

        public string SessionID { get; set; }

        public string IP { get; set; }

        public Database Conexao { get; set; }
    }
}