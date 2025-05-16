using UnityEngine;

public class Platform : MonoBehaviour
{
    [SerializeField] private float jumpForce = 10f;

    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private float moveDistance = 3f;

    private Vector3 startPos;
    private bool movingRight = true;

    private void Start()
    {
        startPos = transform.position;
    }

    private void Update()
    {
        MovePlatform();
    }

    private void MovePlatform()
    {
        float delta = moveSpeed * Time.deltaTime;

        if (movingRight)
        {
            transform.Translate(Vector2.right * delta);
            if (transform.position.x >= startPos.x + moveDistance)
                movingRight = false;
        }
        else
        {
            transform.Translate(Vector2.left * delta);
            if (transform.position.x <= startPos.x - moveDistance)
                movingRight = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.relativeVelocity.y <= 0f)
        {
            Rigidbody2D rigidbody_ = collision.gameObject.GetComponent<Rigidbody2D>();
            if (rigidbody_ != null)
            {
                Vector2 velocity = rigidbody_.linearVelocity;
                velocity.y = jumpForce;
                rigidbody_.linearVelocity = velocity;
            }
        }
    }
}
