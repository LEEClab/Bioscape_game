using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Components;

[CreateAssetMenu(fileName = "NewNewsReport", menuName = "Cellphone/Cloud App/News Report")]
public class Cellphone_CloudApp_NewsReport : ScriptableObject
{
    public LocalizedString _title;
    public Sprite _icon;
    public LocalizedString _contentText;
    public Sprite _contentImage;
    [Tooltip("The mission index at which this report unlocks (e.g., 1 = after Mission1 completes)")]
    public int _unlockAfterMission;
    public string _id;
}