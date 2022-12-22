using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;

public class MainMenuUIController : MonoBehaviourPunCallbacks
{
    public static MainMenuUIController Instance;

    public GameObject mainMenuButton; // ������ �������� ����
    public GameObject infoPanel; // ������ � ��������� �����������
    public TMP_Text infoText; // ���������� �� ������
    public GameObject createRoomPanel; // ������ � ��������� �������
    public TMP_InputField roomNameINput;

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

    /// <summary>
    /// ������������� � �������� �������
    /// </summary>
    public override void OnConnectedToMaster()
    {       
        // �������������� � �����
        PhotonNetwork.JoinLobby();
        infoText.text = "������ ������. �����������...";
    }

    public override void OnJoinedLobby()
    {
        CloseAllUI();

        // ���������� ������ ����
        mainMenuButton.SetActive(true);
    }

    public void Quit()
    {
        Application.Quit();
    }

    /// <summary>
    /// ��������� ��� UI ��������
    /// </summary>
    private void CloseAllUI()
    {
        mainMenuButton.SetActive(false);
        infoPanel.SetActive(false);
        infoText.text = "";
        createRoomPanel.SetActive(false);
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
}
