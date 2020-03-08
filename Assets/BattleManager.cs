using System.Collections;
using UnityEngine;
using TMPro;

public enum BattleState { START, PLAYERTURN, ENEMYTURN, WON, LOST }

public class BattleManager : MonoBehaviour
{
    public BattleState state;
    public GameObject BattleCamera;
    public GameObject PlayerCamera;
    public GameObject PlayerPrefab;
    public GameObject EnemyPrefab;
    public Material material;
    private float count = 0;
    private bool trans = false;

    Player playerUnit;
    Enemy enemyUnit;

    public BattleHUD_Player playerHUD;
    public BattleHUD enemyHUD;
    public TextMeshProUGUI dialogueText;

    public Transform EnemyPodium;
    public Transform PlayerPodium;

    void Start()
    {
        state = BattleState.START;
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
        StartCoroutine(SetupBattle());
    }

    IEnumerator SetupBattle()
    {
        GameObject playerGO = Instantiate(PlayerPrefab, PlayerPodium);
        playerUnit = playerGO.GetComponent<Player>();

        GameObject enemyGO = Instantiate(EnemyPrefab, EnemyPodium);
        enemyUnit = enemyGO.GetComponent<Enemy>();

        dialogueText.text = ("A wild " + enemyUnit.Name + " approaches...");

        playerHUD.SetHUD(playerUnit);
        enemyHUD.SetHUD(enemyUnit);

        yield return new WaitForSeconds(2f);

        state = BattleState.PLAYERTURN;
        PlayerTurn();
    }

    IEnumerator PlayerAttack()
    {
        bool isDead = enemyUnit.TakeDamage(playerUnit.Damage);

        enemyHUD.SetHP(enemyUnit.HP);
        dialogueText.text = "The attack is successful!";
        
        yield return new WaitForSeconds(2f);

        if (isDead)
        {
            state = BattleState.WON;
            EndBattle();
        }
        else
        {
            state = BattleState.ENEMYTURN;
            StartCoroutine(EnemyTurn());
        }
    }

    IEnumerator EnemyTurn()
    {
        dialogueText.text = enemyUnit.Name + " attacks!";

        yield return new WaitForSeconds(1f);

        bool isDead = playerUnit.TakeDamage(enemyUnit.Damage);

        playerHUD.SetHP(playerUnit.HP);
        
        yield return new WaitForSeconds(1f);

        if (isDead)
        {
            state = BattleState.LOST;
            EndBattle();
        }
        else
        {
            state = BattleState.PLAYERTURN;
            PlayerTurn();
        }

    }

    void EndBattle()
    {
        if (state == BattleState.WON)
        {
            dialogueText.text = "You won the battle!";
        }
        else if (state == BattleState.LOST)
        {
            dialogueText.text = "You were defeated.";
        }
    }

    void PlayerTurn()
    {
        dialogueText.text = "Choose an action:";
    }

    IEnumerator PlayerHeal()
    {
        playerUnit.Heal(5);

        playerHUD.SetHP(playerUnit.HP);
        dialogueText.text = "You feel renewed strength!";

        yield return new WaitForSeconds(2f);

        state = BattleState.ENEMYTURN;
        StartCoroutine(EnemyTurn());
    }

    public void OnAttackButton()
    {
        if (state != BattleState.PLAYERTURN)
            return;

        StartCoroutine(PlayerAttack());
    }

    public void OnHealButton()
    {
        if (state != BattleState.PLAYERTURN)
            return;

        StartCoroutine(PlayerHeal());
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