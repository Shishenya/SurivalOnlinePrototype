using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Camera _playerCamera;
    private Creature _creature;

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



    private void Awake()
    {
        _playerCamera = Camera.main;
        _creature = GetComponent<Creature>();
        _moveDetailsCreature = _creature.moveDetails; // Move
        _staminaDetailsCreature = _creature.staminaDetails; // Stamina

        // Устанавливаем скорость
        _activeMoveSpeed = _moveDetailsCreature.walkSpeed;
        // Начальное значение состояние движения
        _creatureMove = CreatureMove.walk;

        // Устанавливаем стамину
        _staminaDetailsCreature.currentAmount = _staminaDetailsCreature.maxAmount;
    }

    private void Start()
    {
        // Блокируем курсор
        CursorPlayerLock();
    }

    private void Update()
    {
        // Считываем движение
        _mouseInput = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y")) * mouseSensitivity;

        // Осмотр положения вокруг себя по горизонтале
        MouseViewHorizontal();

        // Осмотр по вертикали
        MouseViewVertical();

        // Проверка на нахождение на земле
        CheckGround();

        // Движение игрока
        InputMove();

        // Прыжок
        InputJump();

        // Взаиподействие
        InputUse();

        characterController.Move(_movement * Time.deltaTime);
    }

    private void LateUpdate()
    {
        // перемещение камеры согласно туда, куда смотрит игрок
        _playerCamera.transform.position = viewPoint.position;
        _playerCamera.transform.rotation = viewPoint.rotation;
    }

    /// <summary>
    /// Просмотр вокруг себя
    /// </summary>
    private void MouseViewHorizontal()
    {
        // Horizontal 
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y + _mouseInput.x, transform.rotation.eulerAngles.z);
    }

    /// <summary>
    /// Просмотр по верикати с условием оограничения
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
    /// Проверка на нахожение на земле
    /// </summary>
    private void CheckGround()
    {
        isGrounded = Physics.Raycast(groundCheckPoint.position, Vector3.down, 0.25f, groundLayers);
    }

    /// <summary>
    /// Считываем движение игрока
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
    /// Отслеживание прыжка
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
    /// Отслеживание применения кнопки взаимодействия
    /// </summary>
    private void InputUse()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Ray ray = _playerCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
            ray.origin = _playerCamera.transform.position;

            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                // Block PickUp items

                if (hit.collider.GetComponent<Item>() != null)
                {
                    Item item = hit.collider.GetComponent<Item>();
                    PlayerInventory playerInventory = GetComponent<PlayerInventory>();
                    BaseItem baseItem = playerInventory.StorageItems.GetItemInStorage(item.id);
                    if (baseItem != null)
                    {
                        Debug.Log("Объект в мире: " + hit.collider.gameObject.name + "; " + baseItem.basicParameters.itemName + " Дистанция = " + hit.distance);
                        playerInventory.AddItem(item.id);
                    }

                }
                else
                {
                    Debug.Log("Неопределнный Объект в мире: " + hit.collider.gameObject.name + "; Дистанция = " + hit.distance);
                }

                // ---------------------

            }
        }
    }

    /// <summary>
    /// Блокировка курсора
    /// </summary>
    private void CursorPlayerLock()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    /// <summary>
    /// Изменение стамины существа
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


}
