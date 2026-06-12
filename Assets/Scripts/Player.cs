using UnityEngine;

/// <summary>
/// Controlador del jugador.
/// Se encarga del movimiento de la bola usando fisicas y detecta la recogida de objetos para avisar al ScoreManager y GameManager.
/// </summary>
[RequireComponent(typeof(Rigidbody))]
public class Player : MonoBehaviour
{
    public float speed = 10f;

    float moveHorizontal;
    float moveVertical;

    Rigidbody rb;
    Camera cam;

    public int count = 0;

    ScoreManager scoreManager;
    GameManager gm;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        cam = Camera.main;

        // Damping para reducir la inercia del Rigidbody y que la bola no patine
        rb.drag = 0.8f;

        scoreManager = FindFirstObjectByType<ScoreManager>();
        gm = FindFirstObjectByType<GameManager>();
    }

    // Input se lee en Update; el movimiento por fisicas se aplica en FixedUpdate
    void Update()
    {
        moveHorizontal = Input.GetAxis("Horizontal");
        moveVertical = Input.GetAxis("Vertical");
    }

    void FixedUpdate()
    {
        Vector3 forward = cam.transform.forward;
        Vector3 right = cam.transform.right;

        // Anulamos la componente vertical para movernos solo en el plano del suelo
        forward.y = 0;
        right.y = 0;

        // Movimiento relativo a la orientacion de la camara
        Vector3 movement = forward * moveVertical + right * moveHorizontal;

        // Clamp para que el desplazamiento diagonal no sea mas rapido que el recto
        movement = Vector3.ClampMagnitude(movement, 1f);

        rb.AddForce(movement * speed, ForceMode.Acceleration);
    }

    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("PickUp")) return;

        PickUp coin = other.GetComponent<PickUp>();
        if (coin != null) coin.Recoger();

        count++;
        scoreManager?.ControlPunt(100);
        gm?.AddTime(5f);
    }
}
