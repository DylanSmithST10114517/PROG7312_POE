namespace DeweyDecimal.Models
{
    // Represents a structure for finding a Dewey Decimal call number based on a random path in a tree
    public class FindingCallNumber
    {
        // List of nodes representing a random path in a tree
        public List<Node> Nodes { get; set; }

        // The question node, representing the deepest leaf node in the random path
        public Node QuestionNode { get; set; }

        // Constructor to generate a random tree path with a specified count
        public FindingCallNumber(int count)
        {
            // Generate a random path with the specified count
            Nodes = BaseTree.GenerateRandomTreePaths(count);

            // Get the deepest leaf node from the random path and set it as the question node
            QuestionNode = GetLeafNodeFromRandomNode();
        }

        // Parameterless constructor
        public FindingCallNumber()
        {
            // Empty constructor
        }

        // Method to get the deepest leaf node from a random node in the tree
        public Node GetLeafNodeFromRandomNode()
        {
            if (Nodes == null || Nodes.Count == 0)
            {
                // Handle the case where Nodes is not initialized or is empty
                return null;
            }

            Random random = new Random();
            int randomIndex = random.Next(0, Nodes.Count);

            Node randomNode = Nodes[randomIndex];
            return GetDeepestLeafNode(randomNode);
        }

        // Recursive method to find the deepest leaf node in a tree starting from a given node
        private Node GetDeepestLeafNode(Node node)
        {
            // Base case: if the node is a leaf node, return it
            if (node.child == null || node.child.Count == 0)
            {
                return node;
            }

            // Recursive case: find the deepest leaf node among the children
            Node deepestLeaf = null;
            foreach (var childNode in node.child)
            {
                Node childLeaf = GetDeepestLeafNode(childNode);
                if (deepestLeaf == null || (childLeaf != null && childLeaf.child.Count == 0))
                {
                    deepestLeaf = childLeaf;
                }
            }

            return deepestLeaf;
        }
    }
}
