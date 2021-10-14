using System;
using UnityEngine;

namespace UnityTemplateProjects
{
    //The existence of a SINGLETON GAME MANAGER signifies a soon to be crumbling pile of shit code
    //Destroy it. Steer away from it. Do all you can to veer away from the abomination that is
    //the SINGLETON GAME MANAGER.
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance;

        public PlayerController Player;

        private void Awake()
        {
            if (Instance == null) Instance = this;
            if (Instance != this) Destroy(gameObject);
        }
    }
}