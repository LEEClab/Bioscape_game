using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Cellphone : MonoBehaviour
{
    public GameObject _CellContent;
    public GameObject _ApksChild;

    public PlayerController _Player;


    // Start is called before the first frame update

    
    void Start()
    {
        _CellContent.SetActive(false);
        //DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K) && _Player._InDialog)
        {
            if (_CellContent.activeSelf)
            {
                HideCell();
            }
            else
            {
                ShowCell();
            }
        }

    }

    public void DisableAllChildren()
    {
        foreach (Transform child in _ApksChild.transform)
        {
            child.gameObject.SetActive(false);
        }
    }

    public void ShowCell()
    {
        _CellContent.SetActive(true);
    }
    public void HideCell()
    {
        _CellContent.SetActive(false);
    }

    
}

