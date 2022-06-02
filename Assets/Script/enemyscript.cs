using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyscript : MonoBehaviour
{
    [SerializeField]
    public float hýz;
    public float sabitHýz = 3;
    public float serhat = 7.0f;
    public float dusmanYakýnlýgý;
    float gecikme;
    public float gelcekZaman;



    Transform ýlkKonum;
    Transform sýmdýkýKonum;
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
        hýz = sabitHýz;
        ýlkKonum = gameObject.transform;
        gelecekKonum = new Vector2(ýlkKonum.position.x + serhat, ýlkKonum.position.y);



        gecikme = 2;


    }


    void Update()
    {
        move();
        isPlayer();




    }

    public void move()
    {
        sýmdýkýKonum = gameObject.transform;


        if (gelecekKonum.x - sýmdýkýKonum.position.x <= 0)
        {

            transform.eulerAngles = new Vector3(0, -180, 0);

            saga = false;
            sola = true;

        }
        else if (gelecekKonum.x - sýmdýkýKonum.position.x >= serhat)
        {
            sola = false;
            saga = true;
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
        transform.Translate(Vector2.right * hýz * Time.deltaTime);



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

        dusmanYakýnlýgý = Vector2.Distance(transform.position, playerTransform.position);
        if (dusmanYakýnlýgý <= 6 && oyuncuOnunde == true)
        {
            isAttack = true;
            hýz = 0;
            anim.SetBool("attack", isAttack);
            anim.SetBool("walk", false);
            InvokeRepeating("fire", 1.6f, 1.6f);



        }
        else if (dusmanYakýnlýgý > 6 && oyuncuOnunde == false)
        {
            hýz = sabitHýz;
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
