using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;
using Photon.Realtime;

public class MainMenuUIController : MonoBehaviourPunCallbacks
{
    public static MainMenuUIController Instance;

    public GameObject mainMenuButton; // кнопки главного меню
    public GameObject infoPanel; // панель с текстовой информацией
    public TMP_Text infoText; // информация на панели

    public GameObject createRoomPanel; // панель с созданием комнаты
    public TMP_InputField roomNameInput; // Импут для названия комнаты

    public GameObject roomPanelScreen; // панель запуска игры
    public TMP_Text roomNameText;

    public GameObject errorPanel; // панель с ошибками
    public TMP_Text errorText; // текст ошибки

    public GameObject findRoomPanel; // ссылка на панель поиска комнаты
    public RoomButton theRoomNameButton; // ссылка на кнопку комнаты
    [HideInInspector] public List<RoomButton> allRoomNameButtons = new List<RoomButton>();

    public TMP_Text playerNameText; // ссылка на текстовое поле с именем игрока
    [HideInInspector] public List<TMP_Text> allPlayerNickname = new List<TMP_Text>(); // все имена игрков в комнате

    public GameObject optionsPanel; // панель с натстройками
    public TMP_InputField nicknameInput; // текстовое поле с именем игрока
    public bool hasSetNickname; // флаг на установку NickName'a

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

    public void Quit()
    {
        Application.Quit();
    }

    /// <summary>
    /// Закрывает все UI элементы
    /// </summary>
    public void CloseAllUI()
    {
        mainMenuButton.SetActive(false);
        infoPanel.SetActive(false);
        infoText.text = "";
        createRoomPanel.SetActive(false);
        roomPanelScreen.SetActive(false);
        errorPanel.SetActive(false);
        findRoomPanel.SetActive(false);
        optionsPanel.SetActive(false);
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

    /// <summary>
    /// Открываем панель поиска комнаты
    /// </summary>
    public void ShowFindRoomPanel()
    {
        CloseAllUI();
        findRoomPanel.SetActive(true);
    }

    /// <summary>
    /// Открываем панель с настройками
    /// </summary>
    public void ShowOptionsPanel()
    {
        CloseAllUI();
        optionsPanel.SetActive(true);
    }

    /// <summary>
    /// Выход из комнаты
    /// </summary>
    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();

        infoPanel.SetActive(true);
        infoText.text = "Покидаю игру...";
    }

    /// <summary>
    /// Очистка всех кнопок с именами комнат в панели посика игры
    /// </summary>
    public void CLearAllRoomButtons()
    {
        foreach (RoomButton roomButton in allRoomNameButtons)
        {
            Destroy(roomButton.gameObject);
        }

        allRoomNameButtons.Clear();
    }

    /// <summary>
    /// Очищает все поля с именами игроков
    /// </summary>
    public void ClearAllListPlayers()
    {
        foreach (TMP_Text playerName in allPlayerNickname)
        {
            Destroy(playerName.gameObject);
        }
        allPlayerNickname.Clear();
    }

    /// <summary>
    /// Создаем поля с именами игроков
    /// </summary>
    public void ListAllPlayers()
    {
        ClearAllListPlayers();

        Player[] players = PhotonNetwork.PlayerList;

        for (int i = 0; i < players.Length; i++)
        {
            TMP_Text newPlayerName = Instantiate(playerNameText, playerNameText.transform.parent);
            newPlayerName.text = players[i].NickName;
            newPlayerName.gameObject.SetActive(true);

            allPlayerNickname.Add(newPlayerName);
        }
    }

    /// <summary>
    /// Установка Nickname
    /// </summary>
    public void SetNickName()
    {
        if (!string.IsNullOrEmpty(nicknameInput.text))
        {
            PhotonNetwork.NickName = nicknameInput.text;

            PlayerPrefs.SetString("nickname", nicknameInput.text);

            hasSetNickname = true;
            ShowMainMenu();
        }
    }

}
