using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SeleccionEstilo : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Garden()
    {
        GameManager.SingletonGamemanager.Estiloselecionado = GameManager.Estilo.Garden; //accedo a la clase gamemanager q tiene una variable singleton tmb tipo gamenager y este "objeto" es un singleton q ejecuta las funciones de la clase gamemanager
        SceneManager.LoadScene("DifficultyLevels");//carga escena

    }

    public void Greengrocer()
    {
        GameManager.SingletonGamemanager.Estiloselecionado = GameManager.Estilo.Greengrocer; //accedo a la clase gamemanager q tiene una variable singleton tmb tipo gamenager y este "objeto" es un singleton q ejecuta las funciones de la clase gamemanager
        SceneManager.LoadScene("DifficultyLevels");//carga escena

    }
}
