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
    /// ���������� ����� ������ �� �� ����
    /// </summary>
    private string GetErrorText(ErrorCode errorCode)
    {
        switch (errorCode)  
        {
            case ErrorCode.noToolsByHarvest:
                return "��� ����������� ��� �����!";
            case ErrorCode.maxThisItemInInventory:
                return "� ��� ������������ ���������� ������� ��������";
            case ErrorCode.beingHarvestNow:
                return "������ �������� ��� ����";
            default:
                return "���������� ������";
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
