using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bash : MonoBehaviour
{
    public float radius = 1.8f, bashPower = 3;
    private Vector3 direction;
    private GameObject lockedObj;
    public GameObject arrow;
    private bool isBashing;
    // Start is called before the first frame update
    void Start()
    {
        isBashing = false;
        arrow.SetActive (false);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButton ("Fire2")){
            searchShots ();
        }
        else if(Input.GetButtonUp("Fire2") && isBashing){
            shoot ();
        }
    }
    private void searchShots(){
        if(!isBashing){
        Collider2D[] colls = Physics2D.OverlapCircleAll (transform.position, radius);
        foreach(Collider2D coll in colls){
            if(coll.tag == "Shot"){
                lockedObj = coll.gameObject;
                isBashing = true;
                arrow.SetActive (true);
                arrow.transform.position = lockedObj.transform.position;
                Time.timeScale = 0;
            }
        }

    }else calculateDirection();
    }
    


private void calculateDirection(){
    direction = Camera.main.ScreenToWorldPoint (Input.mousePosition) - lockedObj.transform.position;
    float rotZ = Mathf.Atan2 (direction.y, direction.x) * Mathf.Rad2Deg;
    arrow.transform.rotation = Quaternion.Euler (0f, 0f, rotZ);
}

private void shoot(){
    GetComponent<Rigidbody2D> ().velocity = direction * bashPower;
    lockedObj.GetComponent<Rigidbody2D> ().velocity = -direction * bashPower;

    resetPosition ();

    isBashing = false;
    arrow.SetActive (false);
    Time.timeScale = 1;
}

private void resetPosition(){
    float width = lockedObj.GetComponent<SpriteRenderer> ().bounds.size.x;
    float height = lockedObj.GetComponent<SpriteRenderer> ().bounds.size.y;

    float tan = Mathf.Atan2 (direction.y, direction.x);

    int roundCos = Mathf.RoundToInt (Mathf.Cos(tan));
    int roundSin = Mathf.RoundToInt (Mathf.Sin(tan));

    transform.position = new Vector3 (lockedObj.transform.position.x + (width*roundCos),
    lockedObj.transform.position.y + (height*roundSin), 0);
    } 
}