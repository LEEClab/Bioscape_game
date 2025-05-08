using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Localization.Plugins.XLIFF.V12;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;

public class Cellphone_NotesApp : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private PlayerController _Player; //This object will serve to stop player movement when writing in the notes

    [SerializeField] private TMP_InputField _NotesTMPro; //Text field where the player types the notes

    [SerializeField] private TMP_Text _PageIndicatorTMPro; //Simple text that shows what page the player is currently at

    [SerializeField] private TMP_Text _currentPlaceholder; //string that stores the old placeholder for when the laguage is changed

    [SerializeField] private  LocalizedString _localizedString; //String to display sample text in different languages

    [SerializeField] private AudioClip _SFXPageFlipping; //AudioClip containing the sound of flipping a page

    [SerializeField] private AudioSource _audioSource; //AudioSource to play audio clip

    private string[] notes = new string [10]; //array of strings where each element of the array is a page of the notes

    private int currentPage = 0; //keeps track of which page the player is at the moment. It always starts at the first page
    void Start()
    {
        for(int i=0; i<10; i++){
            
            notes[i] = PlayerPrefs.GetString("_NotesAppPage" + i.ToString()); //Initializes the vector with the saved notes.
            // if(notes[i].Equals("")){ //If it's blank, it was never initialized or was erased, so a sample text is shown instead of nothing.
            //     notes[i] = "";
                
            // }
            // Debug.Log(notes[i]);
        }
        ReloadStoredPageToTextField(); //Sets the text to the first page
        
    }

     private void OnEnable() //Whenever the language is changed, it updates the placeholder text
    {
        _localizedString.StringChanged += UpdateMessage;
    }

    private void OnDisable()
    {
        _localizedString.StringChanged -= UpdateMessage;
    }

    private void UpdateMessage(string value)
    {
        _currentPlaceholder.text = value;
    }

    public void SaveNotes()
    {
        StorePageInArray();
        for(int i=0; i<10; i++){
            PlayerPrefs.SetString("_NotesAppPage" + i.ToString(), notes[i]); //Stores all the notes. The PlayerPref name is "_NotesAppPageXX", where the XX is the number of the page.
        
        }
        
        PlayerPrefs.Save();
    }

    public void NextPage()
    {
        if(currentPage == 9){ //Can't go past the tenth page
            Debug.Log("Already at the end.");
            return;
        }
        StorePageInArray(); //stores the current page before moving to the next
        
        currentPage++; //increases current page
        PlaySFXPageFlipping(1.2f, 1.5f);
        _PageIndicatorTMPro.text = (currentPage+1).ToString() + "/10"; //Updates page indicator
        
        ReloadStoredPageToTextField();
    }
    public void PreviousPage()
    {
        if(currentPage == 0){
            Debug.Log("Already at the start.");
            return;
        }
        StorePageInArray();

        currentPage--;
        PlaySFXPageFlipping(1.0f, 1.3f);
        _PageIndicatorTMPro.text = (currentPage+1).ToString() + "/10";
        ReloadStoredPageToTextField();
    }

    private void StorePageInArray(){
        notes[currentPage] = _NotesTMPro.text; //Stores in array the input text
    }

    private void ReloadStoredPageToTextField(){
        _NotesTMPro.text = notes[currentPage]; //Updates content for current page
    }

    private void PlaySFXPageFlipping(float _lowerPitch, float _higherPitch){
        _audioSource.volume = 0.5f;
        _audioSource.pitch = Random.Range(_lowerPitch, _higherPitch); //Randomizes the pitch of the audio to create variability
        _audioSource.PlayOneShot(_SFXPageFlipping);
    }

    public void StopPlayerMovement()
    {
        _Player._InDialog = false;
    }

    public void FreePlayerMovement()
    {
        _Player._InDialog = true;
    }
}
