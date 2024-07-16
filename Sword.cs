class Sword : Item {

    private int newAttackDamage;
    public Sword(string name, string description, int newAttackDamage){
        this.newAttackDamage = newAttackDamage;

    }

    public void SetNewAttackDamage(int newDamage){
        newAttackDamage = newDamage;
    }

    public int GetNewAttackDamage(){
        return newAttackDamage;
    }

    public override void Use(Player player)
    {
        
    }
    
}