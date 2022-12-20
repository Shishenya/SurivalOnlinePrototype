using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalVariable : MonoBehaviour
{
    public static GlobalVariable Instance;

    public GameObject itemsParent;

    private void Awake()
    {
        Instance = this;
    }
}
