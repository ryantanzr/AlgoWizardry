using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/**********************************************************
 * Author: Ryan Tan
 * Graph is a class that is used to represent a graph
 * which is defined as a collection of connected vertices
 * and edges.
 *********************************************************/

 namespace Algowizardry.Core.GraphTheory {
    public class Graph : MonoBehaviour
    {

        public List<Node> vertices;
        public List<Edge> edges;

        private bool isConnected;
        private bool isAcyclic;

        public void SetIsConnected(bool connected)
        {
            isConnected = connected;
        }

        public void SetIsAcyclic(bool acyclic)
        {
            isAcyclic = acyclic;
        }

    }
 }