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
            
            [Required(ErrorMessage = " O campo {0} é obrigatório")]
            [StringLength(100, ErroMessage = " O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 2)]
            
            [Required(ErroMessage = " O campo {0} e obrigatorio")]
            [StringLength(14, ErrorMessage = " O campo {0} precisa ter entre {2} e {1} caracteres", MinimunLength = 11)]
            
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
            
            
            
sobre a controller q foi criada

            criado um context , onde ele foi injetado via construtor
            
            no verbo Get temos uma ActionResult que retorna um IEnumerable de <Fornecedor>
                Um action result é o tipo de retorno de um método de um controller, ou melhor, o tipo de retorno de uma
                action.( retorna models para views )
                
                IEnumerable é uma interface que define um único método GetEnumerator() que retorna uma interface IEnumerator. 
                É a interface base para todas as coleções não genéricas que podem ser enumeradas
                
            é e uma TASK por esta utilizando metodos assincronos
                São métodos que podem executar assincronamente, ou seja, quem chamou não precisa esperar por sua execução e ela pode continuar                               normalmente sem bloquear a aplicação, assim quando o método assíncrono chamado termina ele pode voltar para o ponto em que foi
                chamado e dar continuidade ao que estava fazendo
                
            No id temos o GetFornecedor passando um Guid onde o metodo(FindAsync) vai buscar o fornecedor pelo id
            se o fornecedor não existir ele retorna NotFound, em outros caso retorna o proprio fornecedor
            
            No metodo put temos o PutFornecedor passando um Guid e o fornecedor
            se o id não existir ele retorna BadResquest, em outros caso retorna o NoContext ( 204 )
             
            No metodo Post ele vai receber um fornecedor e vai retornar um CreateAtAction GetFornecedor
            passando os values (id, fornecedor ) 
            
            No metodo delete ele vai receber um Guid e retornar uma Task de ActionResult
            usar o metodo FindAsync para obter o fornecedor pelo id, caso não consiga ele 
            retornar NotFound 
            caso ao contrario ele vai remover o fornecedor, salvar no banco e remover o fornecedor recebido
            
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


1) importe o projeto Astronomy em c#
2) criei um projeto ObeterCalculo
2) Referenciei as depedencia do projeto ObeterCalculo com o projeto Astronomy e DemoHelp
3) criei a class ObterCalculo 
4) defini um namespace 
5) criei 2 vector para retorna um angulo entre os dois vectores

	5.1) public static void Main(string[] args){
	public: isso significa que você pode chamar este método de fora da classe em que você está atualmente
	static: ele tem que ter método estático para permitir a invocação da classe.
	string[] args : são os argumentos do tipo String que o aplicativo C# aceita quando você executá-lo.

	5.2) AstroVector a = new AstroVector(10, 10, 2, new AstroTime(10)); 
	
	criado o objeto AstroVector = um vecto de 3 ponto em um plano cartesiano
	declarei a variavel "a" representando o objeto AstroVector 
	
	// new AstroVector(x, y, z, new Tempo) / horizantal, vertical, profundiade new TEMPO 
	
	
	new = instanciando o objeto dando o mesmo nome da class AstroVector 
	AstroVector.AngleBetween = instancia a class AstroVector.acessando a função AngleBetwen responsavel
	por fazer o calculo do angulo entre os 2 vectores e retornar o valor do angulo

	console.writeline( mostrando o resultado dessa variavle ) 
	valores 

            
