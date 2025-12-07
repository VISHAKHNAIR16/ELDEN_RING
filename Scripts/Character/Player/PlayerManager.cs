using UnityEngine;


namespace VN
{
    public class PlayerManager : CharacterManager
    {

        PlayerLocomotionManager playerLocomotionManager;


        protected override void Awake() {
            base.Awake();



            // Do More Stuff Only For Player

            playerLocomotionManager = GetComponent<PlayerLocomotionManager>();
        }



        protected override void Update()
        {
            base.Update();

            if(!IsOwner)
                return;

            playerLocomotionManager.HandleAllMovement();
        }
    }
}