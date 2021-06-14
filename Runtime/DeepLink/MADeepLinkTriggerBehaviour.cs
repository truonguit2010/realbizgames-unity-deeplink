using System.Collections;
using UnityEngine;

namespace RealbizGames.Platform
{
    [DisallowMultipleComponent]
    public class MADeepLinkTriggerBehaviour : MonoBehaviour
    {
        [SerializeField]
        private float _executeAfterSeconds = 1f;
        void Start()
        {
            StartCoroutine(Execute());
        }

        private IEnumerator Execute() {
            yield return new WaitForSeconds(_executeAfterSeconds);
            if (MADeepLinkBehaviour.Instance != null) {
                MADeepLinkBehaviour.Instance.ExecuteRuleEngine();
            }
        }
    }
}