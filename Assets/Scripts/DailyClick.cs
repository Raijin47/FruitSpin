using Neoxider.Shop;
using UnityEngine;

public class DailyClick : MonoBehaviour
{
    [SerializeField] private int _id;
    [Space(10)]
    [SerializeField] private ButtonBase _button;
    [SerializeField] private RectTransform _transform;

    private void Start() => _button.OnClick.AddListener(Click);    

    private void Click()
    {
        if (Stats.Instance.data.animalClick[_id] <= 0) { 
        }
        else
        {
            Stats.Instance.data.animalClick[_id]--;
            Money.Instance.Add(2);
            ClickFX.Instance.Click(_transform);
        }
    }


#if UNITY_EDITOR
    private void OnValidate()
    {
        _button ??= GetComponent<ButtonBase>();
        _transform ??= GetComponent<RectTransform>();
    }
#endif 
}