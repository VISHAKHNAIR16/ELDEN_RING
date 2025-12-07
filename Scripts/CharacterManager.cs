using UnityEngine;


namespace VN
{
    public class CharacterManager : MonoBehaviour
    {
        private void Awake() {
            DontDestroyOnLoad(this);
        }
    }

}