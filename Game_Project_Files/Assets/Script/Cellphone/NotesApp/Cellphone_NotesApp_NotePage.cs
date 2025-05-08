using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization;

[CreateAssetMenu(menuName="Cellphone/Notes App/Mission Page")]
public class Cellphone_NotesApp_NotePage : ScriptableObject
{
    public int _missionIndex;
    public TaskEntry[] _tasks;

    [System.Serializable]
    public struct TaskEntry
    {
        public string _taskId;
        public LocalizedString _taskDescription;
    }
}
