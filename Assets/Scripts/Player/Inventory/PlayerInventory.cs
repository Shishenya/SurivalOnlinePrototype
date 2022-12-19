using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    [SerializeField] private StorageItemsSO _storageItems; // ������ ���� ���������
    [SerializeField] private GameObject _inventoryGrid; // ������ �� UI ���������
    [SerializeField] private List<InventoryCell> inventoryCells; // ������ �� ������ ���������
    private Dictionary<int, int> _playerInventory; // ������� ��������� ������

    public static PlayerInventory Instance;



    public StorageItemsSO StorageItems
    {
        get => _storageItems;
    }

    private void Awake()
    {
        Instance = this;
        _inventoryGrid.SetActive(false);
        _playerInventory = new Dictionary<int, int>();


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
                _playerInventory.Add(item.id, 1);
            }
        }
    }

    private void Update()
    {
        // test
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            // ��������� ���������
            OpenCloseInventoryUI();
        }
    }

    /// <summary>
    /// ���������� �������� � ���������
    /// </summary>
    public bool AddItem(int id)
    {

        // ������� ������
        ClearAllCell();

        BaseItem baseItem = _storageItems.GetItemInStorage(id);
        if (baseItem != null)
        {
            // ��������� �������
            if (!_playerInventory.ContainsKey(id))
            {
                _playerInventory.Add(id, 1);
                Debug.Log("������� " + baseItem.basicParameters.itemName + " �������� � ��������� ��� �����");
            }
            else
            {
                _playerInventory[id]++;
                Debug.Log("������� " + baseItem.basicParameters.itemName + " ��� ��� � ���������. �� ��������� ��� ����������. ������ ��� " + _playerInventory[id]);
            }
        }

        return true;
    }

    public bool RemoveItem(int id)
    {

        // ������� ������
        ClearAllCell();

        BaseItem baseItem = GlobalStorage.Instance.storageItems.GetItemInStorage(id);
        if (baseItem == null) return false;
        if (!_playerInventory.ContainsKey(id)) return false;

        _playerInventory[id]--;
        if (_playerInventory[id]<=0)
        {
            _playerInventory.Remove(id);
        }

        // �������������
        VisualInventory();

        return true;
    }

    /// <summary>
    /// �������� � �������� ���������
    /// </summary>
    private void OpenCloseInventoryUI()
    {
        if (_inventoryGrid.activeInHierarchy) // ��������� ���������
        {
            _inventoryGrid.SetActive(false);
            gameObject.GetComponent<PlayerController>().enablePlayerController = true;
            Cursor.lockState = CursorLockMode.Locked;
        }
        else // ��������� ���������
        {
            ClearAllCell();
            VisualInventory();

            _inventoryGrid.SetActive(true);
            gameObject.GetComponent<PlayerController>().enablePlayerController = false;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
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

    private void VisualInventory()
    {
        int i = 0;
        foreach (var inventoryPlayer in _playerInventory)
        {
            // Debug.Log(inventoryPlayer.Value.basicParameters.itemName);
            BaseItem baseItem = StorageItems.GetItemInStorage(inventoryPlayer.Key);
            inventoryCells[i].Init(baseItem, inventoryPlayer.Value);
            i++;
        }
    }
}
