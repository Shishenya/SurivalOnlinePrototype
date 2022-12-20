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
    }

    /// <summary>
    /// ������� �� ������
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            PlayerInventory.Instance.RemoveItem(_baseItem.id);
            Debug.Log("����� �� ���� ���");
        }
    }

    /// <summary>
    /// ����� ������ �� ������
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (_baseItem!=null)
        {
            _itemContext = Instantiate(_itemContextPrefab, _itemContextPoint.transform);
            _itemContext.GetComponent<ItemContextUI>().Init(_baseItem.basicParameters.itemName, _baseItem.basicParameters.itemDescription);
        }
    }

    /// <summary>
    /// ����� �� ���� �������� ������
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerExit(PointerEventData eventData)
    {
        Destroy(_itemContext);
        _itemContext = null;
    }
}
