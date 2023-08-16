using Microsoft.Extensions.Logging;
using PokeApiConsumer.Models;
using PokeConsumer.Controllers;

namespace PokeApiConsumerTest
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public  void Test1()
        {
            PokeController poke = new PokeController();
            Pokemon result = poke.GetByNameOrId("1").Result;

            Assert.AreEqual("bulbasaur", result.name);
        }

        [Test]
        public void Test2()
        {
            PokeController poke = new PokeController();
            List<Pokemon> result = poke.GetTenRandomFullInfo().Result;

            Assert.AreEqual(10, result.Count);
        }
    }
}