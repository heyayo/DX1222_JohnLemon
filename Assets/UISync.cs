using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using TMPro;
using UnityEngine;

public class UISync : MonoBehaviour
{
    public static UISync Instance;

    public PhotonView view;

    [SerializeField] private TMP_Text CoinsText;

    private void Awake()
    {
        Instance = this;
        view = GetComponent<PhotonView>();
    }

    private void Start()
    {
        // Set the Coins Text at Game Start
        SyncCoins(0);
    }

    [PunRPC]
    public void SyncCoins(int coins)
    {
        CoinsText.text = "Coins Collected: " + coins.ToString() + "/5";
        JLGameManager.Instance.collectedCoins = coins;
    }
}
