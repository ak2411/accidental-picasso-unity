using UnityEngine;
using System.Collections;
using TMPro;
using Normal.Realtime;
using System;
using UnityEngine.SceneManagement;

public class AccidentalPicassoAppController : MonoBehaviour
{
    public static AccidentalPicassoAppController Instance;

    private TMP_InputField _roomName;
    private TMP_Text _message;

    public bool test;

    public GamePlayerController gamePlayerController;
    public GameController gameController;
    public Vector3 originPosition;
    public Quaternion originRotation;
    public string RoomName;
    public Realtime Realtime;
    public Vector3 userPosition;
    public Quaternion userRotation;

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
        LoadGame();
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
        _message.text = "Connecting to "+RoomName+"...";
    }

    private void LoadGame()
    {
        var starterTransform = GameObject.Find("Starter").transform;
        originPosition = starterTransform.position;
        originRotation = starterTransform.rotation;
        var _cameraRig = FindObjectOfType<OVRCameraRig>().transform;
        userPosition = _cameraRig.position;
        userRotation = _cameraRig.rotation;
        Debug.Log("getting pos" + AccidentalPicassoAppController.Instance.userPosition + AccidentalPicassoAppController.Instance.userRotation);
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
        _message = GameObject.Find("Message").GetComponent<TMP_Text>();
        var _cameraRig = FindObjectOfType<OVRCameraRig>();
        Debug.Log("setting pos" + AccidentalPicassoAppController.Instance.userPosition + AccidentalPicassoAppController.Instance.userRotation);
        _cameraRig.transform.SetPositionAndRotation(AccidentalPicassoAppController.Instance.userPosition, AccidentalPicassoAppController.Instance.userRotation);
        ConnectToRoom(RoomName);
    }

    private void InitializeStartScene()
    {
        _roomName = FindObjectOfType<TMP_InputField>();
        _message = GameObject.Find("Message").GetComponent<TMP_Text>();
    }

}
