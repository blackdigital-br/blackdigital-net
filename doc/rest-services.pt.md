# Sistema de Geração Automática de Serviços REST

## Introdução

O BlackDigital.Rest é um sistema inovador que permite a criação automática de clientes REST através de interfaces decoradas com atributos. Em vez de escrever manualmente todas as chamadas HTTP, você define uma interface com os métodos desejados e o sistema gera automaticamente todo o código necessário para realizar as requisições.

## Conceitos Básicos

### Como Funciona

O sistema utiliza **Reflection** e **Expression Trees** para analisar interfaces decoradas com atributos especiais e gerar automaticamente:
- URLs das requisições
- Cabeçalhos HTTP
- Corpo das requisições (body)
- Parâmetros de consulta (query parameters)
- Parâmetros de rota (path parameters)

### Componentes Principais

1. **RestService<T>**: Classe principal que executa as chamadas
2. **RestClient**: Cliente HTTP que realiza as requisições
3. **Atributos**: Decoradores que definem o comportamento das chamadas
4. **RestCallConfig**: Configurações adicionais para as chamadas

## Exemplo Básico

Vamos começar with um exemplo simples de como criar um serviço REST:

### 1. Definindo a Interface

```csharp
using BlackDigital.Rest;

[Service("api/users")]
public interface IUserService
{
    [Action]
    Task<List<User>> GetAllUsers();
    
    [Action("{id}")]
    Task<User> GetUserById([Path] int id);
}
```

### 2. Usando o Serviço

```csharp
// Configurando o cliente
var restClient = new RestClient("https://api.exemplo.com");
var userService = new RestService<IUserService>(restClient);

// Fazendo as chamadas
var users = await userService.CallAsync(s => s.GetAllUsers());
var user = await userService.CallAsync(s => s.GetUserById(123));
```

## Atributos Disponíveis

### [Service] - Definição do Serviço

Define a rota base e configurações globais do serviço.

```csharp
[Service("api/users", authorize: true, version: "2025-10-08")]
public interface IUserService
{
    // métodos...
}
```

**Parâmetros:**
- `baseRoute`: Rota base do serviço
- `authorize`: Se requer autorização (padrão: false)
- `version`: Versão da API no formato de data (ex: "2025-10-08"). Esta versão é automaticamente enviada no header HTTP `x-api-version` para que o backend possa tratar diferentes versões da API quando necessário

### [Action] - Definição da Ação

Define o comportamento específico de cada método.

```csharp
[Action("search", method: RestMethod.Post, authorize: false)]
Task<List<User>> SearchUsers([Body] SearchRequest request);
```

**Parâmetros:**
- `route`: Rota específica da ação
- `method`: Método HTTP (Get, Post, Put, Delete, Patch)
- `authorize`: Se requer autorização
- `returnIsSuccess`: Se retorna apenas sucesso/falha
- `version`: Versão específica da ação no formato de data (ex: "2025-10-08"). Sobrescreve a versão definida no [Service]

### [Path] - Parâmetros de Rota

Marca parâmetros que serão inseridos na URL.

```csharp
[Action("users/{id}/posts/{postId}")]
Task<Post> GetUserPost([Path] int id, [Path] int postId);

// Gera: GET /api/users/123/posts/456
```

### [Query] - Parâmetros de Consulta

Marca parâmetros que serão adicionados como query string.

```csharp
[Action("users")]
Task<List<User>> GetUsers([Query] int page, [Query] int size);

// Gera: GET /api/users?page=1&size=10
```

### [Body] - Corpo da Requisição

Marca o parâmetro que será serializado como JSON no corpo da requisição.

```csharp
[Action("users", method: RestMethod.Post)]
Task<User> CreateUser([Body] CreateUserRequest request);
```

### [Header] - Cabeçalhos HTTP

Marca parâmetros que serão enviados como cabeçalhos HTTP.

```csharp
[Action("users")]
Task<List<User>> GetUsers([Header] string authorization, [Header] string userAgent);
```

## Exemplos Progressivos

### Exemplo 1: CRUD Básico

