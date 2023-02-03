using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{

    //declare gameobject & rigidbody 2d
    public GameObject Player;
    public Rigidbody2D rbPlayer;
    public GameObject Ball;
    public Rigidbody2D rbBall;

    //declare input axis from keyboard
    public float InputHorizontal;
    public float InputVertical;

    //declare force strength
    public float StrengthHorizontal = 10f;
    public float StrengthVertical = 0.1f;

    //declare force kick
    public float KickHorizontal = 8f;
    public float KickVertical = 2f;

    //declare var to check if user is standing
    public bool isStanding;

    

    // Start is called before the first frame update
    void Start()
    {
        //get gameobject & rigidbody of that
        Player = GameObject.Find("Player");
        rbPlayer= Player.GetComponent<Rigidbody2D>();

        Ball = GameObject.Find("Ball");
        rbBall= Ball.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Code logic physical
    void FixedUpdate()
    {
        //get input key
        InputHorizontal = Input.GetAxis("Horizontal");
        InputVertical = Input.GetAxisRaw("Vertical");

        //set vector for force Horizontal & vertical
        Vector2 ForceHorizontal = new Vector2(StrengthHorizontal * InputHorizontal, 0);
        Vector2 ForceVertical = new Vector2(0, StrengthVertical * InputVertical);

        //check if user press horizontal key
        if (InputHorizontal != 0)
        {
            //add force horizontal for player
            rbPlayer.AddForce(ForceHorizontal, ForceMode2D.Force);
        }

        //check if user press vertical key
        if (InputVertical != 0 && isStanding)
        {
            //add force vertical for player
            rbPlayer.AddForce(ForceVertical, ForceMode2D.Impulse);
        }

        
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        //check if player is standing in ground
        if(collision.gameObject.name == "Ground")
        {
            isStanding = true;
        }
        
        //prevent player falling
        rbPlayer.freezeRotation = true;

        if (collision.gameObject.name == "Ball")
        {
            Vector2 KickBallForce = new Vector2(KickHorizontal * InputHorizontal, KickVertical);
            rbBall.AddForce(KickBallForce, ForceMode2D.Impulse);
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        //check if player is not standing
        if(collision.gameObject.name == "Ground")
        {
            isStanding = false;
        }
    }


}
