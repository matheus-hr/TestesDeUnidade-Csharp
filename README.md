# Teste de Unidade utilizando xUnit

Este é um projeto de teste de unidade em C# que utiliza a estrutura de teste xUnit.

O objetivo deste projeto é demonstrar o uso de várias tecnologias relacionadas a testes de unidade no ecossistema C#. 

## Tecnologias Utilizadas

* **xUnit:** Framework de teste de unidade para .NET.
  
* **Bogus:** Biblioteca para geração de dados falsos para testes.
  
* **Moq:** Biblioteca para criação de objetos simulados (mocks) durante os testes.
  
* **Auto Mock (Moq):** Extensão do Moq para simplificar a criação de mocks e injeção de dependências automaticamente.
  
* **Fluent Assertions:** Biblioteca para escrever asserções de forma fluente e legível.
  
* **Coverlet:** Ferramenta para medir a cobertura de código em projetos .NET.

## Conceitos Utilizados

*  **Arrange, Act, Assert:** Uma abordagem para estruturar testes de unidade de forma clara e organizada
    *  **Arrange:** Nesta etapa, você deve preparar o ambiente para o teste, como instanciar e preparar objetos.
      
    *  **Act:** Nesta etapa, você executa a ação ou o comportamento que deseja testar. Como acionar um método.
      
    *  **Assert:** Nesta etapa, você verifica o resultado esperado do teste. Você compara o resultado obtido com o resultado esperado usando asserções (assertions).
      
*  **Fact:** Identifica um método de teste como um fato independente.
  
*  **Theory:** Identifica um método de teste como uma teoria parametrizada.
  
*  **Trait:** Adiciona metadados aos testes para facilitar a organização e categorização.
  
* **Fixtures:** Fornece um ambiente controlado para a execução de testes.
  
*  **Mock:** Criação objetos simulados para testar dependências em isolamento.
  
*  **TestOutputHelper:** Um helper que ajuda a fazer um output de informações nos testes.
  
*  **Playlist:** Criação de playlist de testes.
  
*  **Cobertura de codigo:** É uma métrica utilizada para avaliar a porcentagem de código fonte de um programa que é executada durante a execução dos testes.  

## Instruções de Uso

1. Clone o repositório.
2. Abra o projeto no Visual Studio ou em qualquer outra IDE compatível com C#.
3. Compile o projeto para garantir que todas as dependências sejam restauradas.
4. Execute os testes de unidade para validar o código.
