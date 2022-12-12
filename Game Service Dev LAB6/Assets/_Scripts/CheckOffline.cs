using System;
using UnityEngine;
using TMPro;

public class CheckOffline : MonoBehaviour
{
    private TextMeshProUGUI statusText;
    public static CheckOffline Instance { get; private set; }
    private void InitSingleton()
    {
        Instance = this;
    }

    private void Awake()
    {
        InitSingleton();
        CheckOfflineStatus();
    }

    private void CheckOfflineStatus()
    {
        TimeSpan ts;
        if (PlayerPrefs.HasKey("LastSession"))
        {
            ts = DateTime.Now - DateTime.Parse(PlayerPrefs.GetString("LastSession"));

            GameObject status = GameObject.Find("Status");
            statusText = status.GetComponent<TextMeshProUGUI>();
            statusText.text = string.Format("Status: Online \n Вы отсутствовали: {0} день(дней/дня), {1} час(а/ов), {2} минут(у/ы), {3} секунд(у/ы)", ts.Days, ts.Hours, ts.Minutes, ts.Seconds);
        }

        PlayerPrefs.SetString("LastSession", DateTime.Now.ToString());
    }
}