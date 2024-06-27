using System.Collections.Generic;

/**********************************************************
* Author: Ryan Tan Zheng Rong
* A script emulating DFS behavior which the player
**********************************************************/

namespace Algowizardry.core.GraphTheory {

    public enum DFSType : byte
    {
        PREORDER,
        INORDER,
        POSTORDER
    }

    public class RealtimeDFS
    {
     

        public Graph graph;
        private List<Node> cyclicNodes;
        private DFSType currentDFSType;


        private void ReselectDFSType()
        {
            currentDFSType = (DFSType)Random.Range(0, System.Enum.GetValues(typeof(DFSType)).Length)
        }

        private void DFS(Node currentNode) 
        {

            switch (currentDFSType)
            {
                case DFSType.PREORDER:
                    currentNode.SetIsVisited(true);
                    break;
                case DFSType.INORDER:
                    if
                    break;
                case DFSType.POSTORDER:
                    break;
            }
        }
    }
}