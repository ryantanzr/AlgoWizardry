using NUnit.Framework;
using Algowizardry.Core.Minigames;
using Algowizardry.Core.GraphTheory;
using Algowizardry.Minigames;
using Algowizardry.Utility;

public class MinimumSpanningTreeMinigameTest
{

    public MinimumSpanningTreeMinigame Setup(FeaturedTopic topic, bool directed)
    {

        DialogueCache.CacheDialogue(FeaturedTopic.Kruskal, DialogueParser.ParseDialogue("Assets/Resources/Dialogue/KruskalDialogue.json"));

        Graph g = new Graph
        {
            vertices = new System.Collections.Generic.List<Node>(),
            edges = new System.Collections.Generic.List<Edge>()
        };

        g.vertices.Add(new Node(0));
        g.vertices.Add(new Node(1));
        g.vertices.Add(new Node(2));
        g.vertices.Add(new Node(3));
        g.vertices.Add(new Node(4));

        g.edges.Add(new Edge(g.vertices[0], g.vertices[1], 12, directed));
        g.edges.Add(new Edge(g.vertices[0], g.vertices[2], 7, directed));
        g.edges.Add(new Edge(g.vertices[0], g.vertices[3], 2, directed));
        g.edges.Add(new Edge(g.vertices[0], g.vertices[4], 10, directed));
        g.edges.Add(new Edge(g.vertices[1], g.vertices[2], 3, directed));
        g.edges.Add(new Edge(g.vertices[2], g.vertices[3], 95, directed));
        g.edges.Add(new Edge(g.vertices[3], g.vertices[4], 6, directed));
        g.edges.Add(new Edge(g.vertices[4], g.vertices[0], 1, directed));
        
        MinimumSpanningTreeMinigame minigame = new MinimumSpanningTreeMinigame(topic);
        minigame.LoadNextGraph(g, 25);

        return minigame;
    }

    // A Test behaves as an ordinary method
    [Test]
    public void KruskalsAlgorithmTest()
    {

        MinimumSpanningTreeMinigame minigame = Setup(FeaturedTopic.Kruskal, false);

        // Test the Kruskal's algorithm
        minigame.Union(minigame.graph.edges[0]);
        Assert.AreEqual(minigame.accumulatedCost, 12);

    }

    [Test]
    public void PrimsAlgorithmTest()
    {

    }

    [Test]
    public void LoadingNewRoundTest()
    {

    }

    [Test]
    public void ResetTest()
    {

    }

}
