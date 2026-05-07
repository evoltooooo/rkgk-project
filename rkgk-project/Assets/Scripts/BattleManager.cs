using UnityEngine;
using UnityEngine.SceneManagement;

public class BattleManager : MonoBehaviour
{
    public void EndBattle()
    {
        SceneManager.LoadScene(BattleLoader.lastScene);
    }
}
