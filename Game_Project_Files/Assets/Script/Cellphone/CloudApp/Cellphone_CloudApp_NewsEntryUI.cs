using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Cellphone_CloudApp_NewsEntryUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _titleText;
    [SerializeField] private Image _iconImage;

    private Cellphone_CloudApp_NewsReport _report;
    private Cellphone_CloudApp_NewsAppSystem _manager;

    private string _readKey;

    // Initialize the entry with its _report data and _manager reference.
    public void Initialize(Cellphone_CloudApp_NewsReport _report, Cellphone_CloudApp_NewsAppSystem mgr)
    {
        this._report = _report;
        _manager = mgr;
        // _titleText.text = _report.title;
        // _iconImage.sprite = _report.icon;

        _report._title.StringChanged += value => _titleText.text = value;

        _report._title.RefreshString(); 

        // Optional: disable button/icon if locked (shouldn't be instantiated)
        GetComponent<Button>().onClick.AddListener(OnClick);

        _readKey = $"NewsEntry_{_report._id}"; // Unique key based on the title

        // Load read status
        if (PlayerPrefs.GetInt(_readKey, 0) == 1)
        {
            _titleText.color = new Color(0.75f, 0.3f, 1f, 1f);
        }
        else
        {
            _titleText.color = new Color(0f, 0f, 1f, 1f);
        }
    }

    private void OnClick()
    {
        _manager.ShowReport(_report);

        _titleText.color = new Color(0.75f, 0.3f, 1f, 1f); // Change color to purple
        PlayerPrefs.SetInt(_readKey, 1);
        PlayerPrefs.Save();
    }
    
}