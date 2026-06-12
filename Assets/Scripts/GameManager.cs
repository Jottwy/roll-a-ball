using UnityEngine;

/// <summary>
/// Controlador principal del estado del juego.
/// Gestiona la cuenta atras, comprueba condiciones de victoria o derrota y controla cuando la partida termina.
/// </summary>
public class GameManager : MonoBehaviour
{
    public Player p;

    public float startingTime = 25f;
    float currentTime;

    public bool partidaTerminada = false;

    int totalPickups;

    void Start()
    {
        currentTime = startingTime;
        p = FindFirstObjectByType<Player>();

        totalPickups = GameObject.FindGameObjectsWithTag("PickUp").Length;
        Debug.Log("Total monedas en escena: " + totalPickups);
    }

    void Update()
    {
        if (partidaTerminada) return;

        currentTime -= Time.deltaTime;

        // Solo logueamos cuando cambia el segundo entero, no cada frame
        if (Mathf.FloorToInt(currentTime) != Mathf.FloorToInt(currentTime + Time.deltaTime))
        {
            Debug.Log("Tiempo restante: " + currentTime.ToString("0"));
        }

        if (p.count >= totalPickups)
        {
            Ganar();
        }
        else if (currentTime <= 0)
        {
            Perder();
        }
    }

    public void AddTime(float extraTime)
    {
        if (!partidaTerminada)
        {
            currentTime += extraTime;
            Debug.Log("Tiempo extra +" + extraTime);
        }
    }

    void Ganar()
    {
        Debug.Log("HAS GANADO");
        partidaTerminada = true;
        Time.timeScale = 0;
    }

    void Perder()
    {
        Debug.Log("HAS PERDIDO");
        partidaTerminada = true;
        Time.timeScale = 0;
    }
}
