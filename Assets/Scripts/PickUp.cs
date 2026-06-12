using UnityEngine;

/// <summary>
/// Script de comportamiento para los objetos recolectables.
/// Cuando el jugador los recoge se desactiva su collider y se reproduce una animacion de desaparicion reduciendo la escala hasta destruir el objeto.
/// </summary>
[RequireComponent(typeof(Collider))]
public class PickUp : MonoBehaviour
{
    public float shrinkSpeed = 10f;

    bool recogido = false;

    void Reset()
    {
        // El collider debe ser Trigger para que el Player detecte la recogida
        GetComponent<Collider>().isTrigger = true;
    }

    void Update()
    {
        if (!recogido) return;

        // Animacion de desaparicion: encogemos hacia cero y destruimos al llegar
        transform.localScale = Vector3.Lerp(transform.localScale, Vector3.zero, shrinkSpeed * Time.deltaTime);

        if (transform.localScale.magnitude < 0.05f)
        {
            Destroy(gameObject);
        }
    }

    public void Recoger()
    {
        recogido = true;

        // Desactivamos el collider para que el Trigger no se dispare varias veces
        GetComponent<Collider>().enabled = false;
    }
}
