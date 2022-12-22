using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class Launcher : MonoBehaviourPunCallbacks
{

    public static Launcher Instance;
    public string startLevel;

    private void Awake()
    {
        Instance = this;
    }

    /// <summary>
    /// Присоединение к главному серверу
    /// </summary>
    public override void OnConnectedToMaster()
    {
        // присоединяемся к Лобби
        PhotonNetwork.JoinLobby();

        PhotonNetwork.AutomaticallySyncScene = true;

        MainMenuUIController.Instance.infoText.text = "Сервер найден. Настраиваем...";
    }

    public override void OnJoinedLobby()
    {
        MainMenuUIController.Instance.CloseAllUI();

        // активируем кнопки меню
        MainMenuUIController.Instance.mainMenuButton.SetActive(true);

        // если не установлен никНейм
        if (!MainMenuUIController.Instance.hasSetNickname)
        {
            MainMenuUIController.Instance.ShowOptionsPanel();

            if (PlayerPrefs.HasKey("nickname"))
            {
                MainMenuUIController.Instance.nicknameInput.text = PlayerPrefs.GetString("nickname");
            }

        }
        else
        {
            PhotonNetwork.NickName = PlayerPrefs.GetString("nickname");
        }
    }

    /// <summary>
    /// Создание комнаты
    /// </summary>
    public void CreateRoom()
    {
        if (!string.IsNullOrEmpty(MainMenuUIController.Instance.roomNameInput.text))
        {
            RoomOptions roomOptions = new RoomOptions();
            roomOptions.MaxPlayers = 4;

            PhotonNetwork.CreateRoom(MainMenuUIController.Instance.roomNameInput.text, roomOptions);

            MainMenuUIController.Instance.CloseAllUI();
            MainMenuUIController.Instance.infoText.text = "Создание игры...";
            MainMenuUIController.Instance.infoPanel.SetActive(true);
        }
    }

    /// <summary>
    /// Вход в комнату
    /// </summary>
    public override void OnJoinedRoom()
    {
        MainMenuUIController.Instance.CloseAllUI();

        MainMenuUIController.Instance.roomPanelScreen.SetActive(true);
        MainMenuUIController.Instance.roomNameText.text = "Создание игры. \nКомната: " + PhotonNetwork.CurrentRoom.Name;

        ListAllPlayers();

        if (PhotonNetwork.IsMasterClient)
        {
            MainMenuUIController.Instance.startButton.SetActive(true);
        }
        else
        {
            MainMenuUIController.Instance.startButton.SetActive(false);
        }
    }

    /// <summary>
    /// Ошибка при создании комнаты
    /// </summary>
    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        string textError = "Не удалость создать комнату: " + message;

        MainMenuUIController.Instance.CloseAllUI();
        MainMenuUIController.Instance.errorPanel.SetActive(true);
        MainMenuUIController.Instance.errorText.text = textError;
    }

    /// <summary>
    /// Выход из комнаты
    /// </summary>
    public override void OnLeftRoom()
    {
        MainMenuUIController.Instance.CloseAllUI();
        MainMenuUIController.Instance.ShowMainMenu();
    }

    /// <summary>
    /// Обновление списка комнат
    /// </summary>
    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        MainMenuUIController.Instance.CLearAllRoomButtons();

        MainMenuUIController.Instance.theRoomNameButton.gameObject.SetActive(false);

        for (int i = 0; i < roomList.Count; i++)
        {
            if (roomList[i].PlayerCount != roomList[i].MaxPlayers && !roomList[i].RemovedFromList)
            {
                RoomButton newRoomButton = Instantiate(MainMenuUIController.Instance.theRoomNameButton,
                                           MainMenuUIController.Instance.theRoomNameButton.transform.parent);
                newRoomButton.SetButtonDetails(roomList[i]);
                newRoomButton.gameObject.SetActive(true);
                MainMenuUIController.Instance.allRoomNameButtons.Add(newRoomButton);
            }
        }

    }

    /// <summary>
    /// Присоединение к комнате
    /// </summary>
    public void JoinRoom(RoomInfo roomInfo)
    {
        PhotonNetwork.JoinRoom(roomInfo.Name);

        MainMenuUIController.Instance.CloseAllUI();
        MainMenuUIController.Instance.infoText.text = "Присоединяюсь к комнате " + roomInfo.Name;
        MainMenuUIController.Instance.infoPanel.SetActive(true);
    }

    /// <summary>
    /// Игрок вошел в комнату
    /// </summary>
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        ListAllPlayers();
    }

    /// <summary>
    /// Игрок вышел из комнаты
    /// </summary>    
    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        ListAllPlayers();
    }

    /// <summary>
    /// Получаем список всех игрков в комнате
    /// </summary>
    private void ListAllPlayers()
    {
        MainMenuUIController.Instance.ListAllPlayers();
    }

    public override void OnMasterClientSwitched(Player newMasterClient)
    {
        if (PhotonNetwork.IsMasterClient)
        {
            MainMenuUIController.Instance.startButton.SetActive(true);
        }
        else
        {
            MainMenuUIController.Instance.startButton.SetActive(false);
        }
    }

    /// <summary>
    /// Запуск игры
    /// </summary>
    public void StartGame()
    {
        PhotonNetwork.LoadLevel(startLevel);
    }
}
