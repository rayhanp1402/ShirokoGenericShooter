using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Nightmare
{
    public class LevelManager : MonoBehaviour
    {
        public levelInfo[] levels;
        private int currentLevel;
        private Scene currentScene;
        private PlayerMovement playerMove;
        private Vector3 playerRespawn;

        private Canvas playerHUD;

        void OnEnable()
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
            SceneManager.sceneUnloaded += OnSceneUnloaded;
        }

        void Start()
        {
            playerMove = FindObjectOfType<PlayerMovement>();
            playerRespawn = playerMove.transform.position;
            playerHUD = GameObject.Find("HUDCanvas").GetComponent<Canvas>();
            LoadInitialLevel();
        }

        public void AdvanceLevel()
        {
            LoadLevel(currentLevel + 1);
        }

        public void LoadInitialLevel()
        {
            LoadLevel(InitialLevel.getLevel() == -1 ? 0 : InitialLevel.getLevel());
        }

        public int GetCurrentLevel()
        {
            return currentLevel;
        }

        private void LoadLevel(int level)
        {
            currentLevel = level;

            //Load next level in background
            string loadingScene = levels[level % levels.Length].name;

            if (levels[level % levels.Length].isCutscene){
                SceneManager.sceneLoaded -= OnCutsceneLoaded;
                SceneManager.sceneLoaded -= OnSceneLoaded;
                SceneManager.sceneLoaded += OnCutsceneLoaded;
            }
            else{
                SceneManager.sceneLoaded -= OnCutsceneLoaded;
                SceneManager.sceneLoaded -= OnSceneLoaded;
                SceneManager.sceneLoaded += OnSceneLoaded;
                
            }


            SceneManager.LoadSceneAsync(loadingScene, LoadSceneMode.Additive);
        }

        void OnCutsceneLoaded(Scene scene, LoadSceneMode mode)
        {
            Debug.Log("Cutscene loaded: " + scene.name);
            if (mode != LoadSceneMode.Additive)
                return;

            playerHUD.enabled = false;

            SceneManager.SetActiveScene(scene);

            DisableOldScene();

            currentScene = scene;
        }

        void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            Debug.Log("Scene loaded: " + scene.name);
            if (mode != LoadSceneMode.Additive)
                return;

            playerMove.transform.position = playerRespawn;
            Debug.Log(playerHUD.GameObject().name);
            playerHUD.enabled = true;
            SceneManager.SetActiveScene(scene);

            DisableOldScene();

            currentScene = scene;
        }

        private void DisableOldScene()
        {
            if (currentScene.IsValid())
            {
                GameObject[] oldSceneObjects = currentScene.GetRootGameObjects();
                for (int i = 0; i < oldSceneObjects.Length; i++)
                {
                    oldSceneObjects[i].SetActive(false);
                }

                // Unload it.
                SceneManager.UnloadSceneAsync(currentScene);
            }
        }

        void OnSceneUnloaded(Scene scene)
        {

        }

        void OnDisable()
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
            SceneManager.sceneUnloaded -= OnSceneUnloaded;
        }
    }
}

[System.Serializable]
public class levelInfo {
    public string name;
    public bool isCutscene;
}

public static class InitialLevel {
    private static int level = -1;

    public static int getLevel(){
        return level;
    }

    public static void setLevel(int newLevel){
        level = newLevel;
    }
}