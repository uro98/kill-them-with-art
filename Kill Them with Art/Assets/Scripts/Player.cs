using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Events;

[System.Serializable]
public class ToggleEnabled : UnityEvent<bool> { }

public class Player : NetworkBehaviour {

    //use dynamic bool in editor
    [SerializeField] ToggleEnabled onToggleShared; //everyone
    [SerializeField] ToggleEnabled onToggleLocal; //the local player
    [SerializeField] ToggleEnabled onToggleRemote; //the remote players

    void Start()
    {
        EnablePlayer();
    }

    void EnablePlayer()
    {
        onToggleShared.Invoke(true);

        if(isLocalPlayer)
        {
            onToggleLocal.Invoke(true);
        } else
        {
            onToggleRemote.Invoke(true);
        }
    }

    void DisablePlayer()
    {
        onToggleShared.Invoke(false);

        if (isLocalPlayer)
        {
            onToggleLocal.Invoke(false);
        }
        else
        {
            onToggleRemote.Invoke(false);
        }
    }
}
