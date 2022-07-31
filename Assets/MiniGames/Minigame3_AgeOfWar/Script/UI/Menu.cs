using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    private GameObject mainMenu;
    private GameObject unitMenu;
    private GameObject turretMenu;
    private Button cancelTurretButton;
    Button[,] menuButton = new Button[3, 5];
    ButtonDetail detail;
    TempTurretImage tempTrImage;
    TurretSlotUI[] turretSlots;
    public bool isSellTurret = false;
    //델리게이트---------------------------------------------
    public System.Action OnClickTurretButton;
    public System.Action OnClickTurretCancel;
    //--------------------------------------------------------

    private void Awake() //menuButton(버튼배열) 정리
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
        turretSlots=GetComponentsInChildren<TurretSlotUI>();
    }
    private void Start() // 메뉴별로 정리 템프터렛이미지, 상세정보
    {
        mainMenu = transform.GetChild(0).gameObject;
        unitMenu = transform.GetChild(1).gameObject;
        turretMenu = transform.GetChild(2).gameObject;
        cancelTurretButton = transform.Find("Cancel").GetComponent<Button>();
        tempTrImage = GetComponentInChildren<TempTurretImage>();
        tempTrImage.hide();
        detail =transform.GetChild(3).GetComponent<ButtonDetail>();
        InitailizeTurretSlot();

        
        ButtonInitialize();
        IdInitializeID();
        unitMenu.SetActive(false);
        turretMenu.SetActive(false);
        OnClickTurretButton += OnOffTurretButon;
        OnClickTurretCancel += OnOffTurretButon;
        OnClickTurretCancel += OffIsSell;
        CancelTurret();
    }
    //프로퍼티--------------------------------------------------------------
    public ButtonDetail Detail => detail;
    public TempTurretImage TempTrImage => tempTrImage;
    
    //함수부-------------------------------------------------------------------------
    void InitailizeTurretSlot()
    {
        turretSlots[1].gameObject.SetActive(false);
        turretSlots[2].gameObject.SetActive(false);
    }
    void AddTurretSlot()
    {
        
        if(GameManager.Inst.NumTurretSlot<2)
        {
            GameManager.Inst.AddTurretSlot();
            turretSlots[GameManager.Inst.NumTurretSlot].gameObject.SetActive(true);
        }
       
    }
    void ButtonInitialize()
    {
        menuButton[0, 4].onClick.AddListener(GameManager.Inst.Revol);
        menuButton[0, 3].onClick.AddListener(AddTurretSlot);
        menuButton[1, 4].onClick.AddListener(OnClickBack);
        menuButton[2, 4].onClick.AddListener(OnClickBack);
        menuButton[2, 0].onClick.AddListener(MakeTurret);  //tmepTurretimage Expose+OnOff
        cancelTurretButton.onClick.AddListener(CancelTurret); //tmepTurretimage hide +OnOff
        menuButton[0, 2].onClick.AddListener(SellTurret);



    }
    void IdInitializeID()
    {
        for(int i=0;i<3;i++)
        {
            buttonSlot unitbuttonSlot = menuButton[1,i].transform.GetComponent<buttonSlot>();
            unitbuttonSlot.id = i;
           
        }
        buttonSlot turretbuttonSlot = menuButton[2,0].transform.GetComponent<buttonSlot>();
        turretbuttonSlot.id = 3;
        buttonSlot addturretSlot = menuButton[0, 3].transform.GetComponent<buttonSlot>();
        addturretSlot.id = 4;
        buttonSlot SellturretSlot = menuButton[0, 2].transform.GetComponent<buttonSlot>();
        SellturretSlot.id = 5;

    }
    
    //메인 버튼(메뉴 이동)
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
    //터렛--------------------------------------------------------------------------
    private void MakeTurret()
    {
        OnClickTurretButton?.Invoke();
    }
    private void CancelTurret()
    {
        OnClickTurretCancel?.Invoke();
    }
    private void OnOffTurretButon()
    {
        cancelTurretButton.gameObject.SetActive(!cancelTurretButton.gameObject.activeSelf);
    }
    private void SellTurret()
    {
        isSellTurret = true;
        OnClickTurretButton?.Invoke();
        TempTrImage.image.sprite = menuButton[0, 2].gameObject.GetComponent<Image>().sprite;
        
    }
    private void OffIsSell()
    {
        isSellTurret = false;
    }
}
