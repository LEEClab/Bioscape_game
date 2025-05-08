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
            _pageTitle.text = photoEntry._photoName.GetLocalizedString();
            _pageDescription.text = photoEntry._photoCaption.GetLocalizedString();
            _pageImage.sprite = photoEntry._photoSpriteImage;
            return;
        }
        _pageTitle.text = "???";
        _pageDescription.text = "????????????";
        _pageImage.sprite = _placeholderSprite; // Set to placeholder sprite
    
    }
}
