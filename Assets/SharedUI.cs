using Photon.Pun;
using TMPro;
using UnityEngine;

public class SharedUI : MonoBehaviour
{
    public static SharedUI Instance;

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
        UpdateCoinsText(0);
    }

    [PunRPC]
    public void UpdateCoinsText(int coins)
    {
        CoinsText.text = "Coins Collected: " + coins.ToString() + "/5";
        JLGameManager.Instance.collectedCoins = coins;
    }
}
