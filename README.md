# 🛡️ Guardian – Redirecionamento Inteligente de Pix para Apostas

### 📘 Projeto desenvolvido em C# (.NET 8) com:
- **ASP .NET Core Web API**
- **Entity Framework Core (EF Core)**
- **Swagger/OpenAPI**
- **LINQ**
- **Integração com API pública (Banco Central – SELIC)**
- **Deploy em Cloud (Render)**

---

## 🎯 Objetivo do Projeto

O **Guardian** é uma API que atua como **assistente financeiro inteligente**.  
Ela detecta quando o usuário tenta fazer uma **transferência Pix para uma casa de apostas** e emite **alertas educativos e persuasivos**, mostrando quanto o valor poderia render se fosse **investido**.

Exemplo:

> “Você está prestes a transferir R$100 para uma plataforma de apostas.  
> Com esse valor, poderia aplicar em uma carteira XP com retorno estimado de X% ao ano.”

Se o usuário insistir e continuar apostando, a API passa a **analisar os hábitos** e enviar **alertas personalizados** com base no histórico dos últimos 30 dias.

---

## 📦 Funcionalidades Principais

| Categoria | Descrição | Endpoints |
|------------|------------|------------|
| **CRUD (Users)** | Criação, listagem, edição e exclusão de usuários | `/api/users` |
| **CRUD (Platforms)** | Cadastro e consulta de casas de apostas | `/api/platforms` |
| **CRUD (Transfers)** | Criação de transferências e registro automático de alertas | `/api/transfers` |
| **Alerts** | Exibição dos alertas gerados pelo sistema | `/api/alerts/user/{id}` |
| **Relatórios LINQ** | Total gasto e média por plataforma (últimos 30 dias) | `/api/transfers/report/last30/{userId}` |
| **Integração API Externa (BCB)** | Consulta da taxa SELIC via API pública | `/api/insights/selic-last` |

---

## 🧠 Tecnologias Utilizadas

- C# / .NET 8  
- ASP.NET Core Web API  
- Entity Framework Core (SQLite / Postgres)  
- AutoMapper  
- Swagger / Swashbuckle  
- LINQ  
- HttpClient / API Pública (Banco Central - SELIC)  
- Docker  
- Render Cloud Hosting

---

## ☁️ Deploy na Nuvem

- **Swagger Cloud:**  
  🔗 https://sprint4-csharp.onrender.com/swagger  

- **Healthcheck:**  
  🔗 https://sprint4-csharp.onrender.com/health  








