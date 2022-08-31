using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashPanel : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))    // Space �Է��� ������ DashPanel ��Ȱ��ȭ
        {
            gameObject.SetActive(false);
        }
    }

    private void OnEnable()
    {
        StartCoroutine(DisableTimer());
    }

    IEnumerator DisableTimer()          // 10�� �Ŀ� DashPanel ��Ȱ��ȭ
    {
        yield return new WaitForSeconds(10.0f);
        gameObject.SetActive(false);
    }

}

