using System;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Collections.Generic;

namespace Carvalho.QuePexinxa.CamadaAcessoDados
{
    public static class LojistaXCategoria
    {
        public static void Gravar(Carvalho.QuePexinxa.CamadaEntidades.LojistaXCategoria lojistaXCateria, Database conexao)
        {
            DbCommand comando;
            try
            {
                using (comando = conexao.GetStoredProcCommand("[dbo].[GravarLojistaXCategoria]"))
                {
                    conexao.AddInParameter(comando, "@LojistaID", DbType.Int32, lojistaXCateria.LojistaID);
                    conexao.AddInParameter(comando, "@CategoriaID", DbType.Int32, lojistaXCateria.CategoriaID);
                    conexao.ExecuteNonQuery(comando);
                }
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

        public static void Excluir(int lojistaID, Database conexao)
        {
            DbCommand comando;
            try
            {
                using (comando = conexao.GetStoredProcCommand("[dbo].[ExcluirLojistaXCategoria]"))
                {
                    conexao.AddInParameter(comando, "@LojistaID", DbType.Int32, lojistaID);
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

        public static List<Carvalho.QuePexinxa.CamadaEntidades.LojistaXCategoria> Listar(int lojistaID, Database conexao)
        {
            List<Carvalho.QuePexinxa.CamadaEntidades.LojistaXCategoria> listaLojistaXCategoria = new List<Carvalho.QuePexinxa.CamadaEntidades.LojistaXCategoria>();
            DbCommand comando;
            IDataReader retorno;
            try
            {
                using (comando = conexao.GetStoredProcCommand("[dbo].[ListarLojistaXCategoria]"))
                {
                    conexao.AddInParameter(comando, "@LojistaID", DbType.Int32, lojistaID);
                    retorno = conexao.ExecuteReader(comando);

                    while (retorno.Read())
                    {
                        Carvalho.QuePexinxa.CamadaEntidades.LojistaXCategoria objLojistaXCategoria = new Carvalho.QuePexinxa.CamadaEntidades.LojistaXCategoria();
                        objLojistaXCategoria.LojistaID = Convert.ToInt32(retorno["LojistaID"]);
                        objLojistaXCategoria.Lojista = retorno["Lojista"].ToString();
                        objLojistaXCategoria.CategoriaID = Convert.ToInt32(retorno["CategoriaID"]);
                        objLojistaXCategoria.Categoria = retorno["Categoria"].ToString();
                        listaLojistaXCategoria.Add(objLojistaXCategoria);
                        objLojistaXCategoria = null;
                    }
                    retorno.Close();
                }
                return listaLojistaXCategoria;
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
                comando = null;
                listaLojistaXCategoria = null;
            }
        }
    }
}