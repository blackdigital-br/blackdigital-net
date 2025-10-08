# Documentação - Model

## Introdução

A pasta `Model` contém classes utilitárias para trabalhar com entidades, filtros e identificadores no sistema BlackDigital. Diferente de outros módulos, essas classes são mais independentes entre si, oferecendo funcionalidades específicas para diferentes aspectos do desenvolvimento.

## Interfaces de Identificação

### IId e Id

**Propósito**: Padronizar o trabalho com identificadores únicos em entidades.

#### IId
Interface genérica que define propriedades `Id` para diferentes tipos de chave:
- `IId` - Id genérico
- `IId<TKey>` - Id tipado (byte, short, int, long, Guid, etc.)

#### Id (struct)
Estrutura que encapsula diferentes tipos de identificadores e permite conversões automáticas.

**Exemplo de uso**:
```csharp
public class Usuario : IId
{
    public Id Id { get; set; }
    public string Nome { get; set; }
}

// Conversões automáticas
Id id1 = new Id("123");
Id id2 = new Id(Guid.NewGuid());
Id id3 = new Id(456);

// Verificação
if (id1.HasId)
{
    Console.WriteLine($"ID: {id1}");
}
```

## Interfaces de Auditoria

### IActive
**Propósito**: Controlar se uma entidade está ativa ou inativa.

```csharp
public class Produto : IActive
{
    public Id Id { get; set; }
    public string Nome { get; set; }
    public bool Active { get; set; }
}
```

### ICreated
**Propósito**: Rastrear quando uma entidade foi criada.

```csharp
public class Pedido : ICreated
{
    public Id Id { get; set; }
    public DateTime Created { get; set; }
}
```

### IUpdated
**Propósito**: Rastrear quando uma entidade foi atualizada (herda de ICreated).

```csharp
public class Cliente : IUpdated
{
    public Id Id { get; set; }
    public DateTime Created { get; set; }
    public DateTime Updated { get; set; }
}
```

### IDeleted
**Propósito**: Implementar exclusão lógica (soft delete) - herda de IUpdated.

```csharp
public class Documento : IDeleted
{
    public Id Id { get; set; }
    public DateTime Created { get; set; }
    public DateTime Updated { get; set; }
    public DateTime? Deleted { get; set; }
}
```

## Classes de Filtro

### BaseFilter
**Propósito**: Classe base para paginação e ordenação de consultas.

**Propriedades principais**:
- `Skip` - Quantidade de registros para pular
- `Take` - Quantidade de registros para retornar
- `Sort` - Lista de critérios de ordenação

```csharp
var filter = new BaseFilter
{
    Skip = 10,
    Take = 20,
    Sort = new List<SortItem>
    {
        new SortItem("Nome", true), // Ascendente
        new SortItem("Created", false) // Descendente
    }
};

var resultado = query.ApplyFilter(filter);
```

### ActiveFilter
**Propósito**: Filtrar entidades por status ativo/inativo.

```csharp
// Buscar apenas registros ativos
var ativos = query.FilterActive(true);

// Buscar apenas registros inativos
var inativos = query.FilterActive(false);

// Buscar todos (ativos e inativos)
var todos = query.FilterActive(null);
```

### CreatedFilter
**Propósito**: Filtrar entidades por data de criação.

```csharp
// Buscar por data específica
var porData = query.FindCreated(DateTime.Today);

// Buscar por período
var porPeriodo = query.FilterCreatedRange(
    DateTime.Today.AddDays(-30), 
    DateTime.Today
);

// Buscar criados após uma data
var recentes = query.FilterMinCreated(DateTime.Today.AddDays(-7));
```

### UpdatedFilter
**Propósito**: Filtrar entidades por data de atualização.

```csharp
// Buscar atualizados hoje
var atualizadosHoje = query.FindUpdated(DateTime.Today);

// Buscar atualizados no último mês
var ultimoMes = query.FilterUpdatedRange(
    DateTime.Today.AddMonths(-1),
    DateTime.Today
);
```

### DeletedFilter
**Propósito**: Filtrar entidades por status de exclusão.

```csharp
// Buscar apenas registros não excluídos
var naoExcluidos = query.HasNotDeleted();

// Buscar apenas registros excluídos
var excluidos = query.HasDeleted();

// Buscar excluídos em um período
var excluidosRecentes = query.FilterDeletedRange(
    DateTime.Today.AddDays(-30),
    DateTime.Today
);
```

### IdFilter
**Propósito**: Filtrar entidades por identificador.

```csharp
// Buscar por ID específico
var porId = query.FindId(new Id(123));

// Filtrar por ID (se fornecido)
var filtrado = query.FilterId(idOpcional);

// Para IDs tipados
var porIdTipado = query.FindId<Usuario, int>(456);
```

