using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChatRoomMsg : MsgBase
{
	public ChatRoomMsg() { protoName = "ChatRoomMsg"; }
	//发送者id
	public string id = "";
	public string content = "";

}

