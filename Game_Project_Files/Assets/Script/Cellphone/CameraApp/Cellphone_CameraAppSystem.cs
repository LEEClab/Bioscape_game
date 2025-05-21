using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;
using UnityEngine.UI;

public class Cellphone_CameraAppSystem : MonoBehaviour
{
    [SerializeField] private GameObject _cellContent;
    [SerializeField] private GameObject _homeScreenPanel;
    [SerializeField] private GameObject _rootPanel;
    [SerializeField] private UnityEngine.UI.Image _photoImage;
    [SerializeField] private Button _takeButton;
    [SerializeField] private Sprite _placeholderImage;
    [SerializeField] private UnityEngine.UI.Image _flashImage;
    private string _currentPhotoId;

    private void Awake()
    {
        if (string.IsNullOrEmpty(_currentPhotoId))
            _photoImage.sprite = _placeholderImage;
        _takeButton.onClick.AddListener(OnTakeClicked);
    }

    public void SetPhoto(Sprite photoSprite, string photoId){
        _photoImage.sprite = photoSprite;
        _currentPhotoId = photoId;
    }
    public void UnsetPhoto(){
        _photoImage.sprite = _placeholderImage;
        _currentPhotoId = null;
    }  
    public void OpenCameraApp()
    {
        Debug.Log("Open Camera App");
        if (gameObject.activeSelf) return;
        
        _homeScreenPanel.SetActive(false);
        _rootPanel.SetActive(true);
        gameObject.SetActive(true);
        _cellContent.SetActive(true);
    }
    private void OnTakeClicked()
    {
        if (string.IsNullOrEmpty(_currentPhotoId))
        {
            Debug.Log("Not near the photo trigger.");
            return;
        }
        else
        {
            StartCoroutine(TakePhotoRoutine());
        }
        
    }

    private IEnumerator TakePhotoRoutine()
    {
        StopCoroutine("TakePhotoRoutine");
        _flashImage.gameObject.SetActive(true);
        float t = 1f;
        while (t > 0f)
        {
            t -= Time.deltaTime * 2f;
            Color c = _flashImage.color; c.a = t; _flashImage.color = c;
            yield return null;
        }
        _flashImage.gameObject.SetActive(false);

        GameProgress.Instance.CapturePhoto(_currentPhotoId);

        // _rootPanel.SetActive(false);
        PlayerController.playercontroller.SetCanMove(true);
    }
}
