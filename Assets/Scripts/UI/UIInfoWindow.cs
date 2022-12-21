using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIInfoWindow : MonoBehaviour
{
    [SerializeField] private TMP_Text _infoText;
    [SerializeField] private float _timeInfo = 2f;

    public void ShowInfoText(string textInfo)
    {
        gameObject.SetActive(true);
        StartCoroutine(ShowInfoRoutine(textInfo));
    }

    private IEnumerator ShowInfoRoutine(string textInfo)
    {
        _infoText.text = textInfo;
        yield return new WaitForSeconds(_timeInfo);
        _infoText.text = "";
        yield return null;
        this.gameObject.SetActive(false);
    }
}
