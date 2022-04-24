using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Codingriver;
using System;


/******************************************************************************************************
    对比字符串查找业务对象和id查找业务对象耗时及HashCode获取业务对象
    方式1：
    点击gameObject 根据GameObject的name字符串查找对应的业务对象
    方式2:
    初始化gameObject时添加一个组件，组件内有id字段，点击gameObject时根据gameObject上的组件找到id，然后根据id找到对应业务对象
    方式3:
    点击gameObject，根据gameObject的HashCode找到对应业务对象

    方式1的耗时：重命名gameObject+通过字符串查找业务对象; 耗时 503+316 毫秒
    方式2的耗时：给gameObject添加组件+通过id查找业务对象； 耗时 3883+191 毫秒
    方式3的耗时：gameObject获取HashCode+通过HashCode查找业务对象；耗时 34+12 毫秒

    结论：
    gameObject的HashCode查找效率最高，其次是字符串查找，添加组件绑定id的方式最慢
******************************************************************************************************/

public class StringAndIntProfiler : MonoBehaviour
{
    // Start is called before the first frame update
    public int GroupCount = 5;
    private int TestCount = 0;
    
    log4net.ILog log = log4net.LogManager.GetLogger(typeof(StringAndIntProfiler));
    private Dictionary<long, string> logicDict1 = new Dictionary<long, string>(); //业务对象
    private Dictionary<long, string> logicDict = new Dictionary<long, string>(); //业务对象
    private List<GameObject> objList = new List<GameObject>();
    private List<IdInfo> idList = new List<IdInfo>();
    //private List<string> nameList = new List<string>();
    void Awake()
    {
        log.Info("Awake");
        TestCount = 100000 * GroupCount;
        GameObject go;
        for (int i = 0; i < TestCount; i++)
        {
            objList.Add(go=new GameObject());
            go.transform.SetParent(transform);
        }

    }

    void Start()
    {
        StartCoroutine(ProfilerCoroutine());
    }

    IEnumerator ProfilerCoroutine()
    {
        yield return new WaitForSeconds(2);
        AddComponents();
        yield return new WaitForSeconds(1);
        Init();
        yield return new WaitForSeconds(1);
        ProfilerTime();
        yield break;
    }


    public void AddComponents()
    {
        int num = objList.Count;
        long start = 0, current = 0, end = 0, time = 0;
        int count = 0;

        //生成随机id，正常业务中会有默认的id，不需要生成器生成
        List<long> ids = new List<long>();
        for (int i = 0; i < num; i++)
        {
            ids.Add(RandomHelper.RandInt64());
        }
        
       //添加组件并设置id
        start = TimeHelper.Ticks;
        for (int i = 0; i < num; i++)
        {
            var go = objList[i];
            go.AddComponent<IdInfo>().Id=ids[i];
            count++;
        }
        end = TimeHelper.Ticks;

        Debug.Log($"添加组件耗时：{TimeHelper.TicksToMillisecond(end - start)},获取数量:{count}");

        for (int i = 0; i < num; i++)
        {
            var info = objList[i].GetComponent<IdInfo>();

            idList.Add(info);
            logicDict.Add(info.Id, info.Id.ToString());
        }


    }
    public void Init()
    {
        int num = idList.Count;
        long start = 0, current = 0, end = 0, time = 0;
        int count = 0;

        //重命名GameObject name
        start = TimeHelper.Ticks;
        for (int i = 0; i < num; i++)
        {
            var info = idList[i];
            info.transform.name=info.Id.ToString();
            count++;
        }
        end = TimeHelper.Ticks;

        Debug.Log($"重命名gameObject耗时：{TimeHelper.TicksToMillisecond(end - start)},获取数量:{count}");

        count = 0;
        start = TimeHelper.Ticks;
        for (int i = 0; i < num; i++)
        {
            var go = objList[i];
            logicDict1.Add( go.GetHashCode(),"1");
            count++;
        }
        end = TimeHelper.Ticks;
        Debug.Log($"获取gameObject HashCode耗时：{TimeHelper.TicksToMillisecond(end - start)},获取数量:{count}");
    }

    public void ProfilerTime()
    {
        long start=0,current = 0,end=0,time=0;
        int count = 0;
        //start = DateTime.UtcNow.Ticks;
        start = TimeHelper.Ticks;

        for (int i = 0; i < objList.Count; i++)
        {
            string str= objList[i].name;
            long v = long.Parse(str);
            if(logicDict.ContainsKey(v))
            {
                count++;
            }
            
        }
        //end= DateTime.UtcNow.Ticks;
        end = TimeHelper.Ticks;
        Debug.Log($"通过字符串获取业务对象耗时：{TimeHelper.TicksToMillisecond(end - start)},获取数量:{count}");




        count = 0;
        //start= DateTime.UtcNow.Ticks;
        start = TimeHelper.Ticks;
        for (int i = 0; i < objList.Count; i++)
        {
            var go= objList[i];
            IdInfo info= go.GetComponent<IdInfo>();
            if (info != null)
            {
                if(logicDict.ContainsKey(info.Id))
                {
                    count++;
                }
            }
        }
        //end = DateTime.UtcNow.Ticks;
        end = TimeHelper.Ticks;
        Debug.Log($"通过组件id获取业务对象耗时：{TimeHelper.TicksToMillisecond(end - start)},获取数量:{count}");

        count = 0;
        //start= DateTime.UtcNow.Ticks;
        start = TimeHelper.Ticks;
        for (int i = 0; i < objList.Count; i++)
        {
            var go = objList[i];
            int hash = go.GetHashCode();
            
                if (logicDict1.ContainsKey(hash))
                {
                    count++;
                }
            
        }
        //end = DateTime.UtcNow.Ticks;
        end = TimeHelper.Ticks;
        Debug.Log($"通过HashCode获取业务对象耗时：{TimeHelper.TicksToMillisecond(end - start)},获取数量:{count}");
    }
}
