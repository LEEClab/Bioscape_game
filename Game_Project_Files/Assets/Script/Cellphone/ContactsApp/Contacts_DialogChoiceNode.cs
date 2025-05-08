    using UnityEngine;
using UnityEngine.Localization;

[CreateAssetMenu(menuName = "Cellphone/Contacts App/Dialog Node", fileName = "DialogNode")]

public class Contacts_DialogChoiceNode : ScriptableObject
{
    public LocalizedString _lineText;    // what NPC says
    public Choice[] _choices;   // up to 2 choices

    [System.Serializable]
    public struct Choice
    {
        public LocalizedString _choiceText;   // text on button
        public Contacts_DialogChoiceNode _nextNode;   // where to go next (null = end)
        public int _moneyDelta; // effect
    }

    public bool HasChoices => _choices != null && _choices.Length > 0; // check if there are choices
}
