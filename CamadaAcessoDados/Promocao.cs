using System;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Collections.Generic;

namespace Carvalho.QuePexinxa.CamadaAcessoDados
{
    public static class Promocao
    {
        public static void Gravar(Carvalho.QuePexinxa.CamadaEntidades.Promocao promocao)
        {
            Database conexao;
            DbCommand comando;
            try
            {
                conexao = DatabaseFactory.CreateDatabase();
                using (comando = conexao.GetStoredProcCommand("[dbo].[GravarPromocao]"))
                {
                    conexao.AddInParameter(comando, "@PromocaoID", DbType.Int32, promocao.PromocaoID);
                    conexao.AddInParameter(comando, "@ProdutoID", DbType.Int32, promocao.ProdutoID);
                    conexao.AddInParameter(comando, "@DataInicio", DbType.DateTime, promocao.DataInicio);
                    conexao.AddInParameter(comando, "@DataFim", DbType.DateTime, promocao.DataFim);
                    conexao.AddInParameter(comando, "@ValorAntigo", DbType.Double, promocao.ValorAntigo);
                    conexao.AddInParameter(comando, "@ValorAtual", DbType.Double, promocao.ValorAtual);
                    conexao.AddInParameter(comando, "@UsuarioID", DbType.Int32, promocao.UsuarioID);
                    conexao.ExecuteNonQuery(comando);
                }
            }
            catch (DbException ex)
            {
                throw new ApplicationException("Erro ao gravar o registro", ex);
            }
        }
                
        public static void Excluir(int promocaoID)
        {
            Database conexao;
            DbCommand comando;
            try
            {
                conexao = DatabaseFactory.CreateDatabase();
                using (comando = conexao.GetStoredProcCommand("[dbo].[ExcluirPromocao]"))
                {
                    conexao.AddInParameter(comando, "@PromocaoID", DbType.Int32, promocaoID);
                    conexao.ExecuteNonQuery(comando);
                }
            }
            catch (DbException ex)
            {
                throw new ApplicationException("Erro ao excluir o registro", ex);
            }
        }

