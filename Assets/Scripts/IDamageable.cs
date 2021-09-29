namespace UnityTemplateProjects
{
    public interface IDamageable
    {
        public enum Grouping
        {
            Player,
            Enemy,
            Prop
        }
        
        public struct DamageData
        {
            public float damage;
        }
        
        public Grouping Group { get; }

        public void Damage(DamageData data);
    }
}