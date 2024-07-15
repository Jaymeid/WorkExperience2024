
    class Room
    {
        public string Name { get; }
        public string Description { get; }
        public Dictionary<string, Room> AdjacentRooms { get; }

        private bool hasMonster = false;

        public Room(string name, string description, bool hasMonster)
        {
            Name = name;
            Description = description;
            AdjacentRooms = new Dictionary<string, Room>();

            this.hasMonster = hasMonster;
        }

        public void AddAdjacentRoom(string direction, Room room)
        {
            AdjacentRooms[direction] = room;
        }

        public void SetHasMonster(bool newHasMonsterValue){
            hasMonster = newHasMonsterValue;
        }

        public bool GetHasMonster(){
            return hasMonster;
        }
    }