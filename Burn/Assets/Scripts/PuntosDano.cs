using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PuntosDano : MonoBehaviour
{
    public int puntos=0;
    public TextMeshProUGUI Textopuntaje;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        ActualizarTexto();
        if (gameObject.GetComponent<JEFE>().inicioCombate == false)
        {
            puntos = 0;
        }
    }

    public void puntaje(int dano)
    {
        puntos += dano;
    }
    void ActualizarTexto()
    {
        if (Textopuntaje != null)
        {
            Textopuntaje.text = puntos.ToString();
        }
    }
}

