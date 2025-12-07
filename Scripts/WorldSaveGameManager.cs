using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

namespace VN
{

    public class WorldSaveGameManager : MonoBehaviour
    {
        public static WorldSaveGameManager instance;


        [SerializeField] int worldSceneIndex = 1;


        private void Awake()
        {

            // There Can Only be One Instance of Save Manager At the screen at one time.
            if (instance == null)
            {
                instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }


        private void Start()
        {
            DontDestroyOnLoad(gameObject);
        }


        public IEnumerator LoadNewGame()
        {
            AsyncOperation loadOperation = SceneManager.LoadSceneAsync(worldSceneIndex);

            yield return null;
        }
    }

}
