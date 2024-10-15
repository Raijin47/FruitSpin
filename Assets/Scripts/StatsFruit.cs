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
    [SerializeField] private Image[] _imageOrder;
    [SerializeField] private TMP_Text[] _textOrder;
    [SerializeField] private TMP_Text _profitOrder;
    [SerializeField] private TMP_Text _totalOrder;

    [Space(10)]
    [SerializeField] private GameObject _blockingObject;
    [SerializeField] private GameObject _panelGameOver, _panelWin;

    [SerializeField] private TMP_Text _textBalance;
    [SerializeField] private TMP_Text _textProfit;
    [SerializeField] private TMP_Text _textTotalSpin;
    [SerializeField] private Image[] _images;
    [SerializeField] private TMP_Text[] _textTask;
    
    private readonly int[] _countTask = new int[4];

    public Image[] Img => _images;
    public int[] Task => _countTask;

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
            _profitOrder.text = _textProfit.text;
        }
    }
    public int TotalSpin 
    {
        get => _totalSpin;
        set 
        {
            _totalSpin = value;
            _textTotalSpin.text = $"Total spin: {_totalSpin}";
            _totalOrder.text = _textTotalSpin.text;
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
        Balance = 50;
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
            _countTask[i] = Random.Range(1, 5);
            _textTask[i].text = $"x{_countTask[i]}";

            _imageOrder[i].sprite = _sprites[RandomNum[i]];
            _imageOrder[i].SetNativeSize();
            _textOrder[i].text = $"x{_countTask[i]}";
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

    public void CheckBalance()
    {
        if (Balance == 0)
        {
            _panelGameOver.SetActive(true);
            AudioControllerBlendera.Instance.GameOver();
        }
        else AudioControllerBlendera.Instance.Fail();

        _blockingObject.SetActive(false);
    }

    private void Check(Sprite[] sprites)
    {
        int[] buffer = new int[4];

        //for(int i = 0; i < _images.Length; i++)
        //{
        //    for (int a = 0; a < sprites.Length; a++)
        //    {
        //        if (sprites[a] == _images[i].sprite && _countTask[i] != 0)
        //        {
        //            buffer[i]++;
        //        }
        //    }
        //}

        for(int i = 0; i < _images.Length; i++)       
            for(int a = 0; a < sprites.Length; a++)           
                if (sprites[a] == _images[i].sprite && _countTask[i] != 0)                
                    buffer[i]++;

        if (IsFoundFruit(buffer))
        {
            List<Sprite> sprite = new();
            for (int i = 0; i < buffer.Length; i++)
            {
                if (buffer[i] >= 2)
                {
                    sprite.Add(_images[i].sprite);
                    int count = buffer[i] == 2 ? 1 : 2;
                    _countTask[i] -= count;
                    if (_countTask[i] < 0) _countTask[i] = 0;
                }
            }

            _slicePanel.SetActive(true);
            _sliceFruit.AddSprite(sprite);
            //AnimationFruitBlender.Instance.StartAnim(sprite);
            AudioControllerBlendera.Instance.SmallWin();
        }
        else CheckBalance();
    }
    [SerializeField] private GameObject _slicePanel;
    [SerializeField] private SliceFruit _sliceFruit;

    private bool IsFoundFruit(int[] task)
    {
        for(int i = 0; i < task.Length; i++)
        {
            if (task[i] >= 2 && _countTask[i] != 0)
                return true;
        }

        return false;
    }

    public void CheckComplated()
    {
        for(int i = 0; i < _images.Length; i++)
        {
            _textTask[i].text = $"x{_countTask[i]}";
        }

        _blockingObject.SetActive(false);

        if(IsComplated())
        {
            _panelWin.SetActive(true);
            AudioControllerBlendera.Instance.Win();

            Diamonds.Instance.Add(Balance);
            Profit += Balance;
            Balance = 50;

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