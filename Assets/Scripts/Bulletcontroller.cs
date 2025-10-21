using UnityEditor.SceneManagement;
using UnityEngine;

public class Bulletcontroller : MonoBehaviour
{
    bool user = true;
    float speed = 5f;
    float lifeTime = 60f;

    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Destroy(gameObject, lifeTime);
        if(!user)
        {
            speed *= -1;
        }
    }
    private void FixedUpdate()
    {
        Vector3 newPosition = transform.position + transform.right * speed * Time.fixedDeltaTime;
        rb.MovePosition(newPosition);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Enemy"))
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }

}