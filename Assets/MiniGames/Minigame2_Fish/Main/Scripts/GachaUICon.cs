using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GachaUICon : MonoBehaviour
{
    Vector3 idealPos;
    [SerializeField]
    float distance = -250.0f;
    [SerializeField]
    float duration = 1.0f;
    [SerializeField]
    GameObject textObj;

    private void Awake()
    {
        textObj = transform.parent.Find("Text").gameObject;
    }

    private void OnEnable()
    {
        idealPos = transform.position;
        StartCoroutine(GachaUIMove());
    }

    IEnumerator GachaUIMove()
    {
        yield return new WaitForSeconds(3.0f);

        transform.DOLocalMoveX(distance, duration);

        yield return new WaitForSeconds(duration * 0.5f);
        textObj.SetActive(true);
    }

    private void OnDisable()
    {
        transform.position = idealPos;
    }
}
