using System;
using System.Collections;
using UnityEngine;

public class Gagame : MonoBehaviour
{
    public static Gagame Instance;

    public event Action onStartGame;

    public static StatsFruit Stats;


    [SerializeField] private StatsFruit _stats;

    private void Start()
    {
        Instance = this;
        _stats.Init();
        Stats = _stats;
    }

    public void StartGame()
    {
        onStartGame?.Invoke();
    }

    private Coroutine _cor;
    public void StartAnum(IEnumerator coroutine)
    {
        _cor = StartCoroutine(coroutine);
    }
}