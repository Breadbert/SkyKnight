using UnityEngine;

public class Player : MonoBehaviour
{
    public string Name;
    public int HP;
    public int MaxHP;
    public int Level;
    public int Damage;

    void Start()
    {
        MaxHP = HP;
    }

    public bool TakeDamage(int dmg)
    {
        HP -= dmg;

        if (HP <= 0)
            return true;
        else
            return false;
    }

    public void Heal(int amount)
    {
        HP += amount;
        if (HP > MaxHP)
            HP = MaxHP;
    }
}
