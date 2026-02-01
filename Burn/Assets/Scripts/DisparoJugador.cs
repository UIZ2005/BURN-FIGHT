using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using Unity.Burst.Intrinsics;
using UnityEngine;

public class DisparoJugador : MonoBehaviour
{
    private Vector3 objetivo;
    [SerializeField] private Camera camara;
    [SerializeField] private Transform disparoPoint;
    [SerializeField] private GameObject balaprefab;
    public int Cargador;
    public float TiempDisparo=2;
    private float disparotime;
    public int comboActual=1;
    private float combotime;
    public TextMeshProUGUI TextoCombo;
    private AudioManager audioManager;
    // Start is called before the first frame update
    void Start()
    {
        disparotime = TiempDisparo;
        audioManager = FindAnyObjectByType<AudioManager>();
    }

    // Update is called once per frame
    void Update()
    {
        ActualizarTexto();
        combotime += Time.deltaTime;
        Cargador =GetComponent<Municiones>().cantBloqueador;
        disparotime+=Time.deltaTime;

        objetivo = camara.ScreenToWorldPoint(Input.mousePosition);
        float anguloRadianes = Mathf.Atan2(objetivo.y - transform.position.y, objetivo.x - transform.position.x);
        float anguloGrados = (180 /Mathf.PI) * anguloRadianes - 90;
        transform.rotation=Quaternion.Euler(0,0,anguloGrados);

        if (Input.GetMouseButtonDown(0) && Cargador > 0 && disparotime > TiempDisparo)
        {
            audioManager.seleccionAudio(0);
            Instantiate(balaprefab, disparoPoint.position, disparoPoint.rotation);
            GetComponent<Municiones>().cantBloqueador-=1;
            disparotime = 0;
            combotime = 0;
        }
       if (combotime > 1.5)
        {
            comboActual = 1;
        }
    }
    void ActualizarTexto()
    {
        if (TextoCombo != null)
        {
            TextoCombo.text = "X"+comboActual.ToString();
        }
    }
}
