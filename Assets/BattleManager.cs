using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    public GameObject BattleCamera;
    public GameObject PlayerCamera;
    public GameObject Player;

    public Transform EnemyPodium;
    public Transform PlayerPodium;

    void Start()
    {
        PlayerCamera.SetActive(true); // Player Camera is on; Battle Camera is off.
        BattleCamera.SetActive(false);
    }

    public void StartBattle(Enemy enemy)
    {
        InitWait();
        GameObject Player = GameObject.FindWithTag("Player");
        Player.GetComponent<Knight_Controller>().isInCombat = true;
    }

    void InitWait()
    {
        StartCoroutine(Wait());
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(4);
        BattleControl();
    }

    void BattleControl()
    {
        PlayerCamera.SetActive(false); // Reverves the camera states, and makes it unable for the player to move.
        BattleCamera.SetActive(true);
    }
}
[System.Serializable]
public class Stat
{
    public float min;
    public float max; 
}