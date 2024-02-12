using Photon.Pun;
using TMPro;
using UnityEngine;

public class SharedUI : MonoBehaviour
{
    public static SharedUI Instance;

    public PhotonView view;

    [SerializeField] private TMP_Text itemsLeftToCollect;

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
        foreach (var o in objects)
        {
            itemsLeftToCollect.text += o.name + '\n';
        }
    }
}
