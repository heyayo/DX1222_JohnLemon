using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class Observer : MonoBehaviour
{
    public Transform player;
    public GameEnding gameEnding;

    private PhotonView _photonView;

    bool m_IsPlayerInRange;

    private void Awake()
    {
        _photonView = transform.parent.GetComponent<PhotonView>();
    }

    void OnTriggerEnter (Collider other)
    {
        if (other.gameObject.name == "JohnLemon(Clone)")
        {
            if (other.GetComponent<PhotonView>().IsMine)
            {
                m_IsPlayerInRange = true;
                player = other.transform;
            }
        }
    }

    void OnTriggerExit (Collider other)
    {
        if (other.gameObject.name == "JohnLemon(Clone)")
        {
            if (other.GetComponent<PhotonView>().IsMine)
                m_IsPlayerInRange = false;
        }
    }

    void Update ()
    {
        if (m_IsPlayerInRange)
        {
            Vector3 direction = player.position - transform.position + Vector3.up;
            Ray ray = new Ray(transform.position, direction);
            RaycastHit raycastHit;
            
            if (Physics.Raycast (ray, out raycastHit))
            {
                if (raycastHit.collider.transform == player)
                {
                    _photonView.RPC("CallCaughtPlayer",RpcTarget.All);
                }
            }
        }
    }

    // public void CaughtPlayer()
    // {
    //     gameEnding.CaughtPlayer ();
    // }
}
