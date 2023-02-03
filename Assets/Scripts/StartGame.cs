using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public static class NetProps
{
    public static string ip { get; set; }
    public static ushort port { get; set; }
    public static string mode { get; set; }
}

public class StartGame : MonoBehaviour
{
    public TextMeshProUGUI ip;
    public TextMeshProUGUI port;
    public string lest;
    public float p;
    public int pr;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Host()
    {
        lest = port.text;
        lest = lest.Remove(lest.Length - 1, 1);
        p = Int32.Parse(lest);
        NetProps.ip = ip.text;
        NetProps.ip = NetProps.ip.Remove(NetProps.ip.Length - 1, 1);
        print(NetProps.ip == "127.0.0.1");
        NetProps.port = (ushort)p;
        NetProps.mode = "host";
        SceneManager.LoadScene(1);
    }

    public void Client()
    {
        lest = port.text;
        lest = lest.Remove(lest.Length - 1, 1);
        p = Int32.Parse(lest);
        NetProps.ip = ip.text;
        NetProps.ip = NetProps.ip.Remove(NetProps.ip.Length - 1, 1);
        print(NetProps.ip == "127.0.0.1");
        NetProps.port = (ushort)p;
        NetProps.mode = "client";
        SceneManager.LoadScene(1);
    }
}
