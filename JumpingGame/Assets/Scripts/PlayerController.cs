using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {
    public float speed;
    public GameObject pickUps;
    public Text countText;
    public Text winText;
    public Joystick joystick;
    public float deathHeight;
    public float joyWeight;
    public float forwardSpeed;

    private Rigidbody rb;
    private int count;
    private int winCount;
    private bool onGround;
    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        count = 0;
        SetCountText();
        winText.text = "";
        winCount = pickUps.transform.childCount;
        onGround = true;
    }

    private void Update()
    {

        rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, forwardSpeed);
        if(onGround && Input.GetAxis("Jump") >0)
        rb.velocity+=Input.GetAxis("Jump")*Vector3.up;
        
        if (transform.position.y < deathHeight)
        {
            transform.position = new Vector3(0.0f, 1.0f, 0.0f);
            rb.velocity = Vector3.zero;
        }
    }

    void FixedUpdate()
    {
        //This is hacky need device dependent controls to be separated
        float moveHorizontal = Input.GetAxis("Horizontal");
        moveHorizontal+=joyWeight*joystick.Horizontal;
        float moveVertical = Input.GetAxis("Vertical");
        moveVertical += joyWeight * joystick.Vertical;
        
        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        rb.AddForce(movement*speed);
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

    void SetCountText()
    {
        countText.text = "Count: " + count.ToString();
        if (count >= winCount)
        {
            winText.text = "You win!";
        }
    }
    
}
