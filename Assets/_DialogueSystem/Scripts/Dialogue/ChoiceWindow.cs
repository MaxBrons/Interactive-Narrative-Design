using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class ChoiceButton
{
    public EventTrigger Button;
    public TMP_Text Text;
}

public class ChoiceWindow
{
    public event Action<int> OnChoiceMade;

    private Transform _root;
    private GameObject _buttonPrefab;
    private List<ChoiceButton> _buttons = new();

    public ChoiceWindow(Transform choiceWindowParent, GameObject buttonPrefab)
    {
        _root = choiceWindowParent;
        _buttonPrefab = buttonPrefab;

        for (int i = 0; i < 4; i++) {
            AddChoiceButton();
        }
    }

    public void AddChoices(params string[] choices)
    {
        for (int i = 0; i < choices.Length - _buttons.Count; i++) {
            AddChoiceButton();
        }

        foreach(ChoiceButton choiceButton in _buttons) {
            choiceButton.Button.gameObject.SetActive(false);
        }

        for (int i = 0; i < choices.Length; i++) {
            _buttons[i].Text.text = choices[i];
            _buttons[i].Button.gameObject.SetActive(true);
        }
    }

    private ChoiceButton AddChoiceButton()
    {
        GameObject button = UnityEngine.Object.Instantiate(_buttonPrefab, _root.transform);
        button.SetActive(false);
        
        ChoiceButton choiceButton = new()
        {
            Button = button.GetComponentInChildren<EventTrigger>(),
            Text = button.GetComponentInChildren<TMP_Text>()
        };

        _buttons.Add(choiceButton);
        _buttons[^1].Button.triggers[^1].callback.AddListener(OnButtonPressed);

        return choiceButton;
    }

    private void OnButtonPressed(BaseEventData data)
    {
        AddChoices();
        OnChoiceMade?.Invoke(_buttons.FindIndex(x => x.Button.gameObject == data.selectedObject));
    }
}