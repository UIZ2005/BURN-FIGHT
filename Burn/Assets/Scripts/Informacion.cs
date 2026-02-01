using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO.Pipes;
using TMPro;
using UnityEngine;

public class Informacion : MonoBehaviour
{
    public List<string> nombres = new List<string>();
    public List<string> correos= new List<string>();
    public List<int> puntajes = new List<int>();
    private List<int> puntajesorganizados= new List<int>();
    private List<string> nombresOrganizados = new List<string>();
    private List<string> correosOrganizados = new List<string>();
    public TextMeshProUGUI[] Textonombres;
    public TextMeshProUGUI[] Textopuntajes;
    private int playerActual=0;
    public TextMeshProUGUI nombre;
    public TextMeshProUGUI puntaje;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void registrarPersona(string nombre, string correo)
    {
        nombres.Add(nombre);
        correos.Add(correo);
    }
    public void registrarPuntaje(int puntaje)
    {
        puntajes.Add(puntaje);
        ordenarPuntajes();
        actualizarRanking();

    }
    public void ordenarPuntajes()
    {
        puntajesorganizados = new List<int>(puntajes);
        nombresOrganizados = new List<string>(nombres);
        correosOrganizados = new List<string>(correos);
        if (puntajesorganizados.Count < 2) return;
        for (int i = 0; i < puntajes.Count - 1; i++)
            {
                for (int j = 0; j < puntajes.Count - i - 1; j++)
                {
                    if (j + 1 < puntajes.Count)
                    {
                        if (puntajesorganizados[j] < puntajesorganizados[j + 1]) // Ordenar de mayor a menor
                        {
                            // Intercambio de puntajes
                            int tempPuntaje = puntajesorganizados[j];
                            puntajesorganizados[j] = puntajesorganizados[j + 1];
                            puntajesorganizados[j + 1] = tempPuntaje;

                            // Intercambio de nombres
                            string tempNombre = nombresOrganizados[j];
                            nombresOrganizados[j] = nombresOrganizados[j + 1];
                            nombresOrganizados[j + 1] = tempNombre;

                            // Intercambio de correos
                            string tempCorreo = correosOrganizados[j];
                            correosOrganizados[j] = correosOrganizados[j + 1];
                            correosOrganizados[j + 1] = tempCorreo;
                        }
                    }
               
                }
            }
        
        
    }
    public void actualizarRanking()
    {
        int cantidad = Mathf.Min(Textonombres.Length, nombresOrganizados.Count, puntajesorganizados.Count);

        for (int i = 0; i <cantidad; i++)
        {

                Textonombres[i].text = nombresOrganizados[i];
                Textopuntajes[i].text = puntajesorganizados[i].ToString();
        }
    }
    public void puntajeActual()
    {
        nombre.text = nombres[playerActual];
        puntaje.text = puntajes[playerActual].ToString();
        playerActual += 1;
    }
}
