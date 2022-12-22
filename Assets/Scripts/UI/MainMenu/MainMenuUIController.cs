using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;
using Photon.Realtime;

public class MainMenuUIController : MonoBehaviourPunCallbacks
{
    public static MainMenuUIController Instance;

    public GameObject mainMenuButton; // ������ �������� ����
    public GameObject infoPanel; // ������ � ��������� �����������
    public TMP_Text infoText; // ���������� �� ������

    public GameObject createRoomPanel; // ������ � ��������� �������
    public TMP_InputField roomNameInput; // ����� ��� �������� �������

    public GameObject roomPanelScreen; // ������ ������� ����
    public TMP_Text roomNameText;

    public GameObject errorPanel; // ������ � ��������
    public TMP_Text errorText; // ����� ������

    public GameObject findRoomPanel; // ������ �� ������ ������ �������
    public RoomButton theRoomNameButton; // ������ �� ������ �������
    [HideInInspector] public List<RoomButton> allRoomNameButtons = new List<RoomButton>();

    public TMP_Text playerNameText; // ������ �� ��������� ���� � ������ ������
    [HideInInspector] public List<TMP_Text> allPlayerNickname = new List<TMP_Text>(); // ��� ����� ������ � �������

    public GameObject optionsPanel; // ������ � ������������
    public TMP_InputField nicknameInput; // ��������� ���� � ������ ������
    public bool hasSetNickname; // ���� �� ��������� NickName'a

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        CloseAllUI();

        infoPanel.SetActive(true);
        infoText.text = "������������� � �������...";

        PhotonNetwork.ConnectUsingSettings();
    }   

    public void Quit()
    {
        Application.Quit();
    }

    /// <summary>
    /// ��������� ��� UI ��������
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
    /// ��������� ������ �������� �������
    /// </summary>
    public void ShowCreateRoomPanel()
    {
        CloseAllUI();
        createRoomPanel.SetActive(true);
    }

    /// <summary>
    /// ��������� ������� ����
    /// </summary>
    public void ShowMainMenu()
    {
        CloseAllUI();
        mainMenuButton.SetActive(true);
    }

    /// <summary>
    /// ��������� ������ ������ �������
    /// </summary>
    public void ShowFindRoomPanel()
    {
        CloseAllUI();
        findRoomPanel.SetActive(true);
    }

    /// <summary>
    /// ��������� ������ � �����������
    /// </summary>
    public void ShowOptionsPanel()
    {
        CloseAllUI();
        optionsPanel.SetActive(true);
    }

    /// <summary>
    /// ����� �� �������
    /// </summary>
    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();

        infoPanel.SetActive(true);
        infoText.text = "������� ����...";
    }

    /// <summary>
    /// ������� ���� ������ � ������� ������ � ������ ������ ����
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
    /// ������� ��� ���� � ������� �������
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
    /// ������� ���� � ������� �������
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
    /// ��������� Nickname
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
