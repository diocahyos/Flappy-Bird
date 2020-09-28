﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Komponen akan ditambahkan jika tidak ada dan komponen tersebut tidak dapat dibuang
public class Ground : MonoBehaviour
{
    //Global Variable
    [SerializeField] private Bird bird;
    [SerializeField] private float speed = 1;
    [SerializeField] private Transform nextPos;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Melakukan pengecekan jika burung null atau belum mati
        if(bird == null || (bird != null && !bird.IsDead()))
        {
            //Membuat pipa bergerak kesebelah kiri dengan kecepatan dari variable speed
            transform.Translate(Vector3.left * speed * Time.deltaTime, Space.World);
        }
    }

    //Method untuk Menempatkan game object pada posisi next ground
    public void SetNextGround(GameObject ground)
    {
        //Pengecekan Null value
        if(ground != null)
        {
            //Meempatkan ground berikutnya pada posisi nextground
            ground.transform.position = nextPos.position;
        }
    }

    //Dipanggil ketika game object bersentuhan dengan game object lain
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Membuat burung mati ketika bersentuhan dengan game object ini
        if(bird != null && !bird.IsDead())
        {
            bird.Dead();
        }
    }
}