using UnityEngine;
using System.Collections;

public class AccidentalPicassoAppController : MonoBehaviour
{
    public static AccidentalPicassoAppController Instance;

    public GamePlayerController gamePlayerController;
    public GameController gameController;

    private void Awake()
    {
        if(Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void CreateRoom()
    {

    }

    public void JoinRoom()
    {

    }

}
