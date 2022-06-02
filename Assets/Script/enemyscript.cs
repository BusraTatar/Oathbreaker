using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyscript : MonoBehaviour
{
    [SerializeField]
    public float h�z;
    public float sabitH�z = 3;
    public float serhat = 7.0f;
    public float dusmanYak�nl�g�;
    float gecikme;
    public float gelcekZaman;



    Transform �lkKonum;
    Transform s�md�k�Konum;
    public Transform playerTransform;
    public Transform fireTransform;


    Vector2 gelecekKonum;

    public bool dusman;
    public bool isAttack;
    public bool oyuncuOnunde;
    public bool saga, sola;



    public Animator anim;
    public GameObject fireBall;











    void Start()
    {
        h�z = sabitH�z;
        �lkKonum = gameObject.transform;
        gelecekKonum = new Vector2(�lkKonum.position.x + serhat, �lkKonum.position.y);



        gecikme = 2;


    }


    void Update()
    {
        move();
        isPlayer();




    }

    public void move()
    {
        s�md�k�Konum = gameObject.transform;


        if (gelecekKonum.x - s�md�k�Konum.position.x <= 0)
        {

            transform.eulerAngles = new Vector3(0, -180, 0);

            saga = false;
            sola = true;

        }
        else if (gelecekKonum.x - s�md�k�Konum.position.x >= serhat)
        {
            sola = false;
            saga = true;
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
        transform.Translate(Vector2.right * h�z * Time.deltaTime);



    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {

            oyuncuOnunde = true;
        }


    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {

            oyuncuOnunde = false;
        }

    }

    public void isPlayer()
    {

        dusmanYak�nl�g� = Vector2.Distance(transform.position, playerTransform.position);
        if (dusmanYak�nl�g� <= 6 && oyuncuOnunde == true)
        {
            isAttack = true;
            h�z = 0;
            anim.SetBool("attack", isAttack);
            anim.SetBool("walk", false);
            InvokeRepeating("fire", 1.6f, 1.6f);



        }
        else if (dusmanYak�nl�g� > 6 && oyuncuOnunde == false)
        {
            h�z = sabitH�z;
            isAttack = false;
            anim.SetBool("attack", isAttack);
            anim.SetBool("walk", true);
            CancelInvoke();

        }




    }
    public void fire()
    {


        if (Time.time > gelcekZaman)
        {
            Instantiate(fireBall, fireTransform.position, transform.rotation);
            gelcekZaman = Time.time + gecikme;

        }



    }


}
