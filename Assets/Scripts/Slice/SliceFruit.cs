using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliceFruit : MonoBehaviour
{
    public static SliceFruit Instance;

    [SerializeField] private SliceElement[] _element;
    [SerializeField] private RectTransform[] _elementTransform;
    [SerializeField] private SliceData _data;
    [SerializeField] private Image _fruitImage;
    [SerializeField] private Sprite[] _sprites;
    [SerializeField] private Image _splash;
    [SerializeField] private Sprite[] _spritesCheck;
    [SerializeField] private GameObject[] _slises;
    [SerializeField] private GameObject _panel;

    [SerializeField] private Sprite[] _spritesSplash;

    private int _countSlice;
    private bool _hasSecondSlice;

    private void Start()
    {
        Instance = this;
    }

    public void Check()
    {
        _countSlice++;
        _splash.rectTransform.localScale = Vector2.one * _countSlice;

        AudioControllerBlendera.Instance.Slice();

        foreach (SliceElement element in _element)
            if (!element.HasReady) return;


        if (Sprites.Count == 2)
        {
            if (!_hasSecondSlice)
            {
                Activate(Sprites[1]);
                _hasSecondSlice = true;
                return;
            }
        }

        GoodSlice();
    }

    public void GoodSlice()
    {
        //AnimationFruitBlender.Instance.StartAnim(Sprites);
        Sprites.Clear();
        _panel.SetActive(false);
        _hasSecondSlice = false;
    }

    public void FailSlice()
    {
        foreach (SliceElement element in _element)
            element.ResetSlice();
    }
    private readonly List<Sprite> Sprites = new();

    public void AddSprite(List<Sprite> Sprite)
    {
        Sprites.AddRange(Sprite);
        Activate(Sprites[0]);
    }

    public void StartAction()
    {

        _slises[id].SetActive(true);
    }

    public void Release() => _slises.SetActiveAll(false);

    private int id;

    public void Activate(Sprite Sprite)
    {
        id = GetID(Sprite);


        _fruitImage.sprite = _sprites[id];
        _fruitImage.SetNativeSize();
        _countSlice = 1;
        _splash.rectTransform.localScale = Vector2.one * _countSlice;
        _splash.sprite = GetSprite(id);

        for (int i = 0; i < _element.Length; i++)
        {
            _element[i].OnClear();

            _elementTransform[i].anchoredPosition = _data.Element[id].Position[i];
            _elementTransform[i].rotation = Quaternion.Euler(new Vector3(0, 0, _data.Element[id].Angle[i]));
            _elementTransform[i].sizeDelta = _data.Element[id].Size[i];
        }
    }


    private int GetID(Sprite sprite)
    {
        for(int i = 0; i < _spritesCheck.Length; i++)
        {
            if (sprite == _spritesCheck[i]) return i;
        }

        return 0;
    }

    private Sprite GetSprite(int id)
    {
        return id switch
        {
            0 => _spritesSplash[2],
            1 => _spritesSplash[1],
            2 => _spritesSplash[3],
            3 => _spritesSplash[3],
            4 => _spritesSplash[1],
            5 => _spritesSplash[0],
            6 => _spritesSplash[3],
            > 6 => _spritesSplash[2],
            _ => throw new System.NotImplementedException(),
        };
    }
}