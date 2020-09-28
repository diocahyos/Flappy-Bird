using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Bird : MonoBehaviour
{
    //Global Variable
    [SerializeField] private float upForce = 100;
    [SerializeField] private bool isDead;
    [SerializeField] private UnityEvent OnJump, OnDead;
    //Animasi bird
    private Animator animator;

    //Score
    [SerializeField] private int score;
    [SerializeField] private UnityEvent OnAddPoint;
    //Text Score
    [SerializeField] private Text scoreText;

    //Shot
    [SerializeField] private Shot shot;
    private Shot prevshot;

    private Rigidbody2D rigidbody2D;

    // Start is called before the first frame update
    void Start()
    {
        //Mendapatkan komponent ketika game baru jalan
        rigidbody2D = GetComponent<Rigidbody2D>();

        //Mendapatkan komponen animator pada game object
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //Melakukan pengecekan jika belum mati dan klik kiri pada mouse
        if(!isDead && Input.GetMouseButtonDown(0))
        {
            //Burung meloncat
            Jump();
        }
        // Pengecekkan jika belum mati dan klik kanan pada mouse
        if (!isDead && Input.GetMouseButtonDown(1))
        {
            Shoter();
        }
        
    }

    //Fungsi untuk memposisikan tembakan susai dengan posisi bird
    void Shoter()
    {
        Shot newShot = Instantiate(shot,transform.position,Quaternion.identity);
        newShot.gameObject.SetActive(true);
    }

    //FUngsi untuk mengecek sudah mati apa belum
    public bool IsDead()
    {
        return isDead;
    }

    //Membuat Burung Mati
    public void Dead()
    {
        //Pengecekkan jika belum mati dan value OnDead tidak sama dengan Null
        if(!isDead && OnDead != null)
        {
            //Memanggil semua event pada OnDead
            OnDead.Invoke();
        }

        //Mengeset variable Dead menjadi True
        isDead = true;
    }

    void Jump()
    {
        //Mengecek rigidBody null atau tidak
        if (rigidbody2D)
        {
            //Menghentikan kecepatan burung ketika jatuh
            rigidbody2D.velocity = Vector2.zero;

            //Menambahkan gaya ke arah sumbu y agar burung meloncat
            rigidbody2D.AddForce(new Vector2(0, upForce));

            //pengecekkan Null Variable
            if(OnJump != null)
            {
                //Menjalankan semua event OnJump event
                OnJump.Invoke();
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Mengehentikan Animasi Burung ketika bersentukan dengan objek lain
        animator.enabled = false;
    }

    public void AddScore(int value)
    {
        //Menambahkan Score 
        score += value;

        //Pengecekan Null value
        if(OnAddPoint != null)
        {
            //Memanggil semua event pada OnAddPoint
            OnAddPoint.Invoke();
        }

        //Mengubah nilai text pada score text
        scoreText.text = score.ToString();
    }
}
