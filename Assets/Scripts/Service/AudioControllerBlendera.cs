using System;
using UnityEngine;

public class AudioControllerBlendera : MonoBehaviour
{
    public static AudioControllerBlendera Instance;
    public static Action onClick;

    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _onClickClip;
    [SerializeField] private AudioClip _startSpinClip;
    [SerializeField] private AudioClip _winClip;
    [SerializeField] private AudioClip _gameOverClip;
    [SerializeField] private AudioClip _failClip;
    [SerializeField] private AudioClip _smallWinClip;
    [SerializeField] private AudioClip _bulkClip;
    [SerializeField] private AudioClip _blenderClip;

    private void Start()
    {
        onClick += OnClick;
        Instance = this;
    }

    private void OnClick()
    {
        _audioSource.clip = _onClickClip;
        _audioSource.Play();
    }

    public void StartSpin()
    {
        Instance._audioSource.PlayOneShot(Instance._startSpinClip);
    }

    public void Win()
    {
        Instance._audioSource.PlayOneShot(Instance._winClip);
    }

    public void GameOver()
    {
        Instance._audioSource.PlayOneShot(Instance._gameOverClip);
    }

    public void Fail()
    {
        Instance._audioSource.PlayOneShot(Instance._failClip);
    }

    public void SmallWin()
    {
        Instance._audioSource.PlayOneShot(Instance._smallWinClip);
    }
    public void Bulk()
    {
        Instance._audioSource.PlayOneShot(Instance._bulkClip);
    }

    public void Blender()
    {
        Instance._audioSource.PlayOneShot(Instance._blenderClip);
    }
}