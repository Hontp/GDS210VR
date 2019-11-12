
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDrop
{
    public class AI_pathing : MonoBehaviour
    {
        //positions
        public List<Transform> Path_points;
        //distances between positions used to create sonstant movement
        public List<float> distances;
        //object were the entitys spawn
        public GameObject spawner;
        public float spawn_time;
        // gameobject being spawned
        public GameObject AI_PAthing_entity;
        //list of struct nititys allows for mass controls

        public List<pathed_entity> entitys = new List<pathed_entity>();
        public List<pathed_entity> entitysToDestroy = new List<pathed_entity>();
        //struct for entitys

        [System.Serializable]
        public struct pathed_entity
        {
            //gamobject
            public GameObject AI_gameObject;
            //gamobject indivisual time on path
            public float Time;
            //gameobject path between to point
            public int Path;
            //entity refrence to position list

        };

        //speed of movement
        public float speed;
        //speed of direction rotation
        public float rotaional_speed;
        //if you want to ride the created path click ride.
        public bool ride = false;
        int pasenger = 0;


        // Start is called before the first frame update
        void Start()
        {


            //creates the distances of the paths
            for (int i = 1; Path_points.Count > i; i++)
            {
                distances.Add(Vector3.Distance(Path_points[i].position, Path_points[i - 1].position));
            }
            //spawn tick
            InvokeRepeating("Spawn_enitiy", 0, spawn_time);

        }

        // Update is called once per frame
        void LateUpdate()
        {
            //runs for every entity in 'entitys' list
            for (int i = 0; entitys.Count > i; i++)
            {
                if (entitys[i].AI_gameObject != null)
                {
                    //refreshes entity index in event off deleted entitys
                    entitys[i].AI_gameObject.GetComponent<entity_collsion>().entity_index = i;

                    //creation of a struct template to reaply to it self because its naot an array 
                    pathed_entity x;
                    x = entitys[i];


                    //time is updated every frame
                    x.Time += Time.deltaTime;

                    if (entitys[i].Path != Path_points.Count - 1)
                    {
                        //movement
                        entitys[i].AI_gameObject.transform.position = Vector3.Lerp(Path_points[entitys[i].Path].position, Path_points[entitys[i].Path + 1].position, Mathf.Clamp(entitys[i].Time * speed / distances[entitys[i].Path], 0, 1));
                        //rotation
                        Vector3 direction = Path_points[entitys[i].Path + 1].position - entitys[i].AI_gameObject.transform.position;
                        Quaternion rotateto = Quaternion.LookRotation(direction, Path_points[entitys[i].Path + 1].transform.up);


                        entitys[i].AI_gameObject.transform.rotation = Quaternion.Lerp(transform.rotation, rotateto, rotaional_speed * entitys[i].Time);

                    }

                    //cleanup and deletion of entitys that finish the end of the final path
                    if (entitys[i].AI_gameObject.transform.position == Path_points[Path_points.Count - 1].position)
                    {
                        pathed_entity del;
                        del = entitys[i];
                        entitys.RemoveAt(i);
                        Destroy(del.AI_gameObject);
                        return;
                    }

                    //moves the entity on the next path
                    if (entitys[i].AI_gameObject.transform.position == Path_points[entitys[i].Path + 1].position)
                    {
                        x.Time = 0;
                        x.Path += 1;
                    }


                    //overides the x template back on to it self
                    entitys[i] = x;
                }

            }

            for (int x = 0; entitysToDestroy.Count > x; x++)
            {
                for (int y = 0; entitys.Count > y; y++)
                    if (entitysToDestroy[x].AI_gameObject == entitys[y].AI_gameObject)
                    {
                        entitys.Remove(entitys[y]);

                        // Destroy(entitysToDestroy[x].AI_gameObject);
                    }
            }
            entitysToDestroy.Clear();



        }

        public void RemoveFromEntityList(int index)
        {
            entitysToDestroy.Add(entitys[index]);


        }

        public void secondCheak(GameObject gameobject)
        {
            for (int i = 0; entitys.Count < i; i++)
            {
                if (entitys[i].AI_gameObject == gameObject)
                {
                    entitys.Remove(entitys[i]);
                }
            }
        }

        void Spawn_enitiy()
        {
            GameObject x;
            //creates gameobect
            x = Instantiate(AI_PAthing_entity, spawner.transform);

            x.AddComponent<entity_collsion>();
            entity_collsion eC = x.GetComponent<entity_collsion>();
            eC.entity_index = entitys.Count;
            eC.AI_P = GetComponent<AI_pathing>();

            //appys gameobject to a new struct and creates a new struct
            pathed_entity y = new pathed_entity()
            {
                AI_gameObject = x,
                Path = 0,
                Time = 0,

            };
            //adds new struct to list
            entitys.Add(y);

            //a bit of fun com on a ride
            pasenger += 1;
            if (pasenger == 4 && ride == true)
            {

                GameObject.Find("Main Camera").transform.parent = x.transform;
                GameObject.Find("Main Camera").transform.position = x.transform.position;
            }
        }


    }
}
