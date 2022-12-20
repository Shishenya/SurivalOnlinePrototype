using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ItemContextUI : MonoBehaviour
{
    [SerializeField] private TMP_Text _nameText;
    [SerializeField] private TMP_Text _descriptionText;
    [SerializeField] private TMP_Text _actionsText;

    private void OnEnable()
    {
        _actionsText.text = "";
    }
    public void Init(BaseItem baseItem)
    {
        _nameText.text = baseItem.basicParameters.itemName;
        _descriptionText.text = baseItem.basicParameters.itemDescription;
        if (ActionItem(baseItem, out string textAction))
        {
            _actionsText.text = textAction;
        }
    }

    public void Clear()
    {
        _nameText.text = "";
        _descriptionText.text = "";
    }

    private bool ActionItem(BaseItem baseItem, out string textAction)
    {
        bool isAction = false;
        textAction = "";

        if (baseItem.toolActions.isChop)
        {
            isAction = true;
            textAction += "����� ������ �������\n";
        }

        if (baseItem.toolActions.isPrickStone)
        {
            isAction = true;
            textAction += "����� �������� ������\n";
        }

        return isAction;
    }
}
