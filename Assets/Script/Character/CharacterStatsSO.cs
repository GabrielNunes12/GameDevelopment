using UnityEngine;

namespace RPG.Character
{
    [CreateAssetMenu(
        fileName = "Character Stats SO",
        menuName = "Character SO",
        order = 0
     )]
    public class CharacterStatsSO : ScriptableObject
    {
        public float health = 100f;
        public float powerAttack = 10f;
        public float walkSpeed = 1f;
        public float runSpeed = 1.5f;
    }
}

