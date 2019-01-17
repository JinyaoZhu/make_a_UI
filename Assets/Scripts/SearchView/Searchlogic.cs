using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class Searchlogic : MonoBehaviour
{
    //private GameObject gridnameshow;
    private GameObject gridnameshow;
    public Button gridcontentbtn;

    int count = 0;

    /// <summary>
    /// List 里存的是场景里所有的被查找物体的名称和位置
    /// </summary>
    /// 
    List<Transform> allnameslist = new List<Transform>();

    string inputtext = "";
    //GameObject searchbg;//生成的每一行的显示物体
    GameObject searchbg;
    GameObject sea;
    // Use this for initialization
    void Start()
    {

        string gridpath = "gridcontentbtn";//生成列表的路径
        gridnameshow = Resources.Load(gridpath, typeof(GameObject)) as GameObject;//加载生成的子物体

    //找到场景中所有的目标物体，然后添加到list里
        GameObject go = GameObject.Find("library");

        if (go != null)
        {
            //找到场景中所有的目标物体，然后添加到list里
            allnameslist = new List<Transform>();

            foreach (Transform child in go.transform)
            {
                allnameslist.Add(child);

                //Debug.Log(child.gameObject.name);

                //显示list中的数据

            }
        }
    }

    void Btn_Test()
    {
        Debug.Log("这是一个按钮点击事件！哈哈");
    }

    // <summary>
    // 查找方法触发
    // </summary>
    void Update()
    {
        GameObject.Find("SearchView").transform.Find("MainArea/ShowField/Scrollbar").GetComponent<CanvasGroup>().alpha = 0.0f;
        count = 0;

        //Grid的长度随着生成物体个数变化
        this.gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(this.gameObject.GetComponent<RectTransform>().sizeDelta.x, 0);
        inputtext = GameObject.Find("SearchView").transform.Find("MainArea/SearchBar/Text").GetComponent<Text>().text;

        // 清空grid里的所有东西
        List<Transform> lst = new List<Transform>();
        foreach (Transform child in transform)
        {
            lst.Add(child);
            //Debug.Log(child.gameObject.name);
        }
        for (int i = 0; i < lst.Count; i++)
        {
            Destroy(lst[i].gameObject);
        }


        compared();
    }

    /// <summary>
    /// 将查找文字与库里的数据对比，然后生成列表
    /// </summary>
    void compared()
    {
        if (inputtext == "")
        {
            for (int j = 0; j < allnameslist.Count; j++)
            {
                Generatenamegrids(allnameslist[j].name);
            }
        }
        for (int i = 0; i < allnameslist.Count; i++)
        {
            

            //Debug.Log("list ：" + allnameslist[i].name);

            //强制大写转换
            inputtext = inputtext.ToString().ToUpper();

            if (inputtext != "" && allnameslist[i].name.Contains(inputtext))
            {
                Debug.Log("include" + "String：" + allnameslist[i]);


                 Generatenamegrids(allnameslist[i].name);//生成列表
            }
            else if(inputtext != "" && (allnameslist[i].name.Contains(inputtext) == false))
            {
                count = count + 1;

                Debug.Log("not include");
            }
    
        }
        if (count == allnameslist.Count)
        {
            Generatenamegrids("Cannot find component!");
        }

    }

 
    void Generatenamegrids(string thename)     
    {
        GameObject.Find("SearchView").transform.Find("MainArea/ShowField/Scrollbar").GetComponent<CanvasGroup>().alpha = 0.5f;

        //生成record的物体、
        // searchbg = Instantiate(gridnameshow, this.transform.position, Quaternion.identity) as GameObject;
        // searchbg.transform.SetParent(this.transform,false);
        // searchbg.transform.SetAsLastSibling();
        // searchbg.transform.localScale = new Vector3(1, 1, 1);
        
        searchbg = Instantiate(gridnameshow, this.transform.position, Quaternion.identity) as GameObject;
        searchbg.transform.Find("Button/positiontext").GetComponent<Text>().text = thename;
        if (searchbg != null)
        {
            searchbg.transform.SetParent(this.transform);
            searchbg.transform.localScale = new Vector3(1, 1, 1);
         }
         //Debug.Log(123);


        gridcontentbtn = searchbg.transform.Find("Button").GetComponent<Button>();
        gridcontentbtn.onClick.AddListener(Btn_Test);


        //本grid长度加60
        this.gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(this.gameObject.GetComponent<RectTransform>().sizeDelta.x, this.gameObject.GetComponent<RectTransform>().sizeDelta.y + this.GetComponent<GridLayoutGroup>().cellSize.y + this.GetComponent<GridLayoutGroup>().spacing.y);
    }



}
