using Imposto.Core.Business;
using Imposto.Core.Business.Interface;
using Imposto.Core.Data;
using Imposto.Core.Domain;
using Imposto.Core.Service;
using Imposto.Core.Util;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Imposto.Test
{
    [TestClass]
    public class NotaFiscalServiceTest
    {
        private NotaFiscalService notaFiscalService;

        public NotaFiscalServiceTest()
        {
            
        }

        [TestMethod]
        public void NotaFiscal_Gerada_Com_Sucesso()
        {
            // Arrange
            Pedido pedido = new Pedido()
            {
                EstadoDestino = "RO",
                EstadoOrigem = "SP",
                ItensDoPedido = new List<PedidoItem>()
                {
                    new PedidoItem()
                    {
                        NomeProduto = "Brand new product",
                        CodigoProduto = "12312-123123",
                        ValorItemPedido = 800,
                        Brinde = false
                    }
                },
                NomeCliente = "Danilo"
            };

            var notaFiscalBusiness = new Mock<NotaFiscalBusiness>();
            var impostoUtil = new Mock<ImpostoUtil>();
            var notaFiscalRepository = new Mock<NotaFiscalRepository>();

            impostoUtil.Setup(c => c.GerarNotaFiscalEmXml(It.IsAny<NotaFiscal>()))
                .Returns(true);

            notaFiscalRepository.Setup(c => c.AdicionarNotaFiscalEItens(It.IsAny<NotaFiscal>()));

            notaFiscalService = new NotaFiscalService(notaFiscalBusiness.Object, 
                impostoUtil.Object, notaFiscalRepository.Object);

            // Act
            bool notaFiscalGerada = notaFiscalService.GerarNotaFiscal(pedido);

            // Assert
            Assert.IsTrue(notaFiscalGerada);

        }

        [TestMethod]
        public void NotaFiscal_Gerada_Com_Erro()
        {
            // Arrange
            Pedido pedido = new Pedido()
            {
                EstadoDestino = "RO",
                EstadoOrigem = "SP",
                ItensDoPedido = new List<PedidoItem>()
                {
                    new PedidoItem()
                    {
                        NomeProduto = "Brand new product",
                        CodigoProduto = "12312-123123",
                        ValorItemPedido = 800,
                        Brinde = false
                    }
                },
                NomeCliente = "Danilo"
            };

            var notaFiscalBusiness = new Mock<NotaFiscalBusiness>();
            var impostoUtil = new Mock<ImpostoUtil>();
            var notaFiscalRepository = new Mock<NotaFiscalRepository>();

            impostoUtil.Setup(c => c.GerarNotaFiscalEmXml(It.IsAny<NotaFiscal>()))
                .Returns(true);

            notaFiscalRepository.Setup(c => c.AdicionarNotaFiscalEItens(It.IsAny<NotaFiscal>()))
                .Throws<TimeoutException>();

            notaFiscalService = new NotaFiscalService(notaFiscalBusiness.Object,
                impostoUtil.Object, notaFiscalRepository.Object);

            // Act
            bool notaFiscalGerada = notaFiscalService.GerarNotaFiscal(pedido);

            // Assert
            Assert.IsFalse(notaFiscalGerada);

        }
    }
}
