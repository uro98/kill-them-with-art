using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerShooting : NetworkBehaviour {

    [SerializeField] float shotCooldown = 0.3f;
    [SerializeField] Transform firePosition;
    //[SerializeField] ShotEffectsManager shotEffects;

    float ellapsedTime;
    bool canShoot;
    float shootDistance = 50f;

    void Start()
    {
        //shotEffects.Initialize();

        if(isLocalPlayer)
        {
            canShoot = true;
        }
    }

    void Update()
    {
        if(!canShoot)
        {
            return;
        }

        ellapsedTime += Time.deltaTime;

        if(Input.GetButtonDown("fire1") && ellapsedTime > shotCooldown)
        {
            ellapsedTime = 0f;
            CmdFireShot(firePosition.position, firePosition.forward);
        }
    }

    [Command] //client to server
    void CmdFireShot(Vector3 origin, Vector3 direction)
    {
        RaycastHit hit;

        Ray ray = new Ray(origin, direction);
        Debug.DrawRay(origin, direction * 3f, Color.red, 1f);

        bool hitSomething = Physics.Raycast(ray, out hit, shootDistance);

        if(hitSomething)
        {

        }

        RpcProcessShotEffects(hitSomething, hit.point);
    }

    [ClientRpc] //server to client
    void RpcProcessShotEffects(bool hitSomething, Vector3 hitLocation)
    {
        //shotEffects.PlayShotEffects();

        if(hitSomething)
        {
            //shotEffects.PlayImpactEffects(hitLocation);
        }
    }

}
