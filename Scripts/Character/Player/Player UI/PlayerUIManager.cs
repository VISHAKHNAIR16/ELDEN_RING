using UnityEngine;
using System.Collections;
using Unity.Netcode;


namespace VN
{


    public class PlayerUIManager : MonoBehaviour
    {

        public static PlayerUIManager instance;

        [Header("NETWORK JOIN")]
        [SerializeField] bool startGameAsClient;

        private void Awake() {
            if(instance == null)
            {
                instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }


        private void Start() {
            DontDestroyOnLoad(gameObject);
        }

        private void Update() {
            if(startGameAsClient)
            {
                startGameAsClient = false;
                // We must ShutDown Because we started as a host during the title screen
                NetworkManager.Singleton.Shutdown();
                // We then start the network as a client

                NetworkManager.Singleton.StartClient();
            }    
        }
    }

}