using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashPanel : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))    // Space 입력이 들어오면 DashPanel 비활성화
        {
            gameObject.SetActive(false);
        }
    }

    private void OnEnable()
    {
        StartCoroutine(DisableTimer());
    }

    IEnumerator DisableTimer()          // 10초 후에 DashPanel 비활성화
    {
        yield return new WaitForSeconds(10.0f);
        gameObject.SetActive(false);
    }

}

