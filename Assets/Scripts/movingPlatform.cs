using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class movingPlatform : MonoBehaviour
{
    // se crea un array de GameObjects con los diferentes puntos a los que va a ir la plataforma
    [SerializeField] private Transform[] puntosMov;

    // velocidad a la que se mueve la plataforma
    [SerializeField] private float velMov;

    private int sigPlataforma = 1;

    private bool ordenPlataformas = true;

    private void Update()
    {
        // en caso de que ya no haya puntos de mov restantes, se invierte el sentido y se hace el recorrido a la inversa
        if (ordenPlataformas && sigPlataforma + 1 >= puntosMov.Length)
        {
            ordenPlataformas = false;
        }

        // se verifica que todavia hay puntos a los cuales moverse
        if (!ordenPlataformas && sigPlataforma <= 0)
        {
            ordenPlataformas = true;
        }

        // verifica si la plataforma ya llego al punto de mov
        if(Vector2.Distance(transform.position, puntosMov[sigPlataforma].position) < 0.1f)
        {
            // si el orden es ascendente se aumenta el indice sigPlataforma
            if(ordenPlataformas)
            {
                sigPlataforma += 1;
            }
            // si el orden es descendente se disminuye el indice sigPlataforma
            else
            {
                sigPlataforma -= 1;
            }
        }

        // param 1: posicion actual, param 2: nueva posicion, Param 3: velocidad de mov
        transform.position = Vector2.MoveTowards(transform.position, puntosMov[sigPlataforma].position, velMov * Time.deltaTime);
    }

    // jugador se queda sobre la plataforma
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.transform.SetParent(this.transform);
        }
    }

    // jugador sale de la plataforma
    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.transform.SetParent(null);
        }
    }
}
