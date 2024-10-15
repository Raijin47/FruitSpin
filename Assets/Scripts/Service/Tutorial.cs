using UnityEngine;

public class Tutorial : MonoBehaviour
{
    [SerializeField] private GameObject _tutorialPanel;
    [SerializeField] private GameObject _order;

    private void OnEnable()
    {
        if(PlayerPrefs.GetInt("tutor", 0) == 0)        
            _tutorialPanel.SetActive(true);
        else _order.SetActive(true);
    }

    public void Skip()
    {
        PlayerPrefs.SetInt("tutor", 1);
        _tutorialPanel.SetActive(false);
        _order.SetActive(true);
    }
}