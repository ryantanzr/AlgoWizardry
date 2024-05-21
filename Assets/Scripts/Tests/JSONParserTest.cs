using NUnit.Framework;
using Algowizardry.Utility;
public class JSONParserTest
{
    // A Test behaves as an ordinary method
    [Test]
    public void ParsingTest()
    {
        DialogueContainer dialogues = DialogueParser.ParseDialogue("Assets/Scripts/Tests/TestDialogue.json");
        Assert.IsNotNull(dialogues);

        foreach (Dialogue d in dialogues.dialogues)
        {
            Assert.IsNotNull(d);
            Assert.IsNotNull(d.id);
            Assert.IsNotNull(d.lines);
            foreach (DialogueLine dl in d.lines)
            {
                Assert.IsNotNull(dl);
                Assert.IsNotNull(dl.text);
            }
        }
        // Use the Assert class to test conditions
    }

    [Test]
    public void ParsingTestTwo()
    {
        DialogueContainer dialogues = DialogueParser.ParseDialogue("Assets/Resources/Dialogue/KruskalDialogue.json");
        Assert.IsNotNull(dialogues);

        foreach (Dialogue d in dialogues.dialogues)
        {
            Assert.IsNotNull(d);
            Assert.IsNotNull(d.id);
            Assert.IsNotNull(d.lines);
            foreach (DialogueLine dl in d.lines)
            {
                Assert.IsNotNull(dl);
                Assert.IsNotNull(dl.text);
            }
        }
    }
}
