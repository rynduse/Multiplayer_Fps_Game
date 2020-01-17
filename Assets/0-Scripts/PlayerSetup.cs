using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

[RequireComponent(typeof(Player))]
public class PlayerSetup : NetworkBehaviour
{
   [SerializeField]
    Behaviour[] componentsToDisable;
    Camera sceneCamera;

    [SerializeField]
    string remoteLayerName = "RemotePlayer";

    void Start()
    {
        if(!isLocalPlayer)
        {
           DisableComponents();
           AssignRemoteLayer();    
        }
        else
        {
            sceneCamera = Camera.main;
            if(sceneCamera != null)
            {
                Camera.main.gameObject.SetActive(false);
            }           
        }

        GetComponent<Player>().Setup();
    }
    public override void OnStartClient()
    {
        base.OnStartClient();

        string _netID = GetComponent<NetworkIdentity>().netId.ToString();
        Player _player = GetComponent<Player>();

        GamaManager.RegisterPlayer(_netID , _player);
    }
    void AssignRemoteLayer()
    {
        gameObject.layer = LayerMask.NameToLayer(remoteLayerName);
    }

    void DisableComponents()
    {
        for(int i =0 ; i<componentsToDisable.Length; i++)
        {
           componentsToDisable[i].enabled = false;
        }
    }

    void OnDisable(){
        if(sceneCamera != null)
        {
          Camera.main.gameObject.SetActive(true);
        }
        GamaManager.UnRegisterPlayer(transform.name);
    }

}
