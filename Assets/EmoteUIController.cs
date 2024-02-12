using UnityEngine;
using UnityEngine.UI;

public class EmoteUIController : MonoBehaviour
{
    private AudioSource _source;

    [SerializeField] private Image emotePicture;

    [Header("Properties")]
    [SerializeField] private Sprite setPicture;
    [SerializeField] private AudioClip clip;

    private void Awake()
    {
        Canvas canvas = GetComponent<Canvas>();
        canvas.worldCamera = Camera.main;

        emotePicture.sprite = setPicture;
        _source = GetComponent<AudioSource>();
        _source.clip = clip;
        _source.Play();
    }

    private void Start()
    {
        Destroy(gameObject, 2);
    }

    private void Update()
    {
        Vector3 pos = transform.position;
        pos.y += Time.deltaTime;
        transform.position = pos;
    }
}
