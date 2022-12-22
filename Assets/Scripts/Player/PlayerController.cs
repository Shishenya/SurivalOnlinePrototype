using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Camera _playerCamera;
    private Creature _creature;

    public bool enablePlayerController = true;

    [SerializeField] bool invertLook = false;
    public Transform viewPoint;
    public float mouseSensitivity = 1f;
    private float verticalRotStore;
    private Vector2 _mouseInput;

    [SerializeField] private CharacterController characterController;
    private Vector3 _moveDirection;
    private Vector3 _movement;

    // Move Details
    private MoveDatailsSO _moveDetailsCreature;
    private float _activeMoveSpeed;
    private CreatureMove _creatureMove;

    // Parameters
    private StaminaDetailsSO _staminaDetailsCreature;

    // Jump Details
    [SerializeField] private float gravityMod = 2f;

    [SerializeField] private Transform groundCheckPoint;
    private bool isGrounded;
    public LayerMask groundLayers;

    public static PlayerController Instance;
    public GameObject playerSpawnPointbyRemove;

    private PlayerInventory _playerInventory;

    private void Awake()
    {

        Instance = this; // ��������� �� ����
        _playerCamera = Camera.main; // ������
        _creature = GetComponent<Creature>(); // ��������
        

    }

    private void Start()
    {
        // ��������� ������
        CursorPlayerLock();

        _moveDetailsCreature = _creature.moveDetails; // ���������� ��������
        _staminaDetailsCreature = _creature.staminaDetails; // ���������� �������
        _playerInventory = _creature.playerInventory; // ���������

        // ������������� ��������
        _activeMoveSpeed = _moveDetailsCreature.walkSpeed;
        // ��������� �������� ��������� ��������
        _creatureMove = CreatureMove.walk;

        // ������������� �������
        _staminaDetailsCreature.currentAmount = _staminaDetailsCreature.maxAmount;

        playerSpawnPointbyRemove = _creature.spawnPointAfterRemove; // ����� ������� �������� �� ���������
    }

    private void Update()
    {
        if (!enablePlayerController) return;

        // ��������� ��������
        _mouseInput = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y")) * mouseSensitivity;

        // ������ ��������� ������ ���� �� �����������
        MouseViewHorizontal();

        // ������ �� ���������
        MouseViewVertical();

        // �������� �� ���������� �� �����
        CheckGround();

        // �������� ������
        InputMove();

        // ������
        InputJump();

        // ��������������
        InputUse();

        characterController.Move(_movement * Time.deltaTime);
    }

    private void LateUpdate()
    {
        // ����������� ������ �������� ����, ���� ������� �����
        _playerCamera.transform.position = viewPoint.position;
        _playerCamera.transform.rotation = viewPoint.rotation;
    }

    /// <summary>
    /// �������� ������ ����
    /// </summary>
    private void MouseViewHorizontal()
    {
        // Horizontal 
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y + _mouseInput.x, transform.rotation.eulerAngles.z);
    }

    /// <summary>
    /// �������� �� �������� � �������� ������������
    /// </summary>
    private void MouseViewVertical()
    {

        // Vertical
        verticalRotStore += _mouseInput.y;
        verticalRotStore = Mathf.Clamp(verticalRotStore, -60f, 60f);

        if (invertLook)
        {
            viewPoint.rotation = Quaternion.Euler(verticalRotStore, viewPoint.rotation.eulerAngles.y, viewPoint.rotation.eulerAngles.z);
        }
        else
        {
            viewPoint.rotation = Quaternion.Euler(-verticalRotStore, viewPoint.rotation.eulerAngles.y, viewPoint.rotation.eulerAngles.z);
        }
    }

    /// <summary>
    /// �������� �� ��������� �� �����
    /// </summary>
    private void CheckGround()
    {
        isGrounded = Physics.Raycast(groundCheckPoint.position, Vector3.down, 0.25f, groundLayers);
    }

    /// <summary>
    /// ��������� �������� ������
    /// </summary>
    private void InputMove()
    {
        // Move
        _moveDirection = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));
        // Move or run
        float testFloat = 10f;
        if (Input.GetKey(KeyCode.LeftShift) && _creature.currentStaminaAmount > testFloat)
        {
            if (_creature.currentStaminaAmount > testFloat)
            {
                _creatureMove = CreatureMove.run;
            }
            else
            {
                _creatureMove = CreatureMove.walk;
            }
            _activeMoveSpeed = _moveDetailsCreature.runSpeed;
            ChangeStaminaCreature(_creatureMove);
        }
        else
        {
            _activeMoveSpeed = _moveDetailsCreature.walkSpeed;
            _creatureMove = CreatureMove.walk;
            ChangeStaminaCreature(_creatureMove);
        }

        float yVelocity = _movement.y;
        _movement = ((transform.forward * _moveDirection.z) + (transform.right * _moveDirection.x)).normalized * _activeMoveSpeed;
        _movement.y = yVelocity;

        _creatureMove = CreatureMove.walk;
    }

    /// <summary>
    /// ������������ ������
    /// </summary>
    private void InputJump()
    {
        if (characterController.isGrounded)
        {
            _movement.y = 0f;
        }


        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            _movement.y = _moveDetailsCreature.jumpForce;
        }

        _movement.y += Physics.gravity.y * Time.deltaTime * gravityMod;
    }

    /// <summary>
    /// ������������ ���������� ������ ��������������
    /// </summary>
    private void InputUse()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Ray ray = _playerCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
            ray.origin = _playerCamera.transform.position;

            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                // ���� ��������
                HarvestItem(hit);

                // ��������� �������
                PickUpItem(hit);                

            }
        }
    }

    /// <summary>
    /// ���������� �������
    /// </summary>
    private void CursorPlayerLock()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    /// <summary>
    /// ��������� ������� ��������
    /// </summary>
    private void ChangeStaminaCreature(CreatureMove creatureMove)
    {
        if (creatureMove == CreatureMove.run)
        {
            _creature.currentStaminaAmount -= _staminaDetailsCreature.rateDownSpeed * Time.deltaTime;
            if (_creature.currentStaminaAmount <= 0)
            {
                _creature.currentStaminaAmount = 0;
            }
        }
        else
        {
            _creature.currentStaminaAmount += _staminaDetailsCreature.rateUpSpeed * Time.deltaTime;
            if (_creature.currentStaminaAmount >= _staminaDetailsCreature.maxAmount)
            {
                _creature.currentStaminaAmount = _staminaDetailsCreature.maxAmount;
            }
        }

        StaminaBar.instance.ChangeAmount(_creature.currentStaminaAmount, _creature.maxStaminaAmount);

        // Debug.Log(_staminaDetailsCreature.currentAmount);

    }

    /// <summary>
    /// �������� - �������� ��������
    /// </summary>
    private void PickUpItem(RaycastHit hit)
    {
        if (hit.collider.GetComponent<Item>() != null)
        {
            Item item = hit.collider.GetComponent<Item>();            
            item.PickUp(_playerInventory);            
        }
    }

    /// <summary>
    /// �������� - ���� ��������
    /// </summary>
    private void HarvestItem(RaycastHit hit)
    {
        if (hit.collider.GetComponent<HarvestableItem>()!=null)
        {            
            hit.collider.gameObject.GetComponent<HarvestableItem>().HarvestItem(_playerInventory);
        }
    }


}
