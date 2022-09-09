namespace ConsoleApp2.Weapone
{
    public abstract class WeaponeBase
    {
        public int Range { get; set; }
        public int Damage { get; set; }
        public int Id { get; set; }
        public int TypeId { get; private set; }

        private WeaponeBase()
        { }

        protected WeaponeBase(int range, int damage, int id, int typeId)
        {
            Damage = damage;
            Range = range;
            Id = id;
            TypeId = typeId;
        }

        public virtual int SendDamage() { return Damage; }
    }
}
