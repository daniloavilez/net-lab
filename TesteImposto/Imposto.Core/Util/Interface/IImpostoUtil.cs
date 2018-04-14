using Imposto.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Imposto.Core.Util.Interface
{
    public interface IImpostoUtil
    {
        bool GerarNotaFiscalEmXml(NotaFiscal notaFiscal);
        List<string> ValidarCampos(string estadoOrigem, string estadoDestino, string nomeCliente);
    }
}
