# Roll A Ball — Extended
🎮 **[Jugar en el navegador (Itch.io)](https://jottwy.itch.io/roll-a-ball)**
Versión ampliada del clásico *Roll a Ball* de Unity. Mueves una bola por un escenario cerrado recogiendo monedas a contrarreloj. Cada moneda suma puntos y algo de tiempo extra, pero hay una penalización que va comiendo puntos según pasan los segundos, así que no puedes ir con calma. El récord se guarda entre partidas.

Partí del tutorial oficial y le fui añadiendo cosas: una cámara propia que no atraviesa paredes, el sistema de puntuación con penalización y récord, y la mecánica de tiempo. Es una práctica del ciclo de Desarrollo de Videojuegos (MasterD), corregida con un 10.

## Qué incluye

- Movimiento físico de la bola (`Rigidbody.AddForce`), relativo a hacia dónde mira la cámara.
- Cámara en tercera persona con suavizado y `SphereCast` para que no se cuele a través de las paredes.
- Puntuación con penalización por tiempo (-50 cada 5s) y récord persistente con `PlayerPrefs`.
- Cuenta atrás donde cada moneda recogida añade tiempo extra.
- Victoria al recoger todas las monedas, derrota si se acaba el tiempo.
- Una escena aparte de prueba de input táctil (toque y doble toque) para Android.
- Render con URP (perfiles de PC y móvil incluidos).

## Controles

| Acción      | Teclado            | Táctil (TouchScene)        |
|-------------|--------------------|----------------------------|
| Mover bola  | WASD / flechas     | —                          |
| Interacción | —                  | Toque simple / doble toque |

## Versión de Unity

Unity 2022.3.62f3 (LTS) · Universal Render Pipeline (URP)

## Estructura del código — `Assets/Scripts/`

```
Scripts/
├── Player.cs            # Movimiento físico + recogida de monedas (OnTriggerEnter)
├── CameraController.cs  # Cámara seguidora con suavizado y SphereCast anti-paredes
├── GameManager.cs       # Estado de la partida: cuenta atrás, victoria y derrota
├── ScoreManager.cs      # Puntuación, penalización por tiempo y récord (PlayerPrefs)
├── PickUp.cs            # Moneda: animación de recogida y desactivación
├── Rotator.cs           # Rotación visual de las monedas
└── TouchController.cs   # Prueba de input táctil
```

En la práctica original toda la lógica vivía en un único script de jugador. Lo separé en `GameManager` (control general de la partida) y `ScoreManager` (puntuación y récord), dejando `Player` solo con el movimiento. Así cada cosa tiene su sitio y tocar la puntuación no implica meterse en el movimiento.

## Algunas decisiones

**`SphereCast` en vez de `Raycast` para la cámara.** Un rayo solo comprueba el punto central, así que la cámara podía asomar por el borde aunque el centro no tocase la pared. Lanzando una esfera del radio de la cámara eso ya no pasa.

**`LateUpdate` para la cámara, `FixedUpdate` para el movimiento.** La bola se mueve con físicas, que van en el paso fijo. La cámara se recoloca en `LateUpdate`, ya con el jugador movido ese frame, para que no se quede medio fotograma por detrás y dé tirones.

**`FindFirstObjectByType` para encontrar los managers.** Es lo más simple y aquí sobra de sobra. Si el proyecto creciera lo cambiaría por referencias serializadas o un sistema de eventos para que `Player` no dependa directamente de los managers, pero para esto lo dejé así a propósito.

## Posibles mejoras

- Una UI en pantalla para el tiempo, la puntuación y el resultado, que ahora salen por `Debug.Log`, más una pantalla de reinicio. Es lo que falta para tener una build realmente jugable de cara al jugador.
- Sustituir `FindFirstObjectByType` por referencias serializadas o un sistema de eventos, para desacoplar `Player` de los managers si el proyecto creciera.
- Audio y partículas en la recogida de monedas para dar algo más de feedback.
