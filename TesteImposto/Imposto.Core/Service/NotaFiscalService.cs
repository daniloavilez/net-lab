using Imposto.Core.Business;
using Imposto.Core.Business.Interface;
using Imposto.Core.Data;
using Imposto.Core.Data.Interface;
using Imposto.Core.Domain;
using Imposto.Core.Service.Interface;
using Imposto.Core.Util;
using Imposto.Core.Util.Interface;
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
        private readonly IImpostoUtil impostoUtil;
        private readonly INotaFiscalRepository notaFiscalRepository;

        /// <summary>
        /// Construtor serviço nota fiscal
        /// </summary>
        /// <param name="notaFiscalBusiness">Objeto de negócio</param>
        /// <param name="impostoUtil">Objeto utilitário</param>
        /// <param name="notaFiscalRepository">Repositório de dados</param>
        public NotaFiscalService(NotaFiscalBusiness notaFiscalBusiness, ImpostoUtil impostoUtil,
            NotaFiscalRepository notaFiscalRepository)
        {
            this.notaFiscalBusiness = notaFiscalBusiness;
            this.impostoUtil = impostoUtil;
            this.notaFiscalRepository = notaFiscalRepository;
        }

        /// <summary>
        /// Gera Nota Fiscal de novos pedidos.
        /// </summary>
        /// <param name="pedido">Pedido que será gerado a nota fiscal.</param>
        /// <returns>Retorna se a nota fiscal foi gerada com sucesso.</returns>
        public bool GerarNotaFiscal(Domain.Pedido pedido)
        {
            try
            {
                NotaFiscal notaFiscal = notaFiscalBusiness.EmitirNotaFiscal(pedido);

                if (impostoUtil.GerarNotaFiscalEmXml(notaFiscal))
                {
                    notaFiscalRepository.AdicionarNotaFiscalEItens(notaFiscal);
                }

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
