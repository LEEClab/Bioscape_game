using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Cellphone_DiaryApp_GalleryUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _pageTitle;
    [SerializeField] private TextMeshProUGUI _pageDescription;
    [SerializeField] private UnityEngine.UI.Image _pageImage;
    [SerializeField] private Sprite _placeholderSprite;

    public void UpdateGalleryUI(Cellphone_CameraApp_PhotoEntry photoEntry)
    {
        if(GameProgress.Instance.HasCaptured(photoEntry._photoId)){ // Check if the photo has been unlocked
            photoEntry._photoName.StringChanged -= OnTitleChanged;
            photoEntry._photoCaption.StringChanged -= OnCaptionChanged;

            photoEntry._photoName.StringChanged += OnTitleChanged;
            photoEntry._photoCaption.StringChanged += OnCaptionChanged;

            _pageImage.sprite = photoEntry._photoSpriteImage;

            photoEntry._photoName.RefreshString();
            photoEntry._photoCaption.RefreshString();
            return;
        }
        _pageTitle.text = "???";
        _pageDescription.text = "????????????";
        _pageImage.sprite = _placeholderSprite; // Set to placeholder sprite
    
    }
    private void OnTitleChanged(string newText) => _pageTitle.text = newText;
    private void OnCaptionChanged(string newText) => _pageDescription.text = newText;
}