        public static Carvalho.QuePexinxa.CamadaEntidades.Promocao Selecionar(Carvalho.QuePexinxa.CamadaEntidades.Promocao promocao)
        {
            Database conexao;
            DbCommand comando;
            IDataReader retorno;
            try
            {
                conexao = DatabaseFactory.CreateDatabase();
                using (comando = conexao.GetStoredProcCommand("[dbo].[SelecionarPromocao]"))
                {
                    conexao.AddInParameter(comando, "@PromocaoID", DbType.Int32, promocao.PromocaoID);
                    retorno = conexao.ExecuteReader(comando);

                    if (retorno.Read())
                    {
                        promocao.PromocaoID = Convert.ToInt32(retorno["PromocaoID"]);
                        promocao.ProdutoID = Convert.ToInt32(retorno["ProdutoID"]);
                        promocao.DataInicio = Convert.ToDateTime(retorno["DataInicio"]);
                        promocao.DataFim = Convert.ToDateTime(retorno["DataFim"]);
                        promocao.ValorAntigo = Convert.ToDouble(retorno["ValorAntigo"]);
                        promocao.ValorAtual = Convert.ToDouble(retorno["ValorAtual"]);
                    }
                    retorno.Close();
                }
                return promocao;
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

        public static List<Carvalho.QuePexinxa.CamadaEntidades.Promocao> Listar(int lojistaID)
        {
            List<Carvalho.QuePexinxa.CamadaEntidades.Promocao> listaPromocao = new List<Carvalho.QuePexinxa.CamadaEntidades.Promocao>();
            Database conexao;
            DbCommand comando;
            IDataReader retorno;
            try
            {
                conexao = DatabaseFactory.CreateDatabase();
                using (comando = conexao.GetStoredProcCommand("[dbo].[ListarPromocaoPorLojista]"))
                {
                    conexao.AddInParameter(comando, "@LojistaID", DbType.Int32, lojistaID);
                    retorno = conexao.ExecuteReader(comando);

                    while (retorno.Read())
                    {
                        Carvalho.QuePexinxa.CamadaEntidades.Promocao objPromocao = new Carvalho.QuePexinxa.CamadaEntidades.Promocao();
                        objPromocao.PromocaoID = Convert.ToInt32(retorno["PromocaoID"]);
                        objPromocao.Lojista = retorno["Lojista"].ToString();
                        objPromocao.Categoria = retorno["Categoria"].ToString();
                        objPromocao.Produto = retorno["Produto"].ToString();
                        objPromocao.DataInicio = Convert.ToDateTime(retorno["DataInicio"]);
                        objPromocao.DataFim = Convert.ToDateTime(retorno["DataFim"]);
                        objPromocao.ValorAntigo = Convert.ToDouble(retorno["ValorAntigo"]);
                        objPromocao.ValorAtual = Convert.ToDouble(retorno["ValorAtual"]);
                        listaPromocao.Add(objPromocao);
                        objPromocao = null;
                    }
                    retorno.Close();
                }
                return listaPromocao;
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

        public static List<Carvalho.QuePexinxa.CamadaEntidades.Promocao> ListarHomologada(int lojistaID)
        {
            List<Carvalho.QuePexinxa.CamadaEntidades.Promocao> listaPromocao = new List<Carvalho.QuePexinxa.CamadaEntidades.Promocao>();
            Database conexao;
            DbCommand comando;
            IDataReader retorno;
            try
            {
                conexao = DatabaseFactory.CreateDatabase();
                using (comando = conexao.GetStoredProcCommand("[dbo].[ListarPromocaoPorLojistaHomologada]"))
                {
                    conexao.AddInParameter(comando, "@LojistaID", DbType.Int32, lojistaID);
                    retorno = conexao.ExecuteReader(comando);

                    while (retorno.Read())
                    {
                        Carvalho.QuePexinxa.CamadaEntidades.Promocao objPromocao = new Carvalho.QuePexinxa.CamadaEntidades.Promocao();
                        objPromocao.PromocaoID = Convert.ToInt32(retorno["PromocaoID"]);
                        objPromocao.Lojista = retorno["Lojista"].ToString();
                        objPromocao.Categoria = retorno["Categoria"].ToString();
                        objPromocao.Produto = retorno["Produto"].ToString();
                        objPromocao.DataInicio = Convert.ToDateTime(retorno["DataInicio"]);
                        objPromocao.DataFim = Convert.ToDateTime(retorno["DataFim"]);
                        objPromocao.ValorAntigo = Convert.ToDouble(retorno["ValorAntigo"]);
                        objPromocao.ValorAtual = Convert.ToDouble(retorno["ValorAtual"]);
                        objPromocao.Ativo = Convert.ToBoolean(retorno["Ativo"]);
                        listaPromocao.Add(objPromocao);
                        objPromocao = null;
                    }
                    retorno.Close();
                }
                return listaPromocao;
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

        public static void Homologar(int promocaoID)
        {
            Database conexao;
            DbCommand comando;
            try
            {
                conexao = DatabaseFactory.CreateDatabase();
                using (comando = conexao.GetStoredProcCommand("[dbo].[HomologarPromocao]"))
                {
                    conexao.AddInParameter(comando, "@PromocaoID", DbType.Int32, promocaoID);
                    conexao.ExecuteNonQuery(comando);
                }
            }
            catch (DbException ex)
            {
                throw new ApplicationException("Erro ao excluir o registro", ex);
            }
            finally
            {
                comando = null;
            }
        }

        public static void Renovar(int promocaoID, DateTime dataFim, bool ativo)
        {
            Database conexao;
            DbCommand comando;
            try
            {
                conexao = DatabaseFactory.CreateDatabase();
                using (comando = conexao.GetStoredProcCommand("[dbo].[RenovarPromocao]"))
                {
                    conexao.AddInParameter(comando, "@PromocaoID", DbType.Int32, promocaoID);
                    conexao.AddInParameter(comando, "@DataFim", DbType.DateTime, dataFim);
                    conexao.AddInParameter(comando, "@Ativo", DbType.Boolean, ativo);
                    conexao.ExecuteNonQuery(comando);
                }
            }
            catch (DbException ex)
            {
                throw new ApplicationException("Erro ao excluir o registro", ex);
            }
            finally
            {
                comando = null;
            }
        }

    }
}