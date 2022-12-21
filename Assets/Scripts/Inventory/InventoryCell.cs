using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.UI;
using System;

public class InventoryCell : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Sprite _transperentSprite; // ������ �� ������ "�������"

    [SerializeField] private GameObject _itemContextPrefab; // ������ �� ������ ��������� �������� ��� ��������
    [SerializeField] private GameObject _itemContextPoint; // ����� ��� ������ �������� ��������
    private GameObject _itemContext = null; // �� �������� ��������

    public Image icon; // ������ �� ����������� �������� � ���������
    public TMP_Text amountText; // ������ �� ����� � ���������� ���������
    private BaseItem _baseItem = null; // ������� � ������

    private void Start()
    {
       
    }

    public void Init(BaseItem baseItem, int amount = 0)
    {

        if (baseItem == null) return;
        _baseItem = baseItem;

        Sprite sprite = _baseItem.basicParameters.itemIcon;

        if (icon != null)
        {
            icon.sprite = sprite;
        }

        amountText.text = amount.ToString();
    }

    /// <summary>
    ///  �������� ������ �� ����������
    /// </summary>
    public void ClearCell()
    {
        _baseItem = null;
        icon.sprite = _transperentSprite;
        amountText.text = "";
        Destroy(_itemContext);
        _itemContext = null;
    }

    /// <summary>
    /// ������� �� ������
    /// </summary>
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            // �������� ��� �������� �� ��������
            if (_baseItem == null) return;
            if (_baseItem.worldParameters.prefabInWorld != null)
            {
                GameObject removeItem = Instantiate(_baseItem.worldParameters.prefabInWorld, GlobalVariable.Instance.itemsParent.transform);
                removeItem.transform.position = PlayerController.Instance.playerSpawnPointbyRemove.transform.position;
                string textInfo = $"�� ��������� ������� {_baseItem.basicParameters.itemName}";
                UIController.Instance.uiInfoWindow.ShowInfoText(textInfo);
            }

            // ������� �� ���������
            PlayerInventory.Instance.RemoveItem(_baseItem.id);

            // Debug.Log("����� �� ���� ���");
        }
    }

    /// <summary>
    /// ����� ������ �� ������
    /// </summary>
     public void OnPointerEnter(PointerEventData eventData)
    {
        if (_baseItem!=null)
        {
            _itemContext = Instantiate(_itemContextPrefab, _itemContextPoint.transform);
            _itemContext.GetComponent<ItemContextUI>().Init(_baseItem);
        }
    }

    /// <summary>
    /// ����� �� ���� �������� ������
    /// </summary>
    public void OnPointerExit(PointerEventData eventData)
    {
        Destroy(_itemContext);
        _itemContext = null;
    }
}
