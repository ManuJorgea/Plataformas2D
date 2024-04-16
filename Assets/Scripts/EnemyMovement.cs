using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private float velocidad;

    [SerializeField] private Transform groundController;

    [SerializeField] private float distance;

    [SerializeField] private bool movingToRight;

    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        RaycastHit2D groundInfo = Physics2D.Raycast(groundController.position, Vector2.down, distance);

        rb.velocity = new Vector2(velocidad, rb.velocity.y);

        // si el rayo no detecta suelo retorna false y el enemigo da la vuelta
        if(groundInfo == false)
        {
            Turn();
        }
    }

    public void Turn()
    {
        movingToRight = !movingToRight;

        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y + 180, 0);

        velocidad *= -1;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawLine(groundController.transform.position, groundController.transform.position + Vector3.down * distance);
    }
}
