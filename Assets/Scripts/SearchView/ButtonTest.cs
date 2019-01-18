using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonTest : MonoBehaviour
{
    List<GameObject> messages = new List<GameObject>();
    public GameObject item;
    GameObject myMessage;
    GameObject parent;
    Vector3 itemLocalPos;
    Vector2 contentSize;
    float itemHeight;

    int count = 0;
    List<Transform> allnameslist = new List<Transform>();
    string inputtext = "";


    void Start()
    {
        parent = GameObject.Find("Content");
        contentSize = parent.GetComponent<RectTransform>().sizeDelta;
        item = Resources.Load("Button") as GameObject;
        itemHeight = item.GetComponent<RectTransform>().rect.height;
        itemLocalPos = item.transform.localPosition;

        //找到场景中所有的目标物体，然后添加到list里
        GameObject go = GameObject.Find("library");

        if (go != null)
        {
            //找到场景中所有的目标物体，然后添加到list里
            //messages = new List<GameObject>();

            foreach (Transform child in go.transform)
            {
                allnameslist.Add(child);
               // AddItem();
                
            }
        }

    }
    void Update()
    {

        count = 0;
        //Grid的长度随着生成物体个数变化
        // this.gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(this.gameObject.GetComponent<RectTransform>().sizeDelta.x, 0);
        inputtext = GameObject.Find("SearchView").transform.Find("MainArea/SearchBar/Text").GetComponent<Text>().text;

        ///-------------------------------------------添加列表清空代码，实现实时更新及结束。
        compared();
    }

    void compared()
    {

        //if (inputtext == "")
        //{
        //    for (int j = 0; j < allnameslist.Count; j++)
        //    {
        //        AddItem(allnameslist[j].name);
        //    }
        //}
        for (int i = 0; i < allnameslist.Count; i++)
        {


            //Debug.Log("list ：" + allnameslist[i].name);

            //强制大写转换
            inputtext = inputtext.ToString().ToUpper();

            if (inputtext != "" && allnameslist[i].name.Contains(inputtext))
            {
                Debug.Log("include" + "String：" + allnameslist[i]);


                AddItem(allnameslist[i].name);//生成列表
            }
            else if (inputtext != "" && (allnameslist[i].name.Contains(inputtext) == false))
            {
                count = count + 1;

                Debug.Log("not include");
            }

        }
        if (count == allnameslist.Count)
        {
           AddItem("Cannot find component!");
        }

    }
    //添加列表项
    public void AddItem(string Thema)
    {

        GameObject a = Instantiate(item) as GameObject;
        
        a.transform.Find("Text").GetComponent<Text>().text = Thema;
        a.GetComponent<Button>().onClick.AddListener(
        delegate () {

                Debug.Log("按下按钮");
                
            }
        );

        /// Debug.Log("按不到按钮");

        a.GetComponent<Transform>().SetParent(parent.GetComponent<Transform>(), false);
        a.transform.localPosition = new Vector3(itemLocalPos.x, itemLocalPos.y - allnameslist.Count * itemHeight, 0);
        //messages.Add(a);

        if (contentSize.y <= allnameslist.Count * itemHeight)//增加内容的高度
        {
            parent.GetComponent<RectTransform>().sizeDelta = new Vector2(contentSize.x, allnameslist.Count * itemHeight);
        }
    }

    // -----------------在此处写成函数
    //public void RemoveItem(GameObject t)
    //{

    //    Debug.Log("按钮被按住");

    //    int index = messages.IndexOf(t);
    //    messages.Remove(t);
    //    // Destroy(t);

    //    //for (int i = index; i < messages.Count; i++)//移除的列表项后的每一项都向前移动
    //    //{
    //    //    messages[i].transform.localPosition += new Vector3(0, itemHeight, 0);
    //    //}

    //    //if (contentSize.y <= messages.Count * itemHeight)//调整内容的高度
    //    //    parent.GetComponent<RectTransform>().sizeDelta = new Vector2(contentSize.x, messages.Count * itemHeight);
    //    //else
    //    //    parent.GetComponent<RectTransform>().sizeDelta = contentSize;
    //}

}



