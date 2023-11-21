namespace DeweyDecimal.Models
{
    public class FindingCallNumber
    {
        public List<Node> Nodes { get; set; }

        public Node QuestionNode { get; set; }

        public FindingCallNumber(int count)
        {
            Nodes = BaseTree.GenerateRandomTreePaths(count);
            QuestionNode = GetLeafNodeFromRandomNode();
        }

        public FindingCallNumber()
        {

        }

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
