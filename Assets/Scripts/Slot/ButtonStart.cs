using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonStart : MonoBehaviour
{
    [SerializeField] private SpinControll _spinController;

    private void OnMouseDown()
    {
        _spinController.StartSpin();
    }
}
