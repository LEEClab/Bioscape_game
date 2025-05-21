using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Localization;
using UnityEngine.UI;

public class Contacts_DialogChoiceManager : MonoBehaviour
{
    [SerializeField] private Button _buttonA, _buttonB; // Buttons for choices
    [SerializeField] private TextMeshProUGUI _npcDialogText, _textA, _textB; // Text fields for dialog and choices
    [SerializeField] AudioSource _characterVoice;
    [SerializeField] private Button _closeAppButton;

    // private Contacts_DialogChoiceNode _currentNode;

    private void Awake()
    {
        _buttonA.gameObject.SetActive(false);
        _buttonB.gameObject.SetActive(false);
    }

    public void BeginConversation(Contacts_DialogChoiceNode node)
    {
        
        _closeAppButton.gameObject.SetActive(false);
        PlayerController.playercontroller.SetCanMove(false); // Set player state to in dialog
        StartCoroutine(RunConversation(node));
        
    }

    // Coroutine to run the conversation, from start node to end node
    IEnumerator RunConversation(Contacts_DialogChoiceNode start)
    {
        var node = start;

        while (node != null)
        {
            yield return DisplayLine(node._lineText);

            if (node.HasChoices) 
            {
                Contacts_DialogChoiceNode.Choice picked = default;
                yield return PresentChoices(node._choices, choice => picked = choice);
                node = picked._nextNode;  // advance to the next nod
            }
            else
            {
                break;
            }
        }

        EndDialog();  // once node == null or no choices left
    }

    // Coroutine to present the choices to the player
    public IEnumerator PresentChoices(
    Contacts_DialogChoiceNode.Choice[] choices,
    Action<Contacts_DialogChoiceNode.Choice> onChosen)
    {
        // 1) Clear any old listeners and ensure _buttons are visible
        _buttonA.onClick.RemoveAllListeners();
        _buttonB.onClick.RemoveAllListeners();
        _buttonA.gameObject.SetActive(true);
        _buttonB.gameObject.SetActive(true);

        _textA.text = choices[0]._choiceText.GetLocalizedString();
        if (choices.Length > 1)
        {
            _textB.text = choices[1]._choiceText.GetLocalizedString();
            _buttonB.gameObject.SetActive(true);
        }
        else
        {
            // if only one choice, hide the second _button
            _textB.text = "";
            _buttonB.gameObject.SetActive(false);
        }

        
        bool chosen = false;
        Contacts_DialogChoiceNode.Choice selectedChoice = default;

        // Hook up the callbacks
        _buttonA.onClick.AddListener(() =>
        {
            selectedChoice = choices[0];
            chosen = true;
        });

        if (choices.Length > 1)
        {
            _buttonB.onClick.AddListener(() =>
            {
                selectedChoice = choices[1];
                chosen = true;
            });
        }

        yield return new WaitUntil(() => chosen); // Wait for the player to click one of the _buttons

        _buttonA.onClick.RemoveAllListeners();
        _buttonB.onClick.RemoveAllListeners();
        _buttonA.gameObject.SetActive(false);
        _buttonB.gameObject.SetActive(false);
        _textA.text = _textB.text = _npcDialogText.text = "";

        
        onChosen?.Invoke(selectedChoice); // Invoke the callback so the caller knows what was picked
    }

    // Hide the choices and clear the text
    public void HideChoices()
    {
        _buttonA.gameObject.SetActive(false);
        _buttonB.gameObject.SetActive(false);
        _textA.text = _textB.text = _npcDialogText.text = "";
        _buttonA.onClick.RemoveAllListeners();
        _buttonB.onClick.RemoveAllListeners();
    }

    // Coroutine to display a text, with typewriter effect and voice
    IEnumerator DisplayLine(LocalizedString lineText)
    {
        // Get the character's dialogue
        var handle = lineText.GetLocalizedStringAsync();
        yield return handle;
        string full = handle.Result;
        _characterVoice.pitch = 1.5f;

        _npcDialogText.text = "";
        for (int i = 0; i < full.Length; i++)
        {
            // If the player hits Space, show the rest and exit
            if (Input.GetKeyDown(KeyCode.Space))
            {
                _npcDialogText.text = full;
                _characterVoice.Play();
                break;
            }

            _npcDialogText.text = full.Substring(0, i + 1);
            _characterVoice.Play();

            // Every frame, detect if they skip
            float t = 0f;
            while (t < 0.08f)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    _npcDialogText.text = full;
                    _characterVoice.Play();
                    i = full.Length; // force exit outer for
                    break;
                }
                t += Time.deltaTime;
                yield return null;
            }
        }
        _characterVoice.Stop();
        _characterVoice.pitch = 1.0f;
        _npcDialogText.text = full;
    }

    public void EndDialog()
    {
        Debug.Log("Dialog ended.");
        HideChoices();
        _closeAppButton.gameObject.SetActive(true);
        gameObject.SetActive(false);
        PlayerController.playercontroller.SetCanMove(true); // Set player state back to normal
        // _currentNode = null;
    }
}
