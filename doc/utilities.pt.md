# Classes Utilitárias - BlackDigital

## Introdução

Este documento apresenta as classes utilitárias que estão diretamente na pasta `src` do projeto BlackDigital. Essas classes são independentes e fornecem funcionalidades auxiliares essenciais para o desenvolvimento de aplicações, incluindo tratamento de exceções, manipulação de dados, validações e diversas operações de apoio.

## Tratamento de Exceções

### BusinessException

Classe personalizada para exceções de negócio que mapeia códigos HTTP para diferentes tipos de erro.

**Principais Características:**
- Herda de `Exception`
- Contém propriedade `Code` com código HTTP
- Métodos estáticos para criação rápida de exceções
- Métodos de validação que lançam exceções quando necessário

**Exemplo de Uso:**
```csharp
// Lançar exceção específica
BusinessException.ThrowNotFound();

// Validar e lançar se nulo
var user = BusinessException.ThrowNotFoundIfNull(userRepository.GetById(id));

// Criar exceção personalizada
throw BusinessException.New("Usuário não possui permissão", 403);
```

### BusinessExceptionType

Enum que define os tipos de exceções de negócio com seus respectivos códigos HTTP.

**Valores Disponíveis:**
- `BadRequest = 400`
- `Forbidden = 403`
- `NotFound = 404`
- `Conflict = 409`
- `Gone = 410`
- `PreconditionFailed = 412`
- `RangeNotSatisfiable = 416`
- `ExpectationFailed = 417`
- `PreconditionRequired = 428`

### ErrorApiResponse

Classe para padronizar respostas de erro em APIs.

**Principais Métodos:**
- `Create(string message, List<string>? errors)` - Criar resposta de erro personalizada
- `Create(Exception exception)` - Criar resposta baseada em exceção

**Exemplo de Uso:**
```csharp
// Criar resposta de erro simples
var errorResponse = ErrorApiResponse.Create("Dados inválidos");

// Criar resposta baseada em exceção
try 
{
    // código que pode falhar
}
catch (Exception ex)
{
    var errorResponse = ErrorApiResponse.Create(ex);
    return BadRequest(errorResponse);
}
```

## Helpers de Dados

### DataValidator

Classe estática para validação de dados comuns como email e telefone.

**Principais Métodos:**
- `IsEmail(string email)` - Valida formato de email
- `IsPhone(string phone)` - Valida formato de telefone
- `IsEmailOrPhone(string value)` - Valida se é email ou telefone
- `ValidateData(object? value, object? otherValue, Symbol symbol)` - Compara valores

**Exemplo de Uso:**
```csharp
// Validar email
bool isValid = DataValidator.IsEmail("usuario@exemplo.com");

// Validar telefone
bool isValidPhone = DataValidator.IsPhone("+5511999999999");

// Comparar valores
bool isEqual = DataValidator.ValidateData(10, 10, Symbol.Equal);
```

### DateTimeHelper

Utilitários para manipulação de datas e horários.

**Principais Métodos:**
- `ToMonthDate(DateTime date)` - Converte para primeiro dia do mês
- `TryToDateTime(string datetime, out DateTime result)` - Tenta converter string para DateTime
- `ToDateTime(string datetime, DateTime defaultValue)` - Converte com valor padrão
- `ToDateTimeUnspecified(DateTime date)` - Remove informação de timezone

**Exemplo de Uso:**
```csharp
// Converter para primeiro dia do mês
DateTime firstDay = DateTime.Now.ToMonthDate();

// Converter string para DateTime
DateTime date = "2023-12-25".ToDateTime();

// Desconstruir TimeSpan
var timespan = TimeSpan.FromHours(25);
var (days, hours, minutes, seconds) = timespan;
```

### DisplayHelper

Helper para trabalhar com atributos `Display` e obter informações de exibição.

**Principais Métodos:**
- `GetDisplay(PropertyInfo property)` - Obtém atributo Display
- `GetDisplayName<T>(Expression<Func<T>> expression)` - Obtém nome de exibição
- `GetDescription<T>(Expression<Func<T>> expression)` - Obtém descrição
- `GetOrder<T>(Expression<Func<T>> expression)` - Obtém ordem de exibição

