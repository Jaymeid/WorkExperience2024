using System.Runtime.InteropServices;

class Character
{
    protected int health;
    protected int attackDamage;
    protected string characterName;

    protected bool isDead = false;

    public Character()
    {
        characterName = "baseCharacter";
    }

    public string GetCharacterName(){
        return characterName;
    }
    public virtual int GetAttackDamage()
    {
        return attackDamage;
    }

    public virtual void Die()
    {
        Console.WriteLine($"{characterName} has died");
        isDead = true;
    }

    public int GetHealth()
    {
        return health;
    }

    public void SetHealth(int newHealth){
        health = newHealth;
    }

    public void TakeDamage(int damageReceived)
    {
        health -= damageReceived;
        if (health <= 0)
        {
            Die();
        }
    }

    public void SetAttackDamage(int newAttackDamage)
    {
        attackDamage = newAttackDamage;
    }

    public bool GetIsDead(){
        return isDead;
    }
}