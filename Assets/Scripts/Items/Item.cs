using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public int id; // ID �������� � Storage

    /// <summary>
    /// �������� ��������
    /// </summary>
    public void PickUp(PlayerInventory playerInventory)
    {

        // ��������� ������� � ��������� ������, ���� �� ����� ��� ���������
        BaseItem baseItem = GlobalStorage.Instance.storageItems.GetItemInStorage(id);
        if (baseItem != null)
        {
            // Debug.Log("������ � ����: " + hit.collider.gameObject.name + "; " + baseItem.basicParameters.itemName + " ��������� = " + hit.distance);
            if (baseItem.worldParameters.isPickUp)
            {
                playerInventory.AddItem(id);
                string textInfo = $"�� ��������� �������: {baseItem.basicParameters.itemName}";
                UIController.Instance.uiInfoWindow.ShowInfoText(textInfo);

                // ���������� ������� � ������� ����
                Destroy(gameObject);
            }
        }
    }

}
