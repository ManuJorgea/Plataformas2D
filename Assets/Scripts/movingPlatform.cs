using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class movingPlatform : MonoBehaviour
{
    [SerializeField] private Transform[] puntosMov;

    [SerializeField] private float velMov;

    private int sigPlataforma = 1;

    private bool ordenPlataformas = true;

    private void Update()
    {
        if (ordenPlataformas && sigPlataforma + 1 >= puntosMov.Length)
        {
            ordenPlataformas = false;
        }

        if(!ordenPlataformas && sigPlataforma <= 0)
        {
            ordenPlataformas = true;
        }

        if(Vector2.Distance(transform.position, puntosMov[sigPlataforma].position) < 0.1f)
        {
            if(ordenPlataformas)
            {
                sigPlataforma += 1;
            }
            else
            {
                sigPlataforma -= 1;
            }
        }

        transform.position = Vector2.MoveTowards(transform.position, puntosMov[sigPlataforma].position, velMov * Time.deltaTime);
    }


    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.transform.SetParent(this.transform);
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.transform.SetParent(null);
        }
    }
}
