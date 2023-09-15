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
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit; // RaycastHit2D kullanılacak

            if (Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity))
            {
                hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity);
    
                Vector3 targetPosition = hit.point;

                // Hedef pozisyonunu 2D düzlemde ayarla (z bileşenini sıfır yap)
                targetPosition.z = 0f;

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
        // İki düğüm arasındaki Manhattan mesafesini hesaplayın (dört yönlü hareket için).
        int deltaX = Mathf.Abs((int)nodeA.position.x - (int)nodeB.position.x);
        int deltaY = Mathf.Abs((int)nodeA.position.y - (int)nodeB.position.y);

        return deltaX + deltaY;
    }

    private List<Node> GetNeighbors(Node node)
    {
        List<Node> neighbors = new List<Node>();

        // Dört yönlü komşu düğümleri ekleyin (sağ, sol, yukarı, aşağı).
        // Dikkat edilmesi gereken, engelleri ve oyun alanının sınırlarını kontrol etmektir.
        // Aşağıdaki kod, düğümlerin koordinatlarını düzgün bir şekilde hesaplar.

        Vector3[] neighborOffsets = new Vector3[]
        {
            new Vector3(1, 0, 0),  // Sağ
            new Vector3(-1, 0, 0), // Sol
            new Vector3(0, 1, 0),  // Yukarı
            new Vector3(0, -1, 0)  // Aşağı
        };

        foreach (Vector3 offset in neighborOffsets)
        {
            Vector3 neighborPos = node.position + offset;

            // Eğer komşu pozisyon oyun alanının sınırları içindeyse ve engel değilse, komşu listesine ekleyin.
            if (!IsPositionOutOfBounds(neighborPos) && !IsPositionObstacle(neighborPos))
            {
                neighbors.Add(new Node(neighborPos));
            }
        }

        return neighbors;
    }

    private bool IsPositionOutOfBounds(Vector3 position)
    {
        // Oyun alanının sınırlarını burada kontrol edin.
        // Örneğin, eğer oyun alanınız bir dikdörtgen sınırla sınırlanmışsa, burada kontrol edebilirsiniz.
        // Örneğin, sınırlar dışında ise true, içinde ise false dönmelisiniz.
        // Sınırların nasıl tanımlandığınıza bağlı olarak bu kodu uyarlamalısınız.
        return false; // Örnek olarak her zaman false döndürüldü.
    }

    private bool IsPositionObstacle(Vector3 position)
    {
        // Verilen pozisyonun engel olup olmadığını burada kontrol edin.
        // Verilen pozisyonda bir engel varsa true, yoksa false dönmelisiniz.
        // Engellerin tanımlandığı yere bağlı olarak bu kodu uyarlamalısınız.
    
        Collider2D hitCollider = Physics2D.OverlapCircle(position, 0.2f, obstacleLayer);

        return hitCollider != null;
    }
    private void MoveObjectAlongPath(List<Vector3> path)
    {
        if (path.Count == 0)
        {
            // Hiç yol yoksa veya hedefe ulaşıldıysa, hareketi durdurabilirsiniz.
            return;
        }

        // İlk hedef pozisyonunu alın
        Vector3 targetPosition = path[0];

        // Objeyi hedefe doğru hareket ettirin (örneğin, Lerp kullanarak)
        float moveSpeed = 5f; // Objenin hareket hızı
        float step = moveSpeed * Time.deltaTime;

        transform.position = Vector3.MoveTowards(transform.position, targetPosition, step);

        // Eğer obje hedef pozisyona ulaştıysa, hedefi listeden çıkarın
        if (transform.position == targetPosition)
        {
            path.RemoveAt(0);
        }
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