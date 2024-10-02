using Neoxider.Shop;
using System.Collections;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class SpinControll : MonoBehaviour
{
    public static SpinControll Instance;

    [SerializeField] private CheckSpining _checkSpin = new();
    [SerializeField] private BetsData _betsData;
    [SerializeField] private SpritesData _allSpritesData;

    [Space, Header("Settings")]
    [SerializeField] private bool _priceOnLine = true;
    [SerializeField] private int _countRowElements = 3;
    [SerializeField] private Rowewr[] _rows;
    [SerializeField] private Sprite[,] _spritesEnd;
    [Range(0f, 1f)] public float chanseWin = 0.5f;

    [Space, Header("Visual")]
    [SerializeField] private float _delaySpinRoll = 0.2f;
    [SerializeField] private bool _isSingleSpeed = true;
    [SerializeField] private SpeedController _speedControll = new();
    [SerializeField] private Vector2 _space = Vector2.one;
    [SerializeField] private VisualSlot _lineSlot = new();

    [Space, Header("Text"), Space]
    [SerializeField] private TMP_Text _textCountLine;

    [Space, Header("Events")]
    public UnityEvent OnStartSpin;

    [Space]
    public UnityEvent<Sprite[]> OnWin;
    public static System.Action<int[]> OnWinLines;
    public UnityEvent OnLose;

    [Space]
    public UnityEvent<string> OnChangeBet;
    public UnityEvent<string> OnChangeMoneyWin;

    [Space, Header("Debug")]
    [SerializeField, Min(1)] private int _countLine = 1;
    [SerializeField, Min(0)] private int _betsId = 0;

    private int price;

    public Sprite[,] SpritesEnd { get => _spritesEnd; set => _spritesEnd = value; }

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        _betsId = 0;

        for (int i = 0; i < _rows.Length; i++)
        {
            _rows[i].SetSprites(_allSpritesData.sprites[0]);
        }

        SetPrice();

        _lineSlot.LineActiv(false);
    }

    private void SetPrice()
    {
        int linePrice = _betsData.bets[_betsId];
        price = _priceOnLine ? _countLine * linePrice : linePrice;

        if (_textCountLine != null)
            _textCountLine.text = _countLine.ToString();

        OnChangeBet?.Invoke(price.ToString());

        int[] sequence = Enumerable.Range(0, _countLine).ToArray();
        _lineSlot.LineActiv(sequence);
    }

    public void AddLine()
    {
        if (IsStop())
        {
            _countLine++;
            if (_countLine > _lineSlot.lines.Length) _countLine = 1;
            SetPrice();
        }
    }

    public void RemoveLine()
    {
        if (IsStop())
        {
            _countLine--;
            if (_countLine < 1) _countLine = _lineSlot.lines.Length;
            SetPrice();
        }
    }

    public void SetMaxBet()
    {
        if (IsStop())
        {
            _betsId = _betsData.bets.Length - 1;
            SetPrice();
        }
    }

    public void AddBet()
    {
        if (IsStop())
        {
            _betsId++;
            if (_betsId >= _betsData.bets.Length) _betsId = 0;
            SetPrice();
        }
    }

    public void RemoveBet()
    {
        if (IsStop())
        {
            _betsId--;
            if (_betsId < 0) _betsId = _betsData.bets.Length - 1;
            SetPrice();
        }
    }

    private void Win(int[] lines, float[] mult)
    {
        _lineSlot.LineActiv(lines);

        Sprite[] sprites = new Sprite[4];

        for(int i = 0; i < sprites.Length; i++)
        {
            sprites[i] = SpritesEnd[i, 1];
        }

        OnWin?.Invoke(sprites);
        OnWinLines?.Invoke(lines);
    }

    private void Lose()
    {
        OnChangeMoneyWin?.Invoke(0.ToString());
        Gagame.Stats.CheckBalance();
        OnLose?.Invoke();
    }

    public void StartSpin()
    {
        if (IsStop())
        {
            if (Gagame.Stats.Spend())
            {
                OnChangeMoneyWin?.Invoke("");
                StartCoroutine(StartSpinCoroutine());
                OnStartSpin?.Invoke();
            }
        }
    }

    private IEnumerator StartSpinCoroutine()
    {
        var delay = new WaitForSeconds(_delaySpinRoll);

        _lineSlot.LineActiv(false);

        CreateRandomSprites();

        for (int x = 0; x < _rows.Length; x++)
        {
            Sprite[] rowSprite = new Sprite[_countRowElements];

            for (int y = 0; y < _countRowElements; y++)
            {
                rowSprite[y] = SpritesEnd[x, y];
            }

            _rows[x].Spin(_allSpritesData.sprites, rowSprite);
            yield return delay;
        }
    }

    private void CreateRandomSprites()
    {
        SpritesEnd = new Sprite[_rows.Length, _countRowElements];

        for (int x = 0; x < SpritesEnd.GetLength(0); x++)
        {
            for (int y = 0; y < SpritesEnd.GetLength(1); y++)
            {
                Sprite randSprite = _allSpritesData.sprites[Random.Range(0, _allSpritesData.sprites.Length)];
                SpritesEnd[x, y] = randSprite;
            }
        }

        if (Random.Range(0, 1f) < chanseWin)
        {
            print(nameof(Win));
            _checkSpin.SetWin(SpritesEnd, _allSpritesData.sprites, _countLine);
        }
        else
        {
            print(nameof(Lose));
            _checkSpin.SetLose(SpritesEnd, _checkSpin.GetWinningLines(SpritesEnd, _countLine), _allSpritesData.sprites, _countLine);
        }

    }

    public bool IsStop()
    {
        for (int i = 0; i < _rows.Length; i++)
        {
            if (_rows[i].is_spinning)
                return false;
        }

        return true;
    }

    private void CheckWin()
    {
        if (IsStop())
        {
            int[] lines = _checkSpin.GetWinningLines(SpritesEnd, _countLine);
            int countWin = lines.Length;
            float[] mult = _checkSpin.GetMultiplayers(SpritesEnd, _countLine);

            Win(lines, mult);

            //if (countWin > 0)
            //    Win(lines, mult);
            //else if (countWin == 0)
            //    Lose();
        }
    }

    private void OnEnable()
    {
        foreach (var item in _rows)
        {
            item.OnStop.AddListener(CheckWin);
        }
    }

    private void OnDisable()
    {
        foreach (var item in _rows)
        {
            item.OnStop.RemoveListener(CheckWin);
        }
    }

    private void OnValidate()
    {
        _rows ??= GetComponentsInChildren<Rowewr>(true);

        if (_rows != null)
        {
            for (int i = 0; i < _rows.Length; i++)
            {
                //if (i > 0)
                //{
                //    Vector3 pos = _rows[i].transform.position;
                //    pos.x = _rows[i - 1].transform.position.x + _space.x;
                //    _rows[i].transform.position = pos;
                //}

                //_rows[i].spaceY = _space.y;

                if (_isSingleSpeed)
                    _rows[i].speedControll = _speedControll;

                if (_allSpritesData != null)
                    _rows[i].SetSprites(_allSpritesData.sprites[0]);

                _rows[i].OnValidate();
            }
        }
    }
}
