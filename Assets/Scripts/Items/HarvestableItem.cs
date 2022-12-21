using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HarvestableItem : Item
{
    [SerializeField] private GameObject _prefabDropAfterHarvest; // предмет, который упадет после "сбора"
    [SerializeField] private GameObject _prefabThisObjectAfterHarvest; // префаб, который появиться на месте текущего, после сбора

    public ToolActionsItems needTool; // какими предметами можно осуществить сбор
    public int amountActionMax = 2; // колисество действия предметом для его сбора
    public float timeOneIterationHarvets = 2f; // количество секунд на сбор одной итерации
    private int _currentAmountAction = 0;  // количество текущий итераций
    private Coroutine harvestRoutine;

    /// <summary>
    /// Сбор предмета после нажатия на действие
    /// </summary>
    public void HarvestItem(PlayerInventory playerInventory)
    {

        if (CheckToolsByHarvest(playerInventory)) // проверяем наличие в инвентаре нужного инструмента
        {
            // Сбор предмета
            if (harvestRoutine == null) // сейчас нет сбора, тогда собираем
            {
                harvestRoutine = StartCoroutine(HarvestItemRoutine(playerInventory));
            }
            else // сбор предмета уже идет
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
    /// Корутина сбора предмета
    /// </summary>
    private IEnumerator HarvestItemRoutine(PlayerInventory playerInventory)
    {

        yield return new WaitForSeconds(timeOneIterationHarvets);
        _currentAmountAction++;

        if (_currentAmountAction >= amountActionMax)
        {
            EndHarvest(playerInventory); // конец сбора предметов
        }
        else
        {
            string textInfo = $"Чтобы завершить сбор необходимо сделать еще {amountActionMax - _currentAmountAction} действий";
            UIController.Instance.uiInfoWindow.ShowInfoText(textInfo);
        }
        harvestRoutine = null;
        yield return null;

    }

    /// <summary>
    /// Окончание сбора предметов
    /// </summary>
    private void EndHarvest(PlayerInventory playerInventory)
    {
        string textInfo = $"Добыча завершена";
        UIController.Instance.uiInfoWindow.ShowInfoText(textInfo);
        if (_prefabDropAfterHarvest != null)
        {
            GameObject harvestItem = Instantiate(_prefabDropAfterHarvest, GlobalVariable.Instance.itemsParent.transform);
            harvestItem.transform.position = RandomDropPositionAfterHarvest();
            _currentAmountAction = 0;
        }
    }

    /// <summary>
    /// Проверяет, что есть необхолимые предметы для сбора
    /// </summary>
    private bool CheckToolsByHarvest(PlayerInventory playerInventory)
    {
        ToolActionsItems toolsInPlayer = playerInventory.ToolsInPlayer();
        if (needTool.isChop && toolsInPlayer.isChop) return true;
        if (needTool.isPrickStone && toolsInPlayer.isPrickStone) return true;

        return false;
    }

    /// <summary>
    /// Вовзаращет пощицию предмета после сбора
    /// </summary>
    private Vector3 RandomDropPositionAfterHarvest()
    {
        return new Vector3(transform.position.x + 1f, transform.position.y + 1f, transform.position.z + 0f);
    }
}
