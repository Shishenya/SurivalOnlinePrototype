using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ItemContextUI : MonoBehaviour
{
    [SerializeField] private TMP_Text descriptionText;

    public void Init(string name, string description)
    {
        descriptionText.text = $"{name} \n\n{description}";
    }

    public void Clear()
    {
        descriptionText.text = "";
    }
}
