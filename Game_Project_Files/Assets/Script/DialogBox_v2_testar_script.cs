using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization;

public class DialogBox_v2_testar_script : MonoBehaviour
{
    [SerializeField] private List<LocalizedString> _lines;
    [SerializeField] private Sprite _characterSprite;
    [SerializeField] private AudioClip _voiceSFX;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.X))
        {
            DialogBox_v2.Instance.RunConversation(_lines, _characterSprite, _voiceSFX);
        }
    }
}
