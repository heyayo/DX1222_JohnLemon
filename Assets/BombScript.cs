using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class BombScript : MonoBehaviour
{
    private float time;

    public bool isMine;

    // Start is called before the first frame update
    void Start()
    {
        time = 2f;
    }

    private void Update()
    {
        if (!isMine) return;

        time -= Time.deltaTime;
        if (time <= 0.0f)
        {
            string data = "" + transform.position;

            RaiseEventOptions options = new RaiseEventOptions{Receivers = ReceiverGroup.All};
            PhotonNetwork.RaiseEvent((byte)RaiseEvents.EVENT_INDEX.BOMB_EXPLOSION,data,options,SendOptions.SendReliable);
        }
    }

    private void OnEnable()
    {
        RaiseEvents.explodeEvent += Explode;
    }
    private void OnDisable()
    { RaiseEvents.explodeEvent -= Explode; }

    void Explode(string data)
    {
        if (data == "" + transform.position)
        {
            var hits = Physics.OverlapSphere(transform.position, 3);
            foreach (var col in hits)
            {
                if (col.CompareTag("Player"))
                {
                    Rigidbody rb = col.GetComponent<Rigidbody>();
                    rb.AddExplosionForce(10,transform.position,3,0,ForceMode.Impulse);
                }
            }
            Destroy(gameObject);
        }
    }
}
