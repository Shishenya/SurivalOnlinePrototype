using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.UI;
using System;

public class InventoryCell : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Sprite _transperentSprite; // ссылка на спрайт "нулевку"

    [SerializeField] private GameObject _itemContextPrefab; // Ссылка на префаб контекста описания для предмета
    [SerializeField] private GameObject _itemContextPoint; // Точка для показа опсиания предмета
    private GameObject _itemContext = null; // ГО описания предмета

    public Image icon; // Ссылка на изображение предмета в инвентаре
    public TMP_Text amountText; // Ссылка на Текст с количством предметов
    private BaseItem _baseItem = null; // Предмет в ячейке

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
    ///  Очищение ячейки от инфомрации
    /// </summary>
    public void ClearCell()
    {
        _baseItem = null;
        icon.sprite = _transperentSprite;
        amountText.text = "";
    }

    /// <summary>
    /// Кликнул по ячейке
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            PlayerInventory.Instance.RemoveItem(_baseItem.id);
            Debug.Log("Нажал на тебя ПКМ");
        }
    }

    /// <summary>
    /// Навел мышкой на ячейку
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
    /// Вышел из зоны действия ячейки
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerExit(PointerEventData eventData)
    {
        Destroy(_itemContext);
        _itemContext = null;
    }
}
