using UnityEngine;
using TMPro;
using System.Collections;

public class UIErrorWindow : MonoBehaviour
{
    [SerializeField] private TMP_Text _errorText;
    [SerializeField] private float _timeError = 3f;

    public void ShowErrorText(ErrorCode errorCode)
    {
        string textError = GetErrorText(errorCode);
        StartCoroutine(ShowTextErrorRoutine(textError));
    }

    /// <summary>
    /// ВОзвращает текст ошибки по ее коду
    /// </summary>
    private string GetErrorText(ErrorCode errorCode)
    {
        switch (errorCode)  
        {
            case ErrorCode.noToolsByHarvest:
                return "Нет инструмента для сбора!";
            case ErrorCode.maxThisItemInInventory:
                return "У вас максимальное количество данного предмета";
            case ErrorCode.beingHarvestNow:
                return "Добыча предмета уже идет";
            default:
                return "незвестная ошибка";
        }
    } 

    private IEnumerator ShowTextErrorRoutine(string textError)
    {
        _errorText.text = textError;
        yield return new WaitForSeconds(_timeError);
        _errorText.text = "";
        yield return null;
        this.gameObject.SetActive(false);
    }

}
