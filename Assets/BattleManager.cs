using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{

    public GameObject BattleCamera;
    public GameObject PlayerCamera;
    public GameObject Player;
    public Material material;
    private float count = 0;
    private bool trans = false;

    public Transform EnemyPodium;
    public Transform PlayerPodium;

    void Start()
    {
        PlayerCamera.SetActive(true); // Player Camera is on; Battle Camera is off.
        BattleCamera.SetActive(false);
        material.SetFloat("_Cutoff", count);
    }

    private void FixedUpdate()
    {
        if (trans == true)
        {
            material.SetFloat("_Cutoff", Add(count));
        }
    }

    public void StartBattle(Enemy enemy)
    {
        trans = true;
        InitWait();
        GameObject Player = GameObject.FindWithTag("Player");
        Player.GetComponent<Knight_Controller>().isInCombat = true;
    }

    void InitWait()
    {
        StartCoroutine(Wait());
    }

    IEnumerator Wait() // Wait before the battle starts.
    {
        yield return new WaitForSeconds(4);
        BattleControl();
    }

    void BattleControl()
    {
        PlayerCamera.SetActive(false); // Reverves the camera states, and makes it unable for the player to move.
        BattleCamera.SetActive(true);

    }

    float Add(float c)
    {
       if (count <= 1)
       {
           count += 0.01f;
       }
       return count;
    }
}
[System.Serializable]
public class Stat
{
    public float min;
    public float max; 
}