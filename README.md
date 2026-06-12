# Roll A Ball — Extended

Versión ampliada del clásico *Roll a Ball* de Unity. Controlas una bola que recoge monedas dentro de un escenario con paredes, contra un cronómetro: cada moneda suma puntos **y** tiempo extra, mientras una penalización va restando puntos con el paso de los segundos. La puntuación máxima se guarda entre partidas. El proyecto parte del tutorial oficial y añade una cámara propia con detección de colisión, un sistema de puntuación persistente y una mecánica de tiempo.

## Vídeo / GIF

> _Gameplay: [pega aquí el enlace de YouTube o el GIF]_

## Características técnicas

- Movimiento de la bola basado en físicas (`Rigidbody.AddForce`), relativo a la orientación de la cámara.
- Cámara en tercera persona que sigue la dirección de movimiento con suavizado y **SphereCast** para no atravesar paredes.
- Sistema de puntuación con penalización por tiempo y récord persistente vía `PlayerPrefs`.
- Mecánica de cuenta atrás: cada moneda recogida añade tiempo extra a la partida.
- Condiciones de victoria (todas las monedas) y derrota (tiempo agotado) gestionadas por un `GameManager`.
- Escena adicional de prueba de **input táctil** (toque simple / doble toque) para móvil.
- Render con URP (perfiles de calidad PC y Mobile incluidos).

## Controles

| Acción      | Teclado            | Táctil (TouchScene)        |
|-------------|--------------------|----------------------------|
| Mover bola  | WASD / flechas     | —                          |
| Interacción | —                  | Toque simple / doble toque |

## Versión de Unity

- **Unity 2022.3.62f3 (LTS)** · Universal Render Pipeline (URP)

## Arquitectura — `Assets/Scripts/`

```
Scripts/
├── Player.cs            # Movimiento por físicas + detección de recogida (OnTriggerEnter)
├── CameraController.cs  # Cámara seguidora con suavizado y SphereCast anti-paredes
├── GameManager.cs       # Estado de la partida: cuenta atrás, victoria y derrota
├── ScoreManager.cs      # Puntuación, penalización por tiempo y récord (PlayerPrefs)
├── PickUp.cs            # Comportamiento de la moneda: animación de recogida y destrucción
├── Rotator.cs           # Rotación visual continua de las monedas
└── TouchController.cs   # Prueba de input táctil (toque simple / doble toque)
```

## Decisiones técnicas

- **`SphereCast` en lugar de `Raycast` para la cámara.** Un rayo solo detecta el punto central; con una esfera del radio de la cámara se evita que el borde del plano de visión asome a través de una pared aunque el centro no la toque.
- **`LateUpdate` para la cámara, `FixedUpdate` para el movimiento.** El movimiento de la bola es físico, así que va en el paso fijo; la cámara se reposiciona en `LateUpdate`, después de que el jugador ya se haya movido ese frame, para evitar tirones.
- **`FindFirstObjectByType` para localizar los managers.** Es una solución sencilla y suficiente para el tamaño de este proyecto. **Es mejorable**: a medida que el juego creciera, lo sustituiría por referencias serializadas o un sistema de eventos para desacoplar `Player` de los managers. Se mantiene así de forma consciente por simplicidad.

## Limitaciones conocidas

- El feedback de partida (tiempo, puntuación, victoria/derrota) sale por `Debug.Log`; **aún no hay UI en pantalla** ni pantalla de reinicio. Es el siguiente paso pendiente antes de publicar una build jugable.
- `Time.timeScale = 0` detiene el juego al terminar sin recargar la escena.
