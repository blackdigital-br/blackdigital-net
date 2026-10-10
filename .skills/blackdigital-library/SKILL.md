---
name: blackdigital-library
description: Understand and work with the BlackDigital library in this repository. Use when the user asks about BlackDigital APIs, docs, examples, or code changes in this library. Do not use for unrelated projects.
---

# BlackDigital Library

Work from the source code first and use the documentation as supporting context.

## Use this skill

Apply this skill when the task is about the `BlackDigital` library in this repository, including:
- explaining APIs or behavior
- writing examples or documentation
- implementing or updating features in the library
- reviewing consistency between `src/`, `README*`, and `doc/`

Do not use this skill for unrelated repositories or for generic .NET questions that do not depend on BlackDigital conventions.

## Core rules

1. Treat `src/` as the source of truth when docs and code disagree.
2. Prefer existing library conventions over generic framework habits.
3. When changing docs or examples, keep English and Portuguese files aligned unless the task explicitly scopes only one language.
4. When you mention library behavior, cite the real implementation files whenever possible.

## High-signal conventions

### REST

- `RestService<TService>` is called through `CallAsync(...)`. Do not document or generate examples as if it exposed the interface methods directly.
- `[Query]` defaults to `QueryParameterType.Raw`.
- Use `QueryParameterType.Parameter` for named scalar query parameters such as `page`, `size`, `search`, and `sort`.
- Use `QueryParameterType.Raw` for complex filter objects, nested objects, and collections that should expand through `ToUrlQueryString()`.
- `RestCallConfig.AddVersion(...)` adds `api-version` to the query string.
- `[Service(version: ...)]` and `[Action(version: ...)]` send `x-api-version` in headers.

Read [references/rest.md](references/rest.md) for details before editing REST code or examples.

### Model

- `IId` and `IActive` are interfaces.
- `ICreated`, `IUpdated`, and `IDeleted` are base classes despite the `I` prefix.
- The `Id` struct is a core abstraction. Prefer it over assuming plain `int` or `Guid` identifiers.
- Filter helpers such as `ActiveFilter`, `CreatedFilter`, `UpdatedFilter`, `DeletedFilter`, `IdFilter`, and `BaseFilterFilter` are extension-based utilities.

Read [references/model.md](references/model.md) before changing model examples or entity guidance.

### Serialization and helpers

- `JsonCast` centralizes the default JSON behavior for the library.
- `JsonCast` registers converters for enums, `TimeSpan`, and `Id`.
- `IdConverter` writes numeric IDs as numbers when possible, unless a legacy converter is intentionally used.
- `DataBuilder` exists for metadata-driven UI and form generation. Do not omit it when summarizing the library surface area.

Read [references/utilities.md](references/utilities.md) for helper behavior and [references/overview.md](references/overview.md) for the library map.

## Documentation workflow

When the task touches documentation:

1. Read the relevant implementation files in `src/`.
2. Read the matching docs in `README.md`, `README.pt.md`, and `doc/`.
3. Fix mismatches before expanding coverage.
4. Prefer examples that compile against the current API shape.
5. Call out non-obvious conventions explicitly when they are easy for users or LLMs to misunderstand.

Read [references/documentation.md](references/documentation.md) before larger documentation edits.

## Reference routing

- Use [references/overview.md](references/overview.md) for the library map and file locations.
- Use [references/rest.md](references/rest.md) for REST generation behavior and query rules.
- Use [references/model.md](references/model.md) for identifiers, audit types, filters, and option collections.
- Use [references/utilities.md](references/utilities.md) for JSON conventions, exceptions, helper classes, and `DataBuilder`.
- Use [references/documentation.md](references/documentation.md) for documentation maintenance rules.
