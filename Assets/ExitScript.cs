using Photon.Pun;
using UnityEngine;

public class ExitScript : MonoBehaviour
{
    private PhotonView _view;

    private void Awake()
    {
        _view = GetComponent<PhotonView>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (PhotonNetwork.IsMasterClient)
        {
            _view.RPC("WinGame",RpcTarget.AllViaServer);
        }
    }

    [PunRPC]
    public void WinGame()
    {
        JLGameManager.Instance.endingScript.PlayerWin();
    }
}
