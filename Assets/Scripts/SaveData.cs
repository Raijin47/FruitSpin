using System;
using UnityEngine;

[Serializable]
public class Data
{
    public bool[] purchasedSkin = new bool[5];
    public int currentEquipSkin;

    public Data()
    {
        purchasedSkin[0] = true;
    }
}

public class SaveData : MonoBehaviour
{
    public event Action OnSavesLoaded;
    public static SaveData Instance;

    public Data savesData;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        Load();
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
 

    public void Save()
    {
        string jsonData = JsonUtility.ToJson(savesData);
        PlayerPrefs.SetString(nameof(Data), jsonData);
    }

    private void OnApplicationQuit() => Save();
}