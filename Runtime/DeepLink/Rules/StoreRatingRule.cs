using RealbizGames.RulePattern;
using UnityEngine;

namespace RealbizGames.Platform
{
    public class StoreRatingRule : IRule<MADeepLinkRuleExecuteOutput, MADeepLink>
    {
        public const string TAG = "StoreRatingRule";
        
        private string linkAction;
        private MonoBehaviour behaviour;

        public StoreRatingRule(string linkAction, MonoBehaviour behaviour)
        {
            this.linkAction = linkAction;
            this.behaviour = behaviour;
        }

        public MADeepLinkRuleExecuteOutput Execute(MADeepLink expression)
        {
            #if UNITY_EDITOR
            UnityEngine.Debug.LogFormat("{0} - Execute {1}", TAG, expression);
            #endif
            StoreRatingInstance.DefaultInstance.Rate(behaviour: behaviour, string.Empty);
            
            return new MADeepLinkRuleExecuteOutput();
        }

        public bool Valuate(MADeepLink expression)
        {
            return linkAction.Equals(expression.LinkAction);
        }
    }
}
