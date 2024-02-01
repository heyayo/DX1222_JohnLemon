using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class GhostRPC : MonoBehaviour
{
    public GameEnding gameEnding;

    [PunRPC]
    public void CallCaughtPlayer()
    {
        if (!gameEnding)
            gameEnding = GameObject.Find("GameEnding").GetComponentInChildren<GameEnding>();
        gameEnding.CaughtPlayer();
    }
}
