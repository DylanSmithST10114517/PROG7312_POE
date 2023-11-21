using System.Text.Json;

namespace DeweyDecimal.Models
{
    public static class BaseTree
    {
        public static Node Root { get; private set; }
        private static readonly Random random = new Random();
        static BaseTree()
        {
            LoadDefaultTree();

        }

        private static void LoadDefaultTree()
        {
            var text = File.ReadAllText("./Data/categories.json");
            var dict = JsonSerializer.Deserialize<Dictionary<string, Dictionary<string, List<string>>>>(text);
            Root = newNode("root");
            int topLevelCount = 0;
            int secondLevelCount = 0;
            foreach (var key in dict.Keys)
            {
                Root.child.Add(newNode(key));
                foreach (var secondKey in dict[key].Keys)
                {
                    Root.child[topLevelCount].child.Add(newNode(secondKey));
                    foreach (string secondLevel in dict[key][secondKey])
                    {
                        Root.child[topLevelCount].child[secondLevelCount].child.Add(newNode(secondLevel));
                    }
                    secondLevelCount++;
                }

                // Reset the secondLevelCount for each top-level iteration
                secondLevelCount = 0;
                topLevelCount++;
            }
        }

        public static List<Node> GenerateRandomTreePaths(int topLevelCount)
        {
            List<Node> randomTree = new List<Node>();

            // Ensure topLevelCount is within the range of available top-level nodes
            topLevelCount = Math.Min(topLevelCount, Root.child.Count);

            Random random = new Random();

            for (int i = 0; i < topLevelCount; i++)
            {
                Node topLevelNode = Root.child[i];

                // Ensure the top-level node has at least one child
                if (topLevelNode.child.Count > 0)
                {
                    Node randomChild = topLevelNode.child[random.Next(topLevelNode.child.Count)];
                    Node randomGrandchild = randomChild.child.Count > 0 ? randomChild.child[random.Next(randomChild.child.Count)] : null;

                    // Create a new node pointing to a random grandchild, or null if there are no grandchild nodes
                    Node newNode = new Node { data = $"RandomNode-{i}" };
                    newNode.child.Add(randomGrandchild);

                    randomTree.Add(newNode);
                }
            }

            return randomTree;
        }


        private static Node newNode(string data)
        {
            Node temp = new Node();
            temp.data = data;
            return temp;
        }
    }
}
