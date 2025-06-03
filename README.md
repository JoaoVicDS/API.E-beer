# 🍺 APIEbeer
APIEbeer é uma API desenvolvida com ASP.NET Core, focada em fornecer recomendações de cervejas artesanais com base nas preferências do usuário. Ela faz parte de um ecossistema maior iniciado com o projeto Ebeer, e tem como objetivo se tornar um serviço independente e escalável para outros nichos no futuro.

# Funcionalidades
- Recebe um JSON estruturado como cardápio com opções de cervejas.
- Gera formularios dinâmicos baseados no conteúdo do JSON.
- Fornece uma página web com o formulário renderizado.
- Retorna uma recomendação personalizada com base nas respostas enviadas.

# Clonando repo
clone o projeto
```
git clone https://github.com/JoaoVicDS/APIEbeer.git
```
# Como rodar localmente
### Pré-requisitos
- .NET 6 ou superior

### Passo a passo
Acesse o diretório do projeto
```
cd APIEbeer
```
Compile o projeto
```
dotnet build
```
Rode a aplicação
```
dotnet run
```
A aplicação estará disponível, por padrão, em:

- https://localhost:5001

- http://localhost:5000

# Status
### Em desenvolvimento
A API ainda está em construção, sem bibliotecas externas integradas. Planejo integrar recursos como cache, autenticação, logs e recomendações baseadas em IA no futuro.

