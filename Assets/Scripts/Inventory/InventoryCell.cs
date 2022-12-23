using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.UI;
using Photon.Pun;
using System;

public class InventoryCell : MonoBehaviourPunCallbacks, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Sprite _transperentSprite; // ������ �� ������ "�������"

    [SerializeField] private GameObject _itemContextPrefab; // ������ �� ������ ��������� �������� ��� ��������
    [SerializeField] private GameObject _itemContextPoint; // ����� ��� ������ �������� ��������
    private GameObject _itemContext = null; // �� �������� ��������

    public Image icon; // ������ �� ����������� �������� � ���������
    public TMP_Text amountText; // ������ �� ����� � ���������� ���������
    private BaseItem _baseItem = null; // ������� � ������

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

            PlayerComponent playerComponent = MatchManager.Instance.GetPlayerComponent(PhotonNetwork.NickName);
            if (playerComponent == null) return;

            // �������� ��� �������� �� ��������
            if (_baseItem == null) return;
            if (_baseItem.worldParameters.prefabInWorld != null)
            {
                //GameObject removeItem = Instantiate(_baseItem.worldParameters.prefabInWorld, GlobalVariable.Instance.itemsParent.transform);
                //removeItem.transform.position = PlayerController.Instance.playerSpawnPointbyRemove.transform.position;

                string namePrefab = _baseItem.resoursePathFolder + _baseItem.worldParameters.prefabInWorld.name;
                GameObject removeItem = PhotonNetwork.Instantiate(namePrefab, 
                    playerComponent.playerController.playerSpawnPointbyRemove.transform.position, Quaternion.identity);
                removeItem.transform.SetParent(GlobalVariable.Instance.itemsParent.transform);

                string textInfo = $"�� ��������� ������� {_baseItem.basicParameters.itemName}";
                UIController.Instance.uiInfoWindow.ShowInfoText(textInfo);
            }

            // ������� �� ���������           
            if (playerComponent != null)
            {
                PlayerInventory playerInventory = playerComponent.playerInventory;
                playerInventory.RemoveItem(_baseItem.id);
            }


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
