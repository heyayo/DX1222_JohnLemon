using Photon.Pun;
using UnityEngine;

public class CaftingTable : MonoBehaviour
{
    private PhotonView _view;

    [SerializeField] private Light light;
    [SerializeField] private Transform exit;
    [SerializeField] private GameObject exitObject;

    private bool _crafted = false;

    private void Awake()
    {
        _view = GetComponent<PhotonView>();
    }

    private void Start()
    {
        exit = GameObject.FindGameObjectWithTag("Exit").transform;
        _crafted = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        var collectables = GameObject.FindGameObjectsWithTag("Collectable");
        if (collectables.Length <= 0)
        {
            _view.RPC("ChangeLightColor", RpcTarget.AllViaServer, new Vector3(0,1,0));
            _view.RPC("CraftExit",RpcTarget.AllViaServer);
        }
        else
        {
            _view.RPC("ChangeLightColor", RpcTarget.AllViaServer, new Vector3(1,0,0));
        }
    }

    [PunRPC]
    public void CraftExit()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            if (_crafted) return;
            PhotonNetwork.InstantiateRoomObject(exitObject.name,exit.position,Quaternion.identity);
            _crafted = true;
        }
    }

    [PunRPC]
    public void ChangeLightColor(Vector3 color)
    {
        light.color = new Color(color.x,color.y,color.z);
    }
}
