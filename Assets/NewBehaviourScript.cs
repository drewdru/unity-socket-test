using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;
using System.Net.Sockets;
using System.Threading;

public class NewBehaviourScript : MonoBehaviour {

	public int Data;
	public Thread listener;
	UdpClient cl = new UdpClient(8001);
	UdpClient cl2 = new UdpClient(8002);
	public ThreadStart translater = new ThreadStart(DoWork);

	public static void DoWork() 
	{
		Console.WriteLine("Static thread procedure."); 
	}
 
    private void SendMSG() {
        try
        {
            cl.Client.ReceiveTimeout = 1000;
            byte[] str = System.Text.Encoding.UTF8.GetBytes("Hellow, World!");
            cl.Send(str, str.Length, "127.0.0.1", 8002);
            CompMSG();
        }
        catch (System.Exception ex)
        {
            Debug.Log(ex.Message);
        }
        // finally
        // {
        //     cl.Close();
        // }
    }
 
    private void CompMSG()
    {
        IPEndPoint ip = null;
        try
        {
            byte[] data;
            cl2.Client.ReceiveTimeout = 1000;
            data = cl2.Receive(ref ip);
            Debug.Log(System.Text.Encoding.UTF8.GetString(data));
        }
        catch (System.Exception ex)
        {
            Debug.Log(ex.Message);
        }
        // finally
        // {
        //     cl2.Close();
        // }
    }
 
    void Start()
    {
        listener = new Thread(new ThreadStart(translater));
        listener.IsBackground = true;
        listener.Start();  
    }
 
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T)) SendMSG();
        if (Input.GetKeyDown(KeyCode.Y)) CompMSG();
    }
}
