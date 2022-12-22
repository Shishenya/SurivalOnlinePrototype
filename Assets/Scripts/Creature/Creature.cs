using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    [HideInInspector] public PlayerController playerController;
    [HideInInspector] public PlayerInventory playerInventory;

    private void Awake()
    {
        playerController = GetComponent<PlayerController>();
        playerInventory = GetComponent<PlayerInventory>();

        maxStaminaAmount = staminaDetails.maxAmount;
        currentStaminaAmount = maxStaminaAmount;
    }

}
