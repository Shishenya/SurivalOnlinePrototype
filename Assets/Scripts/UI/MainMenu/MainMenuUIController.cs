using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;

public class MainMenuUIController : MonoBehaviourPunCallbacks
{
    public static MainMenuUIController Instance;

    public GameObject mainMenuButton; // кнопки главного меню
    public GameObject infoPanel; // панель с текстовой информацией
    public TMP_Text infoText; // информация на панели
    public GameObject createRoomPanel; // панель с созданием комнаты
    public TMP_InputField roomNameINput;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        CloseAllUI();

        infoPanel.SetActive(true);
        infoText.text = "Присоединение к серверу...";

        PhotonNetwork.ConnectUsingSettings();
    }

    /// <summary>
    /// Присоединение к главному серверу
    /// </summary>
    public override void OnConnectedToMaster()
    {       
        // присоединяемся к Лобби
        PhotonNetwork.JoinLobby();
        infoText.text = "Сервер найден. Настраиваем...";
    }

    public override void OnJoinedLobby()
    {
        CloseAllUI();

        // активируем кнопки меню
        mainMenuButton.SetActive(true);
    }

    public void Quit()
    {
        Application.Quit();
    }

    /// <summary>
    /// Закрывает все UI элементы
    /// </summary>
    private void CloseAllUI()
    {
        mainMenuButton.SetActive(false);
        infoPanel.SetActive(false);
        infoText.text = "";
        createRoomPanel.SetActive(false);
    }

    /// <summary>
    /// Открываем панель создания комнаты
    /// </summary>
    public void ShowCreateRoomPanel()
    {
        CloseAllUI();
        createRoomPanel.SetActive(true);
    }

    /// <summary>
    /// Открываем главное меню
    /// </summary>
    public void ShowMainMenu()
    {
        CloseAllUI();
        mainMenuButton.SetActive(true);
    }
}
