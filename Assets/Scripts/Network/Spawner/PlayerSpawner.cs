using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerSpawner : MonoBehaviour
{
    public static PlayerSpawner Instance;

    public GameObject playerPrefab; // ������ ������
    [SerializeField] private List<Transform> spawnPoint;

    private GameObject player;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        if (PhotonNetwork.IsConnected)
        {
            SpawnPlayer();
        }
    }

    /// <summary>
    /// ����� ������
    /// </summary>
    public void SpawnPlayer()
    {
        Transform spawnTransform = GetRandonPoint();

        // player = Instantiate(playerPrefab, spawnTransform.position, Quaternion.identity);

        player = PhotonNetwork.Instantiate(playerPrefab.name, spawnTransform.position, spawnTransform.rotation);
        if (player != null)
        {
            Debug.Log(player.name);
        }
    }

    /// <summary>
    /// ���������� ��������� ������� ������
    /// </summary>
    /// <returns></returns>
    private Transform GetRandonPoint()
    {
        return spawnPoint[Random.Range(0,spawnPoint.Count)];
    }

}
