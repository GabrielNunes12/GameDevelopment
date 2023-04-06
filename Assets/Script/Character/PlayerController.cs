using UnityEngine;

namespace RPG.Character
{
    public class PlayerController : MonoBehaviour
    {
        public CharacterStatsSO stats;
        private void Awake()
        {
            if(stats == null)
            {
                Debug.LogWarning($"{name} does not have stats.");
            }
        }
    }

}
