using System.Collections;
using UnityEngine;

namespace RealbizGames.Platform
{
    [DisallowMultipleComponent]
    public class MADeepLinkTriggerBehaviour : MonoBehaviour
    {
        [SerializeField]
        private float _executeAfterSeconds = 1f;
        [SerializeField]
        private bool executeOnStart = false;

        [SerializeField]
        private bool executeOnResume = true;
        void Start()
        {
            if (executeOnStart) {
                StartCoroutine(Execute());
            }
            
        }

        private void OnApplicationPause(bool pauseStatus) {
            if (!pauseStatus) {
                if (executeOnResume) {
                    StartCoroutine(Execute());
                }
            }
        }

        private IEnumerator Execute() {
            yield return new WaitForSeconds(_executeAfterSeconds);
            if (MADeepLinkBehaviour.Instance != null) {
                MADeepLinkBehaviour.Instance.ExecuteRuleEngine();
            }
        }
    }
}