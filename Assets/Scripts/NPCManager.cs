using UnityEngine;
using System.Collections;

public class NPCManager : MonoBehaviour 
{
    private PlayerManager _playerManager; // Referensi PlayerManager Script

    private bool moveForward = false; // Bwt NPC maju kedepan

    private float movementSpeed = 1.5f; // Bwt kecepatan NPC maju

    private void Awake()
    {
        // Cari gameobject yg namanya Player dan ambil PlayerManager script yg ad di gameobject Player
        _playerManager = GameObject.Find("Player").GetComponent<PlayerManager>();
    }

    private void Update()
    {
        // Kalo NPC maju kedepan
        if (moveForward)
        {
            // Suruh NPC maju kedepan dikaliin dgn kecepatan NPC maju dikaliin per frame
            transform.Translate(Vector3.forward * movementSpeed * Time.deltaTime);
        }

        // Kalo NPC ga maju kedepan
        else
        {
            // Panggil Hold method
            StartCoroutine(Hold());
        }
    }

    private void OnGUI()
    {
        // Kalo karakter lg bicara
        if (_playerManager.isOnChat)
        {
            // Tampilin persegi dgn posisi lebar ambil dgn lebar screen resolution di unity dibagi dgn 2 dan posisi tinggi dgn tinggi screen resolution di unity dibagi dgn 2
            // Tampilin persegi dgn lebar 250 pixels dan tinggi 100 pixels dan tampilin pesan sperti yg ad dibawah
            GUI.Box(new Rect(Screen.width / 2, Screen.height / 2, 250, 100), "Kamu mau bicara sama saya? \n\nApa yang mau kamu bicarakan? \n\nTapi saya sedang sibuk.");
        }
    }

    private void OnTriggerEnter(Collider col)
    {
        // Kalo NPC nabrak game object yg namanya First Point
        if (col.gameObject.name == "First Point")
        {
            // Bwt karakter maju true
            moveForward = true;
        }

        // Kalo NPC nabrak game object yg namanya Second Point
        else if (col.gameObject.name == "Second Point")
        {
            // Bwt karakter maju false
            moveForward = false;
        }
    }

    private IEnumerator Hold()
    {
        // Tunggu 2 detik baru execute kode yg dibawah
        yield return new WaitForSeconds(2.0f);

        // Sruh NPC maju kebelakang dikaliin kecepeatan NPC maju dikaliin per frame
        transform.Translate(-Vector3.forward * movementSpeed * Time.deltaTime);
    }
}
