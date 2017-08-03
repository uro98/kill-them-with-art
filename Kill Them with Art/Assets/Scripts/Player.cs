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

    GameObject mainCamera;

    void Start()
    {
        mainCamera = Camera.main.gameObject;

        EnablePlayer();
    }

    void EnablePlayer()
    {
        onToggleShared.Invoke(true);

        if(isLocalPlayer)
        {
            mainCamera.SetActive(false);
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
            mainCamera.SetActive(true);
            onToggleLocal.Invoke(false);
        }
        else
        {
            onToggleRemote.Invoke(false);
        }
    }
}
