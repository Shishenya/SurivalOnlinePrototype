using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class InventoryCell : MonoBehaviour
{
    [SerializeField] private Sprite _transperentSprite;
    public Image icon; // Ссылка на изображение предмета в инвентаре
    public TMP_Text amountText; // Ссылка на Текст с количством предметов

    public void Init(Sprite sprite, int amount = 0)
    {
        if (icon!=null)
        {
            icon.sprite = sprite;
        }

        amountText.text = amount.ToString();
    }

    public void ClearCell()
    {
        icon.sprite = _transperentSprite;
        amountText.text = "";
    }

}
