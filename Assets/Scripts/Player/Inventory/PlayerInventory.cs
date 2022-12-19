using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    [SerializeField] private StorageItemsSO _storageItems; // ������ ���� ���������
    [SerializeField] private GameObject _inventoryGrid; // ������ �� UI ���������
    [SerializeField] private List<InventoryCell> inventoryCells; // ������ �� ������ ���������
    private Dictionary<int, BaseItem> _playerInventory; // ������� ��������� ������

    
    public StorageItemsSO StorageItems
    {
        get => _storageItems;
    }

    private void Awake()
    {
        _inventoryGrid.SetActive(false);
        _playerInventory = new Dictionary<int, BaseItem>();


        // Test
        // TestLoadInventory();
    }

    private void TestLoadInventory()
    {
        int[] itemsAdd = { 1, 3 };
        for (int i = 0; i < itemsAdd.Length; i++)
        {
            BaseItem item = _storageItems.GetItemInStorage(itemsAdd[i]);
            if (item != null)
            {
                _playerInventory.Add(item.id, item);
            }
        }
    }

    private void Update()
    {
        // test
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            OpenCloseInventoryUI();

            int i = 0;
            foreach (var inventoryPlayer in _playerInventory)
            {
                // Debug.Log(inventoryPlayer.Value.basicParameters.itemName);
                inventoryCells[i].Init(inventoryPlayer.Value.basicParameters.itemIcon, 1);
                i++;
            }
        }
    }

    public bool AddItem(int id)
    {

        // ������� ������
        ClearAllCell();

        BaseItem baseItem = _storageItems.GetItemInStorage(id);
        if (baseItem != null)
        {
            // ��������� �������
            _playerInventory.Add(id, baseItem);
            Debug.Log("������� " + baseItem.basicParameters.itemName + " �������� � ���������");
        }

        return true;
    }

    private void OpenCloseInventoryUI()
    {
        if (_inventoryGrid.activeInHierarchy)
        {
            _inventoryGrid.SetActive(false);
        }
        else
        {
            ClearAllCell();
            _inventoryGrid.SetActive(true);
        }
    }

    /// <summary>
    /// ������� �� ������ ���� ����� �������� (������)
    /// </summary>
    private void ClearAllCell()
    {
        foreach (var cell in inventoryCells)
        {
            cell.ClearCell();
        }
    }

}
