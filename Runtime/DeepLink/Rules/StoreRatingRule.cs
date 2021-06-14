using RealbizGames.RulePattern;

namespace RealbizGames.Platform
{
    public class StoreRatingRule : IRule<MADeepLinkRuleExecuteOutput, MADeepLink>
    {
        public const string TAG = "StoreRatingRule";
        
        private string linkAction;

        public StoreRatingRule(string linkAction)
        {
            this.linkAction = linkAction;
        }

        public MADeepLinkRuleExecuteOutput Execute(MADeepLink expression)
        {
            #if UNITY_EDITOR
            UnityEngine.Debug.LogFormat("{0} - Execute {1}", TAG, expression);
            #endif
            
            return new MADeepLinkRuleExecuteOutput();
        }

        public bool Valuate(MADeepLink expression)
        {
            return linkAction.Equals(expression.LinkAction);
        }
    }
}
