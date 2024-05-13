using UnityEngine;
using System.Collections;
using TMPro;
using Normal.Realtime;
using System;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class AccidentalPicassoAppController : MonoBehaviour
{
    public static AccidentalPicassoAppController Instance;

    private TMP_InputField _roomName;
    private TMP_Text _message;

    public bool test;

    public GamePlayerController gamePlayerController;
    public GameController gameController;
    public string RoomName;
    public Realtime Realtime;
    public bool isOwner = false;

    public delegate void ResetHandler();
    public event ResetHandler OnReset;

    private void Awake()
    {
        if(Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void Update()
    {
        if(test)
        {
            RoomName = _roomName.text;
            LoadGame();
            test = false;
        }
    }

    public void CreateRoom()
    {
        if(!_roomName || string.IsNullOrEmpty(_roomName.text))
        {
            _message.text = "Please enter a room name before clicking make!";
            return;
        }
        RoomName = _roomName.text;
        _message.text = "Entering " + RoomName + "...";
        isOwner = true;
        LoadGame();
    }

    public void Reset()
    {
        AccidentalPicassoAppController.Instance.OnReset();
    }

    public void JoinRoom()
    {
        if (!_roomName || string.IsNullOrEmpty(_roomName.text))
        {
            _message.text = "Please enter a room name before clicking join!";
            return;
        }
        RoomName = _roomName.text;
        _message.text = "Entering " + RoomName + "...";
        LoadGame();
    }

    private void ConnectToRoom(string roomName)
    {
        Realtime.Connect(roomName);
        Realtime.didConnectToRoom += OnConnectToRoom;
        _message.text = "Connecting to "+RoomName+"...";
    }

    private void LoadGame()
    {
        SceneManager.LoadScene("GameScene");
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if(scene.name == "GameScene")
        {
            InitializeGameScene();
        } else
        {
            InitializeStartScene();
        }
    }

    private void InitializeGameScene()
    {
        Realtime = FindObjectOfType<Realtime>();
        gameController = FindObjectOfType<GameController>();
        gamePlayerController = FindObjectOfType<GamePlayerController>();
        _message = GameObject.Find("Message").GetComponent<TMP_Text>();
        ConnectToRoom(RoomName);
    }

    private void InitializeStartScene()
    {
        _roomName = FindObjectOfType<TMP_InputField>();
        _message = GameObject.Find("Message").GetComponent<TMP_Text>();
    }

    private void OnConnectToRoom(Realtime realtime)
    {
        _message.text = "Connected";
    }
}
