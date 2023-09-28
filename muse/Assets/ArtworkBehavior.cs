using UnityEngine;

public class ArtworkBehavior : MonoBehaviour
{
    public enum ReactionType { DoNothing, ChangePicture, TranslatePlayer }

    public float chanceDoNothing = 0.33f;
    public float chanceChangePicture = 0.33f;
    public float chanceTeleportCharacter = 0.33f;


    public Material[] artworkMaterials;

    private Renderer artworkRenderer;

    private void Start()
    {
        artworkRenderer = GetComponent<Renderer>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            float randomValue = Random.value;

            if (randomValue < chanceDoNothing)
            {
            }
            else if (randomValue < chanceDoNothing + chanceChangePicture)
            {
                ChangePicture();
            }
            else
            {
                TranslatePlayer(other.gameObject);
            }
        }
    }

    private void ChangePicture()
    {
        if (artworkMaterials.Length > 0)
        {
            Material newMaterial = artworkMaterials[Random.Range(0, artworkMaterials.Length)];

            artworkRenderer.material = newMaterial;
        }
    }

    private void TranslatePlayer(GameObject player)
{
    Debug.Log("Teleporting player.");

    Debug.Log("Player's current position: " + player.transform.position);

    float randomX = Random.Range(-10f, 10f);
    float randomZ = Random.Range(-10f, 10f);

    Vector3 randomPosition = new Vector3(randomX, player.transform.position.y, randomZ);

    player.transform.position = randomPosition;

    Debug.Log("Player's new position: " + player.transform.position);

    Debug.DrawRay(player.transform.position, Vector3.up * 2f, Color.green, 3f);
}
}
