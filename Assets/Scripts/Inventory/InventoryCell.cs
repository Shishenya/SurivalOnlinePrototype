using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.UI;
using System;

public class InventoryCell : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private Sprite _transperentSprite;
    public Image icon; // Ссылка на изображение предмета в инвентаре
    public TMP_Text amountText; // Ссылка на Текст с количством предметов

    private BaseItem _baseItem = null;

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

    public void ClearCell()
    {
        _baseItem = null;
        icon.sprite = _transperentSprite;
        amountText.text = "";
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            PlayerInventory.Instance.RemoveItem(_baseItem.id);
            Debug.Log("Нажал на тебя ПКМ");
        }
    }
}
