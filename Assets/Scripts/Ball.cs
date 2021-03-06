using UnityEngine;

public class Ball : MonoBehaviour
{
    
    // config params   
    
    [SerializeField] Paddle paddle1;
    [SerializeField] float launchVelocityX = 2f;
    [SerializeField] float launchVelocityY = 10f;
    [SerializeField] AudioClip[] ballSounds;
    [SerializeField] float randomFactor = 0.2f;
    [SerializeField] float correctionFactor = 0.8f;
    [SerializeField] float maxVelocity = 10f;
    [SerializeField] float minVelocity = 5f;

    // state
    Vector2 paddleToBallVector;
    bool hasStarted = false;

    // Cached component reference
    AudioSource myAudioSource;
    Rigidbody2D myRigidBody2d;



    // Start is called before the first frame update
    void Start()
    {
        paddleToBallVector = transform.position - paddle1.transform.position;
        myAudioSource = GetComponent<AudioSource>();
        myRigidBody2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!hasStarted)
        {
            LockBallToPaddle();
            LaunchOnClick();
        }
    }

    private void LaunchOnClick()
    {
        if(Input.GetMouseButtonDown(0))
        {
            hasStarted = true;
            GetComponent<Rigidbody2D>().velocity = new Vector2(launchVelocityX, launchVelocityY);            
        }
    }

    private void LockBallToPaddle()
    {
        Vector2 paddlePos = new Vector2(paddle1.transform.position.x, paddle1.transform.position.y);
        transform.position = paddlePos + paddleToBallVector;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Vector2 velocityTweak = new Vector2
            (Random.Range(0f, randomFactor),
            Random.Range(0f, randomFactor));
        if (hasStarted)
        {
            AudioClip clip = ballSounds[UnityEngine.Random.Range(0, ballSounds.Length)];
            myAudioSource.PlayOneShot(clip);
            myRigidBody2d.velocity += velocityTweak;
            speedCorrection();
        }
    }

    private void speedCorrection()
    {
        Vector2 velocityCorrectionFactor = new Vector2(correctionFactor, correctionFactor);
        if(System.Math.Abs(myRigidBody2d.velocity.magnitude) > maxVelocity)
        {
            myRigidBody2d.velocity *= velocityCorrectionFactor;
        }
        if (System.Math.Abs(myRigidBody2d.velocity.magnitude) < minVelocity)
        {
            myRigidBody2d.velocity /= velocityCorrectionFactor;
        }
        Debug.Log(myRigidBody2d.velocity);
    }


}
