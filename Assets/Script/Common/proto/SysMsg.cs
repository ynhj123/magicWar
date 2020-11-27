public class MsgPing : MsgBase
{
    public MsgPing() { protoName = "MsgPing"; }
}


public class MsgPong : MsgBase
{
    public MsgPong() { protoName = "MsgPong"; }
}

public class EnterMsg : MsgBase
{
    public EnterMsg() { protoName = "EnterMsg"; }
    public string nickname;
    public string token;
    public string code;
    public string msg;
}