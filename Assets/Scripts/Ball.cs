using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Ball : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] Rigidbody rb;
    [SerializeField] float movementSpeed;
    [SerializeField] float brakeForce;
    [SerializeField] float turnSpeed;
    [SerializeField] GameObject playerObject;
    private float speedZ;
    private float speedX;
    private float rotSpeed;
    private Scene currentScene;
    private bool braking;

    [SerializeField] Camera cam;

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        cam = Camera.main;
        currentScene = SceneManager.GetActiveScene();
        braking = false;
    }

    // Update is called once per frame
    void Update()
    {
        playerObject.transform.position = transform.position + new Vector3(0f,1.5f,0f);  

        //rotSpeed = Input.GetAxis("Horizontal") * turnSpeed;
        speedZ = -Input.GetAxis("Vertical") * movementSpeed;
        speedX = -Input.GetAxis("Horizontal") * movementSpeed;

        //cam.transform.position = gameObject.transform.position + new Vector3(0f, 2.25f, -2.5f);

        if (Input.GetKeyDown(KeyCode.R)) 
        {
            SceneManager.LoadScene(currentScene.name);
        }

        if (Input.GetKey(KeyCode.Space)) 
        {
            braking = true;
        }

        if (Input.GetKeyUp(KeyCode.Space)) 
        {
            braking = false;
        }
    }

    private void FixedUpdate()
    {

        rb.AddForce(cam.transform.forward * speedZ);
        rb.AddForce(cam.transform.right * speedX);
        //transform.Rotate(new Vector3(0f, rotSpeed, 0f));

        if (braking) 
        {
            rb.AddForce(-brakeForce * rb.velocity);
        }


    }
}