```csharp
[Service("api/products")]
public interface IProductService
{
    // GET /api/products
    [Action]
    Task<List<Product>> GetAll();
    
    // GET /api/products/{id}
    [Action("{id}")]
    Task<Product> GetById([Path] int id);
    
    // POST /api/products
    [Action(method: RestMethod.Post)]
    Task<Product> Create([Body] CreateProductRequest request);
    
    // PUT /api/products/{id}
    [Action("{id}", method: RestMethod.Put)]
    Task<Product> Update([Path] int id, [Body] UpdateProductRequest request);
    
    // DELETE /api/products/{id}
    [Action("{id}", method: RestMethod.Delete)]
    Task<bool> Delete([Path] int id);
}
```

### Exemplo 2: Busca com Filtros

```csharp
[Service("api/products")]
public interface IProductService
{
    [Action("search")]
    Task<PagedResult<Product>> Search(
        [Query] string? name,
        [Query] decimal? minPrice,
        [Query] decimal? maxPrice,
        [Query] int page = 1,
        [Query] int size = 10);
}

// Uso:
var products = await productService.CallAsync(s => 
    s.Search("notebook", 1000, 5000, 1, 20));
// Gera: GET /api/products/search?name=notebook&minPrice=1000&maxPrice=5000&page=1&size=20
```

### Exemplo 3: Upload de Arquivo

```csharp
[Service("api/files")]
public interface IFileService
{
    [Action("upload", method: RestMethod.Post)]
    Task<FileUploadResult> UploadFile(
        [Header("Content-Type")] string contentType,
        [Body] byte[] fileContent);
}
```

### Exemplo 4: Autenticação e Versionamento

```csharp
[Service("api/users", authorize: true, version: "2025-10-08")]
public interface IUserService
{
    [Action("profile")]
    Task<UserProfile> GetProfile([Header("Authorization")] string token);
    
    [Action("profile", method: RestMethod.Put, version: "2025-12-15")]
    Task<UserProfile> UpdateProfile(
        [Header("Authorization")] string token,
        [Body] UpdateProfileRequest request);
}
```

**Como funciona o versionamento:**
- A versão definida no `[Service]` ("2025-10-08") será enviada no header `x-api-version` para todas as ações
- A ação `UpdateProfile` sobrescreve a versão para "2025-12-15", enviando este valor no header
- O backend pode verificar o header `x-api-version` e tratar diferentes versões da API conforme necessário

### Exemplo 5: Parâmetros Complexos

```csharp
public class SearchFilters
{
    public string? Name { get; set; }
    public int? CategoryId { get; set; }
    public bool? IsActive { get; set; }
}

[Service("api/products")]
public interface IProductService
{
    [Action("advanced-search")]
    Task<List<Product>> AdvancedSearch([Query] SearchFilters filters);
}

// O objeto será serializado automaticamente como query parameters
```

## Configurações Avançadas

### RestCallConfig

Permite adicionar configurações extras para chamadas específicas:

```csharp
var config = RestCallConfig.Create()
    .AddHeader("X-Custom-Header", "valor")
    .AddQueryParameter("debug", "true")
    .AddVersion("2025-10-08");

var result = await service.CallAsync(s => s.GetData(), config);
```

### Tratamento de Erros

```csharp
var restClient = new RestClient("https://api.exemplo.com");
restClient.ThownType = RestThownType.OnlyBusiness; // Apenas erros de negócio
// ou
restClient.ThownType = RestThownType.All; // Todos os erros
// ou
restClient.ThownType = RestThownType.None; // Não lança exceções

// Eventos para tratamento personalizado
restClient.Unauthorized += (args) => {
    // Tratar erro 401
};

restClient.Forbidden += (args) => {
    // Tratar erro 403
};
```

## Como Funciona Internamente

### Fluxo de Execução

