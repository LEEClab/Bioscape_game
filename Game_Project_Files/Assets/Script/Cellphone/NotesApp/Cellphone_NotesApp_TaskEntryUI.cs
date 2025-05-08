using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Cellphone_NotesApp_TaskEntryUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _taskText;

    public void Initialize(int i, string desc, bool done)
    {
        _taskText.text = i + ". " + desc;
        
        if (done){
            _taskText.fontStyle |= FontStyles.Strikethrough;
            Debug.Log("Concluded: " + desc);
        }
            
        else{
            _taskText.fontStyle &= ~FontStyles.Strikethrough;
        }
            
    }
}
