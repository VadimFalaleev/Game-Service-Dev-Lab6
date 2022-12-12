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
            Debug.Log("������������ ������� �������");
        }
        else
        {
            Debug.Log("������������ ������� ��� �������");
        }
    }

    public void OpenAd()
    {
        YandexGame.RewVideoShow(Random.Range(0,2));
    }
}