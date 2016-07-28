using UnityEngine;

namespace Models
{
    public class ItemData : ScriptableObject
    {
        public string Name;
        public string Description;
        public int Weight;
        public CategoryType Category;
    }
}
