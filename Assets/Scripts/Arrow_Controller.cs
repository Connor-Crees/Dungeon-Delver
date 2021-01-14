using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow_Controller : MonoBehaviour
{
    public Vector2 direction;
    public float speed;
    public Rigidbody2D ridigbody_2d;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ArrowDeath());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        ridigbody_2d.MovePosition(ridigbody_2d.position + direction * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag.Equals("Enemy") || other.gameObject.tag.Equals("Wall"))
        {
            Destroy(gameObject);
        }
    }

    IEnumerator ArrowDeath()
    {
        yield return new WaitForSeconds(3.0f);
        Destroy(gameObject);
    }
}
