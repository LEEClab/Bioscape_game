using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.UI;

public class DialogBox_v2 : MonoBehaviour
{
    public static DialogBox_v2 Instance { get; private set; }
    [SerializeReference] private AudioSource _characterVoice;
    [SerializeReference] private Image _characterImage;
    [SerializeReference] private TextMeshProUGUI _textField; // The gameboject "Text" will show the phrase

    private AudioClip _voiceSFX; // The voice SFX to play
    private LocalizedString _currentLine;
    private Coroutine _typingCoroutine;
    private bool _languageChanged;

    void Awake()
    {
        if (Instance != null && Instance != this) Destroy(gameObject);
        else Instance = this;
        _textField.gameObject.SetActive(false);
        gameObject.SetActive(false);
    }

    public void RunConversation(List<LocalizedString> _phrases, Sprite _characterSprite, AudioClip _voiceSFX){
        this._voiceSFX = _voiceSFX;
        if (PlayerController.playercontroller != null)
        {
            PlayerController.playercontroller.SetCanMove(false);
        }
        _characterImage.sprite = _characterSprite;
        _textField.gameObject.SetActive(true);
        gameObject.SetActive(true);

        StartCoroutine(BeginConversation(_phrases));
    }

    IEnumerator BeginConversation(List<LocalizedString> _phrases)
    {   
        

        foreach (var ls in _phrases)
        {
            if (_currentLine != null)
                _currentLine.StringChanged -= OnLanguageChanged;

            _currentLine = ls;
            _currentLine.StringChanged += OnLanguageChanged;

            var handle = _currentLine.GetLocalizedStringAsync();
            yield return handle;
        
            _typingCoroutine = StartCoroutine(RunLine(handle.Result));
            yield return _typingCoroutine;

            // Wait until the player releases the key
            yield return new WaitUntil(() => !Input.GetKey(KeyCode.Space));

            // Wait for the next press
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
        }
        EndDialog();
        
    }

    IEnumerator RunLine(string _line)
    {
        _languageChanged = false;
        _textField.text = "";
        for (int i = 0; i < _line.Length; i++)
        {
            Debug.Log(_languageChanged);
            // If the player hits Space, show the rest and exit
            if (Input.GetKeyDown(KeyCode.Space) || _languageChanged)
            {
                _textField.text = _line;
                _characterVoice.PlayOneShot(_voiceSFX);
                break;
            }

            _textField.text = _line.Substring(0, i + 1);
            _characterVoice.PlayOneShot(_voiceSFX);

            // Every frame, detect if they skip
            float t = 0f;
            while (t < 0.08f && !_languageChanged && !Input.GetKeyDown(KeyCode.Space))
            {
                t += Time.deltaTime;
                yield return null;
            }
        }
        _characterVoice.Stop();
        _textField.text = _currentLine.GetLocalizedStringAsync().Result; // Show the full line after typing
        
        
    }

    public void EndDialog()
    {
        _textField.gameObject.SetActive(false);
        gameObject.SetActive(false);
        if (PlayerController.playercontroller != null)
        {
            PlayerController.playercontroller.SetCanMove(true);
        }
    }

    private void OnLanguageChanged(string newText)
    {
        _languageChanged = true;
    }
}
