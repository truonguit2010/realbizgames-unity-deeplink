using UnityEngine;
using System.Collections;
using RealbizGames.RulePattern;

namespace RealbizGames.Platform
{
    public class MADeepLinkRuleExecuteOutput
    {

    }

    // https://docs.unity3d.com/Manual/enabling-deep-linking.html
    [DisallowMultipleComponent]
    public class MADeepLinkBehaviour : MonoBehaviour
    {
        public const string TAG = "MADeepLinkBehaviour";

        public const string DEEP_LINK_STORE_RATING_ACTION = "store_rating";

        public static MADeepLinkBehaviour Instance { get; private set; }

        [SerializeField]
        private string editorTestDeepLink = "yourscheme://store_rating?showHand=1&b=asdghj";

        private MADeepLink _deepLink = null;

        public MADeepLink DeepLink {
            get {
                return _deepLink;
            }
        }

        [SerializeField]
        private MAAutoExecuteDeepLinkConfig _config;

        private IRuleEngine<MADeepLinkRuleExecuteOutput, MADeepLink> _ruleEngine;

        private void Awake()
        {
            if (Instance == null)
            {
                Debug.LogFormat("{0} - deeplink Awake int {1}", TAG, Application.absoluteURL);

                Instance = this;
                InitRuleEngine();

                Application.deepLinkActivated += onDeepLinkActivated;

                if (!string.IsNullOrEmpty(Application.absoluteURL))
                {
                    // Cold start and Application.absoluteURL not null so process Deep Link.
                    onDeepLinkActivated(Application.absoluteURL);
                }
                else
                {
#if UNITY_EDITOR
                    onDeepLinkActivated(editorTestDeepLink);
#endif
                }

                DontDestroyOnLoad(this.gameObject);
            }
            else
            {
                Destroy(this.gameObject);
            }
        }

        private void OnApplicationPause(bool pause)
        {
            if (pause)
            {
            }
            else
            {
                if (_config.Auto)
                {
                    StartCoroutine(AutoExecuteDeepLinkRuleEngine());
                }
                StartCoroutine(checkDeepLinkByMe());
            }
        }

        private IEnumerator AutoExecuteDeepLinkRuleEngine() {
            yield return new WaitForSeconds(_config.ExecuteAfterSeconds);
            ExecuteRuleEngine();
        }

        // --------------------------------------------------------------------------------
        // --------------------------------------------------------------------------------
        // RuleEngine
        // --------------------------------------------------------------------------------

        private void InitRuleEngine()
        {
            _ruleEngine = new GenericRuleEngine<MADeepLinkRuleExecuteOutput, MADeepLink>();
            _ruleEngine.AddRule(new StoreRatingRule(linkAction: DEEP_LINK_STORE_RATING_ACTION, behaviour: this));
        }

        public void AddRule(IRule<MADeepLinkRuleExecuteOutput, MADeepLink> rule)
        {
            _ruleEngine.AddRule(rule);
        }

        public void ExecuteRuleEngine(bool cleanDeepLink = true)
        {
            if (this._deepLink != null)
            {
                Debug.LogFormat("{0} - ExecuteRuleEngine with {1}", TAG, this._deepLink);
                _ruleEngine.Execute(this._deepLink);
                if (cleanDeepLink)
                {
                    cleanDeepLinkInformation();
                }
            } else {
                Debug.LogFormat("{0} - Skip ExecuteRuleEngine with {1}", TAG, this._deepLink);
            }
        }

        // --------------------------------------------------------------------------------
        // --------------------------------------------------------------------------------
        // --------------------------------------------------------------------------------

        private IEnumerator checkDeepLinkByMe()
        {
            Debug.LogFormat("{0} - deeplink checkDeepLinkByMe Start {1}", TAG, Application.absoluteURL);
            yield return new WaitForSeconds(3);
            Debug.LogFormat("{0} - deeplink checkDeepLinkByMe After Wait {1}", TAG, Application.absoluteURL);
        }

        public void cleanDeepLinkInformation()
        {
            this._deepLink = null;
        }

        public void onDeepLinkActivated(string url)
        {
            Debug.LogFormat("{0} - deeplink onDeepLinkActivated {1}", TAG, url);
            this._deepLink = new MADeepLink(url);
        }
    }
}
