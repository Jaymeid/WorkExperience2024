class HealthPotion : Item
{
    public int healingAmount { get; }

    public HealthPotion(string name, string description, int healingAmount)
        : base(name, description)
    {
        this.healingAmount = healingAmount;
    }

    public override void Use(Player player)
    {
        
        // Implement the healing logic here

        if (player.GetHealth() == 100)
        {
            Console.WriteLine("Player already has max health");
        }
        else
        {
            player.AddHealth(healingAmount);
            Console.WriteLine($"You use the {name} and restore {healingAmount} health points.");
            Console.WriteLine($"You now have {player.GetHealth()} HP");
        }

    }
}