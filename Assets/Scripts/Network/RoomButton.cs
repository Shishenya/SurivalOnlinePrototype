using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Realtime;

public class RoomButton : MonoBehaviour
{
    public TMP_Text buttonText;
    private RoomInfo roomInfo;

    /// <summary>
    /// ������ ��������� ������ ��������
    /// </summary>
    public void SetButtonDetails(RoomInfo inputInfo)
    {
        roomInfo = inputInfo;
        buttonText.text = roomInfo.Name + "(" + roomInfo.PlayerCount + "/" + roomInfo.MaxPlayers + ")";
    }

    /// <summary>
    /// �������� ��� ������� �� ������ ������ �������
    /// </summary>
    public void OpenRoom()
    {
        Launcher.Instance.JoinRoom(roomInfo);
    }
}
