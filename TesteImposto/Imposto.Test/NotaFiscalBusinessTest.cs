using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Imposto.Core.Domain;
using System.Collections.Generic;
using Imposto.Core.Business;

namespace Imposto.Test
{
    [TestClass]
    public class NotaFiscalBusinessTest
    {
        private NotaFiscalBusiness notaFiscalBusiness;

        public NotaFiscalBusinessTest()
        {
            notaFiscalBusiness = new NotaFiscalBusiness();
        }

        [TestMethod]
        public void EstadoOrigemSP_E_DestinoRO_Cfop6006()
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

            // Act
            NotaFiscal notaFiscal = notaFiscalBusiness.EmitirNotaFiscal(pedido);

            // Assert
            Assert.AreEqual("6.006", notaFiscal.ItensDaNotaFiscal[0].Cfop);
        }

        [TestMethod]
        public void CalculoIPI_SemBrinde()
        {
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

            NotaFiscal notaFiscal = notaFiscalBusiness.EmitirNotaFiscal(pedido);

            Assert.AreEqual(800, notaFiscal.ItensDaNotaFiscal[0].BaseIpi);
            Assert.AreEqual(0.1, notaFiscal.ItensDaNotaFiscal[0].AliquotaIpi);
            Assert.AreEqual(80, notaFiscal.ItensDaNotaFiscal[0].ValorIpi);
        }

        [TestMethod]
        public void CalculoIPI_ComBrinde()
        {
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
                        Brinde = true
                    }
                },
                NomeCliente = "Danilo"
            };

            NotaFiscal notaFiscal = notaFiscalBusiness.EmitirNotaFiscal(pedido);

            Assert.AreEqual(800, notaFiscal.ItensDaNotaFiscal[0].BaseIpi);
            Assert.AreEqual(0, notaFiscal.ItensDaNotaFiscal[0].AliquotaIpi);
            Assert.AreEqual(0, notaFiscal.ItensDaNotaFiscal[0].ValorIpi);
        }

        [TestMethod]
        public void Desconto_EstadoDestinoSudeste()
        {
            Pedido pedido = new Pedido()
            {
                EstadoDestino = "RJ",
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

            NotaFiscal notaFiscal = notaFiscalBusiness.EmitirNotaFiscal(pedido);

            Assert.AreEqual(0.1, notaFiscal.ItensDaNotaFiscal[0].Desconto);
        }

        [TestMethod]
        public void EstadoOrigem_Pedido_Igual_EstadoOrigem_NotaFiscal()
        {
            Pedido pedido = new Pedido()
            {
                EstadoDestino = "RJ",
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

            NotaFiscal notaFiscal = notaFiscalBusiness.EmitirNotaFiscal(pedido);

            Assert.AreEqual(pedido.EstadoOrigem, notaFiscal.EstadoOrigem);
        }

        [TestMethod]
        public void EstadoDestino_Pedido_Igual_EstadoDestino_NotaFiscal()
        {
            Pedido pedido = new Pedido()
            {
                EstadoDestino = "RJ",
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

            NotaFiscal notaFiscal = notaFiscalBusiness.EmitirNotaFiscal(pedido);

            Assert.AreEqual(pedido.EstadoDestino, notaFiscal.EstadoDestino);
        }

        [TestCleanup]
        public void Cleanup()
        {
            notaFiscalBusiness = null;
        }
    }
}
