using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class WinPanel : MonoBehaviour
{
    [SerializeField] private GameObject _order;
    [SerializeField] private float _lifeTime;
    [SerializeField] private Image _image;
    [SerializeField] private Sprite[] _sprites;
    private Coroutine _timerCoroutine;

    private float _currentTime;


    private void OnEnable()
    {
        _currentTime = _lifeTime;
        _image.sprite = _sprites[Random.Range(0, _sprites.Length)];
        _image.SetNativeSize();
        StopAction();
        _timerCoroutine = StartCoroutine(TimerCoroutine());
    }

    private void OnDisable() => StopAction();

    private void StopAction()
    {
        if (_timerCoroutine != null)
        {
            StopCoroutine(_timerCoroutine);
            _timerCoroutine = null;
        }
    }

    private IEnumerator TimerCoroutine()
    {
        while(_currentTime >= 0)
        {
            _currentTime -= Time.deltaTime;
            yield return null;
        }
        _order.SetActive(true);
        gameObject.SetActive(false);
    }
}