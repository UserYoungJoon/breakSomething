using UnityEngine;
public class Shield : MonoBehaviour
{
    public Rigidbody2D playerRigidbody;
    public Transform obstacles;
    public UI_ShieldBtn shieldBtn;

    private const float PUSH_FORCE = 5;
    private Vector2 UpForce;
    private Vector2 downForce;

    private void Awake()
    {
        UpForce = Vector2.up * PUSH_FORCE;
        downForce = Vector2.down * PUSH_FORCE * 3;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.TryGetComponent<Obstacle>(out var _obstacle))
        {
            foreach (Transform obstacle in obstacles)
            {
                obstacle.GetComponent<Rigidbody2D>().velocity = UpForce;
            }
            playerRigidbody.AddForce(downForce, ForceMode2D.Force);
            shieldBtn.OnPointerUp(null);
        }
    }
}