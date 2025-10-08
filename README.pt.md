# Framework BlackDigital

![Logo](doc/images/Logo128.png)

[![Versão NuGet](https://img.shields.io/nuget/v/BlackDigital)](https://www.nuget.org/packages/BlackDigital)
[![.NET](https://img.shields.io/badge/.NET-8.0-blue)](https://dotnet.microsoft.com/download/dotnet/8.0)
[![Licença](https://img.shields.io/github/license/blackdigital-br/blackdigital-net)](LICENSE)

Um framework .NET abrangente que fornece utilitários essenciais, geração automática de serviços REST, data annotations avançadas e helpers de modelo para desenvolvimento de aplicações modernas.

## 🚀 Principais Funcionalidades

### 🌐 **Geração Automática de Serviços REST** ⭐
A funcionalidade principal do BlackDigital! Crie clientes REST automaticamente através de interfaces decoradas com atributos. Chega de chamadas HTTP manuais - apenas defina sua interface e deixe o framework cuidar do resto.

```csharp
[Service("api/users", version: "2025-01-15")]
public interface IUserService
{
    [Action]
    Task<List<User>> GetAllUsers();
    
    [Action("{id}", Method = RestMethod.GET)]
    Task<User> GetUserById([Path] int id);
    
    [Action(Method = RestMethod.POST)]
    Task<User> CreateUser([Body] User user);
}

// Uso
var restClient = new RestClient("https://api.exemplo.com");
var userService = new RestService<IUserService>(restClient);
var usuarios = await userService.GetAllUsers();
```

### 📝 **Data Annotations Avançadas**
Atributos poderosos de validação e controle de exibição que estendem as capacidades nativas do .NET:

```csharp
public class RegistroUsuario
{
    [EmailMobile(ErrorMessage = "Informe um email ou telefone válido")]
    public string Contato { get; set; }
    
    [RequiredIfProperty("TipoUsuario", TipoUsuario.Empresa)]
    [ShowIfProperty("TipoUsuario", TipoUsuario.Empresa)]
    public string NomeEmpresa { get; set; }
    
    [CompareValue("Senha", Symbol.Equal)]
    public string ConfirmarSenha { get; set; }
}
```

### 🏗️ **Utilitários de Modelo**
Classes base e interfaces para padrões comuns:
- `IId`, `ICreated`, `IUpdated`, `IActive`, `IDeleted`
- Helpers de filtragem e ordenação
- Listas de opções e construtores de dados

### 🛠️ **Classes Utilitárias**
Helpers essenciais para desenvolvimento cotidiano:
- `HttpHelper` - Utilitários e extensões HTTP
- `DateTimeHelper` - Manipulação de data/hora
- `ReflectionHelper` - Utilitários de reflection
- `ObjectHelper` - Manipulação de objetos
- `UriHelper` - Construção e manipulação de URIs

## 📦 Instalação

Instale via NuGet Package Manager:

```bash
dotnet add package BlackDigital
```

Ou via Package Manager Console:

```powershell
Install-Package BlackDigital
```

## 🏃‍♂️ Início Rápido

### 1. Exemplo de Serviços REST

```csharp
using BlackDigital.Rest;

// Defina sua interface de API
[Service("api/produtos", version: "2025-01-15")]
public interface IProdutoService
{
    [Action]
    Task<List<Produto>> GetProdutos([Query] string? busca = null);
    
    [Action("{id}")]
    Task<Produto> GetProduto([Path] int id);
    
    [Action(Method = RestMethod.POST)]
    Task<Produto> CriarProduto([Body] Produto produto);
    
    [Action("{id}", Method = RestMethod.PUT)]
    Task<Produto> AtualizarProduto([Path] int id, [Body] Produto produto);
    
    [Action("{id}", Method = RestMethod.DELETE)]
    Task DeletarProduto([Path] int id);
}

// Configure e use
var client = new RestClient("https://api.minhaloja.com");
var produtoService = new RestService<IProdutoService>(client);

// Faça as chamadas
var produtos = await produtoService.GetProdutos("notebook");
var produto = await produtoService.GetProduto(1);
```

### 2. Exemplo de Data Annotations

```csharp
using BlackDigital.DataAnnotations;

public class FormularioContato
{
    [Required]
    public string Nome { get; set; }
    
    [EmailMobile(ErrorMessage = "Informe um email ou telefone válido")]
    public string Contato { get; set; }
    
    public TipoContato Tipo { get; set; }
    
    [RequiredIfProperty("Tipo", TipoContato.Empresa)]
    [ShowIfProperty("Tipo", TipoContato.Empresa)]
    public string NomeEmpresa { get; set; }
    
    [ShowIfProperty("Tipo", TipoContato.Pessoal)]
    public DateTime? DataNascimento { get; set; }
}
```

### 3. Exemplo de Utilitários de Modelo

```csharp
using BlackDigital.Model;

public class Usuario : IId, ICreated, IUpdated, IActive
{
    public int Id { get; set; }
    public DateTime Created { get; set; }
    public DateTime Updated { get; set; }
    public bool Active { get; set; }
    
    public string Nome { get; set; }
    public string Email { get; set; }
}

// Use com filtros
var filtro = new ActiveFilter<Usuario> { Active = true };
var filtroUsuario = new BaseFilter { Search = "joão", Take = 10 };
```

## 📚 Documentação

### Documentação Principal
- **[Serviços REST](doc/rest-services.pt.md)** - Guia completo de geração automática de clientes REST
- **[Data Annotations](doc/data-annotations.pt.md)** - Atributos avançados de validação e exibição
- **[Utilitários de Modelo](doc/model.pt.md)** - Classes base e helpers de modelo
- **[Utilitários](doc/utilities.pt.md)** - Classes auxiliares e extensões

### Documentação em Inglês
- **[REST Services](doc/rest-services.md)** - Complete guide to automatic REST client generation
- **[Data Annotations](doc/data-annotations.md)** - Advanced validation and display attributes
- **[Model Utilities](doc/model.md)** - Base classes and model helpers
- **[Utilities](doc/utilities.md)** - Helper classes and extensions

## 🏗️ Arquitetura

O Framework BlackDigital está organizado em vários componentes principais:

```
BlackDigital/
├── Rest/                    # Geração automática de serviços REST
│   ├── RestService<T>      # Classe principal do serviço
│   ├── RestClient          # Wrapper do cliente HTTP
│   └── Attributes/         # Atributos de serviço e ação
├── DataAnnotations/        # Atributos avançados de validação
│   ├── Validation/         # Email, Mobile, RequiredIf, etc.
│   └── Display/           # ShowIf, NotShow, etc.
├── Model/                  # Interfaces e classes base
│   ├── Interfaces/         # IId, ICreated, IUpdated, etc.
│   └── Filters/           # BaseFilter, ActiveFilter, etc.
└── Utilities/             # Classes auxiliares
    ├── HttpHelper         # Utilitários HTTP
    ├── DateTimeHelper     # Helpers de data/hora
    └── ReflectionHelper   # Utilitários de reflection
```

## 🤝 Contribuindo

Contribuições são bem-vindas! Sinta-se à vontade para enviar um Pull Request. Para mudanças importantes, abra primeiro uma issue para discutir o que você gostaria de alterar.

### Configuração de Desenvolvimento

1. Clone o repositório
2. Abra `BlackDigital.sln` no Visual Studio ou sua IDE preferida
3. Compile a solução
4. Execute os testes para garantir que tudo funciona

### Diretrizes

- Siga o estilo de código e convenções existentes
- Adicione testes para novas funcionalidades
- Atualize a documentação conforme necessário
- Certifique-se de que todos os testes passem antes de enviar o PR

## 📄 Licença

Este projeto está licenciado sob a Licença MIT - veja o arquivo [LICENSE](LICENSE) para detalhes.

## 🔗 Links

- [Pacote NuGet](https://www.nuget.org/packages/BlackDigital)
- [Repositório GitHub](https://github.com/blackdigital-br/blackdigital-net)
- [Issues](https://github.com/blackdigital-br/blackdigital-net/issues)

## 📞 Suporte

Se você encontrar problemas ou tiver dúvidas, por favor:

1. Verifique a [documentação](doc/)
2. Pesquise [issues existentes](https://github.com/blackdigital-br/blackdigital-net/issues)
3. Crie uma nova issue se necessário

---

**Framework BlackDigital** - Simplificando o desenvolvimento .NET com utilitários poderosos e geração automática de serviços REST.