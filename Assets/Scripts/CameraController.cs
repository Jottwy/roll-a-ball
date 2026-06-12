using UnityEngine;

/// <summary>
/// Controla la camara que sigue al jugador desde detras.
/// La posicion se calcula usando la direccion de movimiento del jugador con suavizado para evitar movimientos bruscos.
/// Incluye un SphereCast para evitar que la camara atraviese paredes.
/// </summary>
public class CameraController : MonoBehaviour
{
    public Transform player;

    [Header("Follow Settings")]
    public float followDistance = 7f;
    public float followHeight = 5f;
    public float smoothSpeed = 5f;
    public float rotationSpeed = 5f;

    [Header("Collision")]
    public float cameraRadius = 0.3f;
    public LayerMask collisionMask;

    Rigidbody playerRb;

    Vector3 smoothMoveDir;

    void Awake()
    {
        if (player == null)
        {
            GameObject p = GameObject.FindGameObjectWithTag("Player");
            if (p != null) player = p.transform;
        }

        if (player != null)
        {
            playerRb = player.GetComponent<Rigidbody>();
            smoothMoveDir = player.forward;

            // Colocamos la camara ya en su sitio para que no haya salto en el primer frame
            Vector3 startPos = player.position - player.forward * followDistance + Vector3.up * followHeight;
            transform.position = startPos;
            transform.LookAt(player.position);
        }
    }

    // LateUpdate: la camara se mueve despues de que el Player ya se haya desplazado este frame
    void LateUpdate()
    {
        if (player == null) return;

        // Usamos la velocidad del Rigidbody como direccion; si esta casi quieto, su forward
        Vector3 moveDir = playerRb != null ? playerRb.velocity : Vector3.zero;
        if (moveDir.magnitude < 0.1f) moveDir = player.forward;

        moveDir.y = 0;

        smoothMoveDir = Vector3.Lerp(smoothMoveDir, moveDir.normalized, rotationSpeed * Time.deltaTime);

        Vector3 desiredPosition = player.position - smoothMoveDir * followDistance + Vector3.up * followHeight;

        Vector3 direction = desiredPosition - player.position;
        float distance = direction.magnitude;
        direction.Normalize();

        RaycastHit hit;

        // SphereCast (no Raycast) para que el grosor de la camara no clipee la pared
        if (Physics.SphereCast(player.position, cameraRadius, direction, out hit, distance, collisionMask))
        {
            desiredPosition = hit.point - direction * cameraRadius;
        }

        transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
        transform.LookAt(player.position);
    }
}
