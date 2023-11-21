using System.Text.Json;

namespace DeweyDecimal.Models
{
    public static class BaseTree
    {
        private static bool isInitialized = false;

        public static void Initialize()
        {
            if (!isInitialized)
            {
                LoadDefaultTree();
                isInitialized = true;
            }
        }
        public static Node Root { get; private set; }

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
                secondLevelCount = 0;
                topLevelCount++;
            }
        }

        private static int GenerateRandomNumber(int min, int max, HashSet<int> exclusions)
        {
            var exclude = exclusions;
            var range = Enumerable.Range(min, max).Where(i => !exclude.Contains(i));

            var rand = new Random();
            int index = rand.Next(min, max - exclude.Count);
            return range.ElementAt(index);
        }

        public static List<Tree> GetRandomTreePaths(int count)
        {
            int min = 0;
            int max = BaseTree.Root.child.Count;

            HashSet<int> exclusions = new HashSet<int>();
            List<Tree> randomTrees = new List<Tree>();

            for (int i = 0; i < count; i++)
            {
                Node newRoot = new Node { data = "Root" }; // Create a new root for each tree

                for (int level = 0; level < max; level++)
                {
                    int randomNum = GenerateRandomNumber(min, max, exclusions);
                    Node selectedNode = BaseTree.Root.child[randomNum];

                    // Copy the selected node and add it as a child to the new root
                    newRoot.child.Add(new Node
                    {
                        data = selectedNode.data,
                        child = new List<Node>() // Only one child at each level
                    });

                    exclusions.Add(randomNum);
                }

                // Create a new tree with the new root
                Tree newTree = new Tree(newRoot);
                randomTrees.Add(newTree);
            }

            return randomTrees;
        }


        private static Node newNode(string data)
        {
            Node temp = new Node();
            temp.data = data;
            return temp;
        }
    }
}
