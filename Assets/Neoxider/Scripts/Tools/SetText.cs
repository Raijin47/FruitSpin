using Neoxider;
using TMPro;
using UnityEngine;

[AddComponentMenu("Neoxider/" + "Tools/" + nameof(SetText))]
public class SetText : MonoBehaviour
{
    [SerializeField]
    private TMP_Text _text;

    [SerializeField]
    private string _separator = ".";

    public string startAdd = "";
    public string endAdd = "";
    public string text
    {
        get => _text.text;
        set => Set(value);
    }

    public void Set(int value)
    {
        Set(value.FormatWithSeparator(_separator));
    }

    public void Set(float value)
    {
        Set(value.FormatWithSeparator(_separator, 2));
    }

    public void Set(string value)
    {
        string text = startAdd + value + endAdd;
        _text.text = text; 
    }

    private void OnValidate()
    {
        if (_text == null)
        {
            _text = GetComponent<TMP_Text>();
        }
    }
}
