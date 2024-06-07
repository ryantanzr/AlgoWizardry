using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Algowizardry.Core.GraphTheory {

    public class Node : MonoBehaviour
    {
        private bool isVisited;
        public int ID;

        public Node(int id)
        {
            ID = id;
            isVisited = false;
        }

        public void SetIsVisited(bool visited)
        {
            isVisited = visited;
        }
        
    }

}
