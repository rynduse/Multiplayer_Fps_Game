﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerShoot : NetworkBehaviour
{
    private const string PLAYER_TAG = "Player";
    public PlayerWeapon weapon;

    [SerializeField]
    private Camera cam;
    
    [SerializeField]

    private LayerMask mask;
        void Start()
    {
        if( cam == null)
        {
          Debug.LogError("PlayerShoot : No Camera referenced!");
        }
    }
    // Update is called once per frame
    void Update()
    {      
        if(Input.GetButtonDown("Fire1")){
            Shoot();
        }
    }
    [Client]
    void Shoot()
    {
      RaycastHit _hit;
      if(Physics.Raycast(cam.transform.position, cam.transform.forward , out _hit , weapon.range, mask))
      {
          if(_hit.collider.tag == PLAYER_TAG)
          {
              CmdPlayerShot(_hit.collider.name , weapon.damage);
          }
      //Debug.Log("we hit :" + _hit.collider.name);
      }
    }

    [Command] // call server
    void CmdPlayerShot(string _playerID , int _damage)
    {
        Debug.Log(_playerID + " has been shot");

        Player _player = GamaManager.GetPlayer(_playerID);
        _player.RpcTakeDamage(_damage);

    }

}

