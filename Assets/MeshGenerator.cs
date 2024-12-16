using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class MeshGenerator : MonoBehaviour
{
    Mesh mesh;

    Vector3[] vertices;
    int[] triangles;
    Color[] colors;

    public int xSize = 20, zSize = 20;
    public float NoiseIntensetive = 2f;

    public Gradient gradient;
    float minTerrainHeiht, maxTerrainHeiht;
    void Start()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;

        OnUpdateMesh();
    }
    public void OnUpdateMesh()
    {
        CreateShape();
        UpdateMesh();
    }

    void CreateShape()
    {
        vertices = new Vector3[(xSize + 1) * (zSize + 1)];

        for(int i=0, z = 0; z <= zSize; z++)
        {
            for(int x=0;x <= xSize; x++)
            {
                float y = Mathf.PerlinNoise(x * .1f, z * .1f) * NoiseIntensetive;
                vertices[i] = new Vector3(x, y, z);

                if(y>maxTerrainHeiht)
                    maxTerrainHeiht = y;
                if(y < minTerrainHeiht)
                    minTerrainHeiht = y;

                i++;
            }
        }
        triangles = new int[xSize * zSize * 6];

        int vert = 0;
        int tris = 0;

        for (int z = 0; z < zSize; z++)
        {
            for (int x = 0; x < xSize; x++)
            {
                triangles[tris + 0] = vert + 0;
                triangles[tris + 1] = vert + xSize + 1;
                triangles[tris + 2] = vert + 1;
                triangles[tris + 3] = vert + 1;
                triangles[tris + 4] = vert + xSize + 1;
                triangles[tris + 5] = vert + xSize + 2;

                vert++;
                tris += 6;
            }
            vert++;
        }

        colors = new Color[vertices.Length];
        for (int i = 0, z = 0; z <= zSize; z++)
        {
            for (int x = 0; x <= xSize; x++)
            {
                float heidht =Mathf.InverseLerp(minTerrainHeiht, maxTerrainHeiht, vertices[i].y);
                colors[i] = gradient.Evaluate(heidht);
                i++;
            }
        }
    }

    void UpdateMesh()
    {
        mesh.Clear();

        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.colors = colors;

        mesh.RecalculateNormals();
    }

    private void OnDrawGizmos()
    {
        if(vertices == null)
            return;

        for(int i=0;i<vertices.Length; i++)
        {
            Gizmos.DrawSphere(vertices[i], .1f);
        }
    }
}
