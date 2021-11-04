using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UnityTemplateProjects
{
    //The existence of a SINGLETON GAME MANAGER signifies a soon to be crumbling pile of shit code
    //Destroy it. Steer away from it. Do all you can to veer away from the abomination that is
    //the SINGLETON GAME MANAGER.
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance;

        public PlayerController Player;
        public bool IsGameOver = false;
        
        [SerializeField] private CharacterData player;
        [SerializeField] private CharacterData boss;

        [SerializeField] private GameObject gameOverLoseCanvas;
        [SerializeField] private GameObject gameOverWinCanvas;

        private void OnEnable()
        {
            player.OnHealthZero += delegate { OnGameOver(false); };;
            boss.OnHealthZero += delegate { OnGameOver(true); };;
        }

        private void OnGameOver(bool isWin)
        {
            player.OnHealthZero = null;
            boss.OnHealthZero = null;
            
            gameOverLoseCanvas.SetActive(!isWin);
            gameOverWinCanvas.SetActive(isWin);
            Invoke(nameof(GoToMenu), 3f);
            IsGameOver = true;
        }

        private void GoToMenu() => SceneManager.LoadScene("MainMenu");
    
        private void Awake()
        {
            if (Instance == null) Instance = this;
            if (Instance != this) Destroy(gameObject);
        }
    }
}