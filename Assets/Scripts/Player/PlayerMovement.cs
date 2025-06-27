using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float playerSpeed = 0.55f;

    private Rigidbody2D rb;    


    public Vector2 PlayerDirection {get; private set; }


    private void Awake()
    {        
        rb = GetComponent<Rigidbody2D>();

        PlayerEventManager.OnPlayerMovement += UpdatePlayerDirection;
    }      

    private void UpdatePlayerDirection(Vector2 inputDirection)
    {
        PlayerDirection = inputDirection;
    }

    private void Movement()
    {
        if (PlayerDirection == Vector2.zero) return;

        rb.velocity = PlayerDirection * playerSpeed;
    }

    private void FixedUpdate()
    {        
        Movement();
    }

    public void StopMovement()
    {
        rb.velocity = Vector2.zero;
        PlayerDirection = Vector2.zero;
    }
}
