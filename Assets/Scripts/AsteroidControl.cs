using System;
using UnityEngine;

public class AsteroidController : MonoBehaviour
{
    public float speed = 3f; // Vitesse de déplacement de l'astéroïde
    private Vector2 movementDirection;
    void Start()
    {
        // Détruit l'astéroïde après quelques secondes s'il n'a pas été détruit
        if (!gameObject.CompareTag("EditorOnly")) Destroy(gameObject, 10f);
        GetComponent<Rigidbody2D>().gravityScale = 0f;

    }
    
    void Update()
    {
        // Déplace l'astéroïde en fonction de la direction et de la vitesse
        transform.Translate(movementDirection * speed * Time.deltaTime);
        // Vérifie si l'astéroïde est sorti de l'écran
        Vector3 worldPosition = transform.position;


        if (worldPosition.x < Camera.main.ScreenToWorldPoint(Vector3.zero).x ||
            worldPosition.x > Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0, 0)).x ||
            worldPosition.y < Camera.main.ScreenToWorldPoint(Vector3.zero).y ||
            worldPosition.y > Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height, 0)).y)
        {
            // Vérifie si l'astéroïde a le tag "StaticAsteroid" avant de le détruire
            if (!gameObject.CompareTag("EditorOnly"))
            {
                Destroy(gameObject);
            }
        }
    }
    
    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Collision avec un autre astéroïde !");
        if (collision.gameObject.CompareTag("Asteroid"))
        {
            // Collision avec un autre astéroïde
            
            // Change la direction en fonction de l'angle de collision
            Vector2 collisionNormal = collision.contacts[0].normal;
            Vector2 newDirection = Vector2.Reflect(movementDirection, collisionNormal).normalized;
            SetMovementParameters(newDirection, speed);
        }
    }
    
    public void SetMovementParameters(Vector2 direction, float newSpeed)
    {
        movementDirection = direction.normalized; // Normalise la direction pour s'assurer que sa magnitude est de 1
        speed = newSpeed;
    }
}