﻿using Photon.Pun;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float turnSpeed = 20f;

    Animator m_Animator;
    Rigidbody m_Rigidbody;
    AudioSource m_AudioSource;
    Vector3 m_Movement;
    Quaternion m_Rotation = Quaternion.identity;
    private PhotonView _photonView;

    [SerializeField] private Transform marker;

    [SerializeField] private GameObject bomb;
    [SerializeField] private GameObject emote;

    void Awake ()
    {
        m_Animator = GetComponent<Animator> ();
        m_Rigidbody = GetComponent<Rigidbody> ();
        m_AudioSource = GetComponent<AudioSource> ();
        _photonView = GetComponent<PhotonView>();
        if (_photonView.IsMine)
            GetComponent<AudioListener>().enabled = true;
    }

    private void Update()
    {
        if (_photonView.IsMine)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                _photonView.RPC("PlantBomb",RpcTarget.AllViaServer,m_Rigidbody.position);
            }

            if (Input.GetKeyDown(KeyCode.T))
            {
                _photonView.RPC("SpawnEmote", RpcTarget.AllViaServer);
            }
        }
    }

    void FixedUpdate ()
    {
        if (!_photonView.IsMine) return;
        float horizontal = Input.GetAxis ("Horizontal");
        float vertical = Input.GetAxis ("Vertical");
        
        m_Movement.Set(horizontal, 0f, vertical);
        m_Movement.Normalize ();

        bool hasHorizontalInput = !Mathf.Approximately (horizontal, 0f);
        bool hasVerticalInput = !Mathf.Approximately (vertical, 0f);
        bool isWalking = hasHorizontalInput || hasVerticalInput;
        m_Animator.SetBool ("IsWalking", isWalking);
        
        if (isWalking)
        {
            if (!m_AudioSource.isPlaying)
            {
                m_AudioSource.Play();
            }
        }
        else
        {
            m_AudioSource.Stop ();
        }

        Vector3 desiredForward = Vector3.RotateTowards (transform.forward, m_Movement, turnSpeed * Time.deltaTime, 0f);
        m_Rotation = Quaternion.LookRotation (desiredForward);
    }

    void OnAnimatorMove ()
    {
        if (_photonView.IsMine)
        {
            m_Rigidbody.MovePosition (m_Rigidbody.position + m_Movement * m_Animator.deltaPosition.magnitude);
            m_Rigidbody.MoveRotation (m_Rotation);
        }
    }

    [PunRPC]
    public void PlantBomb(Vector3 position)
    {
        GameObject bombInstance = Instantiate(bomb, position, Quaternion.identity);
        bombInstance.GetComponent<BombScript>().isMine = _photonView.IsMine;
    }

    [PunRPC]
    public void SpawnEmote()
    {
        GameObject emoteInstance = Instantiate(emote, marker.position, Quaternion.identity);
    }
}