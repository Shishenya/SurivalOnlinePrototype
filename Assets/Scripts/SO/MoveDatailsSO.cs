using UnityEngine;

[CreateAssetMenu(fileName ="MoveDetailsSO_", menuName ="Scriptable Objects/Creature/MoveDetails")]
public class MoveDatailsSO : ScriptableObject
{
    public float walkSpeed = 5f;
    public float runSpeed = 8f;
    public float jumpForce = 10f;
}
