using UnityEngine;

public class AnimationBlender : MonoBehaviour
{
    public static AnimationBlender Instance;

    private void Awake()
    {
        Instance = this;
    }

    [SerializeField] private Animator _animator;

    public void StartAnim()
    {
        _animator.Play("Play");
        AudioControllerBlendera.Instance.Blender();
    }
    public void EndAnim()
    {
        _animator.Play("Empty");
        Gagame.Stats.CheckComplated();
    }
}