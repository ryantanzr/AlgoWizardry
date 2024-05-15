using System.Collections.Generic;
using NUnit.Framework;
using Algowizardry.Core.GraphTheory;

public class UnionFindTest
{

    private void Setup(ref UnionFind uf, ref Graph g)
    {
        g = new Graph
        {
            vertices = new List<Node>(),
            edges = new List<Edge>()
        };

        // Add vertices for testing
        for (int i = 0; i < 6; i++)
        {
            Node n = new Node(i);
            g.vertices.Add(n);
        }

        // Add edges for testing
        g.edges.Add(new Edge(g.vertices[0], g.vertices[1], 1, false));
        g.edges.Add(new Edge(g.vertices[0], g.vertices[2], 20, false));
        g.edges.Add(new Edge(g.vertices[1], g.vertices[2], 2, false));
        g.edges.Add(new Edge(g.vertices[1], g.vertices[3], 5, false));
        g.edges.Add(new Edge(g.vertices[2], g.vertices[3], 3, false));
        g.edges.Add(new Edge(g.vertices[2], g.vertices[4], 10, false));
        g.edges.Add(new Edge(g.vertices[3], g.vertices[4], 4, false));
        g.edges.Add(new Edge(g.vertices[4], g.vertices[5], 4, false));

        uf = new UnionFind(g.vertices);
    }

    // This test determines if the Union Find joins
    // vertices correctly
    [Test]
    public void UnionTest()
    {
        UnionFind uf = null;
        Graph g = new Graph();

        Setup(ref uf, ref g);

        bool result;

        // Union vertices 0 and 1
        result = uf.Union(g.edges[0].startVertex.ID, g.edges[0].endVertex.ID);
        Assert.IsTrue(result);
        // Union vertices 0 and 2 
        result = uf.Union(g.edges[1].startVertex.ID, g.edges[1].endVertex.ID);
        Assert.IsTrue(result);
        // Union vertices 1 and 2, should yield an error as it has a circuit 
        result = uf.Union(g.edges[2].startVertex.ID, g.edges[2].endVertex.ID);
        Assert.IsFalse(result);
        //Union vertices 1 & 3
        result = uf.Union(g.edges[3].startVertex.ID, g.edges[3].endVertex.ID);
        Assert.IsTrue(result);
        //Union vertices 2 & 3, should yield an error as it has a circuit
        result = uf.Union(g.edges[4].startVertex.ID, g.edges[4].endVertex.ID);
        Assert.IsFalse(result);
        //Union vertices 2 & 4
        result = uf.Union(g.edges[5].startVertex.ID, g.edges[5].endVertex.ID);
        Assert.IsTrue(result);
        //Union vertices 4 & 5
        result = uf.Union(g.edges[7].startVertex.ID, g.edges[7].endVertex.ID);
        Assert.IsTrue(result);


        //Check if the graph is a spanning tree
        Assert.IsTrue(uf.isSpanningTree);
    }

    // This test determines if the Union Find joins
    // vertices correctly
    [Test]
    public void UnionTestWithDevelopedPartitions()
    {
        UnionFind uf = null;
        Graph g = new Graph();

        Setup(ref uf, ref g);

        bool result;

        // Union vertices 0 and 1
        result = uf.Union(g.edges[0].startVertex.ID, g.edges[0].endVertex.ID);
        Assert.IsTrue(result);
        // Union vertices 1 and 2
        result = uf.Union(g.edges[2].startVertex.ID, g.edges[2].endVertex.ID);
        Assert.IsTrue(result);

        //Union vertices 3 & 4
        result = uf.Union(g.edges[6].startVertex.ID, g.edges[6].endVertex.ID);
        Assert.IsTrue(result);
        //Union vertices 4 & 5
        result = uf.Union(g.edges[7].startVertex.ID, g.edges[7].endVertex.ID);
        Assert.IsTrue(result);

        //Union the 2 partitions
        result = uf.Union(0, 3);
        Assert.IsTrue(result);

        //Check if the graph is a spanning tree
        Assert.IsTrue(uf.isSpanningTree);
    }

    // This test determines if the Union Find finds the 
    // representative element of the set correctly
    [Test]
    public void FindTest()
    {
        UnionFind uf = null;
        Graph g = new Graph();

        Setup(ref uf, ref g);

        int LHS, RHS;

        // Union vertices 0 and 1, then 0 and 2
        uf.Union(g.edges[0].startVertex.ID, g.edges[0].endVertex.ID);
        uf.Union(g.edges[1].startVertex.ID, g.edges[1].endVertex.ID);

        // Find the representative element of the set
        LHS = uf.Find(0);
        RHS = uf.Find(2);

        // Representative elements should be 1 in this scenario
        Assert.AreEqual(LHS, RHS);

        // Union vertices 3 and 4
        uf.Union(g.edges[6].startVertex.ID, g.edges[6].endVertex.ID);

        // Find the representative element of the set
        LHS = uf.Find(3);

        // Representative element should be 4 in this scenario
        Assert.AreEqual(LHS, 4);

        // Representative element should not be the same
        Assert.AreNotEqual(LHS, RHS);

    }

    // This test determines if the Union Find splits
    // vertices correctly
    [Test]
    public void SplitTest() {

    }
}
