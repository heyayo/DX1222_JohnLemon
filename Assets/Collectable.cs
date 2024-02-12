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
        Debug.Log("COLLIDING WITH COLLECTABLE");
        if (other.CompareTag("Player"))
        {
            Debug.Log("COLLECTING");
            if (PhotonNetwork.IsMasterClient)
            {
                PhotonNetwork.Destroy(gameObject);
                SharedUI.Instance.view.RPC("SyncCollectables", RpcTarget.AllViaServer);
            }
        }
    }
}
