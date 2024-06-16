using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Normal.Realtime;

public class VotePanelBehavior : MonoBehaviour
{
    public GameObject back;

    public GameObject platformRef;

    public void Vote(int score)
    {
        platformRef.GetComponent<RealtimeView>().RequestOwnership();
        platformRef.GetComponent<PlatformSync>().UpdateVote(score);
        Destroy(gameObject);
    }
}
