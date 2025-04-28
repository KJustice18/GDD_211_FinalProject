using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
    [SerializeField] private bool inAir;

    [SerializeField] Camera cam;
    [SerializeField] GameObject cameraController;
    
    private Animator animator;

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        animator = playerObject.GetComponent<Animator>();
        cam = Camera.main;
        currentScene = SceneManager.GetActiveScene();
        braking = false;

        inAir = true;
    }

    // Update is called once per frame
    void Update()
    {
        playerObject.transform.position = transform.position + new Vector3(0f,.46f,0f);

        playerObject.transform.rotation = Quaternion.Euler(0f, cameraController.transform.rotation.eulerAngles.y, 0f);

        //rotSpeed = Input.GetAxis("Horizontal") * turnSpeed;
        speedZ = -Input.GetAxis("Vertical") * movementSpeed;
        speedX = -Input.GetAxis("Horizontal") * movementSpeed;

        animator.SetFloat("Forward", Input.GetAxis("Vertical"));
        animator.SetFloat("Side", Input.GetAxis("Horizontal"));



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

        RaycastHit hit;

        if (Physics.Raycast(transform.position, Vector3.down, out hit, .6f, 3))
        {
            //Debug.Log(hit.collider.gameObject.name);
            if (hit.transform.CompareTag("Ground"))
            {
                inAir = false;
            }
            

            //Debug.Log(hit.collider.name);
        }
        else 
        {
            inAir = true;
        }

        

        

       // Debug.DrawLine(transform.position, transform.position - new Vector3(0,.6f,0), Color.green);
        //Debug.Log(inAir);

    }

    private void FixedUpdate()
    {
        
        rb.AddForce(cam.transform.forward * speedZ);
        rb.AddForce(cam.transform.right * speedX);
        //transform.Rotate(new Vector3(0f, rotSpeed, 0f));

        


        if (braking && !inAir) 
        {
            rb.AddForce(-brakeForce * rb.velocity);
        }


    }

    /*
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground") 
        {
            inAir = false;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            inAir = true;
        }
    }
    */
    /*
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        for(int i = 0; i < 6; i++)
        {
            Gizmos.DrawWireSphere(transform.position + new Vector3(0,-i * 0.1f), transform.localScale.x + 0.1f);
        }
    }
    */
}
