using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {
    //A lot of these public variables are for testing purposes, should make them constants in the end?
    public float speed;
    public GameObject pickUps;
    public Text countText;
    public Text winText;
    public Joystick joystick;
    public float deathHeight;
    public float joyWeight;
    public float forwardSpeed;
    public float jumpPower;

    private Rigidbody rb;
    private float moveHorizontal;
    private int count;
    private int winCount;
    private bool jumpButtonPressed;
    private bool paused;
    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        count = 0;
        SetCountText();
        winText.text = "";
        winCount = pickUps.transform.childCount;
        jumpButtonPressed = false;
        paused = false;
    }

    private void Update()
    {
        //Jump code
        float distanceToFloor = GetComponent<Collider>().bounds.extents.y;
        bool onGround = Physics.Raycast(transform.position, Vector3.down, distanceToFloor);

#if UNITY_EDITOR || UNITY_STANDALONE || UNITY_WEBGL
        if (onGround && Input.GetKeyDown(KeyCode.Space))
        {
            rb.velocity += jumpPower * Vector3.up;
        }
#endif

#if (UNITY_ANDROID || UNIT_IOS) && !UNITY_EDITOR
        if (onGround && jumpButtonPressed)
            {
                rb.velocity += jumpPower * Vector3.up;
            jumpButtonPressed = false;
            }
#endif

        //Steering left/right code
#if UNITY_EDITOR || UNITY_STANDALONE || UNITY_WEBGL
        moveHorizontal = Input.GetAxis("Horizontal");
#endif

#if (UNITY_ANDROID || UNIT_IOS) && !UNITY_EDITOR
        moveHorizontal = joyWeight * joystick.Horizontal;
#endif

        rb.velocity = new Vector3(speed * moveHorizontal, rb.velocity.y, forwardSpeed);

        //Pause code
#if UNITY_EDITOR || UNITY_STANDALONE || UNITY_WEBGL
        if (Input.GetKeyDown(KeyCode.P))
        {
            paused = !paused;
        }
#endif


        //Death code
        if (transform.position.y < deathHeight)
        {
            foreach (Transform pickUp in pickUps.transform)
            {
                pickUp.gameObject.SetActive(true);
            }
            count = 0;
            SetCountText();
            transform.position = new Vector3(0.0f, 1.0f, 0.0f);
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Pick Up"))
        {
            other.gameObject.SetActive(false);
            count++;
            SetCountText();
        }
    }

    public void Jump()
    {
        jumpButtonPressed = true;
    }

    void SetCountText()
    {
        countText.text = "Count: " + count.ToString() + "/" + winCount;
        if (count >= winCount)
        {
            winText.text = "You win!";
        }
    }
    
}
