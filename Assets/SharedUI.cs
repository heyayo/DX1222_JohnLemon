using Photon.Pun;
using TMPro;
using UnityEngine;

public class SharedUI : MonoBehaviour
{
    public static SharedUI Instance;

    public PhotonView view;

    [SerializeField] private TMP_Text itemsLeftToCollect;

    private int _total = 0;

    private void Awake()
    {
        Instance = this;
        view = GetComponent<PhotonView>();
    }

    private void Start()
    {
        SyncCollectables();
    }

    [PunRPC]
    public void SyncCollectables()
    {
        itemsLeftToCollect.text = "";
        var objects = GameObject.FindGameObjectsWithTag("Collectable");
        itemsLeftToCollect.text = "Items Left To Collect: " + objects.Length + '/' + _total.ToString();
    }

    [PunRPC]
    public void FirstRun()
    {
        _total = GameObject.FindGameObjectsWithTag("Collectable").Length;
    }
}
