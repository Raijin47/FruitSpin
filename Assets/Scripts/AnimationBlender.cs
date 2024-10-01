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
        _animator.Play("Anim");

    }
    public void EndAnim() 
    {
        AudioController.Instance.Bulk();
        _animator.Play("Empty");
        Game.Stats.CheckComplated();
    } 
}