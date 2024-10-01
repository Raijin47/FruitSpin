using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    public static Game Instance;

    public event Action onStartGame;

    public static Stats Stats;


    [SerializeField] private Stats _stats;

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