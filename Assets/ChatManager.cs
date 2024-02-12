using System.Collections.Generic;
using Photon.Pun;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChatManager : MonoBehaviour
{
    struct ChatMessage
    {
        public string message;
        public string sender;

        public ChatMessage(string m, string s)
        {
            message = m;
            sender = s;
        }
    }

    [SerializeField] private Button sendButton;
    [SerializeField] private TMP_InputField textInput;
    [SerializeField] private TMP_Text textBox;

    private List<ChatMessage> _messages;
    private PhotonView _view;

    private void Awake()
    {
        _messages = new List<ChatMessage>();
        _view = GetComponent<PhotonView>();
    }

    private void Start()
    {
        textBox.text = "";

        textBox.gameObject.SetActive(false);
        textInput.gameObject.SetActive(false);
        sendButton.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            textInput.gameObject.SetActive(!textInput.gameObject.activeInHierarchy);
            sendButton.gameObject.SetActive(!sendButton.gameObject.activeInHierarchy);
            textBox.gameObject.SetActive(!textBox.gameObject.activeInHierarchy);
        }
    }

    public void SendMessageButtonCallback()
    {
        if (textInput.text == "") return;
        _view.RPC("SyncMessage", RpcTarget.AllViaServer, textInput.text, PhotonNetwork.LocalPlayer.NickName);
        textInput.text = "";
    }

    [PunRPC]
    public void SyncMessage(string message, string sender)
    {
        if (_messages.Count >= 10)
        {
            _messages.RemoveAt(0);
        }

        _messages.Add(new ChatMessage(message,sender));

        UpdateChatLog();
    }

    private void UpdateChatLog()
    {
        textBox.text = "";
        for (int i = 0; i < _messages.Count; ++i)
        {
            textBox.text += _messages[i].sender + ": " + _messages[i].message + '\n';
        }
    }
}
