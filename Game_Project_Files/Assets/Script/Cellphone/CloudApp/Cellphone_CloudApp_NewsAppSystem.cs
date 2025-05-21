using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class Cellphone_CloudApp_NewsAppSystem : MonoBehaviour
{
    [Header("Data & Prefabs")]
    [Tooltip("All available news reports (ScriptableObjects)")]
    [SerializeField] private List<Cellphone_CloudApp_NewsReport> allReports;
    [SerializeField] private GameObject entryPrefab;

    [Header("UI References")]
    [SerializeField] private Transform _listContainer;
    [SerializeField] private GameObject _noSignalPanel;
    [SerializeField] private GameObject _reportPanel;
    [SerializeField] private TextMeshProUGUI _reportTitle;
    [SerializeField] private TextMeshProUGUI _reportContent;
    [SerializeField] private Image _reportImage;
    [SerializeField] private Button _closeReportButton;

    private int _currentMissionIndex => GameProgress.Instance.CurrentNewsIndex;
    private Cellphone_CloudApp_NewsReport _currentReport;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.J))
        {
            GameProgress.Instance.IncrementMission();
            RefreshList();
            Debug.Log("Mission Index: " + GameProgress.Instance.CurrentNewsIndex);
        }
    }

    private void OnEnable()
    {
        RefreshList();
        _closeReportButton.onClick.AddListener(CloseReport);
    }


    // Rebuilds the news list UI based on unlocked reports.
    public void RefreshList()
    {
        // Clear existing entries
        foreach (Transform child in _listContainer)
            Destroy(child.gameObject);

        // Filter unlocked
        var unlocked = allReports.FindAll(r => _currentMissionIndex >= r._unlockAfterMission);

        if (unlocked.Count == 0)
        {
            _noSignalPanel.SetActive(true);
            _reportPanel.SetActive(false);
            return;
        }

        _noSignalPanel.SetActive(false);

        // Populate list
        foreach (var report in unlocked)
        {
            var entryGO = Instantiate(entryPrefab, _listContainer);
            var entryUI = entryGO.GetComponent<Cellphone_CloudApp_NewsEntryUI>();
            entryUI.Initialize(report, this);
        }
    }


    // Shows a full report page for the given report.
    public void ShowReport(Cellphone_CloudApp_NewsReport report)
    {
        _currentReport = report;
        // Title
        report._title.StringChanged += OnReportTitleChanged;
        report._title.RefreshString();

        // Body
        report._contentText.StringChanged += OnReportContentChanged;
        report._contentText.RefreshString();

        // Image
        _reportImage.sprite = report._contentImage;

        _reportPanel.SetActive(true);
    }

    private void OnReportTitleChanged(string value)
    {
        _reportTitle.text = value;
    }

    private void OnReportContentChanged(string value)
    {
        _reportContent.text = value;
    }

    private void CloseReport()
    {
        _reportPanel.SetActive(false);

        if (_currentReport != null)
        {
            _currentReport._title.StringChanged   -= OnReportTitleChanged;
            _currentReport._contentText.StringChanged -= OnReportContentChanged;
            _currentReport = null;
        }
    }
}
