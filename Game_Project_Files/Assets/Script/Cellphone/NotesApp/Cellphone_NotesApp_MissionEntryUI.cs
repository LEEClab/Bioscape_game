using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.UI;

public class Cellphone_NotesApp_MissionEntryUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _missionName;
    [SerializeField] private LocalizedString _wordMission;
    private Cellphone_NotesApp_NotePage page;
    private System.Action<Cellphone_NotesApp_NotePage> onClick;

    public void Initialize(Cellphone_NotesApp_NotePage p, System.Action<Cellphone_NotesApp_NotePage> callback)
    {
        page = p;
        onClick = callback;
        _wordMission.StringChanged -= OnLanguageChanged;
        _wordMission.StringChanged += OnLanguageChanged;
        _wordMission.RefreshString();

        GetComponent<Button>().onClick.AddListener(() => onClick(page));
    }

    private void OnLanguageChanged(string newText) => _missionName.text = newText + " " + page._missionIndex;
}
