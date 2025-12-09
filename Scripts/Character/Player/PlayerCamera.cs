using UnityEngine;



namespace VN
{
    public class PlayerCamera : MonoBehaviour
    {
        public static PlayerCamera instance;

        public Camera cameraObject;

        public PlayerManager player;

        [Header("Camera Settings")]
        [SerializeField] Transform cameraPivotTransform;
        private float cameraSmoothSpeed = 1;

        [SerializeField] float leftAndRightLookSpeed = 220;

        [SerializeField] float upAndDownLookSpeed = 220;

        [SerializeField] float minimumPivot = -30;

        [SerializeField] float maximumPivot = 60;


        [SerializeField] float cameraCollisionRadius = 0.2f;





        [Header("Camera Values")]
        private Vector3 cameraVelocity;
        private Vector3 cameraObjectPosition;
        [SerializeField] float leftAndRightLookAngle;

        [SerializeField] float upAndDownLookAngle;

        private float cameraZPosition;
        private float targetCameraZPosition;

        [SerializeField] LayerMask collideWithLayers;


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
            cameraZPosition = cameraObject.transform.localPosition.z;
        }
    
    
        public void HandleAllCameraActions()
        {
            // FOLLOW THE PLAYER
            // ROTATE AROUND THE PLAYER
            // COLLIDE WITH OBJECTS

            if(player != null)
            {
                HandleFollowTarget();
                HandleRotation();
                HandleCollisions();
            }


        }


        private void HandleFollowTarget()
        {
            Vector3 targetCameraZPosition = Vector3.SmoothDamp(transform.position,player.transform.position,ref cameraVelocity,cameraSmoothSpeed * Time.deltaTime);
            transform.position = targetCameraZPosition;
        }
    
    

        private void HandleRotation()
        {
            // IF LOCKED ON FORCE ROTATION TOWARD TARGET
            // ELSE ROTATE REGULARLY


            leftAndRightLookAngle += PlayerInputManager.instance.cameraHorizontalInput * leftAndRightLookSpeed * Time.deltaTime;
            upAndDownLookAngle -= PlayerInputManager.instance.cameraVerticalInput * upAndDownLookSpeed * Time.deltaTime;
            upAndDownLookAngle = Mathf.Clamp(upAndDownLookAngle,minimumPivot,maximumPivot);


            Vector3 cameraRotation = Vector3.zero;
            cameraRotation.y = leftAndRightLookAngle;
            Quaternion targetRotation = Quaternion.Euler(cameraRotation);
            transform.rotation = targetRotation;



            cameraRotation = Vector3.zero;
            cameraRotation.x = upAndDownLookAngle;
            targetRotation = Quaternion.Euler(cameraRotation);
            cameraPivotTransform.localRotation = targetRotation;
        }


        private void HandleCollisions()
        {
            targetCameraZPosition = cameraZPosition;
            RaycastHit hit;
            Vector3 direction = cameraObject.transform.position - cameraPivotTransform.position;
            direction.Normalize();

            if(Physics.SphereCast(cameraPivotTransform.position,cameraCollisionRadius,direction,out hit, Mathf.Abs(targetCameraZPosition),collideWithLayers))
            {
                float distanceFromHitObject = Vector3.Distance(cameraPivotTransform.position, hit.point);
                targetCameraZPosition = -(distanceFromHitObject - cameraCollisionRadius);
            }

            if(Mathf.Abs(targetCameraZPosition) < cameraCollisionRadius)
            {
                targetCameraZPosition = -cameraCollisionRadius;
            }


            cameraObjectPosition.z = Mathf.Lerp(cameraObject.transform.localPosition.z,targetCameraZPosition, 0.2f);

            cameraObject.transform.localPosition = cameraObjectPosition;
        }

    }

}