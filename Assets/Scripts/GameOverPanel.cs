using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameOverPanel : MonoBehaviour
{
    [SerializeField] private Image[] _images;
    [SerializeField] private TMP_Text[] _texts;

    private void OnEnable()
    {
        for(int i = 0; i < _images.Length; i++)
        {
            if(Gagame.Stats.Task[i] != 0)
            {
                _images[i].sprite = Gagame.Stats.Img[i].sprite;
                _images[i].SetNativeSize();
                _texts[i].text = $"x{Gagame.Stats.Task[i]}";
            }
            else _images[i].gameObject.SetActive(false);
        }
    }
}