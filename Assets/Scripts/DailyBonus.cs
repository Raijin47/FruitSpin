using UnityEngine;
using UnityEngine.UI;

public class DailyBonus : MonoBehaviour
{
    public ButtonSlovoBase button;

    public void UpdateTime(float time)
    {
        button.Interactable = time == 0;
    }
}