**Exemplo de Uso:**
```csharp
// Obter nome de exibição de uma propriedade
string displayName = DisplayHelper.GetDisplayName(() => user.Name);

// Obter descrição
string description = DisplayHelper.GetDescription(() => user.Email);
```

### ExpressionHelper

Utilitários para trabalhar com expressões LINQ.

**Principais Métodos:**
- `Filter<TEntity, TProperty>(IQueryable<TEntity> query, Expression<Func<TEntity, TProperty>> property, TProperty value)` - Filtra query por propriedade

**Exemplo de Uso:**
```csharp
// Filtrar usuários por nome
var filteredUsers = ExpressionHelper.Filter(users, u => u.Name, "João");
```

### HttpHelper

Utilitários para trabalhar com HTTP e conversões de query string.

**Principais Métodos:**
- `IsSuccess(HttpStatusCode status)` - Verifica se status é de sucesso
- `IsClientError(HttpStatusCode status)` - Verifica se é erro do cliente
- `FromQueryString<T>(string queryString)` - Converte query string para objeto
- `ToUrlQueryString<T>(T item)` - Converte objeto para query string

**Exemplo de Uso:**
```csharp
// Verificar status HTTP
bool isSuccess = HttpStatusCode.OK.IsSuccess();

// Converter query string para objeto
var filter = "name=João&age=30".FromQueryString<UserFilter>();

// Converter objeto para query string
string queryString = userFilter.ToUrlQueryString();
```

### JsonCast

Utilitários para serialização e deserialização JSON com configurações padronizadas.

**Principais Métodos:**
- `To<T>(string json)` - Deserializa JSON para tipo específico
- `ToJson(object item)` - Serializa objeto para JSON
- `ConfigureJson(JsonSerializerOptions options)` - Configura opções padrão

**Comportamentos padrão:**
- Habilita nomes de propriedades case-insensitive
- Ignora valores `null` ao escrever JSON
- Registra `JsonStringEnumConverter` para enums
- Registra `TimeSpanConverter` usando o formato padrão `"c"`
- Registra `IdConverter`, que grava `Id` numérico como número sempre que possível

**Exemplo de Uso:**
```csharp
// Serializar objeto
string json = user.ToJson();

// Deserializar JSON
var user = jsonString.To<User>();

// Configurar opções personalizadas
var options = new JsonSerializerOptions().ConfigureJson();
```

Se você precisar de compatibilidade com payloads legados onde `Id` deve ser sempre serializado como string, pode adicionar `new IdConverter(legacy: true)` em uma instância customizada de `JsonSerializerOptions`.

### ObjectHelper

Utilitários gerais para manipulação de objetos.

**Principais Métodos:**
- `CreateId()` - Gera ID único
- `ToUrlQueryString<T>(T item)` - Converte objeto para query string
- `CloneObject<T>(T item)` - Clona objeto via JSON
- `CastObject<TCast, TOrig>(TOrig item)` - Converte tipo via JSON
- `GetResourceValue(string? value, Type? resourceType)` - Obtém valor de recurso

**Exemplo de Uso:**
```csharp
// Gerar ID único
string id = ObjectHelper.CreateId();

// Clonar objeto
var userCopy = user.CloneObject();

// Converter tipo
var userDto = user.CastObject<UserDto, User>();
```

### ReflectionHelper

Utilitários avançados para reflexão e manipulação de atributos.

**Principais Métodos:**
- `GetAttributes<TAttribute>(Type type)` - Obtém atributos de um tipo
- `GetSingleAttribute<TAttribute>(PropertyInfo property)` - Obtém atributo único
- `GetDisplayName(PropertyInfo property)` - Obtém nome de exibição
- `CreateCustomAttribute<TAttribute>()` - Cria atributo personalizado

