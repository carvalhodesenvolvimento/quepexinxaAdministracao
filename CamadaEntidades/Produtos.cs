using System;

namespace Carvalho.QuePexinxa.CamadaEntidades
{
    [Serializable]
    public class Produtos
    {
        public int ProdutoID { get; set; }

        public int LojistaID { get; set; }

        public string Lojista { get; set; }

        public int CategoriaID { get; set; }

        public string Categoria { get; set; }

        public string Descricao { get; set; }

        public string Imagem { get; set; }

        public string Extensao { get; set; }

        public int UsuarioID { get; set; }
    }
}
