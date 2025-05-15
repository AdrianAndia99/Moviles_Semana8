using UnityEngine;

public class Platform : MonoBehaviour
{
    [SerializeField] private float jumpForce = 10f;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.relativeVelocity.y <= 0f)
        {
            Rigidbody2D rigidbody_ = collision.gameObject.GetComponent<Rigidbody2D>();
            if(rigidbody_ != null)
            {
                Vector2 velocity = rigidbody_.linearVelocity;
                velocity.y = jumpForce;
                rigidbody_.linearVelocity = velocity;
            }
        }
    }
}
