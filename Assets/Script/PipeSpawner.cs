using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeSpawner : MonoBehaviour
{
    //Global Variable
    [SerializeField] private Bird bird;
    [SerializeField] private Pipe pipeUp, pipeDown;
    [SerializeField] private float spawnInterval = 3;
    //Hole
    [SerializeField] public float holeSize = 1f;
    //Variasi Hole
    [SerializeField] private float maxMinOffset = 1;
    //Score
    [SerializeField] private Point point;

    //Variable penampung coroutine yang sedang berjalan
    private Coroutine CR_Spawn;

    // Start is called before the first frame update
    void Start()
    {
        StartSpawn();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void StartSpawn()
    {
        //Menjalankan Fungsi Corotine IeSpawn()
        if (CR_Spawn == null)
        {
            CR_Spawn = StartCoroutine(IeSpawn());
        }
    }

    void StopSpawn()
    {
        //Menghentikan Coroutine IeSpawn jika sebelumnya sudah dijalankan
        if (CR_Spawn != null)
        {
            StopCoroutine(CR_Spawn);
        }
    }

    void SpawnPipe()
    {
        //menduplikasi game object pipeUp dan menempatkan posisinya sama denga game object ini tetapi rotasi 180 derajat
        Pipe newPipeUp = Instantiate(pipeUp, transform.position, Quaternion.Euler(0, 0, 180));

        //Mengaktifkan game object newPipeUp
        newPipeUp.gameObject.SetActive(true);

        //Menduplikasi game object pipeDown dan menempatkan posisinya sama dengan game object
        Pipe newPipeDown = Instantiate(pipeDown, transform.position, Quaternion.identity);

        //Mengaktifkan game object newPipeDown
        newPipeDown.gameObject.SetActive(true);

        //menempatkan posisi dari pipa yang sudah terbentuk agar memiliki lubang ditangahnya
        newPipeUp.transform.position += Vector3.up * (holeSize / 2) * Random.Range(1, 4);
        newPipeDown.transform.position += Vector3.down * (holeSize / 2) * Random.Range(2, 4);

        //menempatkan posisi pipa yang telah dibentuk agar posisinya menyesuaikan dengan fungsi Sin
        float y = maxMinOffset * Mathf.Sin(Time.time);
        newPipeUp.transform.position += Vector3.up * y;
        newPipeDown.transform.position += Vector3.up * y;

        Point newPoint = Instantiate(point, transform.position, Quaternion.identity);
        newPoint.gameObject.SetActive(true);
        newPoint.SetSize(holeSize);
        newPoint.transform.position += Vector3.up * y;
    }

    IEnumerator IeSpawn()
    {
        while (true)
        {
            //Jika burung mati maka menghentikan pembuatan Pipa Baru
            if (bird.IsDead())
            {
                StopSpawn();
            }

            //Membuat Pipa Baru
            SpawnPipe();

            //Menunggu beberapa detik sesuai dengan spawn interval
            yield return new WaitForSeconds(spawnInterval);
            //yield return new WaitForSeconds(spawnInterval);
        }
    }
}
