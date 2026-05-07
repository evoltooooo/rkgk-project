using UnityEngine;
using UnityEngine.SceneManagement;

public class EncounterZone : MonoBehaviour
{
    public static bool playerInsideZone = false;

    public static float encounterChance = 1f;

    public static void PlayerStepped()
    {
        if (!playerInsideZone)
            return;

        // Increase encounter chance every step
        encounterChance += 2f;

        Debug.Log("Encounter Chance: " + encounterChance);

        float randomRoll = Random.Range(0f, 100f);

        if (randomRoll < encounterChance)
        {
            Debug.Log("BATTLE TRIGGERED");

            encounterChance = 1f;

            BattleLoader.lastScene = "Exploration";

            SceneManager.LoadScene("Battle");
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInsideZone = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInsideZone = false;

            encounterChance = 1f;
        }
    }
}