class Troll : Character
{
    public Troll()
    {
        health = 50;
        attackDamage = 10;
        characterName = "Troll";
    }

    public override int GetAttackDamage()
    {
        if (hasAttackLanded())
        {
            Console.WriteLine($"\nAttack from the {characterName} has landed! You take {attackDamage} damage");
            return attackDamage;
        }

        Console.WriteLine($"\nAttack from the {characterName} has missed!");
        return 0;
    }

    private bool hasAttackLanded()
    {
        Random rand = new Random();

        int randNumber = rand.Next(0, 10);
        //return randNumber <= 6 ? true : false;

        if (randNumber <= 6)
        {
            return true;
        }

        return false;
    }


}