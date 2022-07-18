using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public TapperGameManager tapperGameManager;

    [SerializeField]
    BeerController beerController;
    [SerializeField]
    GameObject smokeFx;
    [SerializeField]
    Transform smokeFxPos;
    CharacterActionHandle characterActionHandle;
    [SerializeField]
    Animator animator;

    [Range(0, 1f)]
    public float Scale = 1f;
    public float Speed = 3;
    const float beerfillMaxTime = 2.5f;

    int prDir = 1;
    int lineIdx = 0;

    Vector3 xMinLmit;
    Vector3 xMaxLmit;

    [SerializeField]
    Transform[] tablePos;
    [SerializeField]
    Transform[] linePos;

    private void Awake()
    {
        characterActionHandle = animator.GetComponent<CharacterActionHandle>();
        xMinLmit = new Vector3(-1.7f, transform.position.y, transform.position.z);
        xMaxLmit = new Vector3(1f, transform.position.y, transform.position.z);
    }

    private void Start()
    {
        beerController.SetFill(0f);
    }
    void Update()
    {
        Move();

        if (beerController.GetFill() == true)
        {
            if (isTable == true && Input.GetKeyDown(KeyCode.Space))
            {
                beerController.SetFill(0f);

                beerController.gameObject.SetActive(false);
                GameObject _beer = Instantiate(Resources.Load<GameObject>("Prefabs/Tapper/BeerMove"), tablePos[lineIdx]);
                _beer.transform.position = new Vector3(transform.position.x, 0, 0);
                _beer.transform.localPosition = new Vector3(_beer.transform.localPosition.x, 0.15f, 0);
                _beer.name = "BeerMove";
                _beer.transform.localScale = Vector3.one;
            }
        }
    }

    void Move()
    {
        float x = Input.GetAxisRaw("Horizontal");
        //float y = Input.GetAxisRaw("Vertical");

        if (transform.position.x < xMinLmit.x)
        {
            transform.position = xMinLmit;
        }
        else if (transform.position.x > xMaxLmit.x)
        {
            transform.position = xMaxLmit;
        }

        Vector2 moveVelocity = new Vector2(x, 0) * (Speed * Scale) * Time.deltaTime;

        if (x != 0)
        {
            prDir = (int)x;
        }

        if (moveVelocity != Vector2.zero)
        {
            animator.SetTrigger("Run");
        }
        else
        {
            animator.SetTrigger("Idle");
        }

        transform.position += (Vector3)moveVelocity;
        transform.localScale = new Vector3(prDir * -1, 1, 1) * Scale;


        if (transform.position.x > 0.6f)
        {
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
            {
                if (lineIdx == 0)
                {
                    return;
                }
                Instantiate(smokeFx, smokeFxPos);
                lineIdx--;
            }

            if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
            {
                if (lineIdx == 3)
                {
                    return;
                }
                Instantiate(smokeFx, smokeFxPos);
                lineIdx++;
            }
        }

        transform.position = new Vector3(transform.position.x, linePos[lineIdx].position.y, 0);
    }


    bool isTable = false;
    IEnumerator _beerFillCor;

    IEnumerator BeerReady()
    {
        float fillTime = 0;
        beerController.gameObject.SetActive(true);
        beerController.SetFill(0f);

        tapperGameManager.tapperTextBoxManager.SetDiaLog(transform,"한잔은 떠나버린 너를 위하여...");
        //tapperGameManager.tapperTextBoxManager.SetDiaLog(transform, "한잔은 이미 초라해진 나를 위하여...");
        //tapperGameManager.tapperTextBoxManager.SetDiaLog(transform, "또 한잔은 너와의 영원한 사랑을 위하여...");

        while ((fillTime / beerfillMaxTime) < 1f)
        {
            fillTime += Time.deltaTime;
            beerController.SetFill(fillTime / beerfillMaxTime);
            yield return null;
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        string tag = collision.name;

        switch (tag)
        {
            case "beerBox":

                if (beerController.GetFill() == true)
                {
                    return;
                }

                if (_beerFillCor == null)
                {
                    _beerFillCor = BeerReady();
                }

                StartCoroutine(_beerFillCor);

                break;
            case "woodshop_1":
            case "woodshop_2":
            case "woodshop_3":
            case "woodshop_4":
                isTable = true;
                break;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        string tag = collision.name;

        switch (tag)
        {
            case "beerBox":

                if (_beerFillCor != null)
                {
                    StopCoroutine(_beerFillCor);
                    _beerFillCor = null;
                }

                break;
            case "woodshop_1":
            case "woodshop_2":
            case "woodshop_3":
            case "woodshop_4":
                isTable = false;
                break;
        }
    }

}
