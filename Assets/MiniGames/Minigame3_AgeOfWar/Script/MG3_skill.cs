using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MG3_skill : MonoBehaviour
{
    public GameObject projectilePrefab;

    float rand;
    float sidedistance = 13.0f;
    public int numProjectile = 15;
    Button skillButton;
    Image skillImage;
    Vector3 skillPosition;
    Quaternion skillRotation;

    private void Awake()
    {

        skillButton = GetComponent<Button>();
        skillImage = GetComponent<Image>();
        skillImage.fillAmount = 0;
        skillButton.onClick.AddListener(SkillMeteor);
        skillPosition = new Vector3(0, 10, 0);
        skillRotation = new Quaternion();
    }

    public void SkillMeteor()
    {
        if(skillImage.fillAmount <0.01f)
        {
            StartCoroutine(MeteorCo());
            skillImage.fillAmount = 1;
        }
        
    }

    IEnumerator MeteorCo()
    {
        for(int i=0;i<numProjectile;i++)
        {

            rand = Random.Range(0.0f, 1.0f);
            GameObject obj=Instantiate(projectilePrefab,skillPosition,skillRotation);
            obj.transform.position += Vector3.right * (36 * rand - 18);
            yield return new WaitForSeconds(0.1f);
        }
    }
    private void Update()
    {
        skillImage.fillAmount -= Time.deltaTime*0.05f;
    }
}
