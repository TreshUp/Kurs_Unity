using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ins : MonoBehaviour
{
    private GameObject cube;
    IList<GameObject> elements = new List<GameObject>();
    List<string> file_string = new List<string>();
    private int number_old = 0;
    private int number_new = 0;
    private int i = 0;
    //private int n = 0;
    //int k = 1;
    public void Start ()
    {
        
    }
    private IEnumerator Inst()
    {
        yield return new WaitForSeconds(1);
        //GameObject ob = Instantiate(block, gameObject.transform.position, Quaternion.identity) as GameObject;
        //Destroy(ob, 3);
        //if (k==1)
        //{
        //    for (int x = 0; x < number_new; x++)
        //    {
        //        for (int y = 0; y < number_new; y++)
        //        {
        //            for (int z = 0; z < number_new; z++)
        //            {
        //                cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        //                //cube.AddComponent<Rigidbody>();
        //                //Rigidbody rb = cube.GetComponent<Rigidbody>();
        //                //rb.useGravity = true;
        //                cube.transform.position = new Vector3(x, y, z);
        //                elements.Add(cube);
        //            }
        //        }
        //    }
        //}
        if (number_new>number_old)
        {
            for (int x = number_old; x < number_new; x++)
            {
                for (int y = 0; y < number_new; y++)
                {
                    for (int z = 0; z < number_new; z++)
                    {
                        cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                        cube.transform.position = new Vector3(x, y, z);
                        elements.Add(cube); ;
                    }
                }
            }
            for (int y = number_old; y < number_new; y++)
            {
                for (int x = 0; x < number_old; x++)
                {
                    for (int z = 0; z < number_new; z++)
                    {
                        cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                        cube.transform.position = new Vector3(x, y, z);
                        elements.Add(cube);
                    }
                }
            }
            for (int z=number_old;z<number_new;z++)
            {
                for (int x = 0; x < number_old; x++)
                {
                    for (int y = 0; y < number_old; y++)
                    {
                        cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                        cube.transform.position = new Vector3(x, y, z);
                        elements.Add(cube);
                    }
                }
            }
        }
        //DELETE
        if (number_new<number_old)
        {
            //n = -number_new * number_new * number_new + number_old * number_old * number_old;
            for (i = number_old * number_old * number_old - 1; i >=number_new* number_new * number_new; i--)
            {
                //GameObject del = elements[i];
                Destroy(elements[i]);
                elements.RemoveAt(i);
            }
        }
        //k = k + 1;
        number_old = number_new;
    }
    public void Read_File()
    {
        string line;
        string value;
        int pos = 0;
        int[] coord_check = new int[3];
        System.IO.StreamReader file =new System.IO.StreamReader(@"Assets\Standard Assets\Coords.txt");
        while (file.Peek() >= 0)
        {
            line = file.ReadLine();
            file_string.Add(line);
        }
        for (int p = 0; p < file_string.Count; p++) //бежим по всем строкам из файла
        {
            for (int j = 0; j < 2; j++)
            {
                pos = file_string[p].IndexOf(" ");
                value = file_string[p].Substring(0, pos);
                coord_check[j] = int.Parse(value);
                file_string[p] = file_string[p].Substring(pos + 1);
            }
            coord_check[2] = int.Parse(file_string[p]);
            for (int k = 0; k < elements.Count; k++) //бежим по всем кубикам
            {
                if ((elements[k].transform.position.x == coord_check[0]) && (elements[k].transform.position.y == coord_check[1]) && (elements[k].transform.position.z == coord_check[2]))
                {
                    Material myMaterial = (Material)Resources.Load("Navysmooth.mat"); //не работает текстура
                    //Material gg = (Material)Resources.Load<Material>()
                    if (myMaterial == null) Debug.Log("Loser");
                    else Debug.Log("Success!");
                    elements[k].GetComponent<MeshRenderer>().material = myMaterial;
                }
            }
        }
}
    public void Num_Changed(string Text)
    {
        
        number_new = int.Parse(Text);
        StartCoroutine(Inst());
    }
    private void Repeat()
    {
    }
}
