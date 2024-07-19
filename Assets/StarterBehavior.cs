using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarterBehavior : MonoBehaviour
{
    public void CreateRoom() {
        AccidentalPicassoAppController.Instance.CreateRoom();
    }

    public void JoinRoom()
    {
        AccidentalPicassoAppController.Instance.JoinRoom();
    }
    
}
