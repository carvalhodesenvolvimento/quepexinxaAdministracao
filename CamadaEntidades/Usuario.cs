using System;

namespace Carvalho.QuePexinxa.CamadaEntidades
{
    [Serializable]
    public class Usuario
    {
        public int UsuarioID { get; set; }

        public int NivelUsuarioID { get; set; }

        public string NivelUsuario { get; set; }

        public int LogistaID { get; set; }

        public string Logista { get; set; }

        public string Nome { get; set; }

        public string Senha { get; set; }

        public string SenhaNova { get; set; }

        public bool Ativo { get; set; }
    }
}