using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Creature : MonoBehaviour
{
    // Details CReature
    public MoveDatailsSO moveDetails;
    public StaminaDetailsSO staminaDetails;

    // Points
    public GameObject spawnPointAfterRemove;

    // Stamina
    public float maxStaminaAmount;
    public float currentStaminaAmount;

    public PlayerController playerController;
    public PlayerInventory playerInventory;

    public GameObject inventoryObject; // GO инвентаря
    public string parentInventoryObjectTag = "InventoryParent"; // Тег родителя для инвентаря

    private void Awake()
    {
        playerController = GetComponent<PlayerController>(); // получаем комопонент управления

        // Создаем на сцене инвентарь
        GameObject parentInventory = GameObject.FindGameObjectWithTag("InventoryParent");
        GameObject _inventory = Instantiate(inventoryObject, parentInventory.transform);
        _inventory.name = "Inventory_" + PhotonNetwork.NickName;

        // Получаем ссылку на инвентарь
        playerInventory = _inventory.GetComponent<PlayerInventory>();
        // передаем в него ссылку на контроллер игрока
        playerInventory.Init(playerController);        

        maxStaminaAmount = staminaDetails.maxAmount;
        currentStaminaAmount = maxStaminaAmount;
    }

}
