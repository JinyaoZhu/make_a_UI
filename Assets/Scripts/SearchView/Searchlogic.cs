using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Searchlogic : MonoBehaviour
{
    private GameObject gridnameshow;

    public Button searchbutton;

    int count = 0;

    /// <summary>
    /// List 里存的是场景里所有的被查找物体的名称和位置
    /// </summary>
    /// 
    List<Transform> allnameslist = new List<Transform>();


    string inputtext = "";
    GameObject searchbg;//生成的每一行的显示物体

    // Use this for initialization
    void Start()
    {
        string gridpath = "findnamegridcontent";//生成列表的路径
        gridnameshow = Resources.Load(gridpath, typeof(GameObject)) as GameObject;//加载生成的子物体

        //找到场景中所有的目标物体，然后添加到list里
        GameObject go = GameObject.Find("Tfather");
        if (go != null)
        {
            //找到场景中所有的目标物体，然后添加到list里
            allnameslist = new List<Transform>();

            foreach (Transform child in go.transform)
            {
                allnameslist.Add(child);

                Debug.Log(child.gameObject.name);
            }

        }

        
        //初始化查找按钮
         //searchbutton.onClick.AddListener(Findbutton);
       
        
        

    }

    void Update()
    {
        count = 0;
        //Grid的长度随着生成物体个数变化
        this.gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(this.gameObject.GetComponent<RectTransform>().sizeDelta.x, 0);
        inputtext = GameObject.Find("SearchView").transform.Find("MainArea/SearchBar/Text").GetComponent<Text>().text;

        // 清空grid里的所有东西
        List<Transform> lst = new List<Transform>();
        foreach (Transform child in transform)
        {
            lst.Add(child);
            Debug.Log(child.gameObject.name);
        }
        for (int i = 0; i < lst.Count; i++)
        {
            Destroy(lst[i].gameObject);
        }


        compared();
    }

    //// <summary>
    //// 查找方法触发
    //// </summary>
    //void Findbutton()
    //{
    //    //Grid的长度随着生成物体个数变化
    //    this.gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(this.gameObject.GetComponent<RectTransform>().sizeDelta.x, 0);
    //    inputtext = GameObject.Find("SearchView").transform.Find("MainArea/SearchBar/Text").GetComponent<Text>().text;

    //    // 清空grid里的所有东西
    //    List<Transform> lst = new List<Transform>();
    //    foreach (Transform child in transform)
    //    {
    //        lst.Add(child);
    //        Debug.Log(child.gameObject.name);
    //    }
    //    for (int i = 0; i < lst.Count; i++)
    //    {
    //        Destroy(lst[i].gameObject);
    //    }


    //    compared();
    //}

    /// <summary>
    /// 将查找文字与库里的数据对比，然后生成列表
    /// </summary>
    void compared()
    {

        for (int i = 0; i < allnameslist.Count; i++)
        {
            Debug.Log("list 里有：" + allnameslist[i].name);

            if (inputtext != "" && allnameslist[i].name.Contains(inputtext))
            {
                Debug.Log("包含" + "。。。。该字符串是：" + allnameslist[i]);


                 Generatenamegrids(allnameslist[i].name);//生成列表
            }

            else if (inputtext != "" && (allnameslist[i].name.Contains(inputtext) == false))
            {
                Debug.Log("不包含");

                count = count + 1;
               // Generatenamegrids("nicht_beinhaten");

            }
            else
            {

                Debug.Log("不包含");

            }

        }
        if (count == allnameslist.Count)
        {
            Generatenamegrids("Cannot find component, please enter again");
            count = 0;
        }
    }

    /// <summary>
    /// 生成整个gird子物体
    /// </summary>
    void Generatenamegrids(string thename)     
    {
        
        //生成record的物体、
        searchbg = Instantiate(gridnameshow, this.transform.position, Quaternion.identity) as GameObject;
        searchbg.transform.SetParent(this.transform);
        searchbg.transform.localScale = new Vector3(1, 1, 1);
        searchbg.transform.Find("positiontext").GetComponent<Text>().text = thename;
     

        //本grid长度加60
        this.gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(this.gameObject.GetComponent<RectTransform>().sizeDelta.x, this.gameObject.GetComponent<RectTransform>().sizeDelta.y + this.GetComponent<GridLayoutGroup>().cellSize.y + this.GetComponent<GridLayoutGroup>().spacing.y);
    }



}
