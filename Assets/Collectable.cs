using Photon.Pun;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    private PhotonView _view;

    void Awake()
    {
        _view = GetComponent<PhotonView>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (PhotonNetwork.IsMasterClient)
            {
                PhotonNetwork.Destroy(gameObject);
                SharedUI.Instance.view.RPC("SyncCollectables", RpcTarget.AllViaServer);
            }
        }
    }
}
