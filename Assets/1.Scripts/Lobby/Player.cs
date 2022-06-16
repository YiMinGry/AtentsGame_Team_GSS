using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public AnimConChanger animConChanger;

    int curEventTypr = 0;
    string nextMoveScenes = "";
    string openUIName = "";
    // Start is called before the first frame update
    void Start()
    {
        //animConChanger = GetComponent<AnimConChanger>();

    }



    public void Update()
    {
        animConChanger.Walk(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));


        switch (animConChanger.pRState)
        {
            case AnimConChanger.PRState.move:

                if (Input.GetKeyDown(KeyCode.Space))
                {
                    animConChanger.Roll();
                }

                break;

            case AnimConChanger.PRState.stop:

                if (Input.GetKeyDown(KeyCode.Escape))
                {

                    animConChanger.OpenPhone();
                }

                if (animConChanger.isHandCam == false)
                {
                    if (Input.GetKeyDown(KeyCode.F))
                    {
                        switch (curEventTypr)
                        {
                            case 1:
                                animConChanger.PlayerFnc(1, () =>
                                {
                                    StartCoroutine(ObjEvent(nextMoveScenes));
                                });

                                break;
                            case 2:
                                animConChanger.PlayerFnc(0, () =>
                                {
                                    EventManager.Invoke("OpenUI", openUIName);
                                });
                                break;
                        }
                    }
                }

                break;
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        string _name = other.gameObject.name;

        switch (_name)
        {
            case "MG_S_01":
            case "MG_S_02":
            case "MG_S_03":
            case "MG_S_04":
            case "MG_S_05":
                curEventTypr = 1;
                nextMoveScenes = _name;
                other.transform.parent.GetComponent<Outline>().enabled = true;
                EventManager.Invoke("ActiveFInfo", "미니게임 : " + _name);
                break;
            case "Ranking":
            case "Friends":
            case "Gacha":
                curEventTypr = 2;
                openUIName = _name;
                other.transform.parent.GetComponent<Outline>().enabled = true;
                EventManager.Invoke("ActiveFInfo", _name);
                break;
            default:
                curEventTypr = 0;
                nextMoveScenes = "";
                break;
        }

    }
    private void OnTriggerExit(Collider other)
    {
        string _name = other.gameObject.name;

        switch (_name)
        {
            case "MG_S_01":
            case "MG_S_02":
            case "MG_S_03":
            case "MG_S_04":
            case "MG_S_05":
                curEventTypr = 0;
                nextMoveScenes = "";
                other.transform.parent.GetComponent<Outline>().enabled = false;
                EventManager.Invoke("DeActiveFInfo", _name);
                break;

            case "Ranking":
            case "Friends":
                curEventTypr = 0;
                openUIName = "";
                other.transform.parent.GetComponent<Outline>().enabled = false;
                EventManager.Invoke("DeActiveFInfo", _name);
                break;
            default:

                break;
        }

    }



    IEnumerator ObjEvent(string _name)
    {
        yield return Utill.WaitForSeconds(1f);

        bl_SceneLoaderManager.LoadScene(_name);
    }
}
