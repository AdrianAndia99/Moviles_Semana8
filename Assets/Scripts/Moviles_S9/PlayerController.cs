using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 10f;
    [SerializeField] private Rigidbody2D _rigidbody2D;
    [SerializeField] private GameEvent gameEvent;
    private float moveX;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        moveX = Input.acceleration.x * moveSpeed;

        Vector2 velocity = _rigidbody2D.linearVelocity;
        velocity.x = moveX;
        _rigidbody2D.linearVelocity = velocity;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Finish"))
        {
            gameEvent.Raise();
            Time.timeScale = 0f;
        }
    }
}