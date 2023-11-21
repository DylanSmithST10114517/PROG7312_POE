using Microsoft.AspNetCore.Mvc;
using DeweyDecimal.Models;

namespace DeweyDecimal.Controllers
{
    public class FindingCallNumbersController : Controller
    {
        // GET request handler
        public IActionResult Index()
        {
            FindingCallNumber findingCallNumber = new FindingCallNumber(4);
            return View(findingCallNumber);
        }

        public FindingCallNumber FindingCallNumber { get; set; }    

        // GET request handler
        public IActionResult OnGet()
        {
            FindingCallNumber = new FindingCallNumber(4);
            return View(FindingCallNumber);
        }

        [HttpPost]
        public ActionResult<List<Node>> Round1([FromBody] Node postedNode)
        {
            try
            {
                // Check if the posted node is in the path of the QuestionNode
                bool isMatch = CheckIfNodeIsInQuestionNode(FindingCallNumber.QuestionNode, postedNode);

                if (isMatch)
                {
                    // Get the list of nodes at the same level as one less child below the posted node
                    List<Node> responseNodes = GetNodesAtSameLevelAsOneLessChild(postedNode);

                    // Return the list of nodes
                    return Ok(responseNodes);
                }
                else
                {
                    // Handle the case where the posted node is not in the path of the QuestionNode
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions
                return BadRequest(ex.Message);
            }
        }

        private bool CheckIfNodeIsInQuestionNode(Node questionNode, Node postedNode)
        {
            // Check if the postedNode is in the path of the questionNode
            return CheckIfNodeIsInPath(questionNode, postedNode);
        }

        private bool CheckIfNodeIsInPath(Node currentNode, Node targetNode)
        {
            // Base case: if the current node is null or does not match the target node, return false
            if (currentNode == null || currentNode.data != targetNode.data)
            {
                return false;
            }

            // Check if the current node's children contain the target node
            if (currentNode.child != null && currentNode.child.Any(child => child.data == targetNode.data))
            {
                return true;
            }

            // Recursive case: check if the target node is in the path of any child node
            return currentNode.child.Any(child => CheckIfNodeIsInPath(child, targetNode));
        }

        private List<Node> GetNodesAtSameLevelAsOneLessChild(Node postedNode)
        {
            // Find the parent node of the posted node
            Node parentNode = FindParentNode(FindingCallNumber.QuestionNode, postedNode);

            if (parentNode != null)
            {
                // Find the siblings of the posted node's parent node (excluding the parent)
                List<Node> siblings = parentNode.child
                    .Where(child => child.data != postedNode.data)
                    .ToList();

                // Create a new list of nodes at the same level as one less child
                List<Node> responseNodes = new List<Node>();
                foreach (var sibling in siblings)
                {
                    // Add the sibling
                    responseNodes.Add(sibling);

                    // Add one less child below the sibling (if it has children)
                    if (sibling.child != null && sibling.child.Count > 0)
                    {
                        responseNodes.AddRange(sibling.child.Take(1));
                    }
                }

                return responseNodes;
            }

            // Return an empty list if the parent node is not found
            return new List<Node>();
        }

        private Node FindParentNode(Node currentNode, Node targetNode)
        {
            // Base case: if the current node is null or does not have the target node as a child, return null
            if (currentNode == null || currentNode.child == null || !currentNode.child.Any(child => child.data == targetNode.data))
            {
                return null;
            }

            // Check if the target node is a child of the current node
            if (currentNode.child.Any(child => child.data == targetNode.data))
            {
                return currentNode;
            }

            // Recursive case: check if the target node is a child of any child node
            foreach (var childNode in currentNode.child)
            {
                Node parent = FindParentNode(childNode, targetNode);
                if (parent != null)
                {
                    return parent;
                }
            }

            return null;
        }


        //[HttpPost]
        //public ActionResult<Boolean> Round2([FromBody] List<Book> books)
        //{
        //    try
        //    {
              
        //    }
        //}
    }
}
