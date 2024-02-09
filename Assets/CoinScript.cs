using ExitGames.Client.Photon;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class CoinScript : MonoBehaviour
{
    private PhotonView _view;

    [SerializeField] public JLGameManager manager;
    [SerializeField] public GameEnding gameEnding;

    private void Awake()
    {
        _view = GetComponent<PhotonView>();
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("COLLIDED");
        if (other.CompareTag("Player"))
        {
            Debug.Log("COLLIDED WITH PLAYER");
            if (PhotonNetwork.IsMasterClient)
            {
                PhotonNetwork.Destroy(gameObject);
                manager.collectedCoins += 1;
                UISync.Instance.view.RPC("SyncCoins",RpcTarget.AllViaServer,manager.collectedCoins);
                if (manager.collectedCoins >= 5)
                {
                    RaiseEventOptions options = new RaiseEventOptions();
                    options.Receivers = ReceiverGroup.All;
                    PhotonNetwork.RaiseEvent((byte)RaiseEvents.EVENT_INDEX.END_GAME, "", options,
                        SendOptions.SendReliable);
                }
            }
        }
    }
}
