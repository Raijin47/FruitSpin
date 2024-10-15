using UnityEngine;
using UnityEngine.UI;

public class SliceElement : MonoBehaviour
{
    [SerializeField] private Image _sliceImage;
    [SerializeField] private Sprite _on, _off;
    [SerializeField] private SliceFruit _fruit;

    public bool HasReady { private set; get; }
    private readonly bool[] _isReady = new bool[4];

    public void OnClear()
    {
        HasReady = false;
        _sliceImage.sprite = _off;
        ResetSlice();
    }

    public void Slice(int id)
    {
        if(IsReady(id) && !HasReady)
        {
            _sliceImage.sprite = _on;
            HasReady = true;
            _fruit.Check();
        }
    }

    private bool IsReady(int id)
    {
        _isReady[id] = true;

        for(int i = 0; i < id; i++)
        {
            if (_isReady[i]) continue;
            else
            {
                for (int a = 0; a < _isReady.Length; a++)
                    _isReady[a] = false;

                return false;
            }
        }

        return id >= _isReady.Length-1;
    }

    public void ResetSlice()
    {
        for(int i = 0; i < _isReady.Length; i++)       
            _isReady[i] = false;       
    }
}
