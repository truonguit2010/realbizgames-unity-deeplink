using UnityEngine;

namespace RealbizGames.Platform
{
    [System.Serializable]
    public class MAAutoExecuteDeepLinkConfig
    {
        [SerializeField]
        private bool _auto = true;

        [SerializeField]
        private float _executeAfterSeconds = 1f;

        public bool Auto { get => _auto; set => _auto = value; }
        public float ExecuteAfterSeconds { get => _executeAfterSeconds; set => _executeAfterSeconds = value; }
    }
}
