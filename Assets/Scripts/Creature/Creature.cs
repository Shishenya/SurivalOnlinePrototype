using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Creature : MonoBehaviour
{
    public MoveDatailsSO moveDetails;
    public StaminaDetailsSO staminaDetails;

    // Stamina
    public float maxStaminaAmount;
    public float currentStaminaAmount;

    private void Awake()
    {
        maxStaminaAmount = staminaDetails.maxAmount;
        currentStaminaAmount = maxStaminaAmount;
    }

}
