using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blade : MonoBehaviour
{

    public Transform topBlade,botBlade;
    public Transform swordTrailObj;

    public Queue<Vector3> topBladePoints = new Queue<Vector3>();
    public Queue<Vector3> botBladePoints = new Queue<Vector3>();

    public Vector3 startTop,startBot;
    public Vector3 prevTop,prevBot;
    public Vector3 endTop,endBot;

    public Mesh mesh;
    public MeshFilter meshFilter;
    public MeshRenderer meshRenderer;
    public const int maxSize = 8;

    public float averageVel;

    public Vector3 offset;






    // Start is called before the first frame update
    void Start()
    {
        mesh = new Mesh();
        mesh.vertices = new Vector3[2*maxSize];
        mesh.triangles = new int[3*maxSize];
        //mesh.normals = new Vector3[2*maxSize];
        mesh.uv = new Vector2[2*maxSize];

        
    }

    // Update is called once per frame
    void Update()
    {
        prevBot = endBot;
        prevTop = endTop;
        
        topBladePoints.Enqueue(topBlade.position);
        botBladePoints.Enqueue(botBlade.position);
        
        
        for(int i=0;i<topBladePoints.Count;i++)
        {

        }
        
        if(topBladePoints.Count > maxSize)
        {
            startTop = topBladePoints.Dequeue();
        }
        else
        {
            startTop = topBladePoints.Peek();
        }

        if(botBladePoints.Count > maxSize)
        {
            startBot = botBladePoints.Dequeue();
        }
        else
        {
            startBot = botBladePoints.Peek();
        }

        

        endBot = botBladePoints.ToArray()[botBladePoints.Count-1];
        endTop = topBladePoints.ToArray()[topBladePoints.Count-1];
        
        float botDistance = Vector3.Distance(startBot,endBot);
        float topDistance = Vector3.Distance(startTop,endTop);

        // http://hyperphysics.phy-astr.gsu.edu/hbase/vel2.html
        averageVel =  (botDistance + topDistance) / (Time.deltaTime * maxSize);  

        meshRenderer.material.SetFloat("_alpha",getVel());

        if(botBladePoints.Count >= maxSize)
        {
            generateMesh();
        }
        
        
        mesh.RecalculateBounds();      
        meshFilter.mesh = mesh;
        meshFilter.mesh.RecalculateBounds();

        swordTrailObj.rotation = Quaternion.identity;
        swordTrailObj.position = Vector3.zero;
    }

    public float getVel()
    {
        return Mathf.Clamp(averageVel*0.25f,0.2f,0.6f);
    }
    void generateMesh()
    {
        var verts = new Vector3[2*maxSize];
        for(int i=0;i<maxSize;i++)
        {
            verts[2*i] = botBladePoints.ToArray()[i];
            verts[2*i+1] = topBladePoints.ToArray()[i];    
        }

        mesh.vertices = verts;

        var tris = new int[3*maxSize];
        
        for(int i=0;i<maxSize/2;i++)
        {
 
                tris[i*6] = 0 + i*2;
                tris[i*6 + 1] = 1 + i*2;
                tris[i*6 + 2] = 2 + i*2;
                tris[i*6 + 3] = 2 + i*2;
                tris[i*6 + 4] = 1 + i*2;
                tris[i*6 + 5] = 3 + i*2;
  
        }



        mesh.triangles = tris;

        
        var normals = new Vector3[2*maxSize];
        for(int i=0;i<2*maxSize;i++)
        {
            normals[i] = Vector3.Cross(transform.forward,(-startTop + endTop).normalized);
        }
        mesh.normals = normals;


        var uv = new Vector2[2*maxSize];
        for(int i=0;i<maxSize;i++)
        {

                uv[2*i] = new Vector2(i/maxSize,0);
                uv[2*i+1] = new Vector2((i+1)/maxSize,1);
        }
        mesh.uv = uv;

    }
}
