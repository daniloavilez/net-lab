using Imposto.Core.Business.Interface;
using Imposto.Core.Data;
using Imposto.Core.Data.Interface;
using Imposto.Core.Domain;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Imposto.Core.Business
{
    public class NotaFiscalBusiness : INotaFiscalBusiness
    {
        public NotaFiscalBusiness()
        {
        }

        public NotaFiscal EmitirNotaFiscal(Pedido pedido)
        {
            NotaFiscal notaFiscal = new NotaFiscal
            {
                NumeroNotaFiscal = 99999,
                Serie = new Random().Next(Int32.MaxValue),
                NomeCliente = pedido.NomeCliente,

                // TODO: Erro corrigido
                //EstadoDestino = pedido.EstadoOrigem,
                //EstadoOrigem = pedido.EstadoDestino

                EstadoDestino = pedido.EstadoDestino,
                EstadoOrigem = pedido.EstadoOrigem
            };

            PreencherItensDaNotaFiscal(pedido, notaFiscal);

            return notaFiscal;
        }

        private static void PreencherItensDaNotaFiscal(Pedido pedido, NotaFiscal notaFiscal)
        {
            foreach (PedidoItem itemPedido in pedido.ItensDoPedido)
            {
                NotaFiscalItem notaFiscalItem = new NotaFiscalItem();

                PreencherCfop(notaFiscal, notaFiscalItem);

                CalcularIcms(notaFiscal, itemPedido, notaFiscalItem);

                // TODO: Fazer os testes unitários
                CalcularIpi(notaFiscal, itemPedido, notaFiscalItem);

                VerificarDesconto(notaFiscal, notaFiscalItem);

                notaFiscalItem.NomeProduto = itemPedido.NomeProduto;
                notaFiscalItem.CodigoProduto = itemPedido.CodigoProduto;

                notaFiscal.ItensDaNotaFiscal.Add(notaFiscalItem);
            }
        }

        private static void VerificarDesconto(NotaFiscal notaFiscal, NotaFiscalItem notaFiscalItem)
        {
            if (notaFiscal.EstadoDestino == "SP" || notaFiscal.EstadoDestino == "RJ" ||
                notaFiscal.EstadoDestino == "MG")
            {
                notaFiscalItem.Desconto = 0.1;
            }
        }

        private static void CalcularIpi(NotaFiscal notaFiscal, PedidoItem itemPedido, NotaFiscalItem notaFiscalItem)
        {
            notaFiscalItem.BaseIpi = itemPedido.ValorItemPedido;
            AplicarRegraBrindeIpi(itemPedido, notaFiscalItem);
        }

        private static void CalcularIcms(NotaFiscal notaFiscal, PedidoItem itemPedido, NotaFiscalItem notaFiscalItem)
        {
            if (notaFiscal.EstadoDestino == notaFiscal.EstadoOrigem)
            {
                notaFiscalItem.TipoIcms = "60";
                notaFiscalItem.AliquotaIcms = 0.18;
            }
            else
            {
                notaFiscalItem.TipoIcms = "10";
                notaFiscalItem.AliquotaIcms = 0.17;
            }
            if (notaFiscalItem.Cfop == "6.009")
            {
                notaFiscalItem.BaseIcms = itemPedido.ValorItemPedido * 0.90; //redução de base
            }
            else
            {
                notaFiscalItem.BaseIcms = itemPedido.ValorItemPedido;
            }
            notaFiscalItem.ValorIcms = notaFiscalItem.BaseIcms * notaFiscalItem.AliquotaIcms;

            AplicarRegraBrindeIcms(itemPedido, notaFiscalItem);
        }

        private static void PreencherCfop(NotaFiscal notaFiscal, NotaFiscalItem notaFiscalItem)
        {
            if (notaFiscal.EstadoOrigem == "SP")
            {
                notaFiscalItem.Cfop = VerificarCfopsSP(notaFiscal.EstadoDestino);
            }
            else if (notaFiscal.EstadoOrigem == "MG")
            {
                notaFiscalItem.Cfop = VerificarCfopsMG(notaFiscal.EstadoDestino);
            }
        }

        private static string VerificarCfopsMG(string estadoDestino)
        {
            if (estadoDestino == "RJ")
            {
                 return "6.000";
            }
            else if (estadoDestino == "PE")
            {
                return "6.001";
            }
            else if (estadoDestino == "MG")
            {
                return "6.002";
            }
            else if (estadoDestino == "PB")
            {
                return "6.003";
            }
            else if (estadoDestino == "PR")
            {
                return "6.004";
            }
            else if (estadoDestino == "PI")
            {
                return "6.005";
            }
            else if (estadoDestino == "RO")
            {
                return "6.006";
            }
            else if (estadoDestino == "SE")
            {
                return "6.007";
            }
            else if (estadoDestino == "TO")
            {
                return "6.008";
            }
            else if (estadoDestino == "SE")
            {
                return "6.009";
            }
            else if (estadoDestino == "PA")
            {
                return "6.010";
            }

            return string.Empty;
        }

        private static string VerificarCfopsSP(string estadoDestino)
        {
            if (estadoDestino == "RJ")
            {
                return "6.000";
            }
            else if (estadoDestino == "PE")
            {
                return "6.001";
            }
            else if (estadoDestino == "MG")
            {
                return "6.002";
            }
            else if (estadoDestino == "PB")
            {
                return "6.003";
            }
            else if (estadoDestino == "PR")
            {
                return "6.004";
            }
            else if (estadoDestino == "PI")
            {
                return "6.005";
            }
            else if (estadoDestino == "RO")
            {
                return "6.006";
            }
            else if (estadoDestino == "SE")
            {
                return "6.007";
            }
            else if (estadoDestino == "TO")
            {
                return "6.008";
            }
            else if (estadoDestino == "SE")
            {
                return "6.009";
            }
            else if (estadoDestino == "PA")
            {
                return "6.010";
            }

            return string.Empty;
        }

        private static void AplicarRegraBrindeIcms(PedidoItem itemPedido, NotaFiscalItem notaFiscalItem)
        {
            if (itemPedido.Brinde)
            {
                notaFiscalItem.TipoIcms = "60";
                notaFiscalItem.AliquotaIcms = 0.18;
                notaFiscalItem.ValorIcms = notaFiscalItem.BaseIcms * notaFiscalItem.AliquotaIcms;
            }
        }

        private static void AplicarRegraBrindeIpi(PedidoItem itemPedido, NotaFiscalItem notaFiscalItem)
        {
            if (itemPedido.Brinde)
            {
                notaFiscalItem.ValorIpi = notaFiscalItem.BaseIpi * notaFiscalItem.AliquotaIpi;
            }
            else
            {
                notaFiscalItem.AliquotaIpi = 0.1;
                notaFiscalItem.ValorIpi = notaFiscalItem.BaseIpi * notaFiscalItem.AliquotaIpi;
            }
        }
    }
}
