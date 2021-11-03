using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] Field[] fields; 
    bool change;
    string combination;
    [SerializeField] Rigidbody2D X_rigidbody2D;
    [SerializeField] Rigidbody2D O_rigidbody2D;
    Transform highlightTransform;
    private void Awake()
    {
        highlightTransform = GameObject.Find("Highlight").transform;
        fields = GetComponentsInChildren<Field>();
    }
    void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
        GameObject hittedObject = hit.collider.gameObject;
        if (hittedObject.tag == "Button" && !hittedObject.GetComponent<Field>().fieldWithX && !hittedObject.GetComponent<Field>().fieldWithO) 
        {
            highlightTransform.position = hittedObject.transform.position;
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                if (change)
                {
                    Rigidbody2D sign = Instantiate(X_rigidbody2D, hittedObject.transform.position, hittedObject.transform.rotation);
                    hittedObject.GetComponent<Field>().fieldWithX = true;
                    FindWinner();
                }
                if (!change)
                {
                    Rigidbody2D sign = Instantiate(O_rigidbody2D, hit.collider.transform.position, hit.collider.transform.rotation);
                    hittedObject.GetComponent<Field>().fieldWithO = true;
                    FindWinner();
                }
                change = !change;
            }
        }
        else highlightTransform.position = new Vector3(10f, 10f, 0f);
        ResetSigns();
    }
    void FindWinner()
    {
        if ((fields[0].fieldWithO && fields[1].fieldWithO && fields[2].fieldWithO) || (fields[3].fieldWithO && fields[4].fieldWithO && fields[5].fieldWithO) 
            || (fields[6].fieldWithO && fields[7].fieldWithO && fields[8].fieldWithO) || (fields[0].fieldWithO && fields[3].fieldWithO && fields[6].fieldWithO)
            || (fields[1].fieldWithO && fields[4].fieldWithO && fields[7].fieldWithO) || (fields[2].fieldWithO && fields[5].fieldWithO && fields[8].fieldWithO)
            || (fields[0].fieldWithO && fields[4].fieldWithO && fields[8].fieldWithO) || (fields[2].fieldWithO && fields[4].fieldWithO && fields[6].fieldWithO))
        {
            print("O");
        }
        if ((fields[0].fieldWithX && fields[1].fieldWithX && fields[2].fieldWithX) || (fields[3].fieldWithX && fields[4].fieldWithX && fields[5].fieldWithX)
    || (fields[6].fieldWithX && fields[7].fieldWithX && fields[8].fieldWithX) || (fields[0].fieldWithX && fields[3].fieldWithX && fields[6].fieldWithX)
    || (fields[1].fieldWithX && fields[4].fieldWithX && fields[7].fieldWithX) || (fields[2].fieldWithX && fields[5].fieldWithX && fields[8].fieldWithX)
    || (fields[0].fieldWithX && fields[4].fieldWithX && fields[8].fieldWithX) || (fields[2].fieldWithX && fields[4].fieldWithX && fields[6].fieldWithX))
        {
            print("X");
        }
    }
   private void ResetSigns()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            GameObject[] signs = GameObject.FindGameObjectsWithTag("Sign");
            foreach (GameObject sign in signs)
                Destroy(sign);

            foreach (Field field in fields)
            {
                field.fieldWithX = false;
                field.fieldWithO = false;
            }

            change = false;
        }
    }
}
