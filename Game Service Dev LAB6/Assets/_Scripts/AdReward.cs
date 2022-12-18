using UnityEngine;
using YG;
using TMPro;

public class AdReward : MonoBehaviour
{
    private void OnEnable() => YandexGame.CloseVideoEvent += Rewarded;
    private void OnDisable() => YandexGame.CloseVideoEvent -= Rewarded;

    private int attempts = 5;

    void Rewarded(int id)
    {
        if(id == 1)
        {
            Debug.Log("Пользователь получил награду");
        }
        else
        {
            Debug.Log("Пользователь остался без награды");
        }
    }

    public void OpenAd()
    {
        if (attempts > 0)
        {
            YandexGame.RewVideoShow(UnityEngine.Random.Range(0, 2));
            attempts--;

            GameObject attempt = GameObject.Find("Attempts");
            TextMeshProUGUI attemptsText = attempt.GetComponent<TextMeshProUGUI>();
            attemptsText.text = string.Format("Attempts left: {0}", attempts);
        }
    }
}