using UnityEngine;
using UnityEngine.Windows.Speech;
using System.IO;

public class VoiceManager : MonoBehaviour
{
    [Header("Referencias")]
    public PlayerController player;

    [Header("Configuración de voz")]
    public ConfidenceLevel confidenceLevel = ConfidenceLevel.Low;

    [Tooltip("Nombre del archivo de gramática dentro de StreamingAssets")]
    public string nombreArchivoGramatica = "Grammar.xml";

    private GrammarRecognizer grammarRecognizer;

    void Start()
    {
        if (player == null)
            player = FindFirstObjectByType<PlayerController>();

        if (player == null)
        {
            Debug.LogError("VoiceManager: no se encontró ningún PlayerController en la escena.");
            return;
        }

        string rutaGramatica = Path.Combine(Application.streamingAssetsPath, nombreArchivoGramatica);

        if (!File.Exists(rutaGramatica))
        {
            Debug.LogError($"VoiceManager: no se encontró el archivo de gramática en {rutaGramatica}");
            return;
        }

        try
        {
            grammarRecognizer = new GrammarRecognizer(rutaGramatica, confidenceLevel);
            grammarRecognizer.OnPhraseRecognized += OnVoiceCommand;
            grammarRecognizer.Start();
            Debug.Log("VoiceManager: GrammarRecognizer iniciado. Di 'salta'.");
        }
        catch (System.Exception e)
        {
            Debug.LogError($"VoiceManager: error al iniciar el GrammarRecognizer: {e.Message}");
        }
    }

    void OnVoiceCommand(PhraseRecognizedEventArgs args)
    {
        string textoReconocido = args.text.ToLower().Trim();

        Debug.Log($"VoiceManager: reconocido → '{textoReconocido}'");

        if (textoReconocido.Contains("salta") ||
            textoReconocido.Contains("saltar") ||
            textoReconocido.Contains("salto"))
        {
            player.Jump();
        }
    }

    void OnDestroy()
    {
        if (grammarRecognizer != null && grammarRecognizer.IsRunning)
        {
            grammarRecognizer.Stop();
            grammarRecognizer.Dispose();
        }
    }

    public void ToggleListening(bool activo)
    {
        if (grammarRecognizer == null) return;

        if (activo && !grammarRecognizer.IsRunning)
            grammarRecognizer.Start();
        else if (!activo && grammarRecognizer.IsRunning)
            grammarRecognizer.Stop();
    }
}