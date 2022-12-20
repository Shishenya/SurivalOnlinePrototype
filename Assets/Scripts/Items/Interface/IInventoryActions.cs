using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInventoryActions
{
    public bool AddItem(int id);
    public bool RemoveItem(int id);
}
