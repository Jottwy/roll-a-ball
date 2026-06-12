using UnityEngine;

/// <summary>
/// Gestiona toda la logica de puntuacion del juego.
/// Suma puntos al recoger objetos, resta puntos cada cierto tiempo y guarda la puntuacion maxima usando PlayerPrefs cuando termina la partida.
/// </summary>
public class ScoreManager : MonoBehaviour
{
    public int puntuacion;
    public int puntuacionMax;

    public GameManager gm;

    float contador;

    // Evita que el record se evalue mas de una vez al terminar la partida
    bool puntuacionComprobada;

    void Start()
    {
        gm = FindFirstObjectByType<GameManager>();
        puntuacionMax = PlayerPrefs.GetInt("PuntMax", 0);
    }

    public void ControlPunt(int x)
    {
        puntuacion += x;
        Debug.Log("Puntuacion actual: " + puntuacion);
    }

    void Update()
    {
        if (gm.partidaTerminada)
        {
            if (!puntuacionComprobada)
            {
                if (puntuacion > puntuacionMax)
                {
                    puntuacionMax = puntuacion;
                    PlayerPrefs.SetInt("PuntMax", puntuacionMax);
                    PlayerPrefs.Save();
                    Debug.Log("Nuevo record: " + puntuacionMax);
                }

                puntuacionComprobada = true;
            }

            return;
        }

        contador += Time.deltaTime;

        // Penalizacion por tiempo: -50 puntos cada 5 segundos (sin bajar de 0)
        if (contador >= 5f)
        {
            contador = 0;
            puntuacion -= 50;
            if (puntuacion < 0) puntuacion = 0;
            Debug.Log("Puntuacion: " + puntuacion);
        }
    }
}
