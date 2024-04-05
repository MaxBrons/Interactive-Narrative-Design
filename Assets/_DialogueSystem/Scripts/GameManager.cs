using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.Utilities;

public class GameManager : MonoBehaviour
{
    public static bool IsRunning {
        get => !_cancellationToken.IsCancellationRequested;
        set
        {
            if (value)
                _cancellationToken = new();
            else
                _cancellationToken?.Cancel();
        }
    }

    public static CancellationToken GameCancellationToken => _cancellationToken.Token;

    private static CancellationTokenSource _cancellationToken = new();

    [SerializeField] private TextAsset _dialogueData;
    [SerializeField] private TMP_Text _dialogueBarText;
    [SerializeField] private TextBarSettings _textBarSettings;
    [SerializeField] private RectTransform _choiceButtonsParent;
    [SerializeField] private GameObject _choiceButtonPrefab;

    private DialogueSource _dialogueSource;
    private Dialogue _dialogue;
    private DialogueBar _dialogueBar;
    private ChoiceWindow _choiceWindow;
    private InputActionsCore _input;
    private CancellationTokenSource _skipToken = new();
    private Dialogue.DialogueLine _interuptLineFirst;
    private Dialogue.DialogueLine _interuptLineSecond;
    private Dialogue.DialogueLine _interuptLineThird;
    private Dialogue.DialogueLine _interupLineEnd;
    private int _interuptCount;

    private async void Start()
    {
        _dialogueSource = new(_dialogueData.text);
        _dialogueBar = new(_dialogueBarText, _textBarSettings);

        _dialogue = new(_dialogueSource);
        _dialogue.OnChoiceRequested += OnChoiceRequested;

        _choiceWindow = new(_choiceButtonsParent, _choiceButtonPrefab);
        _choiceWindow.OnChoiceMade += _dialogue.MakeChoice;

        _input = new();
        _input.Default.Space.performed += (ctx) => _skipToken.Cancel();

        _input.Enable();

        _interuptLineFirst = new("Rogier", "Broer, laat me die torrie uitleggen a neef.");
        _interuptLineSecond = new("Rogier", "Broer stop met onderbreken man, ik vertel ishin emotionele torrie en je gooit deze. Is echt tantoe irri man, ik dacht dat je real was, maar je bent kakka.");
        _interuptLineThird = new("...", "Rogier staat boos op en loopt weg van de conversatie...");
        _interupLineEnd = new("...", "EINDE");

        await ShowDialogue();
    }

    private void OnChoiceRequested(IEnumerable<string> choices)
    {
        _choiceWindow.AddChoices(choices.ToArray());
    }

    private async Task ShowDialogue()
    {
        while (!_dialogue.EndReached) {
            var line = await _dialogue.GetNextLine();
            await _dialogueBar.ShowLine(line, _skipToken.Token);

            await CheckForDialogueInteruptions(line);

            if (_interuptCount > 2)
                break;

            _dialogueBar.ShowLineSync(line);

            await WaitForSkip();
        }

        await _dialogueBar.ShowLine(new("Game", "Klik spatie om het spel af te sluiten..."), GameCancellationToken);
        await WaitForSkip();

        print("END REACHED");

        Application.Quit();
    }

    private async Task CheckForDialogueInteruptions(Dialogue.DialogueLine currentLine)
    {
        if (_skipToken.IsCancellationRequested) {
            if (_interuptCount < 2) {
                await _dialogueBar.ShowLine(_interuptCount == 0 ? _interuptLineFirst : _interuptLineSecond, GameCancellationToken);
                await WaitForSkip();
                await _dialogueBar.ShowLine(currentLine, GameCancellationToken);
            }
            else {
                await _dialogueBar.ShowLine(_interuptLineThird, GameCancellationToken);
                await WaitForSkip();
                await _dialogueBar.ShowLine(_interupLineEnd, GameCancellationToken);
                await WaitForSkip();
            }

            _skipToken = new();
            _interuptCount++;
        }
    }

    private async Task WaitForSkip()
    {
        _skipToken = new();
        await Tools.WaitFor(() => _skipToken.IsCancellationRequested, GameCancellationToken);
        _skipToken = new();
    }
}