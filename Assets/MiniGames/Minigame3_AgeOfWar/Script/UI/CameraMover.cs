using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class CameraMover : MonoBehaviour,IPointerEnterHandler, IPointerExitHandler
{
    bool ismove = false;
    int dir = 0;
    [SerializeField] float cameraSpeed = 1.0f;
                
    public void OnPointerEnter(PointerEventData eventData)
    {
        if(eventData.position.x<500)
        {
            dir = 1;
        }
        else
        {
            dir = -1;
        }
        ismove = true;

    }

    public void OnPointerExit(PointerEventData eventData)
    {
        //Debug.Log("³ª°«³×");
        ismove = false;
    }
    private void Update()
    {
        if(ismove)
        {
            Camera.main.transform.position += dir * Time.deltaTime * Vector3.right*cameraSpeed;
        }
    }

}
