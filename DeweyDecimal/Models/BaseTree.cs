using System.Text.Json;

namespace DeweyDecimal.Models
{
    public static class BaseTree
    {
        // Property representing the root node of the tree
        public static Node Root { get; private set; }

        // Private instance of Random class for random operations
        private static readonly Random random = new Random();

        // Static constructor to initialize the default tree
        static BaseTree()
        {
            LoadDefaultTree();
        }

        // Private method to load the default tree structure from a JSON file
        private static void LoadDefaultTree()
        {
            // Read the JSON data from the file
            var text = File.ReadAllText("./Data/categories.json");

            // Deserialize the JSON data into a dictionary
            var dict = JsonSerializer.Deserialize<Dictionary<string, Dictionary<string, List<string>>>>(text);

            // Create the root node
            Root = newNode("root");

            int topLevelCount = 0;
            int secondLevelCount = 0;

            // Traverse the dictionary and populate the tree structure
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

        // Method to generate a list of random tree paths with a specified top-level count
        public static List<Node> GenerateRandomTreePaths(int topLevelCount)
        {
            List<Node> randomTree = new List<Node>();

            // Ensure topLevelCount is within the range of available top-level nodes
            topLevelCount = Math.Min(topLevelCount, Root.child.Count);

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

        // Private method to create a new node with the specified data
        private static Node newNode(string data)
        {
            Node temp = new Node();
            temp.data = data;
            return temp;
        }
    }
}
