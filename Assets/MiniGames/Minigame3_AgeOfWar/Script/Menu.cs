using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    private GameObject mainMenu;
    private GameObject unitMenu;
    private GameObject turretMenu;
    Button[,] menuButton = new Button[3, 5];
    //closeButton.onClick.AddListener(Close);

    private void Awake()
    {
        Transform menuTr;
        for(int i = 0; i <3; i++)
        {
            menuTr = transform.GetChild(i).GetChild(0);
            for (int j=0;j<5; j++)
            {
                menuButton[i, j] = menuTr.GetChild(j).GetComponent<Button>();
            }
        }
    }
    private void Start()
    {
        mainMenu = transform.GetChild(0).gameObject;
        unitMenu = transform.GetChild(1).gameObject;
        turretMenu = transform.GetChild(2).gameObject;

        unitMenu.SetActive(false);
        turretMenu.SetActive(false);
        ButtonInitialize();
    }

    //함수부-------------------------------------------------------------------------
    void ButtonInitialize()
    {
        menuButton[0, 4].onClick.AddListener(GameManager.Inst.Revol);
    }
    
    //메뉴 이동
    public void OnClickUnitMenu()
    {
        mainMenu.SetActive(false);
        unitMenu.SetActive(true);
    }
    public void OnClickBack()
    {
        unitMenu.SetActive(false);
        turretMenu.SetActive(false);
        mainMenu.SetActive(true);
    }
    public void OnClickTurret()
    {
        unitMenu.SetActive(false);
        turretMenu.SetActive(true);
        mainMenu.SetActive(false);
    }
}
