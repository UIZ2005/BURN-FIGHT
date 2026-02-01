using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using TMPro;
using UnityEngine;

public class Interracciones : MonoBehaviour
{
    public GameObject MenuPrincipal;
    public GameObject player;
    public GameObject Datos;
    public TMP_InputField nombre;
    public TMP_InputField Correo;
    public GameObject Tutorial;
    public GameObject Juego;
    public GameObject ranking;
    public GameObject puntaje;
    public JEFE scriptJefe;
    public Informacion informacion;
    private Vector2 posicionInicialPlayer;
    public GameObject[] ambientes;

    // Start is called before the first frame update
    void Start()
    {
        try
        {
            posicionInicialPlayer = player.transform.position;
        }
        catch
        {

        }
       
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void destruir()
    {
        Destroy(gameObject);
    }
    public void quit()
    {
        Debug.Log("Cerrando el juego");
        Application.Quit();
    }
    public void mostrarDatos()
    {
        Datos.SetActive(true);
        MenuPrincipal.SetActive(false);
    }
    public void mostrartutorial()
    {
        if (nombre.text == "" || Correo.text == "")
        {
            Debug.Log("no se ha llenado alguno de los campos");
            return;
        }
        informacion.registrarPersona(nombre.text, Correo.text);
        Tutorial.SetActive(true);
        Datos.SetActive(false);
        nombre.text = "";
        Correo.text = "";
    }
    public void mostrarCombate()
    {
        ambientes[0].SetActive(false);
        ambientes[1].SetActive(true);
        player.transform.position = posicionInicialPlayer;
        player.GetComponent<Movimiento>().enabled = true;
        player.GetComponent<Movimiento>().pina.SetBool("dano", false);
        player.GetComponent<DisparoJugador>().enabled = true;
        Tutorial.SetActive(false);
        Juego.SetActive(true);
        scriptJefe.inicioCombate = true;
    }
    public void mostrarRaking()
    {
        MenuPrincipal.SetActive(false);
        puntaje.SetActive(false);
        ranking.SetActive(true);
    }
    public void OcultarRanking()
    {
        ambientes[0].SetActive(true);
        ambientes[2].SetActive(false);
        ranking.SetActive(false);
        MenuPrincipal.SetActive(true);
    }
}
