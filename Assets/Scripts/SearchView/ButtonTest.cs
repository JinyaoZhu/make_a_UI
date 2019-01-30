using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonTest : MonoBehaviour
{
    List<GameObject> messages = new List<GameObject>();

    public GameObject buttonTemplate;

    public GameObject obj;
    private GameObject item;
    
    GameObject myMessage;
    GameObject parent;
    Vector3 itemLocalPos;
    Vector2 contentSize;
    float itemHeight;

    int count = 0;
    bool showall = true;
    bool showone = true;
    bool comparedIs = false;

    List<Transform> allnameslist = new List<Transform>();
    string inputtext_new = "";
    string inputtext_alt = "";

    void Start()
    {
        parent = GameObject.Find("Content");
        contentSize = parent.GetComponent<RectTransform>().sizeDelta;
        item = Instantiate(buttonTemplate);
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
        if (inputtext_alt == "")
        {
            for (int j = 0; j < allnameslist.Count; j++)
            {
                AddItem(allnameslist[j].name);
            }
        }

    }
    void Update()
    {

        count = 0;
    
        inputtext_new = GameObject.Find("SearchView").transform.Find("MainArea/SearchBar/Text").GetComponent<Text>().text;
  
        if (inputtext_alt != inputtext_new)
        {
            inputtext_alt = inputtext_new;
            compared();
        }
        else
        {
            inputtext_alt = inputtext_new;
        }
          
    }

    void compared()
    {
       
        if (obj == null)
        {
            Debug.Log("find no element in library");
        }
        int childs = obj.transform.childCount;

       // Debug.Log(childs);

        for (int i = childs-1;i>=0;i--)
        {
            GameObject.DestroyImmediate(obj.transform.GetChild(i).gameObject);
        }

        if (inputtext_new == "")
        {
            GameObject.Find("SearchView").transform.Find("MainArea/Panel").GetComponent<CanvasGroup>().alpha = 0f;
            for (int j = 0; j < allnameslist.Count; j++)
            {
           
                AddItem(allnameslist[j].name);
            }
        }


        for (int i = 0; i < allnameslist.Count; i++)
            {
                //Debug.Log("单元个数" + allnameslist.Count);

                //强制大写转换
                inputtext_new = inputtext_new.ToString().ToUpper();

                if (inputtext_new != "" && allnameslist[i].name.Contains(inputtext_new) )
                {
                    //Debug.Log("include" + "String：" + allnameslist[i]);

                  
                  
                    AddItem(allnameslist[i].name);//生成列表
                    if (i == allnameslist.Count)
                    {
                        showone = false;
                    }

                }
                else if (inputtext_new != "" && (allnameslist[i].name.Contains(inputtext_new) == false) )
                {
                    count = count + 1;
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

        GameObject a = Instantiate(item);

        a.transform.Find("Text").GetComponent<Text>().text = Thema;
        a.SetActive(true);

        //a.GetComponent<Button>().GetComponent<Image>().color = Color.black;
        //a.GetComponent<Button>().GetComponentInChildren<Text>().color = Color.white;
        a.GetComponent<Button>().onClick.AddListener(()=>{
           Debug.Log("Choose Item");
           GameObject.Find("SearchView").transform.Find("MainArea/Panel").GetComponent<CanvasGroup>().alpha = 1f;
            //Image InfoPanel = GameObject.Find("SearchView").transform.Find("MainArea/Panel").GetComponent<Image>();
            //InfoPanel.SendMessage("changePictrues", inputtext_alt);
            //Debug.Log("input text:"+ Thema);
           Panel InfoPanel = GameObject.Find("SearchView/MainArea/Panel").GetComponent<Panel>();
           InfoPanel.changePictrues(Thema);
       });

        a.GetComponent<Transform>().SetParent(obj.transform, false);
        a.transform.localPosition = new Vector3(itemLocalPos.x, itemLocalPos.y - messages.Count * itemHeight, 0);
        messages.Add(a);
        

        if (contentSize.y <= allnameslist.Count * itemHeight)//增加内容的高度
        {
            parent.GetComponent<RectTransform>().sizeDelta = new Vector2(contentSize.x, messages.Count * itemHeight);
        }
    }

    // -----------------在此处写成函数
    public void RemoveItem(GameObject t)
    {

        if(comparedIs)
        {
            foreach (Transform child in obj.transform)
            {
                GameObject.Destroy(child.gameObject);
            }
        }
    }

}



