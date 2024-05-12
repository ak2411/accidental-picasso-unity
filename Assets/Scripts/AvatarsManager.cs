using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Normal.Realtime;

public class AvatarsManager : MonoBehaviour
{
    private RealtimeAvatarManager _manager;
    private List<RealtimeAvatar> _avatars = new List<RealtimeAvatar>();
    [SerializeField]
    private Transform fakeAnchor;
    private void Awake()
    {
        _manager = GetComponent<RealtimeAvatarManager>();
        _manager.avatarCreated += OnAvatarCreated;
    }

    private void OnAvatarCreated(RealtimeAvatarManager avatarManager, RealtimeAvatar avatar, bool isLocalAvatar)
    {
        if (isLocalAvatar) return;
        //AlignAvatarToAnchor(avatar);
        _avatars.Add(avatar);
    }

    public void AlignAvatarToAnchor(RealtimeAvatar avatar)
    {
        
    }

    public void AlignCameraToAnchor()
    {
        Debug.Log("STILL BEING CALLED");
        var cameraRigTransform = FindObjectOfType<OVRCameraRig>().transform;
        cameraRigTransform.position = fakeAnchor.InverseTransformPoint(Vector3.zero);
        cameraRigTransform.eulerAngles = new Vector3(0, -fakeAnchor.eulerAngles.y, 0);
    }
}
