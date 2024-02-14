using poolpal_api.Services;

namespace Poolpal.UnitTests
{
    [TestClass]
    public class EloServiceTest
    {
        [TestMethod]
        public void Should_calculate_correct_elo()
        {
            int player1Elo = 1600, player2Elo = 1500;
            int newPlayer1Elo = EloService.CalculateNewPlayerElo(player1Elo,player2Elo,true),newPlayer2Elo = EloService.CalculateNewPlayerElo(player2Elo,player1Elo,false);
            Assert.AreEqual(1611, newPlayer1Elo);
            Assert.AreEqual(1489, newPlayer2Elo);
        }
    }
}