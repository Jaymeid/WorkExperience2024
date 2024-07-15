using System.Security.Cryptography;

class Player : Character{

    public Player(){
        health = 100;
        attackDamage = 10;
    }

    public bool BlockAttack(){
        Random rand = new Random();
        int randNumber = rand.Next(0, 9);

        if(randNumber >= 2){
            return true;
        }

        return false;
    }

    public override void Die(){
        Console.WriteLine("You have succumbed to the monsters.");
        Environment.Exit(0);
    }

}