using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovemtn : MonoBehaviour
{
    [SerializeField]
    public float playerMoveSpeed = 5.0f;
    private float gravity = 9.81f;
    private CharacterController characterController;
    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        //raycast from the center of Main Camera
        if(Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2f, Screen.height / 2f, 0));
            if(Physics.Raycast(ray,Mathf.Infinity))
            {
                Debug.Log("ray got hit");
            }
        }
    }

    private void Movement()
    {
        Vector3 direction = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        Vector3 velocity = direction * playerMoveSpeed;
        velocity.y -= gravity;
        velocity = transform.transform.TransformDirection(velocity);
        characterController.Move(velocity*Time.deltaTime);
    }
}
