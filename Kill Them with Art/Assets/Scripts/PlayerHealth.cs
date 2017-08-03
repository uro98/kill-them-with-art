using UnityEngine;
using UnityEngine.Networking;

public class PlayerHealth : NetworkBehaviour {

    [SerializeField] int maxHealth = 3;

    Player player;
    int health;

    void Awake()
    {
        player = GetComponent<Player>();
    }

    [ServerCallback] //only run on the server, no warnings
    void OnEnable()
    {
        health = maxHealth;
    }

    [Server] //only run on the server
    public bool takeDamage()
    {
        bool died = false;

        //if already dead
        if(health <= 0)
        {
            return died;
        }

        health--;
        died = health <= 0;

        RpcTakeDamage(died);

        return died;
    }

    [ClientRpc]
    void RpcTakeDamage(bool died)
    {
        if(died)
        {
            player.Die();
        }
    }
}
