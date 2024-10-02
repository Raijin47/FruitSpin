using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimationFruitBlender : MonoBehaviour
{
    public Image _image;
    public static AnimationFruitBlender Instance;

    private readonly List<Sprite> Sprites = new();

    private void Awake()
    {
        Instance = this;
    }

    [SerializeField] private Animator _animator;

    public void StartAnim(List<Sprite> Sprite) 
    {
        Sprites.AddRange(Sprite);

        Play();
    }

    public void Play()
    {
        _image.sprite = Sprites[0];
        _image.SetNativeSize();
        Sprites.RemoveAt(0);
        _animator.Play("New State");
    }

    public void EndAnim() 
    {
        AudioControllerBlendera.Instance.Bulk();


        if (Sprites.Count == 0)
        {
            _animator.Play("Empty");
            AnimationBlender.Instance.StartAnim();
        }
        else Play();
    } 
}