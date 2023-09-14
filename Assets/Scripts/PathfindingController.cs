using System.Collections.Generic;
using UnityEngine;

public class PathfindingController : MonoBehaviour
{
    public GameObject cubePrefab; // 1x1 boyutundaki küp prefab'ı
    public LayerMask obstacleLayer; // Engellerin katmanı
    public GameObject targetObject; // Hedef objesi (örneğin, tıkladığınız noktadaki küp)

    private List<Node> openList = new List<Node>(); // Açık liste
    private List<Node> closedList = new List<Node>(); // Kapalı liste
    private List<Vector3> finalPath = new List<Vector3>(); // Son yol

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // Tıklanan noktanın dünya koordinatlarını al
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                // Tıklanan noktanın koordinatlarını al
                Vector3 targetPosition = hit.point;

                // Hedef pozisyonunu 1x1 boyutundaki küpün merkezine ayarla
                targetPosition += Vector3.up * 0.5f;

                // A* yol bulma algoritmasını kullanarak objeyi hedefe gönder
                FindPath(transform.position, targetPosition);
            }
        }
    }

    private void FindPath(Vector3 start, Vector3 end)
    {
        Node startNode = new Node(start);
        Node endNode = new Node(end);

        openList.Clear();
        closedList.Clear();
        finalPath.Clear();

        openList.Add(startNode);

        while (openList.Count > 0)
        {
            Node currentNode = openList[0];

            for (int i = 1; i < openList.Count; i++)
            {
                if (openList[i].FCost < currentNode.FCost || (openList[i].FCost == currentNode.FCost && openList[i].hCost < currentNode.hCost))
                {
                    currentNode = openList[i];
                }
            }

            openList.Remove(currentNode);
            closedList.Add(currentNode);

            if (currentNode == endNode)
            {
                // Hedefe ulaşıldı, son yolu oluştur
                finalPath = RetracePath(startNode, endNode);
                MoveObjectAlongPath(finalPath);
                return;
            }

            foreach (Node neighbor in GetNeighbors(currentNode))
            {
                if (closedList.Contains(neighbor) || neighbor.isObstacle)
                {
                    continue;
                }

                int newCostToNeighbor = currentNode.gCost + GetDistance(currentNode, neighbor);

                if (newCostToNeighbor < neighbor.gCost || !openList.Contains(neighbor))
                {
                    neighbor.gCost = newCostToNeighbor;
                    neighbor.hCost = GetDistance(neighbor, endNode);
                    neighbor.parent = currentNode;

                    if (!openList.Contains(neighbor))
                    {
                        openList.Add(neighbor);
                    }
                }
            }
        }
    }

    private List<Node> GetNeighbors(Node node)
    {
        List<Node> neighbors = new List<Node>();

        // Burada düğümlerin komşularını hesaplamak için uygun kod eklenmelidir.

        return neighbors;
    }

    private List<Vector3> RetracePath(Node startNode, Node endNode)
    {
        List<Vector3> path = new List<Vector3>();
        Node currentNode = endNode;

        while (currentNode != startNode)
        {
            path.Add(currentNode.position);
            currentNode = currentNode.parent;
        }

        path.Reverse();
        return path;
    }

    private int GetDistance(Node nodeA, Node nodeB)
    {
        // Burada iki düğüm arasındaki mesafeyi hesaplamak için uygun kod eklenmelidir.
        return 0;
    }

    private void MoveObjectAlongPath(List<Vector3> path)
    {
        // Burada objenin yolu izlemesini sağlayacak kod eklenmelidir.
    }
}

public class Node
{
    public Vector3 position;
    public int gCost;
    public int hCost;
    public Node parent;
    public bool isObstacle; // Yeni eklenen özellik

    public int FCost { get { return gCost + hCost; } }

    public Node(Vector3 _pos)
    {
        position = _pos;
        isObstacle = false; // Varsayılan olarak bir düğüm engel değil.
    }
}