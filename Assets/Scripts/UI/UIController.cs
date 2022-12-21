using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    public static UIController Instance;
    public UIErrorWindow uiErrorWindow;
    public UIInfoWindow uiInfoWindow;

    private void Awake()
    {
        Instance = this;
    }
}
