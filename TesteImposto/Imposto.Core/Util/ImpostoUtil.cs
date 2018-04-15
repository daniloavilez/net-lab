using Imposto.Core.Domain;
using Imposto.Core.Util.Interface;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Imposto.Core.Util
{
    public class ImpostoUtil : IImpostoUtil
    {
        private const string PATH = @"C:\NotasFiscais\";
        private readonly string[] estadosValidos = {"SP", "RJ", "MG", "RO", "PE", "PB", "PR", "PI",
            "SE", "TO", "PA"};

        public virtual bool GerarNotaFiscalEmXml(NotaFiscal notaFiscal)
        {
            try
            {

                if (!Directory.Exists(PATH))
                {
                    CriarDiretorioESetarPermissoes();
                }

                // TODO: Salvar como outro usuário
                XmlSerializer serializer = new XmlSerializer(typeof(NotaFiscal));
                using (TextWriter writer = new StreamWriter(PATH + "NF_" + notaFiscal.NumeroNotaFiscal + ".xml"))
                {
                    serializer.Serialize(writer, notaFiscal);
                }

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private static void CriarDiretorioESetarPermissoes()
        {
            DirectoryInfo directory = Directory.CreateDirectory(PATH);

            DirectorySecurity security = directory.GetAccessControl();

            security.AddAccessRule(new FileSystemAccessRule(Environment.UserDomainName + "\\" + Environment.UserName,
                                    FileSystemRights.ReadAndExecute,
                                    AccessControlType.Deny));

            directory.SetAccessControl(security);
        }

        public List<string> ValidarCampos(string estadoOrigem, string estadoDestino, string nomeCliente)
        {
            var list = new List<string>();

            if (string.IsNullOrEmpty(nomeCliente))
            {
                list.Add("O Nome do cliente deve ser preenchido.");
            }

            if (string.IsNullOrEmpty(estadoOrigem) || string.IsNullOrEmpty(estadoDestino))
            {
                list.Add("Os dois estados devem ser preenchidos.");
            }

            if (estadoOrigem == estadoDestino)
            {
                list.Add("Estados não podem ser iguais.");
            }

            if (!estadosValidos.Contains(estadoOrigem))
            {
                list.Add("Estado origem não é válido.");
            }

            if (!estadosValidos.Contains(estadoDestino))
            {
                list.Add("Estado destino não é válido.");
            }

            return list;
        }
    }
}
