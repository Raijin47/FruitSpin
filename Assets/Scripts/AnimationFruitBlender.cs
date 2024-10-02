using UnityEngine;

public class AnimationFruitBlender : MonoBehaviour
{
    public static AnimationFruitBlender Instance;

    private void Awake()
    {
        Instance = this;
    }

    [SerializeField] private Animator _animator;

    public void StartAnim() 
    {
        _animator.Play("Anim");

    }
    public void EndAnim() 
    {
        AudioControllerBlendera.Instance.Bulk();
        _animator.Play("Empty");
        AnimationBlender.Instance.StartAnim();
    } 
}