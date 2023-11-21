namespace DeweyDecimal.Models
{
    public class Tree
    {
        public Node Root { get; private set; }

        public Tree(Node root)
        {
            Root = root;
        }
    }
}
