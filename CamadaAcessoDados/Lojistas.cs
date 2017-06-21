using System;
using System.Data;
using System.Data.Common;
using System.Transactions;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Collections.Generic;

namespace Carvalho.QuePexinxa.CamadaAcessoDados
{
    public static class Lojistas
    {
        public static void Gravar(Carvalho.QuePexinxa.CamadaEntidades.Lojistas lojista, bool lojistaModificado)
        {
            Database conexao;
            DbCommand comando;
            int lojistaID;
            try
            {
                using (TransactionScope transaction = new TransactionScope())
                {
                    conexao = DatabaseFactory.CreateDatabase();
                    using (comando = conexao.GetStoredProcCommand("[dbo].[GravarLojista]"))
                    {
                        conexao.AddParameter(comando, "@LojistaID", DbType.Int32, ParameterDirection.InputOutput, String.Empty, DataRowVersion.Default, lojista.LojistaID);
                        conexao.AddInParameter(comando, "@Nome", DbType.String, lojista.Nome);
                        conexao.AddInParameter(comando, "@Endereco", DbType.String, lojista.Endereco);
                        conexao.AddInParameter(comando, "@DDD", DbType.String, lojista.DDD);
                        conexao.AddInParameter(comando, "@Telefone", DbType.String, lojista.Telefone);
                        conexao.ExecuteNonQuery(comando);
                        lojistaID = Convert.ToInt32(conexao.GetParameterValue(comando, "@LojistaID"));
                    }

                    //Verifica se houve alterações
                    if (lojistaModificado)
                    {
                        //Excluir todos os trechos
                        Carvalho.QuePexinxa.CamadaAcessoDados.LojistaXCategoria.Excluir(lojistaID, conexao);

                        //add trecho
                        for (int i = 0; i < lojista.ListaLojistaXCategoria.Count; i++)
                        {
                            //add valor SolicitacaoViagemID
                            lojista.ListaLojistaXCategoria[i].LojistaID = lojistaID;
                            Carvalho.QuePexinxa.CamadaAcessoDados.LojistaXCategoria.Gravar(lojista.ListaLojistaXCategoria[i], conexao);
                        }
                    }
                    transaction.Complete();
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

        public static void Excluir(int lojistaID, Database _conexao)
        {
            DbCommand _comando;
            try
            {
                using (_comando = _conexao.GetStoredProcCommand("[dbo].[ExcluirLojista]"))
                {
                    _conexao.AddInParameter(_comando, "@LojistaID", DbType.Int32, lojistaID);
                    _conexao.ExecuteNonQuery(_comando);
                }
            }
            catch (DbException ex)
            {
                throw new ApplicationException("Erro ao excluir o registro", ex);
            }
            finally
            {
                _comando = null;
            }
        }

        public static Carvalho.QuePexinxa.CamadaEntidades.Lojistas Selecionar(Carvalho.QuePexinxa.CamadaEntidades.Lojistas lojista, Database conexao)
        {
            DbCommand comando;
            IDataReader retorno;
            try
            {
                using (comando = conexao.GetStoredProcCommand("[dbo].[SelecionarLojista]"))
                {
                    conexao.AddInParameter(comando, "@LojistaID", DbType.Int32, lojista.LojistaID);
                    retorno = conexao.ExecuteReader(comando);

                    if (retorno.Read())
                    {
                        lojista.LojistaID = Convert.ToInt32(retorno["LojistaID"]);
                        lojista.Nome = retorno["Nome"].ToString();
                        lojista.Endereco = retorno["Endereco"].ToString();
                        lojista.DDD = retorno["DDD"].ToString();
                        lojista.Telefone = retorno["Telefone"].ToString();
                        lojista.ListaLojistaXCategoria = Carvalho.QuePexinxa.CamadaAcessoDados.LojistaXCategoria.Listar(lojista.LojistaID, conexao);
                    }
                    retorno.Close();
                }
                return lojista;
            }
            catch (DbException ex)
            {
                throw new ApplicationException("Erro ao selecionar o registro", ex);
            }
            catch (IndexOutOfRangeException ex)
            {
                throw new ApplicationException("Erro ao listar os registros", ex);
            }
            finally
            {
                retorno = null;
                comando = null;
            }
        }

        public static List<Carvalho.QuePexinxa.CamadaEntidades.Lojistas> Listar(Database conexao)
        {
            List<Carvalho.QuePexinxa.CamadaEntidades.Lojistas> listaLojista = new List<Carvalho.QuePexinxa.CamadaEntidades.Lojistas>();
            DbCommand comando;
            IDataReader retorno;
            try
            {
                using (comando = conexao.GetStoredProcCommand("[dbo].[ListarLojistas]"))
                {
                    retorno = conexao.ExecuteReader(comando);

                    while (retorno.Read())
                    {
                        Carvalho.QuePexinxa.CamadaEntidades.Lojistas objLojista = new Carvalho.QuePexinxa.CamadaEntidades.Lojistas();
                        objLojista.LojistaID = Convert.ToInt32(retorno["LojistaID"]);
                        objLojista.Nome = retorno["Nome"].ToString();
                        objLojista.Endereco = retorno["Endereco"].ToString();
                        objLojista.DDD = retorno["DDD"].ToString();
                        objLojista.Telefone = retorno["Telefone"].ToString();
                        objLojista.Quantidade = Convert.ToInt32(retorno["Quantidade"]);
                        listaLojista.Add(objLojista);
                        objLojista = null;
                    }
                    retorno.Close();
                }
                return listaLojista;
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
                listaLojista = null;
                comando = null;
                retorno = null;
            }
        }
    }
}