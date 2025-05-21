using System.Collections.Generic;
using UnityEngine;

public class GameProgress : MonoBehaviour
{
    public static GameProgress Instance { get; private set; }

    private const string NewsKey  = "CurrentNewsIndex";
    private const string DialogsKey  = "TriggeredDialogs"; //comma-separated list for all triggered dialogs
    private const string PhotosKey = "CapturedPhotos";

    public int CurrentNewsIndex { get; private set; } = 0;

    // Tracks which dialog IDs have fired
    private HashSet<string> _triggeredDialogs = new HashSet<string>();

    // Tracks which photos have been captured
    private HashSet<string> _capturedPhotos = new HashSet<string>();

    private const string TasksKey = "CompletedTasks";
    private HashSet<string> _completedTasks = new HashSet<string>();

    private void Awake()
    {
        // Singleton setup
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        // Load saved progress
        LoadProgress();
    }
    public void IncrementMission()
    {
        CurrentNewsIndex++;
        SaveProgress();
    }

    // Call this when you complete a mission
    public void SetMission(int newsIndex)
    {
        CurrentNewsIndex = newsIndex;
        SaveProgress();
    }

    // Call this whenever a dialog with ID is triggered
    public void TriggerDialog(string dialogId)
    {
        if (_triggeredDialogs.Add(dialogId))
        {
            SaveProgress(); // only save when it’s new
        }
    }

    // Check if dialog has run before
    public bool HasTriggeredDialog(string dialogId)
    {
        return _triggeredDialogs.Contains(dialogId);
    }

    private void SaveProgress()
    {
        PlayerPrefs.SetInt(NewsKey, CurrentNewsIndex);

        // Join our HashSet into a single string, e.g. "intro,meetBob,finale"
        var joinedDialogs = string.Join(",", _triggeredDialogs);
        PlayerPrefs.SetString(DialogsKey, joinedDialogs);

        // Save the captured photos
        var captured = string.Join(",", _capturedPhotos);
        PlayerPrefs.SetString(PhotosKey, captured);

        //Join completed tasks
        var joinedTasks = string.Join(",", _completedTasks);
        PlayerPrefs.SetString(TasksKey, joinedTasks);

        PlayerPrefs.Save();
    }

    private void LoadProgress()
    {
        CurrentNewsIndex = PlayerPrefs.GetInt(NewsKey, 0);

        var savedDialogs = PlayerPrefs.GetString(DialogsKey, "");
        if (!string.IsNullOrEmpty(savedDialogs))
        {
            _triggeredDialogs = new HashSet<string>(savedDialogs.Split(','));
        }

        var savedPhotos = PlayerPrefs.GetString(PhotosKey, "");
        if (!string.IsNullOrEmpty(savedPhotos))
        {
            _capturedPhotos = new HashSet<string>(savedPhotos.Split(','));
        }

        var savedTaks = PlayerPrefs.GetString(TasksKey, "");
        if (!string.IsNullOrEmpty(savedTaks))
            _completedTasks = new HashSet<string>(savedTaks.Split(','));

    }

    public bool CapturePhoto(string entryId)
    {
        if(_capturedPhotos.Add(entryId)){
            SaveProgress();
            return true;
        }
        else{
            Debug.Log("Photo already captured: " + entryId);
            return false;
        }
        
    }

    public bool HasCaptured(string entryId){
        return _capturedPhotos.Contains(entryId);
    }

    public void CompleteTask(string taskId)
    {
        if (_completedTasks.Add(taskId))
            SaveProgress();
    }

    public bool HasCompletedTask(string taskId)
        => _completedTasks.Contains(taskId);

}
