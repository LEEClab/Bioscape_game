using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thoughts : MonoBehaviour
{
    public static Thoughts thoughts;
    public GameObject TextBoxA; // Primeiro TextBox
    public GameObject TextBoxB; // Segundo TextBox
    public bool _HasRunned = false;
    Transform _Child;
    public int _Order;


   // public int iteste;
    private void Start()
    {
        thoughts = this;

    }
    void Update()
    {
       // teste();
        // Se o TextBoxA estiver desativado, liga o TextBoxB
        if (PlayerPrefs.GetInt("_CanRun") == 1 && DataPersistenceManager.instance.CurrentOrder == _Order)
        {
            if (TextBoxA != null && TextBoxB != null)
            {
                if (!_HasRunned && !TextBoxA.activeSelf)
                {
                    TextBoxB.SetActive(true);
                    _Child = transform.GetChild(0);
                    _Child.gameObject.SetActive(true);
                    _HasRunned = true;
                }
            }
        }
    }
    private void OnApplicationQuit()
    {
        PlayerPrefs.SetInt("_CanRun", 0);
        DataPersistenceManager.instance.CurrentOrder = 1;
    }
 /*   public void teste()
    {
        Debug.Log(DataPersistenceManager.instance.CurrentOrder);
        DataPersistenceManager.instance.CurrentOrder = iteste;
    }*/

}

