using UnityEngine;

namespace Assets._Scripts.Systems
{
    public class PersistDataSystem : MonoBehaviour
    {
        public bool IsHard;
        public bool IsNormal;

        void Start()
        {
            DontDestroyOnLoad(this);
        }
    }
}
