using Imposto.Core.Data.Interface;
using Imposto.Core.Domain;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Imposto.Core.Data
{
    public class NotaFiscalRepository : INotaFiscalRepository
    {
        public NotaFiscalRepository()
        {

        }

        public virtual void AdicionarNotaFiscalEItens(NotaFiscal notaFiscal)
        {
            try
            {
                using (SqlConnection conexao = new SqlConnection(ConfigurationManager.ConnectionStrings["testeConnectionString"].ConnectionString))
                {
                    conexao.Open();

                    using (SqlTransaction transacao = conexao.BeginTransaction())
                    {
                        try
                        {
                            int idNotaFiscal = AdicionarNotaFiscal(notaFiscal, conexao, transacao);
                            AdicionarItensNotaFiscal(notaFiscal, conexao, idNotaFiscal, transacao);

                            transacao.Commit();
                        }
                        catch (Exception)
                        {
                            transacao.Rollback();
                            throw;
                        }

                    }
                }
            }
            catch(Exception)
            {
                throw;
            }
        }

        private static void AdicionarItensNotaFiscal(NotaFiscal notaFiscal, SqlConnection conexao, int idNotaFiscal, SqlTransaction transacao)
        {
            foreach (NotaFiscalItem item in notaFiscal.ItensDaNotaFiscal)
            {
                SqlCommand comandoItens = new SqlCommand("P_NOTA_FISCAL_ITEM", conexao, transacao);

                comandoItens.CommandType = CommandType.StoredProcedure;

                comandoItens.Parameters.AddWithValue("@pId", item.Id);
                comandoItens.Parameters.AddWithValue("@pIdNotaFiscal", idNotaFiscal);
                comandoItens.Parameters.AddWithValue("@pCfop", item.Cfop);
                comandoItens.Parameters.AddWithValue("@pTipoIcms", item.TipoIcms);
                comandoItens.Parameters.AddWithValue("@pBaseIcms", item.BaseIcms);
                comandoItens.Parameters.AddWithValue("@pAliquotaIcms", item.AliquotaIcms);
                comandoItens.Parameters.AddWithValue("@pValorIcms", item.ValorIcms);
                comandoItens.Parameters.AddWithValue("@pNomeProduto", item.NomeProduto);
                comandoItens.Parameters.AddWithValue("@pCodigoProduto", item.CodigoProduto);
                comandoItens.Parameters.AddWithValue("@pBaseIpi", item.BaseIpi);
                comandoItens.Parameters.AddWithValue("@pAliquotaIpi", item.AliquotaIpi);
                comandoItens.Parameters.AddWithValue("@pValorIpi", item.ValorIpi);
                comandoItens.Parameters.AddWithValue("@pDesconto", item.Desconto);

                comandoItens.ExecuteNonQuery();
            }
        }

        private static int AdicionarNotaFiscal(NotaFiscal notaFiscal, SqlConnection conexao, SqlTransaction transacao)
        {
            SqlCommand comando = new SqlCommand("P_NOTA_FISCAL", conexao, transacao);

            comando.CommandType = CommandType.StoredProcedure;

            SqlParameter outputIdParam = new SqlParameter("@pId", SqlDbType.Int)
            {
                Direction = ParameterDirection.InputOutput,
                Value = notaFiscal.Id
            };
            comando.Parameters.Add(outputIdParam);

            comando.Parameters.AddWithValue("@pNumeroNotaFiscal", notaFiscal.NumeroNotaFiscal);
            comando.Parameters.AddWithValue("@pSerie", notaFiscal.Serie);
            comando.Parameters.AddWithValue("@pNomeCliente", notaFiscal.NomeCliente);
            comando.Parameters.AddWithValue("@pEstadoDestino", notaFiscal.EstadoDestino);
            comando.Parameters.AddWithValue("@pEstadoOrigem", notaFiscal.EstadoOrigem);

            comando.ExecuteNonQuery();

            int idNotaFiscal = outputIdParam.Value as int? ?? default(int);
            return idNotaFiscal;
        }
    }
}
