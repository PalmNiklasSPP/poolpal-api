namespace poolpal_api.Services
{
    public class EloService
    {
        static public int CalculateNewPlayerElo(int playerElo, int opponentElo, bool winner = false, int kValue = 30, bool draw = false)
        {
            float winnerValue = winner ? 1 : 0;
            winnerValue = draw ? 0.5f : winnerValue;
            double denominator = 1 + Math.Pow(10f, (opponentElo - playerElo) / 400f);
            double playerExpectedElo = 1/ denominator;
            int newPlayerRating = (int)Math.Round(playerElo + kValue * (winnerValue - playerExpectedElo));

            return newPlayerRating;
        }
    }
}
