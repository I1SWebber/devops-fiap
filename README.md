# Projeto - Cidades ESGInteligentes (EnergyAPI)

Back-end em **.NET 8 / ASP.NET Core** com **SQL Server**, conteinerizado em **Docker** e orquestrado via **Docker Compose**. Pipeline **CI/CD** com **GitHub Actions** (build, teste, imagem Docker e deploy em _staging_ e _production_).

## Como executar localmente com Docker

Pré-requisitos: Docker e Docker Compose instalados.

```bash
# 1) copie o .env.example
cp .env.example .env

# (opcional) edite valores no .env (senha do SA, nome do DB, etc.)

# 2) suba os serviços
docker compose --env-file .env up -d --build

# 3) verifique os containers
docker ps

# API disponível em: http://localhost:8080/swagger
```

> Observação: o container da API expõe a porta **8080**; o SQL Server expõe **1433**.

## Pipeline CI/CD

Ferramenta: **GitHub Actions**  
Arquivo do pipeline: `.github/workflows/ci-cd.yml`

Etapas:
1. **Build & Test**: restaura dependências, compila a solução `EnergyAPI/EnergyAPI.sln` e executa testes.
2. **Docker Build & Push**: constrói a imagem da API e publica no **GitHub Container Registry (GHCR)**.
3. **Deploy Staging**: via SSH no servidor de staging, roda `docker compose up -d` com variáveis seguras (secrets).
4. **Deploy Production**: mesma estratégia do staging, em ambiente de produção.

Secrets esperados (em **Settings > Secrets and variables > Actions**):
- `STAGING_HOST`, `STAGING_USER`, `STAGING_SSH_KEY`, `STAGING_APP_PATH`
- `PROD_HOST`, `PROD_USER`, `PROD_SSH_KEY`, `PROD_APP_PATH`
- `GITHUB_TOKEN` (padrão do GitHub)
- `SA_PASSWORD`, `DB_NAME`

## Containerização

### Dockerfile (API)
O projeto já inclui um `Dockerfile` multi-stage em `EnergyAPI/Dockerfile` baseado nas imagens oficiais .NET 8, expondo 8080/8081.

### docker-compose.yml
Orquestra **API** + **SQL Server** com:
- **Volumes** para persistir dados do SQL (`mssql-data`)
- **Variáveis de ambiente** via `.env` / `--env-file`
- **Rede** interna `app-net`
- **depends_on** garantindo que o banco suba antes da API

## Prints do funcionamento

Inclua aqui prints (ou links) de:
- `docker compose ps`
- Swagger em **http://SEU-HOST:8080/swagger**
- Jobs do pipeline no GitHub (build, tests, deploy)
- Containers rodando no staging e production

## Tecnologias utilizadas

- .NET 8 / ASP.NET Core
- Entity Framework Core
- SQL Server 2022 (Docker)
- Docker & Docker Compose
- GitHub Actions (CI/CD)
- GHCR (container registry)

## Estrutura do repositório

```
EnergyAPI_Entrega/
├── EnergyAPI/                # código da API (ASP.NET Core)
├── EnergyAPI.Tests/          # testes automatizados
├── docker-compose.yml        # orquestração local da API + SQL Server
├── .env.example              # variáveis de ambiente de exemplo
├── .github/workflows/ci-cd.yml
└── README.md
```

## Checklist de Entrega

| Item                                                    | OK |
|---------------------------------------------------------|----|
| Projeto compactado em .ZIP com estrutura organizada     | ☐  |
| Dockerfile funcional                                    | ☐  |
| docker-compose.yml ou arquivos Kubernetes               | ☐  |
| Pipeline com etapas de build, teste e deploy            | ☐  |
| README.md com instruções e prints                       | ☐  |
| Documentação técnica com evidências (PDF ou PPT)        | ☐  |
| Deploy realizado nos ambientes staging e produção       | ☐  |

---

> Dica: para rodar migrações EF Core dentro do container, você pode abrir um shell na API e executar `dotnet ef database update` (se o CLI estiver incluído) ou publicar as migrações no entrypoint. Como alternativa, rode as migrações localmente apontando para o connection string do container.
