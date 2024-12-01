using UnityEngine;
using UnityEngine.UI;

public class BallController : MonoBehaviour
{
    public float moveSpeed = 5f;  // Velocidad de movimiento de la bola
    public float boundaryLeft = -4f; // Límite izquierdo de la pista
    public float boundaryRight = 4f;  // Límite derecho de la pista
    public float maxLaunchForce = 20f;  // Fuerza máxima de lanzamiento
    public float launchForce = 0f;   // Fuerza actual de lanzamiento
    public Slider forceSlider; // Slider para mostrar la barra de fuerza
    public GameObject backgroundBar; // Fondo de la barra de fuerza
    public GameObject forceBar; // Barra de la fuerza

    private Rigidbody rb;
    private bool isCharging = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            Debug.LogError("No se encontró un Rigidbody en el objeto " + gameObject.name);
        }

        forceSlider.gameObject.SetActive(true); // Slider visible desde el inicio
    }

    void Update()
    {
        // Movimiento horizontal de la bola
        float horizontalMove = Input.GetAxis("Horizontal"); // Teclas de dirección (izquierda/derecha)
        Vector3 movement = new Vector3(horizontalMove, 0f, 0f);
        transform.Translate(movement * moveSpeed * Time.deltaTime);

        // Limita la posición de la bola para que no se salga de los límites de la pista
        if (transform.position.x < boundaryLeft)
        {
            transform.position = new Vector3(boundaryLeft, transform.position.y, transform.position.z);
        }
        if (transform.position.x > boundaryRight)
        {
            transform.position = new Vector3(boundaryRight, transform.position.y, transform.position.z);
        }

        // Controla el lanzamiento con la barra espaciadora
        if (Input.GetKeyDown(KeyCode.Space))
        {
            isCharging = true;
            forceSlider.gameObject.SetActive(true); // Muestra la barra de fuerza
        }

        if (isCharging)
        {
            // Aumenta la fuerza de la barra mientras se mantiene presionada la barra espaciadora
            if (launchForce < maxLaunchForce)
            {
                launchForce += Time.deltaTime * 5f; // Incrementar la fuerza con el tiempo
            }

            forceSlider.value = launchForce / maxLaunchForce; // Actualiza el valor del slider con la fuerza actual

            // Actualizar el tamaño de la barra de fuerza visual
            forceBar.transform.localScale = new Vector3(forceSlider.value, 1, 1);

            // Cuando se suelta la barra espaciadora, lanza la bola
            if (Input.GetKeyUp(KeyCode.Space))
            {
                rb.AddForce(new Vector3(0f, 0f, launchForce), ForceMode.Impulse);
                isCharging = false;
                forceSlider.gameObject.SetActive(false); // Oculta la barra de fuerza
                launchForce = 0f; // Resetea la fuerza de lanzamiento
            }
        }

        // Reinicia el juego si la bola se sale de la pista o si se presiona "R"
        if (transform.position.y < -1f || Input.GetKeyDown(KeyCode.R))
        {
            RestartGame();
        }
    }

    // Método para reiniciar el juego
    void RestartGame()
    {
        // Reiniciar la escena
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }
}