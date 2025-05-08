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
            _currentPageIndex++;
            _currentEntry = _galleryPhotoEntries[_currentPageIndex];
            _galleryUI.UpdateGalleryUI(_currentEntry); //Update the UI with the next page
        }
    }

    public void PreviousPage()
    {
        if (_currentPageIndex > 0)
        {
            _currentPageIndex--;
            _currentEntry = _galleryPhotoEntries[_currentPageIndex];
            _galleryUI.UpdateGalleryUI(_currentEntry); //Update the UI with the previous page
        }
    }

}
