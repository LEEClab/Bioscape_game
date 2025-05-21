using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Localization;

public class Cellphone_NotesApp_TaskEntryUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _taskText;

    private LocalizedString _currentDesc;
    private int _index;

    public void Initialize(int i, LocalizedString desc, bool done)
    {
        _index = i;
        
        if (_currentDesc != null)
        _currentDesc.StringChanged -= OnLanguageChanged;

        _currentDesc = desc;
        _currentDesc.StringChanged += OnLanguageChanged;


        desc.RefreshString();

        
        if (done){
            _taskText.fontStyle |= FontStyles.Strikethrough;
            _taskText.color = new Color(0f, 0f, 0f, 0.67f); // grey color
            Debug.Log("Concluded: " + desc);
        }
            
        else{
            _taskText.fontStyle &= ~FontStyles.Strikethrough;
        }
            
    }

    private void OnLanguageChanged(string newValue)
    {
        _taskText.text = _index + ". " + newValue;
    }

    private void OnDestroy()
{
    if (_currentDesc != null)
        _currentDesc.StringChanged -= OnLanguageChanged;
}
}
