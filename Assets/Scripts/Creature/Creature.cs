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

    public GameObject inventoryObject; // GO ���������
    public string parentInventoryObjectTag = "InventoryParent"; // ��� �������� ��� ���������

    private void Awake()
    {
        playerController = GetComponent<PlayerController>(); // �������� ���������� ����������

        // ������� �� ����� ���������
        GameObject parentInventory = GameObject.FindGameObjectWithTag("InventoryParent");
        GameObject _inventory = Instantiate(inventoryObject, parentInventory.transform);
        _inventory.name = "Inventory_" + PhotonNetwork.NickName;

        // �������� ������ �� ���������
        playerInventory = _inventory.GetComponent<PlayerInventory>();
        // �������� � ���� ������ �� ���������� ������
        playerInventory.Init(playerController);        

        maxStaminaAmount = staminaDetails.maxAmount;
        currentStaminaAmount = maxStaminaAmount;
    }

}
