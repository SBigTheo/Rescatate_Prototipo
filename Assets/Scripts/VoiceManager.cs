using UnityEngine;
using UnityEngine.Windows.Speech;
using System.Collections.Generic;
using System.Linq;

public class VoiceManager : MonoBehaviour
{
    public ConfidenceLevel confidenceLevel = ConfidenceLevel.Medium;

    private KeywordRecognizer keywordRecognizer;
    private Dictionary<string, System.Action> commands = new Dictionary<string, System.Action>();
    private PlayerController player;

    void Start()
    {
        player = FindFirstObjectByType<PlayerController>();
        if (player == null) return;

        //commands.Add("izquierda", () => { player.MoveLeft(); });
        //commands.Add("izkierda", () => { player.MoveLeft(); });
        //commands.Add("isquierda", () => { player.MoveLeft(); });
        //commands.Add("iskierda", () => { player.MoveLeft(); });
        //commands.Add("izquieda", () => { player.MoveLeft(); });
        //commands.Add("izquiera", () => { player.MoveLeft(); });
        //commands.Add("izquerda", () => { player.MoveLeft(); });
        //commands.Add("izquirda", () => { player.MoveLeft(); });
        //commands.Add("izquiedda", () => { player.MoveLeft(); });
        //commands.Add("izkienda", () => { player.MoveLeft(); });
        //commands.Add("isquieda", () => { player.MoveLeft(); });
        //commands.Add("left", () => { player.MoveLeft(); });
        //commands.Add("lef", () => { player.MoveLeft(); });
        //commands.Add("leff", () => { player.MoveLeft(); });

        //commands.Add("derecha", () => { player.MoveRight(); });
        //commands.Add("dereca", () => { player.MoveRight(); });
        //commands.Add("dereka", () => { player.MoveRight(); });
        //commands.Add("derecga", () => { player.MoveRight(); });
        //commands.Add("derejcha", () => { player.MoveRight(); });
        //commands.Add("derejca", () => { player.MoveRight(); });
        //commands.Add("derexcha", () => { player.MoveRight(); });
        //commands.Add("derexca", () => { player.MoveRight(); });
        //commands.Add("derehs", () => { player.MoveRight(); });
        //commands.Add("right", () => { player.MoveRight(); });
        //commands.Add("rigth", () => { player.MoveRight(); });
        //commands.Add("raig", () => { player.MoveRight(); });
        //commands.Add("raith", () => { player.MoveRight(); });
        //commands.Add("raigt", () => { player.MoveRight(); });

        commands.Add("salta",   () => { player.Jump(); });
        commands.Add("saltar",  () => { player.Jump(); });
        commands.Add("salto",   () => { player.Jump(); });
        commands.Add("salte",   () => { player.Jump(); });
        commands.Add("salti",   () => { player.Jump(); });
        commands.Add("sarta",   () => { player.Jump(); });
        commands.Add("saltra",  () => { player.Jump(); });
        commands.Add("saltarr", () => { player.Jump(); });
        commands.Add("arriba",  () => { player.Jump(); });
        commands.Add("arribba", () => { player.Jump(); });
        commands.Add("ariba",   () => { player.Jump(); });
        commands.Add("jump",    () => { player.Jump(); });
        commands.Add("jamp",    () => { player.Jump(); });
        commands.Add("yamp",    () => { player.Jump(); });
        commands.Add("yam",     () => { player.Jump(); });
        commands.Add("yump",    () => { player.Jump(); });
        commands.Add("yum",     () => { player.Jump(); });

        //commands.Add("detente",  () => { player.Stop(); });
        //commands.Add("detene",   () => { player.Stop(); });
        //commands.Add("deten",    () => { player.Stop(); });
        //commands.Add("detenete", () => { player.Stop(); });
        //commands.Add("detenme",  () => { player.Stop(); });
        //commands.Add("para",     () => { player.Stop(); });
        //commands.Add("parra",    () => { player.Stop(); });
        //commands.Add("parar",    () => { player.Stop(); });
        //commands.Add("pararr",   () => { player.Stop(); });
        //commands.Add("alto",     () => { player.Stop(); });
        //commands.Add("altoo",    () => { player.Stop(); });
        //commands.Add("alton",    () => { player.Stop(); });
        //commands.Add("alrto",    () => { player.Stop(); });
        //commands.Add("halto",    () => { player.Stop(); });
        //commands.Add("stop",     () => { player.Stop(); });
        //commands.Add("stap",     () => { player.Stop(); });
        //commands.Add("estop",    () => { player.Stop(); });
        //commands.Add("estap",    () => { player.Stop(); });
        //commands.Add("stopp",    () => { player.Stop(); });
        //commands.Add("estopp",   () => { player.Stop(); });

        try
        {
            keywordRecognizer = new KeywordRecognizer(commands.Keys.ToArray(), confidenceLevel);
            keywordRecognizer.OnPhraseRecognized += OnVoiceCommand;
            keywordRecognizer.Start();
        }
        catch (System.Exception) { }
    }

    void OnVoiceCommand(PhraseRecognizedEventArgs args)
    {
        if (commands.ContainsKey(args.text))
        {
            commands[args.text]?.Invoke();
        }
    }

    void OnDestroy()
    {
        if (keywordRecognizer != null && keywordRecognizer.IsRunning)
        {
            keywordRecognizer.Stop();
            keywordRecognizer.Dispose();
        }
    }

    public void ToggleListening(bool active)
    {
        if (keywordRecognizer == null) return;

        if (active && !keywordRecognizer.IsRunning)
            keywordRecognizer.Start();
        else if (!active && keywordRecognizer.IsRunning)
            keywordRecognizer.Stop();
    }
}