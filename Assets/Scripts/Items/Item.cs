using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public int id; // ID предмета и Storage

    /// <summary>
    /// Поднятие предмета
    /// </summary>
    public void PickUp(PlayerInventory playerInventory)
    {

        // Добавляем предмет в инвентарь игрока, если он может его подобрать
        BaseItem baseItem = GlobalStorage.Instance.storageItems.GetItemInStorage(id);
        if (baseItem != null)
        {
            // Debug.Log("Объект в мире: " + hit.collider.gameObject.name + "; " + baseItem.basicParameters.itemName + " Дистанция = " + hit.distance);
            if (baseItem.worldParameters.isPickUp)
            {
                playerInventory.AddItem(id);
                string textInfo = $"Вы подобрали предмет: {baseItem.basicParameters.itemName}";
                UIController.Instance.uiInfoWindow.ShowInfoText(textInfo);

                // Уничтожаем предмет в игровом мире
                Destroy(gameObject);
            }
        }
    }

}
