using System;
using System.Data;
using System.Data.Common;
using System.Collections.Generic;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace Carvalho.QuePexinxa.CamadaAcessoDados
{
    public static class PaginaPadrao
    {
        public static int Gravar(Carvalho.QuePexinxa.CamadaEntidades.PaginaPadrao paginaPadrao, Database conexao)
        {
            DbCommand comando;
            int retorno;
            try
            {
                using (comando = conexao.GetStoredProcCommand(paginaPadrao.Procedure))
                {
                    conexao.AddParameter(comando, "@Retorno", DbType.Int32, ParameterDirection.InputOutput, String.Empty, DataRowVersion.Default, 0);
                    conexao.AddInParameter(comando, paginaPadrao.CampoID, DbType.Int32, paginaPadrao.ValorID);
                    conexao.AddInParameter(comando, paginaPadrao.Campo, DbType.String, paginaPadrao.Valor);

                    if (!string.IsNullOrEmpty(paginaPadrao.ForeignKey))
                        conexao.AddInParameter(comando, "@ForeignKey", DbType.Int32, Convert.ToInt32(paginaPadrao.ForeignKey));

                    conexao.ExecuteNonQuery(comando);
                    retorno = Convert.ToInt32(conexao.GetParameterValue(comando, "@Retorno"));
                }
                return retorno;
            }
            catch (DbException ex)
            {
                throw new ApplicationException("Erro ao gravar o registro", ex);
            }
            finally
            {
                comando = null;
            }
        }

        public static Carvalho.QuePexinxa.CamadaEntidades.PaginaPadrao Selecionar(Carvalho.QuePexinxa.CamadaEntidades.PaginaPadrao paginaPadrao, Database conexao)
        {
            DbCommand comando;
            IDataReader retorno;
            try
            {
                using (comando = conexao.GetStoredProcCommand(paginaPadrao.Procedure))
                {
                    conexao.AddInParameter(comando, paginaPadrao.CampoID, DbType.Int32, paginaPadrao.ValorID);
                    retorno = conexao.ExecuteReader(comando);

                    if (retorno.Read())
                    {
                        paginaPadrao.ValorID = Convert.ToInt32(retorno["CampoID"]);
                        paginaPadrao.Valor = retorno["Campo"].ToString();
                    }
                    retorno.Close();
                }
                return paginaPadrao;
            }
            catch (DbException ex)
            {
                throw new ApplicationException("Erro ao selecionar o registro", ex);
            }
            catch(IndexOutOfRangeException ex)
            {
                throw new ApplicationException("Erro ao selecionar o registro", ex);
            }
            finally
            {
                retorno = null;
                comando = null;
            }
        }

        public static void Excluir(Carvalho.QuePexinxa.CamadaEntidades.PaginaPadrao paginaPadrao, Database conexao)
        {
            DbCommand comando;
            try
            {
                using (comando = conexao.GetStoredProcCommand(paginaPadrao.Procedure))
                {
                    conexao.AddInParameter(comando, paginaPadrao.CampoID, DbType.Int32, paginaPadrao.ValorID);
                    conexao.ExecuteNonQuery(comando);
                }
            }
            catch (DbException ex)
            {
                throw new ApplicationException("Erro ao excluir o registro.", ex);
            }
            finally
            {
                comando = null;
            }
        }

        public static List<Carvalho.QuePexinxa.CamadaEntidades.PaginaPadrao> Listar(Carvalho.QuePexinxa.CamadaEntidades.PaginaPadrao paginaPadrao, Database conexao)
        {
            List<Carvalho.QuePexinxa.CamadaEntidades.PaginaPadrao> listaPaginaPadrao = new List<Carvalho.QuePexinxa.CamadaEntidades.PaginaPadrao>();
            DbCommand comando;
            IDataReader retorno;
            try
            {
                using (comando = conexao.GetStoredProcCommand(paginaPadrao.Procedure))
                {
                    if (!string.IsNullOrEmpty(paginaPadrao.ForeignKey))
                        conexao.AddInParameter(comando, "@ForeignKey", DbType.Int32, paginaPadrao.ForeignKey);

                    retorno = conexao.ExecuteReader(comando);

                    while (retorno.Read())
                    {
                        Carvalho.QuePexinxa.CamadaEntidades.PaginaPadrao _objPaginaPadrao = new Carvalho.QuePexinxa.CamadaEntidades.PaginaPadrao();
                        _objPaginaPadrao.CampoID = paginaPadrao.CampoID;
                        _objPaginaPadrao.Campo = paginaPadrao.Campo;
                        _objPaginaPadrao.ValorID = Convert.ToInt32(retorno["CampoID"]);
                        _objPaginaPadrao.Valor = retorno["Campo"].ToString();
                        listaPaginaPadrao.Add(_objPaginaPadrao);
                        _objPaginaPadrao = null;
                    }
                    retorno.Close();
                }
                return listaPaginaPadrao;
            }
            catch (DbException ex)
            {
                throw new ApplicationException("Erro ao listar os registros", ex);
            }
            catch (IndexOutOfRangeException ex)
            {
                throw new ApplicationException("Erro ao selecionar o registro", ex);
            }
            finally
            {
                comando = null;
                retorno = null;
            }
        }

        public static List<Carvalho.QuePexinxa.CamadaEntidades.PaginaPadrao> ListarFiltros(Carvalho.QuePexinxa.CamadaEntidades.PaginaPadrao paginaPadrao, Database conexao)
        {
            List<Carvalho.QuePexinxa.CamadaEntidades.PaginaPadrao> listaPaginaPadrao = new List<Carvalho.QuePexinxa.CamadaEntidades.PaginaPadrao>();
            DbCommand comando;
            IDataReader retorno;
            try
            {
                using (comando = conexao.GetStoredProcCommand(paginaPadrao.Procedure))
                {
                    retorno = conexao.ExecuteReader(comando);

                    while (retorno.Read())
                    {
                        Carvalho.QuePexinxa.CamadaEntidades.PaginaPadrao objPaginaPadrao = new Carvalho.QuePexinxa.CamadaEntidades.PaginaPadrao();
                        objPaginaPadrao.CampoID = retorno["CampoID"].ToString();
                        objPaginaPadrao.Valor = retorno["Campo"].ToString();
                        listaPaginaPadrao.Add(objPaginaPadrao);
                        objPaginaPadrao = null;
                    }
                    retorno.Close();
                }
                return listaPaginaPadrao;
            }
            catch (DbException ex)
            {
                throw new ApplicationException("Erro ao listar os registros", ex);
            }
            catch (IndexOutOfRangeException ex)
            {
                throw new ApplicationException("Erro ao selecionar o registro", ex);
            }
            finally
            {
                comando = null;
                retorno = null;
            }
        }

        public static List<Carvalho.QuePexinxa.CamadaEntidades.PaginaPadrao> ExecutarProcedure(string procedure, string query, Database conexao)
        {
            List<Carvalho.QuePexinxa.CamadaEntidades.PaginaPadrao> listaPaginaPadrao = new List<Carvalho.QuePexinxa.CamadaEntidades.PaginaPadrao>();
            DbCommand comando;
            IDataReader retorno;
            try
            {
                using (comando = conexao.GetStoredProcCommand(procedure))
                {
                    conexao.AddInParameter(comando, "@Query", DbType.String, query);
                    retorno = conexao.ExecuteReader(comando);

                    while (retorno.Read())
                    {
                        Carvalho.QuePexinxa.CamadaEntidades.PaginaPadrao objPaginaPadrao = new Carvalho.QuePexinxa.CamadaEntidades.PaginaPadrao();
                        objPaginaPadrao.Campo = retorno["Campo"].ToString();
                        objPaginaPadrao.Valor = retorno["Quantidade"].ToString();
                        listaPaginaPadrao.Add(objPaginaPadrao);
                        objPaginaPadrao = null;
                    }
                    retorno.Close();
                }
                return listaPaginaPadrao;
            }
            catch (DbException ex)
            {
                throw new ApplicationException("Erro ao listar os registros", ex);
            }
            catch (IndexOutOfRangeException ex)
            {
                throw new ApplicationException("Erro ao listar os registros", ex);
            }
            finally
            {
                listaPaginaPadrao = null;
                comando = null;
                retorno = null;
            }
        }
    }
}