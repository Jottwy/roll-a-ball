using UnityEngine;

/// <summary>
/// Controlador de entrada tactil para dispositivos moviles.
/// Detecta toque simple y doble toque en pantalla.
/// Cada tipo de toque activa o desactiva un objeto distinto para probar la deteccion de input tactil.
/// </summary>
public class TouchController : MonoBehaviour
{
    public GameObject objetoSimple;
    public GameObject objetoDoble;

    float ultimoToque;

    // Ventana maxima entre dos toques para considerarlos doble toque
    float delay = 0.3f;

    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch t = Input.GetTouch(0);

            if (t.phase == TouchPhase.Began)
            {
                if (Time.time - ultimoToque < delay)
                {
                    ToggleObjeto(objetoDoble);
                }
                else
                {
                    ToggleObjeto(objetoSimple);
                }

                ultimoToque = Time.time;
            }
        }
    }

    void ToggleObjeto(GameObject obj)
    {
        obj.SetActive(!obj.activeSelf);
    }
}
