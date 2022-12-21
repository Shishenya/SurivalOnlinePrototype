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
        Destroy(_itemContext);
        _itemContext = null;
    }

    /// <summary>
    /// Кликнул по ячейке
    /// </summary>
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            // Временно это отвечает за удаление
            if (_baseItem == null) return;
            if (_baseItem.worldParameters.prefabInWorld != null)
            {
                GameObject removeItem = Instantiate(_baseItem.worldParameters.prefabInWorld, GlobalVariable.Instance.itemsParent.transform);
                removeItem.transform.position = PlayerController.Instance.playerSpawnPointbyRemove.transform.position;
                string textInfo = $"Вы выбросили предмет {_baseItem.basicParameters.itemName}";
                UIController.Instance.uiInfoWindow.ShowInfoText(textInfo);
            }

            // Удаляем из инвентаря
            PlayerInventory.Instance.RemoveItem(_baseItem.id);

            // Debug.Log("Нажал на тебя ПКМ");
        }
    }

    /// <summary>
    /// Навел мышкой на ячейку
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
    /// Вышел из зоны действия ячейки
    /// </summary>
    public void OnPointerExit(PointerEventData eventData)
    {
        Destroy(_itemContext);
        _itemContext = null;
    }
}
