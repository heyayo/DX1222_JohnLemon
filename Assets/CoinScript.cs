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
        if (other.CompareTag("Player"))
        {
            if (PhotonNetwork.IsMasterClient)
            {
                PhotonNetwork.Destroy(gameObject);
                manager.collectedCoins += 1;
                SharedUI.Instance.view.RPC("UpdateCoinsText",RpcTarget.AllViaServer,manager.collectedCoins);
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
