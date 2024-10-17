using UnityEngine;

public class AnimationBlender : MonoBehaviour
{
    public static AnimationBlender Instance;

    private void Awake()
    {
        Instance = this;
    }

    [SerializeField] private Animator _animator;

    private bool _isActive;
    private int _countFruit;

    public void StartAnim()
    {
        _countFruit++;

        if(_countFruit > 10)
        {
            if (_isActive) return;
            _animator.Play("Play");
            AudioControllerBlendera.Instance.Blender();
            _isActive = true;
        }
    }
    public void EndAnim()
    {
        _isActive = false;
        _animator.Play("Empty");
        Gagame.Stats.CheckComplated();
        _countFruit = 0;
        SliceFruit.Instance.Release();
    }
}