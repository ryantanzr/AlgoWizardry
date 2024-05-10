using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Algowizardry.Core.GraphTheory {

    public class Node : MonoBehaviour
    {
        public List<Edge> edges;
        private bool isVisited;
        public int ID { get; private set; }

        public void Visit()
        {
            isVisited = true;
        }

        public void Unvisit()
        {
            isVisited = false;
        }
        
    }

}
