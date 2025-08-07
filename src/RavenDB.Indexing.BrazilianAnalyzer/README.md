# RavenDB.Indexing.BrazilianAnalyzer

Um analisador de texto em português brasileiro para indexação no RavenDB usando Lucene.Net.

## Descrição

Este pacote fornece um analisador de texto especializado para o português brasileiro, otimizado para uso com RavenDB e Lucene.Net. O analisador inclui filtros específicos para normalização de caracteres e tokens em português brasileiro.

## Instalação

```bash
dotnet add package RavenDB.Indexing.BrazilianAnalyzer
```

## Uso

```csharp
using RavenDB.Indexing.BrazilianAnalyzer;

// Criar o analisador
var analyzer = new RavenBrazilianAnalyzer(Version.LUCENE_30);

// Usar em índices do RavenDB
var indexDefinition = new IndexDefinition
{
    Maps = { "from doc in docs select new { doc.Name }" },
    Analyzers = { { "Name", typeof(RavenBrazilianAnalyzer).AssemblyQualifiedName } }
};
```

## Funcionalidades

- Normalização de caracteres específicos do português brasileiro
- Filtros de token otimizados para o idioma
- Compatível com RavenDB e Lucene.Net
- Suporte a .NET Standard 2.0

## Licença

MIT License 