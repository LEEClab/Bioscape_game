using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cellphone_CameraApp_PhotoTrigger : MonoBehaviour
{
    [SerializeField] private Cellphone_CameraApp_PhotoEntry _photoEntry;
    [SerializeField] private Cellphone_CameraAppSystem _cameraAppSystem;
    private SpriteRenderer _cameraIcon;
    private bool _near;

    private void Awake()
    {
        _cameraIcon = GetComponent<SpriteRenderer>();
        _cameraIcon.enabled = false;
    }
    void OnTriggerEnter2D(Collider2D c)
    {
        _near = true;
        _cameraIcon.enabled = true;
        _cameraAppSystem.SetPhoto(_photoEntry._photoSpriteImage, _photoEntry._photoId);
    }
    void OnTriggerExit2D(Collider2D c)
    {
        _near = false;
        _cameraIcon.enabled =false;
        _cameraAppSystem.UnsetPhoto();
    }

    void Update()
    {
        if (_near && Input.GetKeyDown(KeyCode.E))
        {            
            _cameraAppSystem.OpenCameraApp();
        }
    }

}
