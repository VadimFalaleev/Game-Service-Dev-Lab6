
namespace YG
{
    [System.Serializable]
    public class SavesYG
    {
        public bool isFirstSession = true;
        public string language = "ru";
        public bool feedbackDone;
        public bool promptDone;

        // Ваши сохранения
        public int score;
        public int bestScore;
        public int deaths;
        public int brokenShields;
        public int time;
        public int startGames;
        public int attempts = 5;
    }
}