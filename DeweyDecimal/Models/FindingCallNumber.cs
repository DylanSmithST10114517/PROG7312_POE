namespace DeweyDecimal.Models
{
    public class FindingCallNumber
    {
        public List<Tree> RandomTreePaths { get; set; }  

        public Node QuestionNode { get; set; }

        public FindingCallNumber(int count)
        {
            RandomTreePaths = BaseTree.GetRandomTreePaths(count);
            QuestionNode = GetLowestLevelNodeFromRandomTree();
        }

        public FindingCallNumber()
        {

        }

        public Node GetLowestLevelNodeFromRandomTree()
        {
            if (RandomTreePaths == null || RandomTreePaths.Count == 0)
            {
                // Handle the case where there are no random trees
                return null;
            }

            // Choose a random tree index
            int randomTreeIndex = new Random().Next(0, RandomTreePaths.Count);
            Tree randomTree = RandomTreePaths[randomTreeIndex];

            // Get the lowest-level node from the random tree
            Node lowestLevelNode = GetLowestLevelNode(randomTree.Root);

            return lowestLevelNode;
        }

        private Node GetLowestLevelNode(Node currentNode)
        {
            if (currentNode == null || currentNode.child.Count == 0)
            {
                // If the current node is null or has no children, it is the lowest-level node
                return currentNode;
            }

            // Recursively find the lowest-level node among the children
            Node lowestLevelNode = null;
            foreach (var childNode in currentNode.child)
            {
                Node childLowestLevelNode = GetLowestLevelNode(childNode);
                if (childLowestLevelNode != null && (lowestLevelNode == null || childLowestLevelNode.child.Count < lowestLevelNode.child.Count))
                {
                    lowestLevelNode = childLowestLevelNode;
                }
            }

            return lowestLevelNode;
        }
    }
}


