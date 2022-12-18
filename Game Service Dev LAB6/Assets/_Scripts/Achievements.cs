using UnityEngine;
using TMPro;
using YG;

public class Achievements : MonoBehaviour
{
    private GameObject[] achievements;

    private void Awake()
    {
        achievements = GameObject.FindGameObjectsWithTag("Achievement");

        foreach (var a in achievements)
        {
            if (a.name == "10 Points" && YandexGame.savesData.bestScore >= 10) AchievementComplete(a);
            if (a.name == "20 Points" && YandexGame.savesData.bestScore >= 20) AchievementComplete(a);
            if (a.name == "30 Points" && YandexGame.savesData.bestScore >= 30) AchievementComplete(a);

            if (a.name == "1 Death" && YandexGame.savesData.deaths >= 1) AchievementComplete(a);
            if (a.name == "3 Death" && YandexGame.savesData.deaths >= 3) AchievementComplete(a);
            if (a.name == "5 Death" && YandexGame.savesData.deaths >= 5) AchievementComplete(a);

            if (a.name == "Shields" && YandexGame.savesData.brokenShields >= 20) AchievementComplete(a);
            if (a.name == "Time" && YandexGame.savesData.time >= 300) AchievementComplete(a);
            if (a.name == "Attempts" && YandexGame.savesData.startGames >= 10) AchievementComplete(a);
        }
    }

    void AchievementComplete(GameObject text)
    {
        text.GetComponent<TextMeshProUGUI>().color = Color.green;
    }
}