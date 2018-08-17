using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sort : MonoBehaviour {
    
    public float spacing = .01f;
    private int left;
    private int right;
    MeshRenderer[] childs;
    // Use this for initialization
    void Awake () {
        childs = this.GetComponentsInChildren<MeshRenderer>();
        float width = childs[0].GetComponent<BoxCollider>().size.x;
        if (childs.Length<=0)
        {
            return;
        }
        
        float length =((width) * childs.Length )+ spacing * (childs.Length);
        for (int i = 0; i < childs.Length; i++)
        {
            
            //-Vector3.right * (length / 2) + (length / (childs.Length) * (i)) * Vector3.right * .5f; //+ Vector3.right*0.5f*(width + spacing)/2;
            //childs[i].transform.localScale *= 0.5f;
        }
        GroupPlane();
    }

    void GroupPlane()
    {
        MeshRenderer[] childs = this.GetComponentsInChildren<MeshRenderer>();
        float width = childs[0].GetComponent<BoxCollider>().size.x;
        if (childs.Length % 2 == 0)
        {
            // 偶数
            for (int i = 0; i < childs.Length; i++)
            {
                //index
                if (i % 2 == 0)
                {
                    childs[i].transform.position = this.transform.position + (width/2 + spacing ) * Vector3.right *(i/2)+ (width/2 + spacing) * Vector3.right;
                    right++;
                    //childs[i].transform.position = Vector3.right * (0.15f + spacing) * right - Vector3.right * ((0.15f + spacing) / 2);
                    //childs[i].transform.localScale *= 0.5f;

                }
                else
                {
                    childs[i].transform.position = this.transform.position - (width + spacing ) * Vector3.right+2 * (-left * width) * Vector3.right;
                    left++;
                    //childs[i].transform.position = -Vector3.right * (0.15f + spacing) * left + Vector3.right * ((0.15f + spacing) / 2);
                    //childs[i].transform.localScale *= 0.5f;

                }
            }
        }
        else
        {
            //奇数
            for (int i = 0; i < childs.Length; i++)
            {
                if (i == 0)
                {
                    continue;
                }
                if (i % 2 == 0)
                {
                    right++;
                    childs[i].transform.position = Vector3.right * (0.15f) * right + Vector3.right * (spacing / 2) * right;
                    childs[i].transform.localScale *= 0.5f;

                }
                else
                {
                    left++;
                    childs[i].transform.position = -Vector3.right * (0.15f) * left - Vector3.right * (spacing / 2) * left;
                    childs[i].transform.localScale *= 0.5f;

                }
            }
        }
    }
 
}
