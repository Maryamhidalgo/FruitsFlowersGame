using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SelecionDificultad : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Facil()
    {
       GameManager.SingletonGamemanager.NivelesDificultad = GameManager.Dificultad.Facil; //accedo a la clase gamemanager q tiene una variable singleton tmb tipo gamenager y este "objeto" es un singleton q ejecuta las funciones de la clase gamemanager
       GameManager.SingletonGamemanager.parejasCreadas = 10;
       SceneManager.LoadScene("Level1");//carga escena

    }
    public void Medio()
    {
        GameManager.SingletonGamemanager.NivelesDificultad = GameManager.Dificultad.Medio;
        GameManager.SingletonGamemanager.parejasCreadas = 15;
        SceneManager.LoadScene("Level1");//carga escena

    }
    public void Dificil()
    {
        GameManager.SingletonGamemanager.NivelesDificultad = GameManager.Dificultad.Dificil;
        GameManager.SingletonGamemanager.parejasCreadas = 20;
        SceneManager.LoadScene("Level1");//carga escena

    }
}
