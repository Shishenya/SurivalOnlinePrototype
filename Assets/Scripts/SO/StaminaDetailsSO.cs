using UnityEngine;

[CreateAssetMenu(fileName ="StaminaDetailsSO_", menuName = "Scriptable Objects/Creature/StaminaDetails")]
public class StaminaDetailsSO : ScriptableObject
{
    public float maxAmount = 100f;
    public float currentAmount;
    public float rateDownSpeed = 0.2f;
    public float rateUpSpeed = 0.1f;
}
