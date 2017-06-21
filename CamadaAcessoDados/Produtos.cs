using System;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Collections.Generic;

namespace Carvalho.QuePexinxa.CamadaAcessoDados
{
    public static class Produtos
    {
        public static int Gravar(Carvalho.QuePexinxa.CamadaEntidades.Produtos produto)
        {
            Database conexao;
            DbCommand comando;
            try
            {
                conexao = DatabaseFactory.CreateDatabase();
                using (comando = conexao.GetStoredProcCommand("[dbo].[GravarProduto]"))
                {
                    conexao.AddParameter(comando, "@ProdutoID", DbType.Int32, ParameterDirection.InputOutput, String.Empty, DataRowVersion.Default, produto.ProdutoID);
                    conexao.AddInParameter(comando, "@LojistaID", DbType.Int32, produto.LojistaID);
                    conexao.AddInParameter(comando, "@CategoriaID", DbType.Int32, produto.CategoriaID);
                    conexao.AddInParameter(comando, "@Descricao", DbType.String, produto.Descricao);
                    conexao.AddInParameter(comando, "@Imagem", DbType.String, produto.Imagem);
                    conexao.AddInParameter(comando, "@Extensao", DbType.String, produto.Extensao);
                    conexao.AddInParameter(comando, "@UsuarioID", DbType.Int32, produto.UsuarioID);
                    conexao.ExecuteNonQuery(comando);
                    return Convert.ToInt32(conexao.GetParameterValue(comando, "@ProdutoID"));
                }
            }
            catch (DbException ex)
            {
                throw new ApplicationException("Erro ao gravar o registro", ex);
            }
        }
                
        public static void Excluir(int produtoID)
        {
            Database conexao;
            DbCommand comando;
            try
            {
                conexao = DatabaseFactory.CreateDatabase();
                using (comando = conexao.GetStoredProcCommand("[dbo].[ExcluirProduto]"))
                {
                    conexao.AddInParameter(comando, "@ProdutoID", DbType.Int32, produtoID);
                    conexao.ExecuteNonQuery(comando);
                }
            }
            catch (DbException ex)
            {
                throw new ApplicationException("Erro ao excluir o registro", ex);
            }
        }

        public static Carvalho.QuePexinxa.CamadaEntidades.Produtos Selecionar(Carvalho.QuePexinxa.CamadaEntidades.Produtos produto)
        {
            Database conexao;
            DbCommand comando;
            IDataReader retorno;
            try
            {
                conexao = DatabaseFactory.CreateDatabase();
                using (comando = conexao.GetStoredProcCommand("[dbo].[SelecionarProduto]"))
                {
                    conexao.AddInParameter(comando, "@ProdutoID", DbType.Int32, produto.ProdutoID);
                    retorno = conexao.ExecuteReader(comando);

                    if (retorno.Read())
                    {
                        produto.ProdutoID = Convert.ToInt32(retorno["ProdutoID"]);
                        produto.LojistaID = Convert.ToInt32(retorno["LojistaID"]);
                        produto.CategoriaID = Convert.ToInt32(retorno["CategoriaID"]);
                        produto.Descricao = retorno["Descricao"].ToString();
                        produto.Imagem = retorno["Imagem"].ToString();
                        produto.Extensao = retorno["Extensao"].ToString();
                    }
                    retorno.Close();
                }
                return produto;
            }
            catch (DbException ex)
            {
                throw new ApplicationException("Erro ao selecionar o registro", ex);
            }
            catch (IndexOutOfRangeException ex)
            {
                throw new ApplicationException("Erro ao listar os registros", ex);
            }
        }

        public static List<Carvalho.QuePexinxa.CamadaEntidades.Produtos> Listar(int lojistaID)
        {
            List<Carvalho.QuePexinxa.CamadaEntidades.Produtos> listaProdutos = new List<Carvalho.QuePexinxa.CamadaEntidades.Produtos>();
            Database conexao;
            DbCommand comando;
            IDataReader retorno;
            try
            {
                conexao = DatabaseFactory.CreateDatabase();
                using (comando = conexao.GetStoredProcCommand("[dbo].[ListarProdutosPorLojista]"))
                {
                    conexao.AddInParameter(comando, "@LojistaID", DbType.Int32, lojistaID);
                    retorno = conexao.ExecuteReader(comando);

                    while (retorno.Read())
                    {
                        Carvalho.QuePexinxa.CamadaEntidades.Produtos objProduto = new Carvalho.QuePexinxa.CamadaEntidades.Produtos();
                        objProduto.ProdutoID = Convert.ToInt32(retorno["ProdutoID"]);
                        objProduto.LojistaID = Convert.ToInt32(retorno["LojistaID"]);
                        objProduto.Categoria = retorno["Categoria"].ToString();
                        objProduto.Descricao = retorno["Descricao"].ToString();
                        objProduto.Imagem = retorno["Imagem"].ToString();
                        listaProdutos.Add(objProduto);
                        objProduto = null;
                    }
                    retorno.Close();
                }
                return listaProdutos;
            }
            catch (DbException ex)
            {
                throw new ApplicationException("Erro ao listar os registros", ex);
            }
            catch (IndexOutOfRangeException ex)
            {
                throw new ApplicationException("Erro ao listar os registros", ex);
            }
        }
    }
}