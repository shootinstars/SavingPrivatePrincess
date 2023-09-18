using UnityEngine;
using System.Collections;

public class ObjectBehaviour : MonoBehaviour
{
    public float speed;
    private Rigidbody2D rb;

    private void Awake()
    {
        rb = this.GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(-speed, 0);
        Physics2D.IgnoreLayerCollision(6, 7);
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
    

}

public class RockBehaviour : MonoBehaviour
{
    public float speed;
    private Rigidbody2D rb;

    private void Awake()
    {
        rb = this.GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(-speed, 0);
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    void Start()
    {
        Physics2D.IgnoreLayerCollision(6,7);
    }
}