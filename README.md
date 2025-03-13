TeeMug Shop - Especificação do Projeto

Visão Geral

TeeMug Shop é uma plataforma de e-commerce focada na venda de camisetas e canecas personalizadas. O projeto foi desenvolvido seguindo as melhores práticas de desenvolvimento de software, garantindo escalabilidade, segurança e uma excelente experiência do usuário.

Tecnologias Utilizadas

Frontend:

ReactJS com Vite

TypeScript (.tsx)

Bootstrap (Mobile-First)

Prettier e ESLint para boas práticas de codificação

React Hooks

Suporte a temas dinâmicos

Suporte a múltiplos idiomas (Português, Inglês, Espanhol, Francês)

Autenticação via Google e Facebook

Backend:

ASP.NET Core 9.0

Entity Framework (Code-First)

ASP.NET Identity para gerenciamento de usuários

MySQL (em container Docker)

Swagger para documentação das APIs

Cache para sessão de login

Pipeline de CI/CD (GitHub Actions ou similar)

Deploy somente se os testes passarem e não houver vulnerabilidades

Infraestrutura:

Docker para hospedar backend, frontend e banco de dados

Repositório GitHub para controle de versão

CI/CD para verificação de segurança e bugs

Requisitos do Sistema

1. Estrutura do Site

Menu (Navbar):

Menu horizontal com as opções:

T-Shirts

Mugs

Cart (Carrinho de Compras)

Ícone de usuário com menu suspenso contendo:

Entrar

Cadastrar

Dropdown para seleção de idioma

Responsividade garantida via Bootstrap (Mobile-First)

2. Autenticação e Cadastro

Login via Google e Facebook

Cadastro de novos usuários com os seguintes dados:

Nome

Endereço (Morada)

Email

NIF

Telefone

Senha

3. Catálogo de Produtos

Páginas T-Shirts e Mugs:

Listagem dos produtos em grid-template (4 colunas por linha)

Cada item deve conter:

Foto do produto

Descrição

Preço

Botão "Add to Cart"

Totalmente responsivo para dispositivos móveis

4. Carrinho de Compras (Cart)

Listagem dos produtos adicionados ao carrinho

Opção de finalizar compra

Se o usuário não estiver logado, será obrigado a se cadastrar antes de concluir a compra

5. Finalização da Compra

Página de pagamento via cartão de crédito

Registro da compra no banco de dados

6. Admin Dashboard

Página restrita ao administrador contendo:

6 gráficos estatísticos relacionados às compras

Ao clicar no gráfico de pedidos, o admin é redirecionado para uma lista de pedidos

Opção de marcar um pedido como "Enviado" e "Finalizar Compra"

7. Gerenciamento de Usuários

Página para listagem e pesquisa de usuários

Opção para bloquear ou desbloquear usuários

Implantacão

O frontend, backend e banco de dados serão hospedados via Docker

Deploy será realizado apenas após verificação de bugs e segurança via CI/CD

Uso de cache para login de usuários

Observações

Todo o projeto deverá seguir boas práticas de desenvolvimento

O design deve ser responsivo e otimizado para dispositivos móveis

O código deve ser limpo, bem estruturado e documentado

