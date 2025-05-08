using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class Cellphone_ContactsApp_ContactEntryUI : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI _contactNameText;
    [SerializeField] private Image _profileImage;

    private Cellphone_ContactsApp_Contact _contact;
    private Cellphone_ContactsAppSystem _manager;

    private string readKey;

    public void Initialize(Cellphone_ContactsApp_Contact _contact, Cellphone_ContactsAppSystem mgr)
    {
       
        this._contact = _contact;
        _manager = mgr;
        _contactNameText.text = _contact.name;
        _profileImage.sprite = _contact._profileImage;


        // Optional: disable button/icon if locked (shouldn't be instantiated)
        GetComponent<Button>().onClick.AddListener(OnClick);

        
    }
    private void OnClick()
    {
        _manager.ShowContact(_contact);

    }
}
