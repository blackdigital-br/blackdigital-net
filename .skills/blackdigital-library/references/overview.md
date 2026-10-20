# BlackDigital Overview

## Purpose

BlackDigital is a shared .NET library with reusable REST, data annotation, model, serialization, and helper utilities.

## Main source areas

- `src/Rest/`
  - automatic REST client generation through attributes and expression-based calls
- `src/DataAnnotations/`
  - validation and visibility attributes
- `src/Model/`
  - identifiers, audit contracts and base classes, filters, options, and sorting
- `src/DataBuilder/`
  - model metadata inspection for dynamic UIs
- `src/Converters/`
  - JSON converters for `Id` and `TimeSpan`
- `src/Authentication/`
  - shared security abstractions
- root `src/*.cs`
  - helper classes such as `JsonCast`, `HttpHelper`, `ObjectHelper`, `ReflectionHelper`, `DateTimeHelper`, `DisplayHelper`, `ExpressionHelper`, `DataValidator`, `BusinessException`, and `ModelList<T>`

## Main docs

- `README.md`
- `README.pt.md`
- `doc/rest-services.md`
- `doc/rest-services.pt.md`
- `doc/data-annotations.md`
- `doc/data-annotations.pt.md`
- `doc/model.md`
- `doc/model.pt.md`
- `doc/utilities.md`
- `doc/utilities.pt.md`
- `doc/data-builder.md`
- `doc/data-builder.pt.md`

## Source-first files to consult

- REST behavior
  - `src/Rest/RestService.cs`
  - `src/Rest/RestClient.cs`
  - `src/Rest/QueryAttribute.cs`
  - `src/Rest/RestCallConfig.cs`
- Model behavior
  - `src/Model/IId.cs`
  - `src/Model/Id.cs`
  - `src/Model/IActive.cs`
  - `src/Model/ICreated.cs`
  - `src/Model/IUpdated.cs`
  - `src/Model/IDeleted.cs`
  - `src/Model/CreatedModel.cs`
  - `src/Model/UpdatedModel.cs`
  - `src/Model/DeletedModel.cs`
  - `src/Model/*.cs` filter helpers
- Serialization
  - `src/JsonCast.cs`
  - `src/Converters/IdConverter.cs`
  - `src/Converters/TimeSpanConverter.cs`
- Metadata-driven UI
  - `src/DataBuilder/TypeBuilder.cs`
  - `src/DataBuilder/PropertyBuilder.cs`
  - `src/DataBuilder/DataTypeTable.cs`

## Common LLM mistakes to avoid

- Treating `RestService<TService>` like a generated runtime proxy with direct method calls
- Assuming `[Query]` defaults to named scalar parameters
- Assuming the audit fields come from the `ICreated`, `IUpdated`, and `IDeleted` interfaces instead of the `CreatedModel`, `UpdatedModel`, and `DeletedModel` base classes
- Ignoring `Id` and replacing it with plain primitives in examples
- Forgetting `DataBuilder` when summarizing the library
