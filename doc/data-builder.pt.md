# Data Builder

## Introdução

O namespace `BlackDigital.DataBuilder` fornece helpers de inspeção de metadados para modelos. Ele é útil quando você precisa montar formulários dinâmicos, telas administrativas, editores low-code ou definições de UI guiadas por atributos já existentes no modelo.

Em vez de descrever cada campo duas vezes, você pode inspecionar um tipo e reutilizar metadados que já estão em atributos como `Display`, `DataType`, `Range`, `MaxLength`, `MinLength`, `Editable` e também nos atributos de visibilidade do BlackDigital.

## Componentes Principais

### TypeBuilder

`TypeBuilder` inspeciona um tipo .NET e expõe suas propriedades legíveis/escrevíveis como instâncias de `PropertyBuilder`.

**Principais membros:**
- `Create<T>()` - Cria um builder para um tipo de modelo
- `Properties` - Lista original de propriedades
- `OrderProperties` - Propriedades ordenadas por `DisplayAttribute.Order`

```csharp
var builder = TypeBuilder.Create<ProductForm>();

foreach (var property in builder.OrderProperties)
{
    Console.WriteLine($"{property.PropertyName} -> {property.ComponentType}");
}
```

### PropertyBuilder

`PropertyBuilder` encapsula uma propriedade refletida e expõe uma visão amigável para UI sobre seus metadados.

**Principais membros:**
- `PropertyName` - Nome bruto da propriedade no CLR
- `Name` - Nome de exibição ou nome da propriedade
- `Description` - `DisplayAttribute.Description`
- `Prompt` - `DisplayAttribute.Prompt`
- `Order` - Ordem de exibição
- `ComponentType` - Tipo de campo resolvido para geração de UI
- `CurrentDataType` - Valor do enum `DataType` quando aplicável
- `MinLength` / `MaxLength`
- `MinValue` / `MaxValue`
- `AllowEdit`
- `PropertyType`
- `Show(object value)` - Avalia regras de visibilidade com base em `ShowAttribute`
- `GetValue(object model)` / `SetValue(object model, object value)`

```csharp
var builder = TypeBuilder.Create<ProductForm>();
var nameProperty = builder.Properties.First(p => p.PropertyName == "Name");

Console.WriteLine(nameProperty.Name);
Console.WriteLine(nameProperty.MaxLength);
Console.WriteLine(nameProperty.AllowEdit);
```

## Inferência de Tipos

Quando uma propriedade não declara `DataTypeAttribute`, o `PropertyBuilder` infere um `DataType` a partir do tipo CLR.

**Mapeamentos nativos incluem:**
- `bool` -> `Boolean`
- `short`, `int`, `long`, `sbyte` -> `Integer`
- `byte`, `ushort`, `uint`, `ulong` -> `UnsignedInteger`
- `float`, `double`, `decimal` -> `Decimal`
- `DateTime`, `DateTimeOffset` -> `DateTime`
- `DateOnly` -> `Date`
- `TimeOnly` -> `Time`
- `TimeSpan` -> `TimeSpan`
- enums -> `Enumeration`
- coleções -> `List`
- todo o restante -> `Text`

Esse mapeamento fica centralizado em `DataTypeTable`, o que deixa o comportamento previsível para geração de UI.

## Exemplo Prático

```csharp
using System.ComponentModel.DataAnnotations;
using BlackDigital.DataAnnotations;
using BlackDigital.DataBuilder;

public class ProductForm
{
    [Display(Name = "Nome do produto", Order = 1, Prompt = "Digite o nome do produto")]
    [MaxLength(120)]
    public string Name { get; set; } = string.Empty;

    [Display(Name = "Preço", Order = 2)]
    [Range(typeof(decimal), "0", "999999")]
    public decimal Price { get; set; }

    [Display(Name = "Categoria", Order = 3)]
    public ProductCategory Category { get; set; }

    [ShowIfProperty("Category", ProductCategory.Service)]
    public int? DurationInMinutes { get; set; }
}

var builder = TypeBuilder.Create<ProductForm>();

foreach (var property in builder.OrderProperties)
{
    Console.WriteLine($"{property.Name} ({property.ComponentType})");
}
```

Saída possível:

```text
Nome do produto (Text)
Preço (Decimal)
Categoria (Enumeration)
DurationInMinutes (Integer)
```

## Cenário de Formulário Dinâmico

Um uso comum é transformar o modelo em uma definição genérica de campos:

```csharp
var fields = TypeBuilder.Create<ProductForm>()
    .OrderProperties
    .Select(property => new
    {
        property.PropertyName,
        property.Name,
        property.ComponentType,
        Required = property.MinLength.HasValue && property.MinLength.Value > 0,
        property.MinLength,
        property.MaxLength,
        property.MinValue,
        property.MaxValue,
        property.AllowEdit
    })
    .ToList();
```

Isso é especialmente útil para:
- montar telas de backoffice
- expor metadados do modelo para um frontend
- gerar formulários a partir de contratos compartilhados
- centralizar regras de validação e exibição

## Observações

- `TypeBuilder` inclui apenas propriedades que podem ser lidas e escritas
- `ComponentType` dá prioridade ao `DataTypeAttribute` quando ele existe
- regras de visibilidade podem reutilizar derivados de `ShowAttribute` do BlackDigital
- `GetValue` e `SetValue` permitem fluxos genéricos de leitura e escrita sem reflection extra

## Conclusão

`DataBuilder` funciona como uma ponte leve entre metadados do modelo e geração de UI. Ele ajuda a manter regras de validação, exibição e formato do campo próximas ao modelo, sem abrir mão de experiências dinâmicas.
