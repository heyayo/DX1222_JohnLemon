using Photon.Pun;
using UnityEngine;

public class DiamondController : MonoBehaviour
{
    [SerializeField] private MeshRenderer _renderer;
    [SerializeField] private PhotonView _pView;

    [SerializeField] private float rotateSpeed = 1;

    private void Awake()
    {
        _renderer.material.color = JLGame.GetColor(_pView.CreatorActorNr);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, Time.deltaTime * rotateSpeed, 0);
    }
}
