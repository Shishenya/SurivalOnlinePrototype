using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Item : MonoBehaviourPunCallbacks
{
    public int id; // ID �������� � Storage

    /// <summary>
    /// �������� ��������
    /// </summary>
    public void PickUp(PlayerInventory playerInventory)
    {

        // ��������� ������� � ��������� ������, ���� �� ����� ��� ���������
        BaseItem baseItem = GlobalStorage.Instance.storageItems.GetItemInStorage(id);
        if (baseItem != null)
        {
            // Debug.Log("������ � ����: " + hit.collider.gameObject.name + "; " + baseItem.basicParameters.itemName + " ��������� = " + hit.distance);
            if (baseItem.worldParameters.isPickUp)
            {
                playerInventory.AddItem(id);
                string textInfo = $"�� ��������� �������: {baseItem.basicParameters.itemName}";
                UIController.Instance.uiInfoWindow.ShowInfoText(textInfo);

                // ���������� ������� � ������� ����
                // PhotonNetwork.Destroy(photonView);
                GetComponent<PhotonView>().RPC("DestroyRpc", RpcTarget.All, photonView.ViewID);
                // Destroy(gameObject);
            }
        }
    }

    [PunRPC]
    private void DestroyRpc(int id)
    {
        //PhotonNetwork.Destroy(photonView);
        //yield return 0; // if you allow 1 frame to pass, the object's OnDestroy() method gets called and cleans up references.        
        //PhotonNetwork.AllocateViewID(photonView.ViewID);
        // Destroy(destroyGO);
        PhotonView.Find(id).gameObject.SetActive(false);
    }

}
