using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    public GameObject asteroidPrefab; // Référence au prefab de l'astéroïde
    public float spawnInterval = 2f; // Intervalle de temps entre chaque apparition d'astéroïde
    public float minSpeed = 2f; // Vitesse minimale de l'astéroïde
    public float maxSpeed = 5f; // Vitesse maximale de l'astéroïde

    void Start()
    {
        // Lance la fonction SpawnAsteroid à intervalles réguliers
        InvokeRepeating("SpawnAsteroid", 0f, spawnInterval);
    }

    void SpawnAsteroid()
    {
        // Choix d'un bord de l'écran (0 pour le haut, 1 pour la droite, 2 pour le bas, 3 pour la gauche)
        int randomEdge = Random.Range(0, 4);

        // Position et direction de départ
        Vector3 spawnPosition = Vector3.zero;
        Vector2 spawnDirection = Vector2.zero;

        switch (randomEdge)
        {
            case 0: // Haut de l'écran
                spawnPosition = new Vector3(Random.Range(-5f, 5f), Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height, 0)).y, 0f);
                spawnDirection = Vector2.down;
                break;

            case 1: // Droite de l'écran
                spawnPosition = new Vector3(Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0, 0)).x, Random.Range(-5f, 5f), 0f);
                spawnDirection = Vector2.left;
                break;

            case 2: // Bas de l'écran
                spawnPosition = new Vector3(Random.Range(-5f, 5f), Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0)).y, 0f);
                spawnDirection = Vector2.up;
                break;

            case 3: // Gauche de l'écran
                spawnPosition = new Vector3(Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0)).x, Random.Range(-5f, 5f), 0f);
                spawnDirection = Vector2.right;
                break;
        }

        // Vitesse aléatoire
        float randomSpeed = Random.Range(minSpeed, maxSpeed);

        // Crée un nouvel astéroïde avec une position, une direction et une vitesse aléatoires
        GameObject newAsteroid = Instantiate(asteroidPrefab, spawnPosition, Quaternion.identity);

        newAsteroid.tag = "Asteroid";
        // Accède au script AsteroidController pour ajuster la direction et la vitesse
        AsteroidController asteroidController = newAsteroid.GetComponent<AsteroidController>();
        if (asteroidController != null)
        {
            asteroidController.SetMovementParameters(spawnDirection, randomSpeed);
        }
    }
}