**Exemplo de Uso:**
```csharp
// Obter atributos de uma propriedade
var attributes = property.GetAttributes<DisplayAttribute>();

// Obter nome de exibição
string displayName = property.GetDisplayName();
```

### UriHelper

Utilitários para manipulação de URIs.

**Principais Métodos:**
- `Append(Uri uri, params string[] paths)` - Adiciona caminhos à URI
- `GetQueryString(Uri uri)` - Extrai query string como dicionário

**Exemplo de Uso:**
```csharp
// Adicionar caminhos à URI
var baseUri = new Uri("https://api.exemplo.com");
var fullUri = baseUri.Append("users", "123", "profile");

// Obter query string
var queryParams = uri.GetQueryString();
```

## Utilitários Diversos

### SecurityFilter

Abstração base compartilhada para contextos de autenticação e autorização.

**Principais Propriedades:**
- `UserId` - Identificador do usuário atual como `Id`
- `Roles` - Papéis do usuário atual

**Exemplo de Uso:**
```csharp
public class ApiSecurityFilter : SecurityFilter
{
    public ApiSecurityFilter(Id userId, IEnumerable<string> roles)
    {
        UserId = userId;
        Roles.AddRange(roles);
    }
}
```

### LocalRandom

Gerador de strings aleatórias com diferentes tipos de caracteres.

**Principais Métodos:**
- `GenerateString(int length, bool useUpper, bool useLower, bool useNumbers, bool useSpecial)` - Gera string aleatória
- `GenerateNumber(int length)` - Gera string numérica aleatória

**Exemplo de Uso:**
```csharp
// Gerar senha aleatória
string password = LocalRandom.GenerateString(12, true, true, true, true);

// Gerar código numérico
string code = LocalRandom.GenerateNumber(6);
```

### ModelList<T>

Classe para representar listas paginadas com metadados de paginação.

**Principais Propriedades:**
- `Itens` - Lista de itens
- `TotalItens` - Total de itens disponíveis
- `PageNumber` - Número da página atual
- `PageSize` - Tamanho da página
- `TotalPages` - Total de páginas
- `HasPreviousPage` - Indica se há página anterior
- `HasNextPage` - Indica se há próxima página

**Exemplo de Uso:**
```csharp
var pagedUsers = new ModelList<User>
{
    Itens = users.Skip(skip).Take(take),
    TotalItens = totalUsers,
    PageNumber = pageNumber,
    PageSize = pageSize
};

// Verificar se há mais páginas
if (pagedUsers.HasNextPage)
{
    // carregar próxima página
}
```

### Symbol

Enum que define símbolos de comparação para validações.

**Valores Disponíveis:**
- `Equal` - Igual
- `NotEqual` - Diferente
- `LessThan` - Menor que
- `LessThanOrEqual` - Menor ou igual
- `GreaterThan` - Maior que
- `GreaterThanOrEqual` - Maior ou igual

**Exemplo de Uso:**
```csharp
// Usar em validações
bool isValid = DataValidator.ValidateData(age, 18, Symbol.GreaterThanOrEqual);
```

## Vantagens das Classes Utilitárias

### **🔧 Funcionalidades Prontas**
- Métodos comuns já implementados e testados
- Reduz código repetitivo
- Padroniza operações frequentes

### **⚡ Performance**
- Implementações otimizadas
- Reutilização de código
- Menos overhead de desenvolvimento

### **🛡️ Confiabilidade**
- Código testado e validado
- Tratamento de casos extremos
- Validações consistentes

### **📚 Facilidade de Uso**
- APIs simples e intuitivas
- Documentação clara
- Exemplos práticos

## Boas Práticas

1. **Use as validações do DataValidator** para garantir consistência
2. **Aproveite os helpers de conversão** para evitar código repetitivo
3. **Utilize BusinessException** para padronizar tratamento de erros
4. **Configure JsonCast** uma vez e use em todo o projeto
5. **Combine as classes** para soluções mais robustas

Essas classes utilitárias formam a base sólida para desenvolvimento eficiente, fornecendo funcionalidades essenciais de forma padronizada e confiável.
