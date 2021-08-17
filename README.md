# DesafioLeo
Repositório com o teste de back end da Leo Madeiras

# Docker

Para compilar a imagem do docker:

Acesse o diretório:
DesafioLeoGit\DesafioLeo\Test.DesafioLel

## Use os Comandos

```bash
docker build --network=host -t apitesteleo:v1  -f ./API/Dockerfile .
```

## Carregando a Imagem

```bash
docker run --rm -it -p 5000:5000 a30e0830ec15
```

# Testes
Para gerar relatório de cobertura de teste de unidade e poder acompanhar de maneira mais intuitiva, instale o componente abaixo.
(https://www.nuget.org/packages/ReportGenerator/)

## Use os Comandos

```bash
dotnet test Test/Test.DesafioLeo.Test/Test.DesafioLeo.Test.csproj /p:CollectCoverage=true /p:CoverletOutputFormat=opencover /p:CoverletOutput=Coverage/ /p:excludebyattribute=*.ExcludeFromCodeCoverage*
```

```bash
C:\Users\alter\.nuget\packages\reportgenerator\4.8.12\tools\net5.0\ReportGenerator.exe "-reports:Test/Test.DesafioLeo.Test/Coverage/coverage.opencover.xml" "-targetdir:tests/Coverage"
```

Veja esses endereços de pastas são exemplos, quem for rodar precisa se atentar a pasta do seu computador.

## Para ver o relatório

Exemplo:
(file:///D:/Projetos/DesafioLeoGit/DesafioLeo/Test.DesafioLel/tests/Coverage/index.htm)
