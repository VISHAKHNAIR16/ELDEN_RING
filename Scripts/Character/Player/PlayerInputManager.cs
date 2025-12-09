using UnityEngine;
using UnityEngine.SceneManagement;


namespace VN
{


    public class PlayerInputManager : MonoBehaviour
    {

        public static PlayerInputManager instance;

        // Thinking about goals in Steps 
        // 1. Find a way to read the values of a joy stick 
        // 2. Move Character based on those values
        PlayerControls playerControls;
        [SerializeField] Vector2 movementInput;
        public float horizontalInput;
        public float verticalInput;

        public float moveAmount;

        
        [SerializeField] Vector2 cameraInput;
        public float cameraHorizontalInput;
        public float cameraVerticalInput;

        



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

        private void OnSceneChanged(Scene oldScene,Scene newScene)
        {
            // If we are loading into our world scene, Enable Our Player Controls 
            if(newScene.buildIndex == WorldSaveGameManager.instance.GetWorldSceneIndex())
            {
                instance.enabled = true;
            }
            // If we are loading to any other scene then we are disabling the Player controls
            // This is so our Player cant move around if we enter things like a character creation menu etc.
            else
            {
                instance.enabled = false;
            }
        }

        private void OnDestroy() {
            // If we destroy this Object Unsubscribe from this event
            SceneManager.activeSceneChanged -= OnSceneChanged;
        }


        private void OnEnable() {
            if (playerControls == null)
            {
                playerControls = new PlayerControls();

                playerControls.PlayerMovement.Movement.performed += i => movementInput = i.ReadValue<Vector2>();
                playerControls.PlayerCamera.Camera.performed += i => cameraInput = i.ReadValue<Vector2>();
            }

            playerControls.Enable();
        }
        
        private void Start() {
            DontDestroyOnLoad(gameObject);

            // When The Scene Changes, Run this logic
            SceneManager.activeSceneChanged += OnSceneChanged;


            instance.enabled = false;
        }
        

        private void Update() {
            HandleMovementInput();
            HandleCameraMovementInput();
        }

        private void HandleMovementInput()
        {
            verticalInput = movementInput.y;
            horizontalInput = movementInput.x;


            moveAmount = Mathf.Clamp01(Mathf.Abs(verticalInput) + Mathf.Abs(horizontalInput));


            if(moveAmount <= 0.5 && moveAmount > 0)
            {
                moveAmount = 0.5f;
            }
            else if (moveAmount > 0.5 && moveAmount <= 1)
            {
                moveAmount = 1;
            }
        }


        private void OnApplicationFocus(bool focus)
        {
            if(enabled)
            {
                if(focus)
                {
                    playerControls.Enable();
                }
                else
                {
                    playerControls.Disable();
                }
            }
        }



        private void HandleCameraMovementInput()
        {
            cameraVerticalInput = cameraInput.y;
            cameraHorizontalInput = cameraInput.x;
        }

    }

}