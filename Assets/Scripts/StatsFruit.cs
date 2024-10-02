using Neoxider.Shop;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

[Serializable]
public class StatsFruit
{
    [SerializeField] private GameObject _blockingObject;
    [SerializeField] private GameObject _panelGameOver, _panelWin;


    [SerializeField] private Image _animationImage;
    [SerializeField] private TMP_Text _textBalance;
    [SerializeField] private TMP_Text _textProfit;
    [SerializeField] private TMP_Text _textTotalSpin;
    [SerializeField] private Image[] _images;
    [SerializeField] private TMP_Text[] _textTask;
    
    private readonly int[] _countTask = new int[4];


    [SerializeField] private Sprite[] _sprites;


    private int _balance;
    private int _profit;
    private int _totalSpin;

    public int Balance 
    { 
        get => _balance;
        set 
        {
            _balance = value;
            _textBalance.text = $"{_balance}$";
        } 
    }
    public int Profit
    {
        get => _profit;
        set
        {
            _profit = value;
            _textProfit.text = $"Profit: {_profit}$";
        }
    }
    public int TotalSpin 
    {
        get => _totalSpin;
        set 
        {
            _totalSpin = value;
            _textTotalSpin.text = $"Total spin: {_totalSpin}";
        } 
    }

    public void Init()
    {
        Gagame.Instance.onStartGame += StartGame;
        SpinControll.Instance.OnStartSpin.AddListener(() => { TotalSpin++; _blockingObject.SetActive(true); });
        SpinControll.Instance.OnWin.AddListener(Check);
    }

    private void StartGame()
    {
        Balance = 20;
        Profit = 0;
        TotalSpin = 0;

        GetTask();
    }

    private void GetTask()
    {
        List<int> RandomNum = new();

        for (int r = 0; r < _images.Length; r++)
        {
            int num = Random.Range(0, _sprites.Length);

            while (RandomNum.Contains(num))
            {
                num = Random.Range(0, _sprites.Length);
            }

            RandomNum.Add(num);
        }

        for (int i = 0; i < _images.Length; i++)
        {
            _images[i].sprite = _sprites[RandomNum[i]];
            _images[i].SetNativeSize();
            _countTask[i] = 4;
            _textTask[i].text = $"x{_countTask[i]}";
        }
    }

    public bool Spend()
    {
        if (Balance > 0)
        {
            Balance--;
            return true;
        }
        else return false;
    }

    private int _count;

    private void Check(Sprite sprite, int count)
    {
        if (IsFoundFruit(sprite, count))
        {
            _animationImage.sprite = sprite;
            _animationImage.SetNativeSize();
            AnimationFruitBlender.Instance.StartAnim();
            AudioControllerBlendera.Instance.SmallWin();
        }
        else 
        {
            if (Balance == 0) 
            {
                _panelGameOver.SetActive(true);
                AudioControllerBlendera.Instance.GameOver();
            }
            else AudioControllerBlendera.Instance.Fail();

            _blockingObject.SetActive(false);
        } 
    }

    private bool IsFoundFruit(Sprite sprite, int count)
    {
        for (int i = 0; i < _images.Length; i++)
        {
            if (sprite == _images[i].sprite && _countTask[i] != 0)
            {
                _count = _countTask[i] == 1 ? 1 : count;
                _countTask[i] -= count;
                if (_countTask[i] <= 0) _countTask[i] = 0;
                return true;
            }
        }

        return false;
    }

    public void CheckComplated()
    {
        for(int i = 0; i < _images.Length; i++)
        {
            _textTask[i].text = $"x{_countTask[i]}";
        }

        Balance += _count * 2;
        Profit += _count * 2;

        _blockingObject.SetActive(false);

        if(IsComplated())
        {
            _panelWin.SetActive(true);
            AudioControllerBlendera.Instance.Win();
            Balance += 20;
            Profit += 100;
            Diamonds.Instance.Add(Profit);
            GetTask();
        }
    }

    private bool IsComplated()
    {
        for(int i = 0; i < _images.Length; i++)      
            if (_countTask[i] != 0)
                return false;
        
        return true;
    }
}