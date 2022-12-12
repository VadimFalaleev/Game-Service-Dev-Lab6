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
            if (a.name == "First Death" && YandexGame.savesData.deaths >= 1) AchievementComplete(a);
        }
    }

    void AchievementComplete(GameObject text)
    {
        text.GetComponent<TextMeshProUGUI>().color = Color.green;
    }
}