using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Cellphone_NotesAppSystem : MonoBehaviour
{
    [SerializeField] private Cellphone_NotesApp_NotePage[] _allPages;
    [SerializeField] private GameObject _missionListScreen;
    [SerializeField] private Transform _missionListContent;
    [SerializeField] private GameObject _taskListScreen;
    [SerializeField] private Transform _taskListContent;
    [SerializeField] private Button _backButton;
    [SerializeField] private GameObject _missionButtonPrefab;
    [SerializeField] private GameObject _taskEntryPrefab;

    private void Awake()
    {
        _backButton.onClick.AddListener(ShowMissionList);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.N)){
            GameProgress.Instance.CompleteTask("M1_CleanBathroom");
            PopulateMissions();
            print("Task Completed!");
        }
        
    }

    private void OnEnable()
    {
        ShowMissionList();
    }

    void ShowMissionList()
    {
        _taskListScreen.SetActive(false);
        _missionListScreen.SetActive(true);
        PopulateMissions();
    }

    void PopulateMissions()
    {
        // clear
        foreach (Transform t in _missionListContent) Destroy(t.gameObject);

        // instantiate one per page
        foreach (var page in _allPages.OrderBy(p => p._missionIndex))
        {
            var go = Instantiate(_missionButtonPrefab, _missionListContent);
            var missionUI = go.GetComponent<Cellphone_NotesApp_MissionEntryUI>();
            missionUI.Initialize(page, OnMissionSelected);
        }
    }

    void OnMissionSelected(Cellphone_NotesApp_NotePage page)
    {
        _missionListScreen.SetActive(false);
        _taskListScreen.SetActive(true);
        PopulateTasks(page);
    }

    void PopulateTasks(Cellphone_NotesApp_NotePage page)
    {
        foreach (Transform t in _taskListContent) Destroy(t.gameObject);
        int i=0;
        foreach (var task in page._tasks)
        {
            i++;
            var go = Instantiate(_taskEntryPrefab, _taskListContent);
            var taskUI = go.GetComponent<Cellphone_NotesApp_TaskEntryUI>();
            bool done = GameProgress.Instance.HasCompletedTask(task._taskId);
            taskUI.Initialize(i, task._taskDescription, done);
        }
    }

}