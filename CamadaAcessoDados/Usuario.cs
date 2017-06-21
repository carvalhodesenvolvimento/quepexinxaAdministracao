using System;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace Carvalho.QuePexinxa.CamadaAcessoDados
{
    public static class Usuario
    {
        public static Carvalho.QuePexinxa.CamadaEntidades.UsuarioSessao Selecionar(Carvalho.QuePexinxa.CamadaEntidades.Usuario usuario, Database conexao)
        {
            DbCommand comando;
            IDataReader retorno;
            Carvalho.QuePexinxa.CamadaEntidades.UsuarioSessao usuarioSessao = new Carvalho.QuePexinxa.CamadaEntidades.UsuarioSessao();
            try
            {
                using (comando = conexao.GetStoredProcCommand("[dbo].[SelecionarUsuarioLogin]"))
                {
                    conexao.AddInParameter(comando, "@Nome", DbType.String, usuario.Nome);
                    conexao.AddInParameter(comando, "@Senha", DbType.String, usuario.Senha);
                    retorno = conexao.ExecuteReader(comando);

                    if (retorno.Read())
                    {
                        usuarioSessao.Conexao = conexao;
                        usuarioSessao.UsuarioID = Convert.ToInt32(retorno["UsuarioID"]);
                        usuarioSessao.Nome = retorno["Nome"].ToString();
                        usuarioSessao.NivelUsuarioID = Convert.ToInt32(retorno["NivelUsuarioID"]);
                        usuarioSessao.LojistaID = Convert.ToInt32(retorno["LojistaID"]);
                        usuarioSessao.Lojista = retorno["Lojista"].ToString();
                    }
                    retorno.Close();
                }
                return usuarioSessao;
            }
            catch (DbException ex)
            {
                throw new ApplicationException("Erro ao selecionar o registro", ex);
            }
            finally
            {
                comando = null;
                retorno = null;
                usuarioSessao = null;
            }
        }

        public static int AlterarSenha(Carvalho.QuePexinxa.CamadaEntidades.Usuario usuario, Database conexao)
        {
            DbCommand comando;
            int retorno;
            try
            {
                using (comando = conexao.GetStoredProcCommand("[dbo].[AlterarSenhaUsuario]"))
                {
                    conexao.AddParameter(comando, "@retorno", DbType.Int32, ParameterDirection.InputOutput, String.Empty, DataRowVersion.Default, 0);
                    conexao.AddInParameter(comando, "@UsuarioID", DbType.Int32, usuario.UsuarioID);
                    conexao.AddInParameter(comando, "@SenhaAntiga", DbType.String, usuario.Senha);
                    conexao.AddInParameter(comando, "@SenhaNova", DbType.String, usuario.SenhaNova);
                    conexao.ExecuteNonQuery(comando);
                    retorno = Convert.ToInt32(conexao.GetParameterValue(comando, "@retorno"));
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
    }
}