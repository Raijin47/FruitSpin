using Neoxider.Shop;
using System;
using TMPro;
using UnityEngine;

[System.Serializable]
public class Data
{
    public int totalBet;
    public int totalWin;
    public int totalSpin;
    public bool[] purchasedSkin = new bool[5];
    public int currentEquipSkin;

    public Data()
    {
        purchasedSkin[0] = true;
    }
}

public class Stats : MonoBehaviour
{
    public static event Action OnSavesLoaded;
    public static Stats Instance;
    public TMP_Text textTotalBet;
    public TMP_Text textTotalWin;
    public TMP_Text textTotalSpin;

    public Data savesData;

    public void SetStats(int bet, int win, int spin)
    {
        savesData.totalBet = bet;
        savesData.totalWin = win;
        savesData.totalSpin = spin;
    }

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        Load();
        UpdateText();
        Application.targetFrameRate = 60;
    }

    private void Load()
    {
        string jsonData = PlayerPrefs.GetString(nameof(Data), String.Empty);

        if (!String.IsNullOrEmpty(jsonData))
        {
            savesData = JsonUtility.FromJson<Data>(jsonData);
        }

        OnSavesLoaded?.Invoke();
    }

    private void OnApplicationQuit() => Save();
    

    public void Save()
    {
        string jsonData = JsonUtility.ToJson(savesData);
        PlayerPrefs.SetString(nameof(Data), jsonData);
    }

    private void UpdateText()
    {
        textTotalBet.text = ConvertNumber.Convert(savesData.totalBet, end: "\nTotal bet");
        textTotalWin.text = ConvertNumber.Convert(savesData.totalWin, "Profit: ");
        textTotalSpin.text = ConvertNumber.Convert(savesData.totalSpin, "Total spin: ");
    }

    public void AddTotalBet(int count)
    {
        savesData.totalBet += count;
        savesData.totalSpin += 1;
        UpdateText();
        Save();
    }

    public void AddTotalWin(int count)
    {
        savesData.totalWin += count;
        UpdateText();
        Save();
    }
}