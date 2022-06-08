using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    AnimConChanger animConChanger;

    int curEventTypr = 0;
    string nextMoveScenes = "";

    // Start is called before the first frame update
    void Start()
    {
        animConChanger = GetComponent<AnimConChanger>();
        StartCoroutine(State());
    }

    // Update is called once per frame
    IEnumerator State()
    {
        while (true)
        {
            animConChanger.Walk(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
            animConChanger.MouseLook();
            //±¸¸£±â
            if (Input.GetKeyDown(KeyCode.Space))
            {
                animConChanger.Roll();
            }


            switch (animConChanger.pRState)
            {
                case AnimConChanger.PRState.move:

                    break;

                case AnimConChanger.PRState.stop:

                    if (Input.GetKeyDown(KeyCode.F))
                    {
                        animConChanger.PlayerFnc(curEventTypr, () => { StartCoroutine(ObjEvent(nextMoveScenes)); });
                    }

                    break;
            }

            yield return null;
        }

    }

    private void OnTriggerStay(Collider other)
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
                break;

            default:
                curEventTypr = 0;
                nextMoveScenes = "";
                break;
        }

    }

    IEnumerator ObjEvent(string _name)
    {
        yield return Utill.WaitForSeconds(1f);

        bl_SceneLoaderManager.LoadScene(_name);
    }
}
