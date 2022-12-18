using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using YG;
using TMPro;

public class DragonPicker : MonoBehaviour
{
    private void OnEnable() => YandexGame.GetDataEvent += GetLoadSave;
    private void OnDisable() => YandexGame.GetDataEvent -= GetLoadSave;

    public GameObject energyShieldPrefab;
    public int numEnergyShield = 3;
    public float energyShieldBottomY = -6;
    public float energyShieldRadius = 1.5f;
    public List<GameObject> shieldList;
    public TextMeshProUGUI scoreGT;
    public TextMeshProUGUI playerName;
    public GameObject adPanel;
    public bool died = false;
    public float timeLeft = 0;

    private void Start()
    {
        if (YandexGame.SDKEnabled) GetLoadSave();

        YandexGame.savesData.startGames++;
        YandexGame.SaveProgress();

        GetShields();
    }

    private void Update()
    {
        timeLeft += Time.deltaTime;
        if (timeLeft >= 1)
        {
            YandexGame.savesData.time++;
            YandexGame.SaveProgress();

            timeLeft = 0;
        }
    }

    public void GetShields()
    {
        shieldList = new();

        for (int i = 1; i <= numEnergyShield; i++)
        {
            GameObject tShieldGo = Instantiate(energyShieldPrefab);
            tShieldGo.transform.position = new(0, energyShieldBottomY, 0);
            tShieldGo.transform.localScale = new(1 * i, 1 * i, 1 * i);
            shieldList.Add(tShieldGo);
        }
    }

    public void DragonEggDestroyed()
    {
        GameObject tDragonEgg = GameObject.FindGameObjectWithTag("Dragon Egg");
        Destroy(tDragonEgg);

        int shieldIndex = shieldList.Count - 1;
        GameObject tShieldGo = shieldList[shieldIndex];
        shieldList.RemoveAt(shieldIndex);
        Destroy(tShieldGo);

        YandexGame.savesData.brokenShields++;
        YandexGame.SaveProgress();

        if (shieldList.Count == 0 && !died)
        {
            adPanel.SetActive(true);
            Time.timeScale = 0;
            died = true;
        }
        else if (shieldList.Count == 0 && died)
        {
            Die();
        }
    }

    public void WatchAdd()
    {
        YandexGame.RewVideoShow(0);
        adPanel.SetActive(false);
        GetShields();
    }

    public void Die()
    {
        UserSave(int.Parse(GetScore().text), YandexGame.savesData.bestScore);

        YandexGame.NewLeaderboardScores("TOPPlayerScore", int.Parse(scoreGT.text));

        //YandexGame.RewVideoShow(0);
        SceneManager.LoadScene("_0Scene");
        GetLoadSave();
    }

    public TextMeshProUGUI GetScore()
    {
        GameObject scoreGO = GameObject.Find("Score");
        scoreGT = scoreGO.GetComponent<TextMeshProUGUI>();

        return scoreGT;
    }

    public void GetLoadSave()
    {
        Debug.Log(YandexGame.savesData.score);

        GameObject playerNamePrefabGUI = GameObject.Find("PlayerName");
        playerName = playerNamePrefabGUI.GetComponent<TextMeshProUGUI>();
        playerName.text = YandexGame.playerName;
    }

    public void UserSave(int currentScore, int currentBestScore)
    {
        YandexGame.savesData.score = currentScore;
        if (currentScore > currentBestScore) YandexGame.savesData.bestScore = currentScore;
        
        YandexGame.savesData.deaths++;

        YandexGame.SaveProgress();
    }
}