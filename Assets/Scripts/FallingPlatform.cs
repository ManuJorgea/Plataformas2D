    using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.VFX;

public class FallingPlatform : MonoBehaviour
{
    [SerializeField] private float tiempoEspera;

    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        // el rigidbody tiene freezeadas la posicion X Y y la rotacion Z
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        // se verifica que el objeto con el que colisiona es el player
        if(other.gameObject.CompareTag("Player"))
        {
            StartCoroutine(Caida(other));
        }
    }

    private IEnumerator Caida(Collision2D other)
    {
        // se espera durante tiempoEspera antes de que la plataforma caiga
        yield return new WaitForSeconds(tiempoEspera);

        // desactiva la colision entre el player y la plataforma una vez esta cae
        Physics2D.IgnoreCollision(transform.GetComponent<Collider2D>(), other.transform.GetComponent<Collider2D>());

        // se desactivan las restricciones de la plataforma, por lo que la plataforma cae
        rb.constraints = RigidbodyConstraints2D.None;

        rb.AddForce(new Vector2(0.1f, 0)); 
    }
}
