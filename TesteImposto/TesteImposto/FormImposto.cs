using Imposto.Core.Service;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Imposto.Core.Domain;
using Imposto.Core.Service.Interface;
using Imposto.Core.Data.Interface;
using Imposto.Core.Util.Interface;

namespace TesteImposto
{
    public partial class FormImposto : Form
    {
        private Pedido pedido = new Pedido();
        private readonly INotaFiscalService notaFiscalService;
        private readonly IImpostoUtil impostoUtil;

        public FormImposto(INotaFiscalService notaFiscalService, 
            IImpostoUtil impostoUtil)
        {
            InitializeComponent();
            dataGridViewPedidos.AutoGenerateColumns = true;
            dataGridViewPedidos.DataSource = GetTablePedidos();
            ResizeColumns();

            this.notaFiscalService = notaFiscalService;
            this.impostoUtil = impostoUtil;
        }

        public FormImposto()
        {
            
        }

        private void ResizeColumns()
        {
            double mediaWidth = dataGridViewPedidos.Width / dataGridViewPedidos.Columns.GetColumnCount(DataGridViewElementStates.Visible);

            for (int i = dataGridViewPedidos.Columns.Count - 1; i >= 0; i--)
            {
                var coluna = dataGridViewPedidos.Columns[i];
                coluna.Width = Convert.ToInt32(mediaWidth);
            }   
        }

        private object GetTablePedidos()
        {
            DataTable table = new DataTable("pedidos");
            table.Columns.Add(new DataColumn("Nome do produto", typeof(string)));
            table.Columns.Add(new DataColumn("Codigo do produto", typeof(string)));
            table.Columns.Add(new DataColumn("Valor", typeof(decimal)));
            table.Columns.Add(new DataColumn("Brinde", typeof(bool)));
                     
            return table;
        }

        private void buttonGerarNotaFiscal_Click(object sender, EventArgs e)
        {
            DataTable table = (DataTable)dataGridViewPedidos.DataSource;

            if (ValidarCampos() || ValidarItens(table))
            {
                pedido.EstadoOrigem = txtEstadoOrigem.Text.ToUpper();
                pedido.EstadoDestino = txtEstadoDestino.Text.ToUpper();
                pedido.NomeCliente = textBoxNomeCliente.Text;
                
                foreach (DataRow row in table.Rows)
                {
                    pedido.ItensDoPedido.Add(
                        new PedidoItem()
                        {
                            // TODO: Erro Corrigido
                            Brinde = Convert.ToBoolean(row["Brinde"] == DBNull.Value ? default(bool) : row["Brinde"]),
                            CodigoProduto = row["Codigo do produto"].ToString(),
                            NomeProduto = row["Nome do produto"].ToString(),
                            ValorItemPedido = Convert.ToDouble(row["Valor"].ToString())
                        });
                }

                bool notaFiscalGerada = notaFiscalService.GerarNotaFiscal(pedido);

                if (notaFiscalGerada)
                {
                    txtEstadoDestino.Text = "";
                    txtEstadoOrigem.Text = "";
                    textBoxNomeCliente.Text = "";
                    dataGridViewPedidos.AutoGenerateColumns = true;
                    dataGridViewPedidos.DataSource = GetTablePedidos();
                    ResizeColumns();

                    MessageBox.Show("Operação efetuada com sucesso");
                }
                else
                {
                    MessageBox.Show("Houve algum erro contate os desenvolvedores");
                }
            }
        }

        private bool ValidarItens(DataTable table)
        {
            if (table.Rows.Count == 0)
            {
                MessageBox.Show("Ao menos um item deve ser inserido");
                return false;
            }

            foreach (DataRow row in table.Rows)
            {
                if (string.IsNullOrEmpty(row["Codigo do produto"].ToString()))
                {
                    MessageBox.Show("Código do produto deve ser preenchido.");
                    return false;
                }

                if (string.IsNullOrEmpty(row["Nome do produto"].ToString()))
                {
                    MessageBox.Show("Nome do produto deve ser preenchido.");
                    return false;
                }
            }

            return true;
        }

        private bool ValidarCampos()
        {
            bool flagValidado = true;

            foreach (string item in impostoUtil
                .ValidarCampos(txtEstadoOrigem.Text.ToUpper(), 
                    txtEstadoDestino.Text.ToUpper(), textBoxNomeCliente.Text))
            {
                MessageBox.Show(item);
                flagValidado = false;
            }

            return flagValidado;
        }
    }
}
