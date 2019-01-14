using UnityEngine;

public class Ball : MonoBehaviour {

    // config params
    [SerializeField] Paddle paddle1;
    [SerializeField] float xPush = 2.5f;
    [SerializeField] float yPush = 15f;
    [SerializeField] AudioClip[] ballSounds;
    //[SerializeField] float randomFactor = 0.2f;

    // state
    Vector2 paddleToBallVector;
    bool hasStarted = false;
    Collider2D paddleCollider;
    float paddleLength;

    // Cached component references
    AudioSource myAudioSource;
    Rigidbody2D myRigidBody2D;

	// Use this for initialization
	void Start ()
    {
        paddleToBallVector = transform.position - paddle1.transform.position;
        paddleCollider = paddle1.GetComponent<Collider2D>();
        paddleLength = paddleCollider.bounds.size.x;
        myAudioSource = GetComponent<AudioSource>();
        myRigidBody2D = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (!hasStarted)
        {
            LockBallToPaddle();
            LaunchOnMouseClick();
        }
    }

    private void LaunchOnMouseClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            hasStarted = true;
            myRigidBody2D.velocity = new Vector2(xPush, yPush);
        }
    }

    private void LockBallToPaddle()
    {
        Vector2 paddlePos = new Vector2(paddle1.transform.position.x, paddle1.transform.position.y);
        transform.position = paddlePos + paddleToBallVector;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Vector2 velocityTweak = new Vector2
        //    (randomFactor, randomFactor);

        Vector2 ballFromPaddleVelocity = new Vector2(0f, 10f);
        Vector2 ballFromPaddleVelocityRight1 = new Vector2(4f, 10f);
        Vector2 ballFromPaddleVelocityLeft1 = new Vector2(-4f, 10f);

        if (hasStarted)
        {
            AudioClip clip = ballSounds[UnityEngine.Random.Range(0, ballSounds.Length)];
            myAudioSource.PlayOneShot(clip);
            if (collision.gameObject.name == "PaddleWood")
            {
                CollideWithPaddle(ballFromPaddleVelocity, ballFromPaddleVelocityRight1, ballFromPaddleVelocityLeft1);
            }
        }
    }

    private void CollideWithPaddle(Vector2 ballFromPaddleVelocity, Vector2 ballFromPaddleVelocityRight1, Vector2 ballFromPaddleVelocityLeft1)
    {
        float ballToPaddleoffset = myRigidBody2D.transform.position.x - paddle1.transform.position.x;

        if (ballToPaddleoffset <= 0.2f && ballToPaddleoffset >= -0.2f)
        {
            myRigidBody2D.velocity = ballFromPaddleVelocity;
        }
        else if (ballToPaddleoffset < 0.2f)
        {
            myRigidBody2D.velocity = ballFromPaddleVelocityLeft1;
        }
        else
        {
            myRigidBody2D.velocity = ballFromPaddleVelocityRight1;
        }
    }
}