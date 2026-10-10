# Documentation Reference

## Goal

Keep examples and docs aligned with the real API surface of BlackDigital.

## Documentation rules

1. Read the relevant implementation files before editing docs.
2. Treat `src/` as authoritative when there is any mismatch.
3. Keep `README.md` and `README.pt.md` synchronized for core examples.
4. Keep `doc/*.md` and `doc/*.pt.md` synchronized when the same topic exists in both languages.
5. Prefer examples that compile against the current public API.
6. When a convention is surprising, explain it directly instead of hiding it.

## Known mismatch patterns to watch for

- direct method calls on `RestService<TService>` instead of `CallAsync(...)`
- `[Query]` examples using simple scalars without clarifying `Parameter` vs `Raw`
- treating `ICreated`, `IUpdated`, and `IDeleted` as interfaces
- model examples that use unsupported `BaseFilter` fields
- summaries that skip `DataBuilder`, `JsonCast`, or `Id`

## Recommended update flow

1. inspect source files
2. inspect the matching docs
3. fix incorrect examples first
4. expand coverage second
5. verify both English and Portuguese versions when needed

## File map

- root guides
  - `README.md`
  - `README.pt.md`
- detailed docs
  - `doc/rest-services*.md`
  - `doc/data-annotations*.md`
  - `doc/model*.md`
  - `doc/utilities*.md`
  - `doc/data-builder*.md`
