# README
## MagicCrawler - Checkpoint I - ABD .NET (2024)
Este projeto é um web scraper feito em C# dedicado a coletar informações sobre *magic cards* de jogos de cartas colecionáveis, particularmente Magic: The Gathering e armazená-los em no arquivo `card.csv` fácil de acessar.

## Fontes de Informações
O scraper obtém suas informações do site Scryfall ([https://scryfall.com](https://scryfall.com)), que é uma fonte confiável e amplamente utilizada para dados de *magic cards*.

## Dependências
- [.NET 6.0](https://dotnet.microsoft.com/download/dotnet/6.0): Versão atual do framework .NET.
- [HtmlAgilityPack](https://www.nuget.org/packages/HtmlAgilityPack/): Biblioteca para manipulação de HTML.

## Requisitos
- .NET Core 3.1 ou superior.
- Acesso a internet para acessar o site Scryfall.

## Git Clone

1. Clone o repositório:
   ```sh
   git clone https://github.com/usuario/repositorio.git
   cd repositorio
   ```

2. Restaure as dependências:
   ```sh
   dotnet restore
   ```

3. Execute o projeto:
   ```sh
   dotnet run
   ```

## Ao Executar o Programa
1. Insira o ID da coleção que deseja escrapar.
2. O programa irá coletar as informações das cartas e exibi-las no console.
3. Automaticamente, um arquivo CSV será gerado com todos os dados coletados.

## Armazenamento de Dados
- O arquivo CSV com as informações coletadas é salvo no diretório de build do projeto.
- O caminho exato é: `MagicCrawler\bin\Debug\net8.0\cards.csv`.
- O arquivo `cards.csv` contém campos para Nome da Carta e Descrição, e será exibido no seguinte formato:
```
Name # Description
Nome da Carta # Descrição da Carta
...
``` 

## Limitações
- Este scraper é projetado para coletar dados públicos específicos do site ([https://scryfall.com](https://scryfall.com)).
- Certifique-se de respeitar os termos de serviço do site alvo.
- **⚠ O uso excessivo pode ser detectado e bloqueado pelo site ⚠**.

*Este projeto é uma ferramenta útil para colecionadores e jogadores de Magic: The Gathering que desejam organizar seus dados de cartas rapidamente e eficientemente.*
