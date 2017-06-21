using System;
using System.Collections.Generic;

namespace Carvalho.QuePexinxa.CamadaEntidades
{
    [Serializable]
    public class Lojistas
    {
        public int LojistaID { get; set; }

        public string Nome { get; set; }

        public string Endereco { get; set; }

        public string DDD { get; set; }

        public string Telefone { get; set; }

        public int Quantidade { get; set; }

        public List<Carvalho.QuePexinxa.CamadaEntidades.LojistaXCategoria> ListaLojistaXCategoria { get; set; }
    }
}