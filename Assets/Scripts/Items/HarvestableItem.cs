using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HarvestableItem : Item
{
    [SerializeField] private GameObject _prefabDropAfterHarvest; // �������, ������� ������ ����� "�����"
    [SerializeField] private GameObject _prefabThisObjectAfterHarvest; // ������, ������� ��������� �� ����� ��������, ����� �����

    public ToolActionsItems needTool; // ������ ���������� ����� ����������� ����
    public int amountActionMax = 2; // ���������� �������� ��������� ��� ��� �����
    public float timeOneIterationHarvets = 2f; // ���������� ������ �� ���� ����� ��������
    private int _currentAmountAction = 0;  // ���������� ������� ��������
    private Coroutine harvestRoutine;

    /// <summary>
    /// ���� �������� ����� ������� �� ��������
    /// </summary>
    public void HarvestItem(PlayerInventory playerInventory)
    {

        if (CheckToolsByHarvest(playerInventory)) // ��������� ������� � ��������� ������� �����������
        {
            // ���� ��������
            if (harvestRoutine == null) // ������ ��� �����, ����� ��������
            {
                harvestRoutine = StartCoroutine(HarvestItemRoutine(playerInventory));
            }
            else // ���� �������� ��� ����
            {
                UIController.Instance.uiErrorWindow.gameObject.SetActive(true);
                UIController.Instance.uiErrorWindow.ShowErrorText(ErrorCode.beingHarvestNow);
            }
        }
        else
        {
            UIController.Instance.uiErrorWindow.gameObject.SetActive(true);
            UIController.Instance.uiErrorWindow.ShowErrorText(ErrorCode.noToolsByHarvest);
        }
    }

    /// <summary>
    /// �������� ����� ��������
    /// </summary>
    private IEnumerator HarvestItemRoutine(PlayerInventory playerInventory)
    {

        yield return new WaitForSeconds(timeOneIterationHarvets);
        _currentAmountAction++;

        if (_currentAmountAction >= amountActionMax)
        {
            EndHarvest(playerInventory); // ����� ����� ���������
        }
        else
        {
            string textInfo = $"����� ��������� ���� ���������� ������� ��� {amountActionMax - _currentAmountAction} ��������";
            UIController.Instance.uiInfoWindow.ShowInfoText(textInfo);
        }
        harvestRoutine = null;
        yield return null;

    }

    /// <summary>
    /// ��������� ����� ���������
    /// </summary>
    private void EndHarvest(PlayerInventory playerInventory)
    {
        string textInfo = $"������ ���������";
        UIController.Instance.uiInfoWindow.ShowInfoText(textInfo);
        if (_prefabDropAfterHarvest != null)
        {
            GameObject harvestItem = Instantiate(_prefabDropAfterHarvest, GlobalVariable.Instance.itemsParent.transform);
            harvestItem.transform.position = RandomDropPositionAfterHarvest();
            _currentAmountAction = 0;
        }
    }

    /// <summary>
    /// ���������, ��� ���� ����������� �������� ��� �����
    /// </summary>
    private bool CheckToolsByHarvest(PlayerInventory playerInventory)
    {
        ToolActionsItems toolsInPlayer = playerInventory.ToolsInPlayer();
        if (needTool.isChop && toolsInPlayer.isChop) return true;
        if (needTool.isPrickStone && toolsInPlayer.isPrickStone) return true;

        return false;
    }

    /// <summary>
    /// ���������� ������� �������� ����� �����
    /// </summary>
    private Vector3 RandomDropPositionAfterHarvest()
    {
        return new Vector3(transform.position.x + 1f, transform.position.y + 1f, transform.position.z + 0f);
    }
}
