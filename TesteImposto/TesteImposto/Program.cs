using Imposto.Core.Business;
using Imposto.Core.Business.Interface;
using Imposto.Core.Data;
using Imposto.Core.Data.Interface;
using Imposto.Core.Service;
using Imposto.Core.Service.Interface;
using Imposto.Core.Util;
using Imposto.Core.Util.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Unity;

namespace TesteImposto
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            UnityContainer container = new UnityContainer();
            container.RegisterType<INotaFiscalBusiness, NotaFiscalBusiness>()
                .RegisterType<INotaFiscalService, NotaFiscalService>()
                .RegisterType<INotaFiscalRepository, NotaFiscalRepository>()
                .RegisterType<IImpostoUtil, ImpostoUtil>();

            var service = container.Resolve<INotaFiscalService>();
            var util = container.Resolve<IImpostoUtil>();

            Application.Run(new FormImposto(service, util));
        }
    }
}
