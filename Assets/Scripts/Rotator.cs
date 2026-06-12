using UnityEngine;

/// <summary>
/// Hace rotar continuamente los objetos recolectables para darles un efecto visual de moneda girando.
/// La rotacion se calcula en funcion del tiempo.
/// </summary>
public class Rotator : MonoBehaviour
{
    public float velocidadRotacion = 120f;

    void Update()
    {
        // Multiplicamos por Time.deltaTime para que la rotacion sea independiente de los FPS
        transform.Rotate(0f, 0f, velocidadRotacion * Time.deltaTime);
    }
}
