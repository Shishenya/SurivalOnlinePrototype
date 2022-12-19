using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class VisualItemInWorld
{
    public GameObject prefabInWorld; // Префаб представления в игровом мире
    public bool isPickUp = false; // можно ли взять предмет в инвентарь

}
