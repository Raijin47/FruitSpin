using UnityEngine;

[System.Serializable]
public class VisualSlot
{
    public GameObject[] lines;

    public void LineActiv(int[] idList)
    {
        foreach (GameObject item in lines)
            item.SetActive(false);

        foreach (int id in idList)
            if (id >= 0 && id < lines.Length)
                lines[id].SetActive(true);
    }

    public void LineActiv(bool activ)
    {
        foreach (GameObject item in lines)
            item.SetActive(activ);
    }
}
