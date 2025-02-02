using UnityEngine.UIElements;

namespace AISystem.BehaviourTrees.Editor
{
    public class BTSplitView : TwoPaneSplitView
    {
        public new class UxmlFactory : UxmlFactory<BTSplitView, TwoPaneSplitView.UxmlTraits>
        {
        }
    }
}