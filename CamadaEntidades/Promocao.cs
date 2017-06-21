using System;

namespace Carvalho.QuePexinxa.CamadaEntidades
{
    [Serializable]
    public class Promocao : Produtos
    {
        public int PromocaoID { get; set; }

        public int ProdutoID { get; set; }

        public string Produto { get; set; }

        public DateTime DataInicio { get; set; }

        public DateTime DataFim { get; set; }

        public double ValorAntigo { get; set; }

        public double ValorAtual { get; set; }

        public bool Ativo { get; set; }

        public int UsuarioID { get; set; }
    }
}
