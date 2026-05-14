using UnityEngine;
using UnityEngine.SceneManagement;

public class SafeZoneLoader : MonoBehaviour
{
    [SerializeField] private string sceneToLoad;
    [SerializeField] private Vector2 playerSpawnPosition;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerPrefs.SetFloat("SpawnX", playerSpawnPosition.x);
            PlayerPrefs.SetFloat("SpawnY", playerSpawnPosition.y);

            SceneManager.LoadScene(sceneToLoad);
        }
    }
}
