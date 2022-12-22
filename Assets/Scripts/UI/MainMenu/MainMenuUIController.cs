using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuUIController : MonoBehaviour
{
    public static MainMenuUIController Instance;

    private void Awake()
    {
        Instance = this;
    }

    public void Quit()
    {
        Application.Quit();
    }
}
