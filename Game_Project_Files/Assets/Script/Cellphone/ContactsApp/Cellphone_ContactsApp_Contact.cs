using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[CreateAssetMenu(fileName = "NewContact", menuName = "Cellphone/Contacts App/Contact")]
public class Cellphone_ContactsApp_Contact : ScriptableObject
{
    public string _contactName;
    public Sprite _profileImage;
    public string _dialogNpcId;
    public int _unlockAfterDialog;
    public Contacts_DialogChoiceNode _startingDialogNode;
}
