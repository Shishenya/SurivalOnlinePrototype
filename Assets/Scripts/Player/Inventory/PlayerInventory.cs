using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour, IInventoryActions
{
    [SerializeField] private GameObject _inventoryGrid; // ������ �� UI ���������
    [SerializeField] private List<InventoryCell> inventoryCells; // ������ �� ������ ���������
    private Dictionary<int, int> _playerInventory; // ������� ��������� ������
    private PlayerController _playerController; // ������ �� ���������� ������

    public static PlayerInventory Instance;
    public bool isInventoryOpen = false;

    private void Awake()
    {
        Instance = this;

        _inventoryGrid.SetActive(false);
        _playerInventory = new Dictionary<int, int>();
    }

    /// <summary>
    /// ������������� ���������
    /// </summary>
    public void Init(PlayerController playerController)
    {
        _playerController = playerController;
    }

    /// <summary>
    /// ���������� �������� � ���������
    /// </summary>
    public bool AddItem(int id)
    {

        // ������� ������
        ClearAllCell();

        BaseItem baseItem = GlobalStorage.Instance.storageItems.GetItemInStorage(id);
        if (baseItem != null)
        {
            // ��������� �������
            if (!_playerInventory.ContainsKey(id))
            {
                _playerInventory.Add(id, 1);
                // Debug.Log("������� " + baseItem.basicParameters.itemName + " �������� � ��������� ��� �����");
            }
            else
            {

                if (_playerInventory[id] >= baseItem.basicParameters.maxAmount)
                {
                    UIController.Instance.uiErrorWindow.gameObject.SetActive(true);
                    UIController.Instance.uiErrorWindow.ShowErrorText(ErrorCode.maxThisItemInInventory);
                }
                else
                {
                    _playerInventory[id]++;
                    // Debug.Log("������� " + baseItem.basicParameters.itemName + " ��� ��� � ���������. �� ��������� ��� ����������. ������ ��� " + _playerInventory[id]);
                }

            }
        }

        return true;
    }

    /// <summary>
    /// �������� �������� �� ���������
    /// </summary>
    public bool RemoveItem(int id)
    {

        // ������� ������
        ClearAllCell();

        BaseItem baseItem = GlobalStorage.Instance.storageItems.GetItemInStorage(id);
        if (baseItem == null) return false;
        if (!_playerInventory.ContainsKey(id)) return false;

        _playerInventory[id]--;
        if (_playerInventory[id] <= 0)
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
    public void OpenCloseInventoryUI()
    {
        if (isInventoryOpen) // ��������� ���������
        {
            isInventoryOpen = false;
            _inventoryGrid.SetActive(false);
            ClearAllCell();

            _playerController.InActionsModeOff();
            _playerController.enablePlayerController = true;
        }
        else // ��������� ���������
        {
            isInventoryOpen = true;
            ClearAllCell();
            VisualInventory();

            _inventoryGrid.SetActive(true);
            _playerController.InActionsModeOn();
            _playerController.enablePlayerController = false;
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

    /// <summary>
    /// ������������ ����� ���������
    /// </summary>
    private void VisualInventory()
    {
        int i = 0;
        foreach (var inventoryPlayer in _playerInventory)
        {
            BaseItem baseItem = GlobalStorage.Instance.storageItems.GetItemInStorage(inventoryPlayer.Key);
            inventoryCells[i].Init(baseItem, inventoryPlayer.Value);
            i++;
        }
    }

    /// <summary>
    /// ���������� �������� ������������, ������� ���� � ������
    /// </summary>
    public ToolActionsItems ToolsInPlayer()
    {
        ToolActionsItems toolsInPlayer = new ToolActionsItems();
        foreach (var item in _playerInventory)
        {
            BaseItem baseItem = GlobalStorage.Instance.storageItems.GetItemInStorage(item.Key);
            if (baseItem != null)
            {
                if (baseItem.toolActions.isChop) toolsInPlayer.isChop = true;
                if (baseItem.toolActions.isPrickStone) toolsInPlayer.isPrickStone = true;
            }
        }

        return toolsInPlayer;

    }
}
