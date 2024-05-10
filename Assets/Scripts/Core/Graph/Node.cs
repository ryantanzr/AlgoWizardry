using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Algowizardry.Core.GraphTheory {

    public class Node : MonoBehaviour
    {
        private bool isVisited;
        public int ID { get; private set; }

        public Node(int id)
        {
            ID = id;
            isVisited = false;
        }

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