1. **Análise da Expression**: O `RestService` recebe uma expression tree que representa a chamada do método
2. **Extração de Metadados**: Usa Reflection para obter os atributos `[Service]` e `[Action]`
3. **Processamento de Parâmetros**: Analisa cada parâmetro e seus atributos (`[Path]`, `[Query]`, `[Body]`, `[Header]`)
4. **Geração da URL**: Combina a rota base do serviço com a rota da ação, substituindo parâmetros de path
5. **Montagem dos Headers**: Coleta todos os parâmetros marcados com `[Header]`
6. **Criação do Body**: Serializa o parâmetro marcado com `[Body]` para JSON
7. **Adição de Query Parameters**: Adiciona parâmetros de consulta à URL
8. **Execução da Requisição**: Chama o `RestClient` para executar a requisição HTTP

### Exemplo de Transformação

```csharp
// Interface definida:
[Service("api/users", version: "2025-10-08")]
public interface IUserService
{
    [Action("{id}/posts", method: RestMethod.Post)]
    Task<Post> CreatePost([Path] int id, [Header] string auth, [Body] CreatePostRequest request);
}

// Chamada:
await service.CallAsync(s => s.CreatePost(123, "Bearer token", new CreatePostRequest()));

// Resultado gerado:
// POST /api/users/123/posts
// Headers: { 
//   "auth": "Bearer token",
//   "x-api-version": "2025-10-08"
// }
// Body: { "title": "...", "content": "..." } (JSON serializado)
```

### Exemplo Detalhado de Versionamento

```csharp
[Service("api/products", version: "2025-10-08")]
public interface IProductService
{
    // Usa a versão do serviço: "2025-10-08"
    [Action]
    Task<List<Product>> GetAll();
    
    // Sobrescreve com versão específica: "2025-12-15"
    [Action("search", version: "2025-12-15")]
    Task<List<Product>> Search([Query] string term);
}

// Requisições geradas:
// GET /api/products
// Headers: { "x-api-version": "2025-10-08" }

// GET /api/products/search?term=notebook
// Headers: { "x-api-version": "2025-12-15" }
```

## Vantagens do Sistema

1. **Produtividade**: Elimina código boilerplate para chamadas HTTP
2. **Type Safety**: Verificação de tipos em tempo de compilação
3. **Manutenibilidade**: Mudanças na API refletem automaticamente no cliente
4. **Testabilidade**: Fácil criação de mocks para testes
5. **Consistência**: Padronização na forma de fazer chamadas REST
6. **IntelliSense**: Suporte completo do IDE para autocompletar

## Boas Práticas

### 1. Organização de Interfaces

```csharp
// Separe por domínio
public interface IUserService { }
public interface IProductService { }
public interface IOrderService { }
```

### 2. Nomenclatura Consistente

```csharp
[Service("api/users")]
public interface IUserService
{
    [Action] Task<List<User>> GetAll();           // GET /api/users
    [Action("{id}")] Task<User> GetById([Path] int id);  // GET /api/users/{id}
    [Action(method: RestMethod.Post)] Task<User> Create([Body] User user);
}
```

### 3. Versionamento

```csharp
[Service("api/users", version: "2020-05-10")]
public interface IUserServiceV1 { }

[Service("api/users", version: "2025-10-08")]
public interface IUserServiceV2 { }
```

**Importante sobre versionamento:**
- Use sempre datas no formato "YYYY-MM-DD" para as versões
- A versão é automaticamente enviada no header HTTP `x-api-version`
- O backend pode usar este header para determinar qual versão da API processar
- Versões mais específicas nas ações sobrescrevem a versão do serviço

### 4. Tratamento de Erros

```csharp
try
{
    var user = await userService.CallAsync(s => s.GetById(id));
}
catch (BusinessException ex) when (ex.Code == 404)
{
    // Usuário não encontrado
}
```

## Conclusão

O sistema de geração automática de serviços REST do BlackDigital oferece uma forma elegante e produtiva de consumir APIs REST em aplicações .NET. Através de interfaces simples decoradas com atributos, você pode eliminar grande parte do código repetitivo necessário para chamadas HTTP, mantendo type safety e facilidade de manutenção.

O sistema é flexível o suficiente para atender desde cenários simples até requisitos complexos de APIs corporativas, sempre mantendo o código limpo e expressivo.