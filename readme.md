# Teste Imposto - Refatoração

## Objetivo

O objetivo da tela é o faturamento de itens criados pela indústria.

> São duas as principais hipóteses de ocorrência do fato gerador do IPI:
>1. Na importação: o desembaraço aduaneiro de produtos de procedência estrangeira;
>2. Na operação interna: a saída de produto de estabelecimento industrial, ou equiparado a industrial.

Gerando assim uma nota fiscal, com os devidos cálculos dos impostos e descontos.

## Atividades Desenvolvidas

- Método EmitirNotaFiscal foi transferido para a nova camada Business saindo da camada Domain (SOLID)
- Classes `NotaFiscal` e `NotaFiscalItem` recebeu anotação `Serializable` afim de facilitar a serialização dos objetos para XML
- Foi criado uma nova camada Util para validação dos campos do Windows Form e geração do XML da NF
- Foi inserido novos campos na tabela NotaFiscalItem e inserido novas regras na camada Business
- Alguns Bugs foram resolvidos
- Testes unitários da camada Business e Service foram criados
- Foi inserido Injeção de Dependência e Inversão de Controle para realizar Mocks nos testes unitários
- Criado nova procedure `P_SUM_ICMS_IPI_BY_CFOP` retornando o layout:
> CFOP | Valor Total da Base de ICMS | Valor Total do ICMS | Valor Total da Base de IPI | Valor Total do IPI
- Utilizado `SqlTransaction` para encadear a gravação da nota fiscal e seus itens (Banco de Dados Relacional)
- `DropDownLists` foram adicionados para Estado origem e Estado Destino no Windows Form

---

## Injeção de Dependência e Inversão de Controle

- Inserção de [Unity](https://github.com/unitycontainer/unity) para Injeção de Dependência e Inversão de Controle
- Facilitando os testes unitários feito durante a refatoração

```C#
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
```
- As classes agora recebem as suas dependências da seguinte forma:

```C#
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
```

## Testes Unitários

- Os testes unitários utilizaram [moq4](https://github.com/moq/moq4) com as dependências das classes testadas

```C#
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
```