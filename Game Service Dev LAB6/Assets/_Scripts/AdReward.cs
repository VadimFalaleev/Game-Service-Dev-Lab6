using UnityEngine;
using YG;

public class AdReward : MonoBehaviour
{
    private void OnEnable() => YandexGame.CloseVideoEvent += Rewarded;
    private void OnDisable() => YandexGame.CloseVideoEvent -= Rewarded;

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
        YandexGame.RewVideoShow(Random.Range(0,2));
    }
}