# ASP.NET-Core-WebAPI / REST API

CRUD rápido e básico em ASP.NET Core WebAPI
 


Protocolo HTTP: Conversar com o servidor atraves de REQUEST E RESPONSE ( Client faz um request ao Server que retorna um response )

Um REQUEST: Possui o Verbo HTTP : GET para obter, POST para inserir, PATH ou PUT para atualizar e DELETE para excluir
            Precisa informar a URI : qual o endereço da internet
            Versão: 1.1 
                         
            Conceito de Request HEADER  : É o cabeçalho da requisição (e passado a versão do broweser, cockier de autenticacao... )
            Conceito de Request MASSAGE : É passado as informações ( Dados do produto )
                       
Um RESPONSE: Códigos de status de respostas do servidor HTTP: 100 = informaçao 200 = ok , 300 = redirecionamente, 400 = não permitido, 500 = erro
 
            Conceito de Response HEADER  : Passar o cockie, token 
            Conceito de Response MASSAGE : Informar a resposta q vai ser exibida ( lista de produto, pagina html e etc..)
                        
 
 
ARQUITETURA REST : Envio de dados com base em REST - Estado de representação do dado

        CLIENTE -> DADOS -> HTTP -> SERVIDOR 

           1° CLIENTE = começa a transferir Dados
           2° DADOS   = vai na parte do body do verbo http, transmiti os dados atraves do protocolo htttp
           3° PROTOCOLO HTTP: possui o request q é uma ENVELOPE(mensagem) pois esta formatado padrao de HEADER e MASSAGE
           4° SERVIDOR: o servidor recebe a mensagem 
        
           Simplesmente pega um dado via texto, passa via texto e recebe via texto
           

Crud Básico e Rápido em ASP.NET Core usando Scafolding

            create a new project > ASP.NET Core Web Application
            .NET Core > ASP.NET Core 2.2 > API

Criar a pasta Model > botão direito sobre o projeto ( nova pasta)

            na pasta Model > criar a class FornecedorViewModel.cs 
            
FornecedorViewModel.cs

            public class FornecedorViewModel{
            
            [key]
            public Guid id {get; set;}
            
            public string Nome {get; set;}
            
            public int TipoFornecedor {get; set;}
            
            public bool Ativo {get; set;}
            
Pasta Model criar a class > ApiDbContext.cs

            public class ApiDbContext : DbContext{
            
Ir em Startup.cs dizendo q esta sendo usado a ApiDbContext.cs

            public void ConfigureServices(IServiceCollection services){
            
                 services.AddDbContext<ApiDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
                 
                 services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            }
            
            
                        
ApiDbContext.cs utilizar um construtor para passar as options necessaria, onde a class base vai receber as options
Mapear a viewModel usando dbset 


            public class ApiDbContext : DbContext{
            
            public ApiDbContext(DbContextOptions options) : base(options){
            
            }
            
            public DbSet<FornecedorViewModel> Fornecedores {get; set; }

            }
            
Apos escrito minha string de conexão no meu arquivo appsettings.json e trazê-lo para o meu arquivo de inicialização

            Add-Migration Initial
            
Definir uma conection string apontando para o banco local > AppSetting.json

            "ConnectionStrings" : {
            "DefaultConnection" : "Server=(localdb)\\mssqllocaldb;Database=MinhaApiCore;Trusted_Connection=True;MultipleActiveResultSets=true"
            }}           


Gerar a migration agora com a string de conexao criada e criar o banco > pagckage manage console
            
            Add-Migration Initial
            update-database
                        
Agora atraves do Scalfolding criar a Controller

            Botao direito na pasta Controllers > Add > New Controller > API Controller with actions, using Entity Framework
            
            Model Class        :  Fornecedo (MinhaAPICore.Model)
            Data COntext Class :  ApiDbContext(MinhaAPICore.Model)
            
            Nome da Class : ForncedoresController
            
           
facilitar o lançamento da url > Propties > lauchSetting.json
            
            "profiles": {
            "lauchUrl" : "api/fornecedores"
            
            "MinhaAPICore" : {
            "lauchUrl" : "api/fornecedores"

           
Fazer o primeiro cadastro via banco

            Sql Sever Object Explore > Localdb> database> MinhaApiCore > Tables >  botao direito>  dbo.Fornecdores > view data >
            Tools > Create Guid > New GUID > Registy Format > Copy
            
            dbb.fornecedores colar o GUID em id e preencher os demais campos (Nome, Documento, TipoFornecedor, Ativo )
            
            roda a api no brosew para testar o metodo get , devera aparecer a informações em formato de json :  https:localhost:44315//api/fornecedores
            
O que é um JSON e como funciona

            O JSON se baseia na notação NOME : VALOR, onde NOME pode ser o nome que você deseja usar para identificar um objeto
            e VALOR é o valor deste objeto.( Formatação utilizada p/ estruturar dados em formato de texto e transmiti-los de um sistema para outro )
            

Agora para testar o metodo post, put e delete e necessario usar o POSTMAN

            vou copiar minha url de fornecedores : https:localhost:44315//api/fornecedores
            colar na aba de busca GET no Postman e dar Send pra executar
            no postman consigo chamar meus metodos atraves dos verbos
            
            postman  > body do respnse > prety >  copiar os dados q veio no get e utilizar eles para fazer um post
            
            {
            "id" : "",
            "nome" : "",
            "documento : "",
            "tipoFornecedor" : "",
            "ativo : true
            }
            
Para testar o post apenas alterar o verbo da aba de busca de GET para POST

            POST > https:localhost:44315//api/fornecedores > SEND
            Body > raw > JSON(application/json) > colar o resultado do get no response acima
            
            vai ser trocado apenas o numero do id por outro qualquer 
            
            
            {
            "id" : "",
            "nome" : "",
            "documento : "",
            "tipoFornecedor" : "",
            "ativo : true
            }
            
            
Colocar um breakPoint no FornecedorController.cs

            linha 77 > Verbo Post  >  colocar um breakPoint
            voltar no post e dar um > SEND
            
fazer um PUT o mesmo procedimento, colocando um breakPoint no FornecedorController.cs
            
            linha 46 > Verbo PUT  >  colocar um breakPoint

Para testar o post apenas alterar o verbo da aba de busca de GET para POST

            mudar o vebo para put 

            vai ser trocado uma informação qualquer pra ter alteração            
            
            E agora alem da url dps do ultimo barra vai ser passado o id gerado nos verbos anteriores
            
            https:localhost:44315//api/fornecedores/321615646546461616164            
            

            {
            "id" : "",
            "nome" : "",
            "documento : "",
            "tipoFornecedor" : "",
            "ativo : true
            }            
            
            Dar um > SEND
            
Mudar o verbo para delete

            dar mesma forma feito no PUT vai ser passado o id posteriomente na url 
            
-------------------------

vector = a posição em um espaço
angulo = rotacao do objeto

1) importado projeto Astronomy em c#
2) Referenciei as dependência com os projetos Astronomy e DemoHelp
3) criado uma class ObterCalculo 
4) criado uma função com 2 vector para que me retorna-se o (valor) ângulo entre eles 

using System;

	namespace ObterCalculo
	{
	    public class ObterCalculoVector
	    {
		static void Main(string[] args)
		{
		    Console.WriteLine("teste");
		    AstroVector a = new AstroVector(10,10, 2, new AstroTime(10));
		    AstroVector b = new AstroVector(10, 10, 2, new AstroTime(10));

		    var angulo = Astronomy.AngleBetween(a, b);
		    Console.WriteLine(angulo);
		}
	    }
	}
