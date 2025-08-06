# Configuração do GitHub Actions

Este documento explica como configurar o workflow do GitHub Actions para compilar, testar e publicar o pacote NuGet.

## Pré-requisitos

1. **Chave da API do NuGet**: Você precisa de uma chave de API do NuGet.org
   - Acesse https://www.nuget.org/account/apikeys
   - Crie uma nova chave de API
   - Copie a chave

2. **Configurar Secret no GitHub**:
   - Vá para o repositório no GitHub
   - Acesse Settings > Secrets and variables > Actions
   - Clique em "New repository secret"
   - Nome: `NUGET_API_KEY`
   - Valor: Cole a chave da API do NuGet

## Configuração do Projeto

### 1. Atualizar Informações do Pacote

Edite o arquivo `src/RavenDB.Indexing.BrazilianAnalyzer/RavenDB.Indexing.BrazilianAnalyzer.csproj` e atualize as seguintes propriedades:

```xml
<Authors>Seu Nome</Authors>
<Company>Sua Empresa</Company>
<RepositoryUrl>https://github.com/seuusuario/RavenDB.Indexing.BrazilianAnalyzer</RepositoryUrl>
```

### 2. Verificar Branch Principal

O workflow está configurado para executar na branch `main`. Se sua branch principal for diferente, atualize o arquivo `.github/workflows/ci-cd.yml`:

```yaml
on:
  push:
    branches: [ main ]  # Altere se necessário
  pull_request:
    branches: [ main ]  # Altere se necessário
```

## Como Funciona

### Jobs do Workflow

1. **build-and-test**: Compila a solução e executa os testes
2. **create-nuget-package**: Cria o pacote NuGet (apenas na branch main)
3. **publish-nuget**: Publica o pacote no NuGet.org (apenas na branch main)

### Triggers

- **Push para main**: Executa todos os jobs, incluindo publicação
- **Push para develop**: Executa apenas build e testes
- **Pull Request**: Executa apenas build e testes

### Versionamento

O pacote usa o número da execução do GitHub Actions como versão (`${{ github.run_number }}`). Para versões semânticas, você pode modificar o workflow para usar tags do Git.

## Personalização

### Usar Tags para Versionamento

Para usar tags do Git como versão, modifique o workflow:

```yaml
env:
  NUGET_PACKAGE_VERSION: ${{ github.ref_name }}
```

E configure o trigger para tags:

```yaml
on:
  push:
    tags: [ 'v*' ]
```

### Adicionar Mais Testes

O workflow já está configurado para executar todos os testes. Certifique-se de que seus testes estão na pasta `test/`.

### Configurar Cache

Para melhorar a performance, você pode adicionar cache do NuGet:

```yaml
- name: Cache NuGet packages
  uses: actions/cache@v3
  with:
    path: ~/.nuget/packages
    key: ${{ runner.os }}-nuget-${{ hashFiles('**/*.csproj') }}
    restore-keys: |
      ${{ runner.os }}-nuget-
```

## Troubleshooting

### Erro de Autenticação NuGet

- Verifique se o secret `NUGET_API_KEY` está configurado corretamente
- Certifique-se de que a chave da API tem permissões de push

### Falha na Compilação

- Verifique se todas as dependências estão corretas
- Execute `dotnet restore` localmente para testar

### Testes Falhando

- Execute os testes localmente: `dotnet test`
- Verifique os logs do workflow para detalhes específicos 