# 📺 StreamWorld – Catálogo de Filmes e Séries

> Aplicação web desenvolvida em **ASP.NET Core MVC (.NET 8)** para exibição e administração de um catálogo de filmes e séries da plataforma fictícia **StreamWorld**.

---

## 📘 Visão Geral

A aplicação permite que visitantes:

- Explorem o catálogo de produções
- Pesquisem filmes e séries
- Visualizem detalhes de artistas e produções
- Enviem mensagens via formulário de contato

Administradores autenticados podem gerenciar:

- Produções  
- Artistas  
- Gêneros  
- Mensagens enviadas pelos usuários  

---

# 🧩 Funcionalidades

---

## 🏠 1. Página Inicial

Exibe:

- Os **10 títulos mais recentes**
- Capa e nome da produção
- Link direto para os detalhes

---

## 🔍 2. Busca de Produções

Busca por:

- Título  
- Artista  
- Gênero  

Retorno apresenta:

- Nome
- Capa
- Ano de lançamento

---

## 🎬 3. Detalhes da Produção

Inclui:

- Título  
- Gêneros associados  
- Ano de lançamento  
- Diretor  
- Lista de artistas + personagens  
- Capa da produção  

---

# 👥 Artistas

---

## 👤 4. Busca de Artistas

Busca por:

- Nome  
- País de origem  

Exibe:

- Foto  
- Nome  
- Link para detalhes  

---

## ⭐ 5. Detalhes do Artista

Mostra:

- Nome completo  
- Data de nascimento  
- País  
- Foto  
- Produções + personagens interpretados  

---

# 📨 Formulário de Contato

---

## 📬 6. Envio de Mensagens

Campos:

- Nome completo  
- E-mail  
- Assunto  
- Mensagem  

As mensagens são salvas no banco e apenas administradores autenticados podem visualizar.

---

# 🔐 Área Administrativa

Funções disponíveis:

- CRUD de Produções  
- CRUD de Artistas  
- CRUD de Gêneros  
- Visualização das mensagens recebidas  

Usuário padrão (exemplo):

admin / 1234

---

# 🗃️ Modelo de Dados

---

## 🎞️ Produção
- ID
- Título
- Data de lançamento
- Diretor
- Foto
- Gêneros (N:N)
- Artistas (N:N) + personagem

## 🎭 Artista
- ID
- Nome
- Data de nascimento
- País
- Foto

## 🏷️ Gênero
- ID
- Nome

## ✉️ Contato
- ID
- Nome
- E-mail
- Assunto
- Mensagem
- Data/Hora

---

# ⚙️ Como Executar o Projeto


## 1️. Clonar o repositório

git clone https://github.com/Leonardo4u/StreamWorld

## 2️. Configurar o appsettings.json
```json
"ConnectionStrings": {
  "DefaultConnection": "SUA_CONNECTION_STRING_AQUI"
}
```
---

## 3️. Aplicar migrações
```
dotnet ef database update
```

---

## 4. Executar o servidor
```
dotnet run
```

---

Estrutura da Solução
- /Models
- /Views
- /Controllers
- /wwwroot
