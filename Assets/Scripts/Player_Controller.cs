using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Controller : MonoBehaviour
{
    public float moveSpeed = 5.0f;
    public Rigidbody2D ridigbody_2d;
    public Animator animator;
    Vector2 movement;
    Vector3 mousePosition;
    float mouseAngle;
    public bool attack;
    public GameObject arrow;
    bool arrowReloading = false;
    bool splitArrowReloading = false;
    public int health = 5;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Speed", movement.sqrMagnitude);

        attack = Input.GetMouseButton(0);
        animator.SetBool("Attack", attack);

        mousePosition = Input.mousePosition;
        mousePosition.z = Camera.main.nearClipPlane;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        mouseAngle = AngleBetweenTwoPoints(mousePosition, transform.position);
        animator.SetFloat("Mouse_X", mousePosition.x - transform.position.x);
        animator.SetFloat("Mouse_Y", mousePosition.y - transform.position.y);

        if (attack == true && arrowReloading == false)
        {
            StartCoroutine(FireArrows());
        }

        if (Input.GetKeyDown(KeyCode.Alpha1) && splitArrowReloading == false)
        {
            StartCoroutine(SplitArrow());
        }
    }

    private void FixedUpdate()
    {
        ridigbody_2d.MovePosition(ridigbody_2d.position + movement * moveSpeed * Time.deltaTime);
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
    }

    IEnumerator FireArrows()
    {
        arrowReloading = true;
        arrow.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, -90 + mouseAngle));
        arrow.GetComponent<Arrow_Controller>().speed = 8.0f;
        arrow.GetComponent<Arrow_Controller>().direction = (new Vector2(mousePosition.x - transform.position.x, mousePosition.y - transform.position.y)).normalized;
        Instantiate(arrow, transform.position, arrow.transform.rotation);
        yield return new WaitForSeconds(0.25f);
        arrowReloading = false;
    }

    IEnumerator SplitArrow()
    {
        splitArrowReloading = true;
        arrow.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, -90 + mouseAngle));
        arrow.GetComponent<Arrow_Controller>().speed = 8.0f;
        arrow.GetComponent<Arrow_Controller>().direction = (new Vector2(mousePosition.x - transform.position.x, mousePosition.y - transform.position.y)).normalized;
        Instantiate(arrow, transform.position, arrow.transform.rotation);
        Vector2 direction = arrow.GetComponent<Arrow_Controller>().direction;

        float angle = 15;
        arrow.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, -90 + mouseAngle + angle));
        arrow.GetComponent<Arrow_Controller>().speed = 8.0f;
        arrow.GetComponent<Arrow_Controller>().direction = (new Vector2(direction.x * Mathf.Cos(angle * Mathf.Deg2Rad) - direction.y * Mathf.Sin(angle * Mathf.Deg2Rad),
            direction.x * Mathf.Sin(angle * Mathf.Deg2Rad) + direction.y * Mathf.Cos(angle * Mathf.Deg2Rad)).normalized);
        Instantiate(arrow, transform.position, arrow.transform.rotation);

        angle = -15;
        arrow.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, -90 + mouseAngle + angle));
        arrow.GetComponent<Arrow_Controller>().speed = 8.0f;
        arrow.GetComponent<Arrow_Controller>().direction = (new Vector2(direction.x * Mathf.Cos(angle * Mathf.Deg2Rad) - direction.y * Mathf.Sin(angle * Mathf.Deg2Rad),
            direction.x * Mathf.Sin(angle * Mathf.Deg2Rad) + direction.y * Mathf.Cos(angle * Mathf.Deg2Rad)).normalized);
        Instantiate(arrow, transform.position, arrow.transform.rotation);

        yield return new WaitForSeconds(2.0f);
        splitArrowReloading = false;
    }

    float AngleBetweenTwoPoints(Vector3 a, Vector3 b)
    {
        return Mathf.Atan2(a.y - b.y, a.x - b.x) * Mathf.Rad2Deg;
    }
}
