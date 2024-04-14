using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CanvasController : MonoBehaviour
{
    public void StartMenu()
    {
        SceneManager.LoadScene("Menu_Start_UI");
    }

    public void PlayLevel01()
    {
        SceneManager.LoadScene("Level1");
    }

    public void PlayLevel02()
    {
        SceneManager.LoadScene("Level2");
    }

    public void QuitGame()
    {
        Application.Quit();
    }


    // Men� de pausa de nivel
    ///Panel oculto
    public GameObject menuPause;
    public bool juegoPausado = false;

    void Udpate(){

        //Activaci�n por medio del teclado
        if (Input.GetKeyDown(KeyCode.P))
        {
            //Condici�n que revisa si el juego est� o no en pausa
            if (juegoPausado)
            {
                //Juego activo
                Debug.Log("El juego est� activo");
                ContinuarJuego();
            }
            else
            {
                //Juego pausado
                Debug.Log("El juego est� pausado");
                PausaJuego();
            }
        }

    }

    public void PausaJuego()
    {
        juegoPausado = true;
        Time.timeScale = 0; //Pausar el juego
        menuPause.SetActive(true);
    }

    public void ContinuarJuego()
    {
        juegoPausado = false;
        Time.timeScale = 1;
        menuPause.SetActive(false);
    }
}
