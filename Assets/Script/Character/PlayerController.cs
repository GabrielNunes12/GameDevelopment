using UnityEngine;

namespace RPG.Character
{
    public class PlayerController : MonoBehaviour
    {
        public CharacterStatsSO stats;
        private Health healthComponent;
        private Combat combatComponent;
        private void Awake()
        {
            if(stats == null)
            {
                Debug.LogWarning($"{name} does not have stats.");
            }
            healthComponent = GetComponent<Health>();
            combatComponent = GetComponent<Combat>();
        }
        private void Start()
        {
            healthComponent.healthPoints = stats.health;
            combatComponent.damage = stats.powerAttack;
        }
    }

}
