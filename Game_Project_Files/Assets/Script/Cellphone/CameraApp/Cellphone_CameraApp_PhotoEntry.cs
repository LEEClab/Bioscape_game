using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization;

[CreateAssetMenu(menuName="Cellphone/Camera App/Photo Entry")]
public class Cellphone_CameraApp_PhotoEntry : ScriptableObject
{
    public string _photoId;
    public LocalizedString _photoName;
    public Sprite _photoSpriteImage;
    public LocalizedString  _photoCaption;
}
