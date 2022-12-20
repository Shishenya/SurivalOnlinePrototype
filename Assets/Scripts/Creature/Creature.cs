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

    private void Awake()
    {
        maxStaminaAmount = staminaDetails.maxAmount;
        currentStaminaAmount = maxStaminaAmount;
    }

}
