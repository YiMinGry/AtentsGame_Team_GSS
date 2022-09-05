using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class UIMoveCon : MonoBehaviour
{
    Vector3 EndPos;
    Vector3 StartPos;

    [SerializeField]
    float moveDis = 20.0f;

    [SerializeField]
    float animTime = 0.5f;

    [SerializeField]
    string dir = "up";

    //public void UIMove(string dir)
    private void OnEnable()
    {
        EndPos = transform.position;
        switch (dir)
        {
            case "up":
                {
                    StartPos = EndPos + new Vector3(0, -moveDis, 0);
                    break;
                }
            case "down":
                {
                    StartPos = EndPos + new Vector3(0, moveDis, 0);
                    break;
                }
            case "left":
                {
                    StartPos = EndPos + new Vector3(-moveDis, 0, 0);
                    break;
                }
            case "right":
                {
                    StartPos = EndPos + new Vector3(moveDis, 0, 0);
                    break;
                }
            default:
                Debug.Log("up, down, left, right �� �ϳ��� �Է��ϼ���");
                StartPos = EndPos;
                break;
        }
        transform.position = StartPos;
        gameObject.SetActive(true);
        StartCoroutine(UIMoveAnim());
    }

    IEnumerator UIMoveAnim()
    {
        transform.DOMove(EndPos, animTime);
        yield return new WaitForSeconds(animTime);
    }

}
