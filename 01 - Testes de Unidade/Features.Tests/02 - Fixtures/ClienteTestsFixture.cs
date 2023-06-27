using Bogus;
using Bogus.DataSets;
using Features.Clientes;
using System;
using Xunit;

namespace Features.Tests
{
    [CollectionDefinition(nameof(ClienteCollection))] //Fala que ela é uma Coleção/Define que ela é uma coleção
    public class ClienteCollection : ICollectionFixture<ClienteTestsFixture> //Uma Classe de coleção de fixture que atende uma coleção de textes
    { }

    public class ClienteTestsFixture : IDisposable
    {
        public Cliente GerarClienteValido()
        {
            var genero = new Faker().PickRandom<Name.Gender>();
            //var email = new Faker().Internet.Email("eduardo", "pires", "gmail");

            //var clienteFaker = new Faker<Cliente>();
            //clienteFaker.RuleFor(c => c.Nome, (f, c) => f.Name.FirstName());

            var cliente = new Faker<Cliente>("pt_BR").CustomInstantiator(f => new Cliente(
                    Guid.NewGuid(),
                    f.Name.FirstName(genero),
                    f.Name.LastName(genero),
                    f.Date.Past(80, DateTime.Now.AddYears(-18)),
                    "", //email vazio pois é gerado posteriormente
                    true,
                    DateTime.Now
            )).RuleFor(c => c.Email, (f, c) => f.Internet.Email(c.Nome.ToLower(), c.Sobrenome.ToLower()));

            return cliente;
        }

        public Cliente GerarClienteInValido()
        {
            var cliente = new Cliente(
                Guid.NewGuid(),
                "",
                "",
                DateTime.Now,
                "edu2edu.com",
                true,
                DateTime.Now);

            return cliente;
        }

        public void Dispose()
        {
        }
    }
}