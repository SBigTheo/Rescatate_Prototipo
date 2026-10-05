using UnityEngine;
using Mediapipe.Tasks.Vision.HandLandmarker;
using Mediapipe.Tasks.Components.Containers;
using Mediapipe.Unity.Sample.HandLandmarkDetection;

public class HandGestureController : MonoBehaviour
{
    [Header("Referencias")]
    public PlayerController player;

    [Header("Configuración de gestos")]
    [Tooltip("Umbral: si la distancia media punta-muñeca / tamaño de palma es menor, se considera puño cerrado")]
    [Range(0.5f, 2.5f)]
    public float umbralPunioCerrado = 1.2f;

    [Header("Debug")]
    public bool mostrarDebug = true;

    private readonly int[] puntasDedos = { 8, 12, 16, 20 };
    private const int MUNECA = 0;
    private const int BASE_INDICE = 5;

    private volatile int gestoPendiente = -2;
    private int gestoActual = -2;

    void OnEnable()
    {
        HandLandmarkerRunner.OnHandLandmarkDetected += ProcesarManos;
    }

    void OnDisable()
    {
        HandLandmarkerRunner.OnHandLandmarkDetected -= ProcesarManos;
    }

    void Start()
    {
        if (player == null)
            player = FindFirstObjectByType<PlayerController>();

        if (player == null)
            Debug.LogError("HandGestureController: no se encontró PlayerController.");
    }

    void Update()
    {
        int gesto = gestoPendiente;
        if (gesto == -2) return;
        if (gesto == gestoActual) return;

        gestoActual = gesto;

        switch (gestoActual)
        {
            case 1:
                player.MoveRight();
                break;
            case -1:
                player.MoveLeft();
                break;
            case 0:
            default:
                player.Stop();
                break;
        }
    }

    private void ProcesarManos(HandLandmarkerResult result)
    {
        if (result.handLandmarks == null || result.handLandmarks.Count == 0)
        {
            gestoPendiente = 0;
            return;
        }

        var mano = result.handLandmarks[0];
        if (mano.landmarks == null || mano.landmarks.Count < 21)
        {
            gestoPendiente = 0;
            return;
        }

        var puntos = mano.landmarks;

        Vector3 muneca = new Vector3(puntos[MUNECA].x, puntos[MUNECA].y, puntos[MUNECA].z);
        Vector3 baseIndice = new Vector3(puntos[BASE_INDICE].x, puntos[BASE_INDICE].y, puntos[BASE_INDICE].z);
        float tamanoPalma = Vector3.Distance(muneca, baseIndice);

        if (tamanoPalma < 0.001f)
        {
            gestoPendiente = 0;
            return;
        }

        float sumaDistancias = 0f;
        foreach (int indicePunta in puntasDedos)
        {
            Vector3 punta = new Vector3(puntos[indicePunta].x, puntos[indicePunta].y, puntos[indicePunta].z);
            sumaDistancias += Vector3.Distance(muneca, punta);
        }
        float distanciaMedia = sumaDistancias / puntasDedos.Length;
        float ratio = distanciaMedia / tamanoPalma;

        if (ratio < umbralPunioCerrado)
            gestoPendiente = 1;
        else
            gestoPendiente = -1;

        if (mostrarDebug)
            Debug.Log($"Ratio mano: {ratio:F2} (umbral: {umbralPunioCerrado}) → gesto: {gestoPendiente}");
    }
}