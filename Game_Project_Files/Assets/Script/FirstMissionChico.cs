using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstMissionChico : MonoBehaviour
{
    public GameObject _Chico;
    public GameObject _Pecuarista;
    public static bool _B = true;
    // Start is called before the first frame update
    void Start()
    {
        if (_B)
        {
            Debug.Log("aws");
            _B = false;
            TutorialMission.TextCurrentOrder = 3;
        }
        if (StartDialog2._TurnedOn)
        {
            _Chico.SetActive(true);
            _Pecuarista.SetActive(false);
        }
        else
        {
            _Chico.SetActive(false);
            _Pecuarista.SetActive(true);
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
