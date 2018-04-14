using Imposto.Core.Business;
using Imposto.Core.Business.Interface;
using Imposto.Core.Domain;
using Imposto.Core.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Imposto.Core.Service
{
    public class NotaFiscalService : INotaFiscalService
    {
        private readonly INotaFiscalBusiness notaFiscalBusiness;

        /// <summary>
        /// Serviço para geração de notas fiscais
        /// </summary>
        /// <param name="notaFiscalBusiness"></param>
        public NotaFiscalService(NotaFiscalBusiness notaFiscalBusiness)
        {
            this.notaFiscalBusiness = notaFiscalBusiness;
        }

        /// <summary>
        /// Gera Nota Fiscal de novos pedidos.
        /// </summary>
        /// <param name="pedido">Pedido que será gerado a nota fiscal.</param>
        public NotaFiscal GerarNotaFiscal(Domain.Pedido pedido)
        {
            return notaFiscalBusiness.EmitirNotaFiscal(pedido);
        }
    }
}
