# 🌦️ integration.weather
Projeto desenvolvido para integração de múltiplas fontes de dados meteorológicos, utilizando princípios de arquitetura de software modernos e padrões de projeto para garantir flexibilidade, manutenção e escalabilidade.

## 👥 Integrantes do Grupo

- **23012069** – Jéssica Silva Kushida  
- **23013238** – Marcela Franco  
- **23008255** – Natália Naomi Sumida  
- **23009486** – Nicole Silvestrini Garrio  

## 🧠 Arquitetura e Boas Práticas
Este projeto foi construído com base nos **princípios SOLID** e na **Clean Architecture**, com o objetivo de criar um sistema altamente coeso e desacoplado. A organização do código segue uma separação clara por camadas:
- `Api`: exposição dos endpoints REST via Swagger.
- `Application`: lógica de orquestração, services e DTOs.
- `Domain`: entidades e contratos (interfaces), sem dependências externas.
- `Adapters`: implementações de integração com APIs externas.

### 🧱 Padrões de Projeto (GoF) Utilizados
- **Adapter**: abstrai as diferenças entre as APIs externas (OpenWeatherMap e WeatherBit), expondo um contrato comum para o sistema.
- **Template Method**: define um fluxo fixo para requisições HTTP com personalizações específicas para cada provedor.
- **Strategy**: permite que o `IntegrationService` selecione dinamicamente o provedor adequado, com base na entrada recebida.

## ⚙️ Como Funciona
O sistema oferece uma API RESTful para consulta de dados climáticos, que pode ser acessada e testada diretamente pelo Swagger (UI gerada automaticamente ao rodar o projeto). O usuário pode escolher o provedor de dados meteorológicos (OpenWeatherMap ou WeatherBit), e o sistema retorna as informações de forma unificada e padronizada.

## 🔐 Requisitos
Para executar o projeto, é necessário obter **chaves de API** gratuitas dos seguintes serviços:
- [OpenWeatherMap](https://openweathermap.org/api)
- [WeatherBit](https://www.weatherbit.io/)

Essas chaves devem ser configuradas no arquivo `appsettings.json`, conforme exemplo:

```json
{
  "WeatherProviders": {
    "OpenWeather": {
      "ApiKey": "SUA_CHAVE_AQUI"
    },
    "WeatherBit": {
      "ApiKey": "SUA_CHAVE_AQUI"
    }
  }
}
