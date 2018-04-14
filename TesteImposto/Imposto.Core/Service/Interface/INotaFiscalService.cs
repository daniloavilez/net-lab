using Imposto.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Imposto.Core.Service.Interface
{
    public interface INotaFiscalService
    {
        NotaFiscal GerarNotaFiscal(Domain.Pedido pedido);
    }
}