### BaseFilterFilter
**Propósito**: Aplicar filtros de BaseFilter (paginação e ordenação) em consultas.

```csharp
// Aplicar filtro completo (ordenação + paginação)
var resultado = query.ApplyFilter(filter);

// Aplicar apenas ordenação
var ordenado = query.ApplyOnlyOrderBy(filter);

// Aplicar apenas paginação
var paginado = query.ApplyOnlyFilter(filter);
```

## Classes de Opções

### OptionItem
**Propósito**: Representar um item de opção com identificador, rótulo e conexões.

**Propriedades**:
- `Id` - Identificador único
- `Label` - Texto exibido
- `Code` - Código opcional
- `Description` - Descrição opcional
- `Connections` - Dicionário de conexões com outras entidades

```csharp
var opcao = new OptionItem
{
    Id = new Id(1),
    Label = "Categoria A",
    Code = "CAT_A",
    Description = "Primeira categoria",
    Connections = new Dictionary<string, ListId>
    {
        ["subcategorias"] = new ListId(new[] { 10, 11, 12 })
    }
};
```

### Options
**Propósito**: Coleção de OptionItem com métodos de busca e filtro.

```csharp
var opcoes = new Options(listaDeOpcoes);

// Buscar por ID
var opcao = opcoes[1];

// Verificar se existe
bool existe = opcoes.ContainsKey(1);

// Filtrar por conexões
var filtradas = opcoes.FilterConnections("categoria", new Id(5));

// Buscar por texto
var encontradas = opcoes.FindText("produto");

// Filtrar por rótulo
var porLabel = opcoes.FilterLabel("categoria");
```

## Classes Auxiliares

### ListId
**Propósito**: Lista especializada para trabalhar com identificadores, com conversões automáticas.

```csharp
// Criação de diferentes formas
var lista1 = new ListId(new[] { "1", "2", "3" });
var lista2 = new ListId(new[] { 1, 2, 3 });
var lista3 = new ListId(new[] { Guid.NewGuid(), Guid.NewGuid() });

// Conversões implícitas
ListId fromArray = new int[] { 1, 2, 3 };
ListId fromList = new List<string> { "a", "b", "c" };

// Conversões para outros tipos
int[] paraArray = lista1;
List<Guid> paraListGuid = lista1;
```

### SortItem
**Propósito**: Definir critérios de ordenação para consultas.

```csharp
// Ordenação ascendente
var sortAsc = new SortItem("Nome", true);

// Ordenação descendente
var sortDesc = new SortItem("Created", false);

// Ordenação padrão (ascendente)
var sortDefault = new SortItem("Id");

// Uso em BaseFilter
var filter = new BaseFilter
{
    Sort = new List<SortItem>
    {
        new SortItem("Categoria"),
        new SortItem("Created", false)
    }
};
```

## Vantagens do Sistema

### 1. **Padronização**
- Interfaces consistentes para auditoria e identificação
- Padrões uniformes em toda a aplicação

### 2. **Flexibilidade**
- Suporte a diferentes tipos de identificadores
- Filtros compostos e reutilizáveis

### 3. **Facilidade de Uso**
- Conversões automáticas entre tipos
- Métodos de extensão intuitivos

### 4. **Manutenibilidade**
- Código organizado e modular
- Fácil extensão e customização

## Exemplo Prático Completo

```csharp
// Definir entidade
public class Produto : IId, IActive, ICreated, IUpdated, IDeleted
{
    public Id Id { get; set; }
    public string Nome { get; set; }
    public bool Active { get; set; }
    public DateTime Created { get; set; }
    public DateTime Updated { get; set; }
    public DateTime? Deleted { get; set; }
}

// Usar filtros
var filter = new BaseFilter
{
    Skip = 0,
    Take = 10,
    Sort = new List<SortItem> { new SortItem("Nome") }
};

var produtos = query
    .HasNotDeleted()           // Não excluídos
    .FilterActive(true)        // Apenas ativos
    .FilterMinCreated(DateTime.Today.AddDays(-30)) // Criados nos últimos 30 dias
    .ApplyFilter(filter);      // Paginação e ordenação

// Trabalhar com opções
var opcoesProduto = new Options(produtos.Select(p => new OptionItem
{
    Id = p.Id,
    Label = p.Nome,
    Code = p.Id.ToString()
}));

var produtoEncontrado = opcoesProduto.FindText("notebook");
```

Este sistema Model fornece uma base sólida e flexível para trabalhar com entidades, filtros e identificadores, mantendo o código organizado e reutilizável.