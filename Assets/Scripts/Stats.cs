using Neoxider.Bonus;
using Neoxider.Shop;
using System;
using TMPro;
using UnityEngine;

[System.Serializable]
public class SaveData
{
    public int totalBet;
    public int totalWin;
    public int totalSpin;
    public int[] animalClick = new int[48];
    public bool daylyBonusTake = false;
}

public class Stats : MonoBehaviour
{
    public static Stats Instance;
    public SetText textTotalBet;
    public SetText textTotalWin;
    public TMP_Text textTotalSpin;
    public TimeReward TimeReward;

    public SaveData data;

    public void SetStats(int bet, int win, int spin)
    {
        data.totalBet = bet;
        data.totalWin = win;
        data.totalSpin = spin;
    }

    private void Start()
    {
        data.animalClick = new int[48];

        Load();
        UpdateText();
        Instance = this;
        TimeReward.OnRewardAvailable.AddListener(GetReward);
        Application.targetFrameRate = 60;
    }

    private void GetReward()
    {
        Debug.Log("GetReward");
        for (int i = 0; i < data.animalClick.Length; i++)
            data.animalClick[i] = 5;

        Money.Instance.Add(50);
        Save();

        TimeReward.TakeReward();
    }

    private void Load()
    {
        string jsonData = PlayerPrefs.GetString(nameof(SaveData), String.Empty);

        if (!String.IsNullOrEmpty(jsonData))
        {
            data = JsonUtility.FromJson<SaveData>(jsonData);
        }
    }

    private void OnApplicationQuit() => Save();
    

    private void Save()
    {
        string jsonData = JsonUtility.ToJson(data);
        PlayerPrefs.SetString(nameof(SaveData), jsonData);
    }

    private void UpdateText()
    {
        textTotalBet.text = data.totalBet.ToString();
        textTotalWin.text = data.totalWin.ToString();
        textTotalSpin.text = data.totalSpin.ToString();
    }

    public void AddTotalBet(int count)
    {
        data.totalBet += count;
        data.totalSpin += 1;
        UpdateText();
        Save();
    }

    public void AddTotalWin(int count)
    {
        data.totalWin += count;
        UpdateText();
        Save();
    }
}