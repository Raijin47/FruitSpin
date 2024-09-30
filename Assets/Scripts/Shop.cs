using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Neoxider.Shop;
using System;

public class Shop : MonoBehaviour
{
    [SerializeField] private ShopElement[] _shopElement;
    [SerializeField] private Image _spriteSkin;


    [SerializeField] private GameObject _panel;
    [SerializeField] private Image _image;
    [SerializeField] private TMP_Text _priceText;
    [SerializeField] private TMP_Text _nameText;

    private readonly string TextEquip = "Equip";
    private readonly string TextBuy = "Buy";
    private readonly string TextEquiped = "Equiped";

    private int _equip;
    private int _current;


    private void Start() => Stats.OnSavesLoaded += GetData;

    private void GetData()
    {
        Equip = Stats.Instance.savesData.currentEquipSkin;

        for (int i = 0; i < _shopElement.Length; i++)
        {
            _shopElement[i].PriceObject.SetActive(!Stats.Instance.savesData.purchasedSkin[i]);
            _shopElement[i].text = Stats.Instance.savesData.purchasedSkin[i] ? (_equip == i ? TextEquiped : TextEquip) : TextBuy;
        }
    }

    public void Action(int id)
    {
        if (_equip == id) return;

        if (Stats.Instance.savesData.purchasedSkin[id])
        {
            _shopElement[_equip].text = TextEquip;
            _shopElement[id].text = TextEquiped;

            Equip = id;
        }
        else
        {
            _current = id;
            _panel.SetActive(true);
            _image.sprite = _shopElement[id].Sprite;
            _image.SetNativeSize();
            _priceText.text = ConvertNumber.Convert(_shopElement[id].Price);
            _nameText.text = _shopElement[id].Name;
        }            
    }

    public void Buy()
    {
        if (Money.Instance.Spend(_shopElement[_current].Price))
        {
            _panel.SetActive(false);
            _shopElement[_current].text = TextEquip;
            _shopElement[_current].PriceObject.SetActive(false);
            Stats.Instance.savesData.purchasedSkin[_current] = true;
            Stats.Instance.Save();
        }
    }

    private int Equip
    {
        set
        {
            _equip = value;
            _spriteSkin.sprite = _shopElement[_equip].Sprite;
            _spriteSkin.SetNativeSize();

            Stats.Instance.savesData.currentEquipSkin = _equip;
            Stats.Instance.Save();
        }
    }
}

[Serializable]
public class ShopElement
{
    public Sprite Sprite;

    [SerializeField] private TMP_Text _buttonText;
    public string Name;
    public GameObject PriceObject;
    public int Price;

    public string text
    {
        set => _buttonText.text = value;
    }
}