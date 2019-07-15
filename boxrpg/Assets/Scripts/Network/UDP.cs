using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;
using System.Net.Sockets;
using System;
public class UDP : MonoBehaviour
{
	// Buffer size is depends on in needs.
	// However smaller is better
	//byte[] rB = new byte[10000];
//	Thread serverCheckThread;

	public int port = 5555;
	public string serverAddr = "192.168.1.14";
	UdpClient client;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
	// Do not use update for networking, it can't handle many packets in real time.
    void Update()
    {
    }

	public void udpConnection(){
		client = new UdpClient();
		try {
			client.Connect(serverAddr, port);
		}
		catch(SocketException e) {
			print(e.ToString());
		}
	}

	void OnReceive()
	{
		try {
			IPAddress ipAddr = IPAddress.Parse(serverAddr);
			IPEndPoint RemoteIpEndPoint = new IPEndPoint(ipAddr, port);
			byte[] receivedData = client.Receive(ref RemoteIpEndPoint);
			string data = System.Text.Encoding.UTF8.GetString(receivedData);
			// Do something with received data
			// Code:

		}
		catch(SocketException e) {
			print(e.ToString());
		}
	}

	void send(string data){
		try {
			byte[] byteData = System.Text.Encoding.UTF8.GetBytes(data);
			client.Send(byteData, byteData.Length);
		}
		catch(SocketException e) {
			print(e.ToString());
		}
	}

	public void Close(){
		client.Close();
	}

}
