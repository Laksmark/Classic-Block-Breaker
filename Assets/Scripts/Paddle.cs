using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour {

    // configuration paramaters
    [SerializeField] float minX = 1.572803f;
    [SerializeField] float maxX = 14.427197f;
    [SerializeField] float screenWidthInUnits = 16f;
    [SerializeField] float paddleSpeed = 50f;

    // cached references
    GameSession theGameSession;
    Ball theBall;
    Rigidbody2D myRigidBody2D;
    Vector2 paddleVelocity;

    // Use this for initialization
    void Start () {
        theGameSession = FindObjectOfType<GameSession>();
        theBall = FindObjectOfType<Ball>();
        myRigidBody2D = GetComponent<Rigidbody2D>();
    }
	
	// Update is called once per frame
	void Update () {
        //Vector2 paddlePos = new Vector2(transform.position.x, transform.position.y);
        //transform.position = paddlePos;
        //paddlePos.x = Mathf.Clamp(GetXPos(), minX, maxX);
        float translation = Input.GetAxis("Mouse X") * paddleSpeed;
        //paddleVelocity = new Vector2(translation, 0);
        paddleVelocity.x = translation;
        myRigidBody2D.velocity = paddleVelocity;

        /*if (paddlePos.x < minX)
        {
            paddlePos.x = minX;
        }
        else if (paddlePos.x > maxX)
        {
            paddlePos.x = maxX;
        }*/
        
	}

    private float GetXPos()
    {
        if(theGameSession.IsAutoPlayEnabled())
        {
            return theBall.transform.position.x;
        }
        else
        {
            return Input.mousePosition.x / Screen.width * screenWidthInUnits;
        }
    }

}
