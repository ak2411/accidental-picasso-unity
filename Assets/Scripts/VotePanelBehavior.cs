using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Normal.Realtime;

public class VotePanelBehavior : MonoBehaviour
{
    public GameObject back;

    public GameObject platformRef;

    //public bool voted;

    private void Start()
    {
        //AccidentalPicassoAppController.Instance.OnReset += ResetVote;
    }

    //public void ResetVote()
    //{
    //    voted = false;
    //}

    public void Vote(int score)
    {
        platformRef.GetComponent<RealtimeView>().RequestOwnership();
        platformRef.GetComponent<PlatformSync>().UpdateVote(score);
        Destroy(gameObject);
    }
}
