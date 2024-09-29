using UnityEngine;
using Neoxider.Shop;

public class Animals : MonoBehaviour
{
    [SerializeField] private int _id;
    [SerializeField] private int _price;
    [SerializeField] private GameObject[] _animals;
    [SerializeField] private ButtonBase _button;

    private int _current;

    private readonly string SaveName = "SaveAnimal";

    public int Current
    {
        get => _current;
        set
        {
            PlayerPrefs.SetInt(SaveName + _id, value);
            _current = value;
            _animals[_current].SetActive(true);
            _button.Interactable = _current < _animals.Length - 1;
        }
    }

    private void Start()
    {
        Current = PlayerPrefs.GetInt(SaveName + _id, 0);

        for (int i = 0; i < Current; i++)
            _animals[i].SetActive(true);

        _button.OnClick.AddListener(Buy);
    }

    private void Buy()
    {
        if (Current >= _animals.Length - 1)  return;
        if (Money.Instance.Spend(_price)) Current++;
    }
}