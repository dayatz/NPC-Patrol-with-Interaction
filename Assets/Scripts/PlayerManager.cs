using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CharacterController))] // Butuh CharacterController di game object yg lu mw mskin script ini
public class PlayerManager : MonoBehaviour 
{
    private CharacterController controller; // Referensi CharacterController, namain controller

    private Vector3 moveDirection = Vector3.zero; // Bwt arah kmana karakter jalan

    private float gravity = 20.0f, speed = 5.0f; // Bwt daya berat karakter sm cepet jalan nya

    public bool isOnChat = false; // Bwt tw kalo player lg bicara

    private void Awake()
    {
        // Ambil referensi CharacterController dr game object yg lu mskin script ini
        controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        // Kalo karakter ada di lantai
        if (controller.isGrounded)
        {
            // Render karakter spy bs diliat oleh kt
            gameObject.renderer.enabled = true;

            // Horizontal n Vertical Axis (Unity Default Axis), Horizontal (A, D), Vertical (W, S)
            moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

            // Bwt arah karakter sm dgn arah karakter based on ap yg lu pncet di keyboard (W, A, S ato D)
            moveDirection = transform.TransformDirection(moveDirection);

            // Kali in arah karakter jalan dgn kecepatan jalan
            moveDirection *= speed;
        }

        // Kalo karakter it ada di langit, bwt karakter turun dan dikurang daya berat dikali dgn per frame
        moveDirection.y -= gravity * Time.deltaTime;

        // Bwt karakter denger ap yg lu pncet di keyboard (W, A, S ato D) based on arah karakter dikali dgn per frame
        controller.Move(moveDirection * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider col)
    {
        // Kalo karakter nabrak gameobjet yg namanya NPC Follower
        if (col.gameObject.name == "NPC Follower")
        {
            // Bwt karakter lg bicara
            isOnChat = true;
        }
    }

    private void OnTriggerExit(Collider col)
    {
        // Kalo karakter lg ga nabrak gameobject yg namanya NPC Follower
        if (col.gameObject.name == "NPC Follower")
        {
            // Bwt karakter ga bicara lg
            isOnChat = false;
        }
    }
}
