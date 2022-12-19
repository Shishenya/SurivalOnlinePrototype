using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StorageItems_", menuName = "Scriptable Objects/Items/StorageItems")]
public class StorageItemsSO : ScriptableObject
{
    public List<BaseItem> items;

    /// <summary>
    /// Возвраащает предмет по его ID
    /// </summary>
    public BaseItem GetItemInStorage(int id)
    {
        foreach (var baseItem in items)
        {
            if (baseItem.id == id)
            {
                return baseItem;
            }
        }

        return null;

    }

}
