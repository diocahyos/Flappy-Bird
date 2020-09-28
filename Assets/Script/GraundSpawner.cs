using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]

public class GraundSpawner : MonoBehaviour
{
    //Menampung referensi ground yang ingin dibuat
    [SerializeField] private Ground groundRef;

    //Menampung ground sebelumnya
    private Ground prevGround;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Method ini akan membuat Ground game object baru
    private void SpawnGround()
    {
        //Pengecekan Null Variable
        if(prevGround != null)
        {
            //menduplikasi Ground ref
            Ground newGround = Instantiate(groundRef);

            //mengaktifkan game object
            newGround.gameObject.SetActive(true);

            //menempatkan new ground dengan posisi next ground dari precground agar posisinya sejajar dengan ground sebelumnya
            prevGround.SetNextGround(newGround.gameObject);
        }
    }

    //Method ini akan dipangil ketika terdapat game object lain yang memiliki komponen collider keluar dari area collider
    private void OnTriggerExit2D(Collider2D collision)
    {
        //Mencari komponent ground dari object yang keluar dari area trigger
        Ground ground = collision.GetComponent<Ground>();

        //Pengecekkan null variable
        if (ground)
        {
            //Mengisi variable prevGround 
            prevGround = ground;

            //Membuat ground baru
            SpawnGround();
        }
    }
}
