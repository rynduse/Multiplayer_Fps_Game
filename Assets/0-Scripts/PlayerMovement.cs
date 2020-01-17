using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float _gravity = 9.81f;

    private CharacterController _controller;
    [SerializeField]
    private AudioSource _weaponAudio;
    [SerializeField]
    private float _speed = 3.5f;
    //[SerializeField]
    //private GameObject _weapon;
    void Start()
    {
        _controller = GetComponent<CharacterController>();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

    }

    void Update()
    {
        /** if(Input.GetMouseButton(0)){

            if(_weaponAudio.isPlaying == false){

                 _weaponAudio.Play();
            }
          

            //Ray rayOrigin = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2f , Screen.height / 2f,0));
            Ray rayOrigin = Camera.main.ViewportPointToRay(new Vector3(0.5f,0.5f,0));
            RaycastHit hitInfo;

            if(Physics.Raycast(rayOrigin , out hitInfo)){

                Debug.Log("Hit: " + hitInfo.transform.name);
            }

        }
        else{

            _weaponAudio.Stop();
          
        }
**/
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        CalculateMovement();


    }

    void CalculateMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 direction = new Vector3(horizontalInput, 0, verticalInput);
        Vector3 velocity = direction * _speed;
        velocity.y -= _gravity;

        velocity = transform.transform.TransformDirection(velocity);
        _controller.Move(velocity * Time.deltaTime);

    }

    //public void EnableWeapons()
    //{

    //    _weapon.SetActive(true);
    //}
}
