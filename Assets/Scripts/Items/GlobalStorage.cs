using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalStorage : MonoBehaviour
{
    public StorageItemsSO storageItems;
    public static GlobalStorage Instance;
    private void Awake()
    {
        Instance = this;
    }
}
