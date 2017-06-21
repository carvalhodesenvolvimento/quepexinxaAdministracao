using System;

namespace Carvalho.QuePexinxa.CamadaEntidades
{
    [Serializable]
    public class LojistaXCategoria
    {
        public int LojistaID { get; set; }

        public string Lojista { get; set; }

        public int CategoriaID { get; set; }

        public string Categoria { get; set; }
    }
}