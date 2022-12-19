using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    [SerializeField] private StorageItemsSO _storageItems; // список всех предметов
    [SerializeField] private GameObject _inventoryGrid; // ссылка на UI инвентаря
    [SerializeField] private List<InventoryCell> inventoryCells; // ссылки на ячейки инвентаря
    private Dictionary<int, int> _playerInventory; // словарь предметов игрока

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
            // Открываем инвентарь
            OpenCloseInventoryUI();
        }
    }

    /// <summary>
    /// Добавление предмета в инвентарь
    /// </summary>
    public bool AddItem(int id)
    {

        // Очищаем ячейки
        ClearAllCell();

        BaseItem baseItem = _storageItems.GetItemInStorage(id);
        if (baseItem != null)
        {
            // Добавляем предмет
            if (!_playerInventory.ContainsKey(id))
            {
                _playerInventory.Add(id, 1);
                Debug.Log("Предмет " + baseItem.basicParameters.itemName + " добавлен в инвентарь как новый");
            }
            else
            {
                _playerInventory[id]++;
                Debug.Log("Предмет " + baseItem.basicParameters.itemName + " уже был в инвентаре. Мы увеличели его количество. Теперь его " + _playerInventory[id]);
            }
        }

        return true;
    }

    public bool RemoveItem(int id)
    {

        // Очищаем ячейки
        ClearAllCell();

        BaseItem baseItem = GlobalStorage.Instance.storageItems.GetItemInStorage(id);
        if (baseItem == null) return false;
        if (!_playerInventory.ContainsKey(id)) return false;

        _playerInventory[id]--;
        if (_playerInventory[id]<=0)
        {
            _playerInventory.Remove(id);
        }

        // Визуализируем
        VisualInventory();

        return true;
    }

    /// <summary>
    /// Открытие и закрытие инвентаря
    /// </summary>
    private void OpenCloseInventoryUI()
    {
        if (_inventoryGrid.activeInHierarchy) // закрываем инвентарь
        {
            _inventoryGrid.SetActive(false);
            gameObject.GetComponent<PlayerController>().enablePlayerController = true;
            Cursor.lockState = CursorLockMode.Locked;
        }
        else // открываем инвентарь
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
    /// Очистка от данных всех ячеек инветаря (визуал)
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
