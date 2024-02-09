using UnityEngine;

[RequireComponent(typeof(PolygonCollider2D))]
[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(MeshFilter))]
public class FOV : MonoBehaviour
{
    public float height = 2f; // Alçada de la forma cònica
    public float radius = 1f; // Radi de la base de la forma cònica
    public int segments = 16; // Nombre de segments que formen la base de la forma cònica

    void Start()
    {
        GenerateConicalShape();
    }

    void GenerateConicalShape()
    {
        MeshFilter meshFilter = GetComponent<MeshFilter>();
        PolygonCollider2D polygonCollider = GetComponent<PolygonCollider2D>();
        MeshRenderer meshRenderer = GetComponent<MeshRenderer>();

        if (meshFilter == null || polygonCollider == null || meshRenderer == null)
        {
            Debug.LogError("Un dels components requerits (MeshFilter, PolygonCollider2D, MeshRenderer) no està adjuntat a l'objecte.");
            return;
        }

        Mesh mesh = new Mesh();
        meshFilter.mesh = mesh;

        Vector2[] vertices2D = new Vector2[segments + 1 + 1]; // +1 per al centre de la base, +1 per al vèrtex superior
        Vector3[] vertices3D = new Vector3[segments + 1 + 1]; // +1 per al centre de la base, +1 per al vèrtex superior
        int[] triangles = new int[segments * 3]; // Cada segment té tres vèrtexs (un triangle)

        // Creació dels vèrtexs de la base
        for (int i = 0; i <= segments; i++)
        {
            float angle = Mathf.PI * 2f / segments * i;
            vertices3D[i] = new Vector3(Mathf.Cos(angle) * radius, Mathf.Sin(angle) * radius, 0f);
            vertices2D[i] = new Vector2(vertices3D[i].x, vertices3D[i].y); // Convertim a 2D
        }

        // Vèrtex superior
        vertices3D[segments + 1] = new Vector3(0f, height, 0f);
        vertices2D[segments + 1] = new Vector2(0f, height); // Convertim a 2D

        // Creació dels triangles
        for (int i = 0; i < segments; i++)
        {
            triangles[i * 3] = i;
            triangles[i * 3 + 1] = (i + 1) % segments;
            triangles[i * 3 + 2] = segments + 1; // Vèrtex superior
        }

        // Assignació dels vèrtexs i triangles a la mesh
        mesh.vertices = vertices3D;
        mesh.triangles = triangles;

        // Assignació de la mesh al PolygonCollider2D
        polygonCollider.pathCount = 1;
        polygonCollider.SetPath(0, vertices2D);

        // Recalcul de normals i tangents per a una correcta il·luminació
        mesh.RecalculateNormals();
        mesh.RecalculateTangents();

        // Actualització del mesh renderer
        meshRenderer.sharedMaterial = new Material(Shader.Find("Sprites/Default"));
    }
}