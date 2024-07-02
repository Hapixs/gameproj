using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    
    public float speed = 5f; // Vitesse de déplacement de la fusée

    void Start()
    {
        GetComponent<Rigidbody2D>().gravityScale = 0f;
    }

    void Update()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector2 movement = new Vector2(moveHorizontal, moveVertical);

        // Récupère la taille de l'écran en pixels
        Vector2 screenSize = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));

        // Ajuste la position en fonction des limites de l'écran
        transform.position = new Vector2(
            Mathf.Clamp(transform.position.x + movement.x * speed * Time.deltaTime, -screenSize.x, screenSize.x),
            Mathf.Clamp(transform.position.y + movement.y * speed * Time.deltaTime, -screenSize.y, screenSize.y)
        );

        if (movement != Vector2.zero)
        {
            // Calcule l'angle de rotation en radians
            float angle = Mathf.Atan2(movement.y, movement.x) * Mathf.Rad2Deg;

            // Crée un quaternion de rotation à partir de l'angle
            Quaternion targetRotation = Quaternion.Euler(new Vector3(0, 0, angle));

            // Applique la rotation à la fusée
            transform.rotation =
                Quaternion.Slerp(transform.rotation, targetRotation,
                    0.1f); // Utilise Slerp pour une rotation plus douce
        }
    }
    
    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision.gameObject.tag);
        
    }
}
