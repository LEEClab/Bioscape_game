using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cellphone_DiaryApp_GallerySystem : MonoBehaviour
{
    [SerializeField] private Cellphone_DiaryApp_GalleryUI _galleryUI; //Reference to the UI component that displays the gallery page
    [SerializeField] private List<Cellphone_CameraApp_PhotoEntry> _galleryPhotoEntries; //List of gallery pages to be displayed
    [SerializeField] private Button _nextButton;
    [SerializeField] private Button _previousButton;
    [SerializeField] private AudioSource _audioSource; //Audio source for playing sounds
    [SerializeField] private AudioClip _flipPageSound; //Sound to play when navigating pages
    private int _currentPageIndex = 0; //Index of the current page being displayed
    private Cellphone_CameraApp_PhotoEntry _currentEntry;
    private void Start()
    {
        _currentEntry = _galleryPhotoEntries[_currentPageIndex]; //Get the first page entry
        _galleryUI.UpdateGalleryUI(_currentEntry); //Initialize the gallery UI with the first page
        _nextButton.onClick.AddListener(NextPage); //Add listener for next button
        _previousButton.onClick.AddListener(PreviousPage); //Add listener for previous button
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P)) //Debug key to unlock photo
        {
            GameProgress.Instance.CapturePhoto("03");
        }
        if(Input.GetKeyDown(KeyCode.O)) //Debug key to unlock photo
        {
            GameProgress.Instance.CapturePhoto("02");
        }
    }

    public void NextPage()
    {
        if (_currentPageIndex < _galleryPhotoEntries.Count - 1)
        {
            PlaySFXPageFlipping(1.2f, 1.4f);
            _audioSource.PlayOneShot(_flipPageSound); //Play the flip page sound
            _currentPageIndex++;
            _currentEntry = _galleryPhotoEntries[_currentPageIndex];
            _galleryUI.UpdateGalleryUI(_currentEntry); //Update the UI with the next page
        }
    }

    public void PreviousPage()
    {
        if (_currentPageIndex > 0)
        {
            PlaySFXPageFlipping(1.1f, 1.3f);
            _audioSource.PlayOneShot(_flipPageSound); //Play the flip page sound
            _currentPageIndex--;
            _currentEntry = _galleryPhotoEntries[_currentPageIndex];
            _galleryUI.UpdateGalleryUI(_currentEntry); //Update the UI with the previous page
        }
    }

    private void PlaySFXPageFlipping(float _lowerPitch, float _higherPitch){
        _audioSource.volume = 0.5f;
        _audioSource.pitch = Random.Range(_lowerPitch, _higherPitch); //Randomizes the pitch of the audio to create variability
        _audioSource.PlayOneShot(_flipPageSound); //Play the page flip sound
    }


}
