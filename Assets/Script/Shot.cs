using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shot : MonoBehaviour
{
    [SerializeField] private float speed = 4;
    [SerializeField] public GameObject birdShot;
    [SerializeField] private Bird bird;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Jika burung tidak mati maka shot akan berjalan
        if (!bird.IsDead())
        {
            transform.Translate(Vector3.right * speed * Time.deltaTime, Space.World);
        }
        //Jika burung mati maka peluru berhenti
        else if (bird.IsDead())
        {
            transform.Translate(Vector3.zero);
        }
        
    }

    // Untuk menghilankan pipe dan shot saat bertabrakan
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.gameObject.tag == "Pipe")
        {
            Destroy(birdShot);
            Destroy(collision.gameObject);
        }
    }

}
