using System;

namespace Carvalho.QuePexinxa.CamadaEntidades
{
    [Serializable]
    public class PaginaPadrao
    {
        public string CampoID { get; set; }

        public int ValorID { get; set; }

        public string Campo { get; set; }

        public string Valor { get; set; }

        public string ForeignKey { get; set; }

        public string Procedure { get; set; }
    }
}