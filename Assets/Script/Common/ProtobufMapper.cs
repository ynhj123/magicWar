using Google.Protobuf;
using System.Collections.Generic;
using System.IO;
using System.Linq;

public static class ProtobufMapper
{
    static Dictionary<short, string> intToString = new Dictionary<short, string>();
    static Dictionary<string, short> stringToInt = new Dictionary<string, short>();



    static ProtobufMapper()
    {
        intToString.Add(100, "MsgPing");
        intToString.Add(101, "MsgPong");
        intToString.Add(1001, "EnterMsg");
        intToString.Add(1002, "RoomListMsg");
        intToString.Add(1003, "CreateRoomMsg");
        intToString.Add(1004, "EnterRoomMsg");
        intToString.Add(1005, "ChatRoomMsg");
        intToString.Add(1006, "KickRoomMsg");
        intToString.Add(1007, "LeaveRoomMsg");
        intToString.Add(1008, "GetRoomInfoMsg");
        intToString.Add(1009, "StartMsg");
        intToString.Add(1010, "UnreadyStartMsg");
        intToString.Add(1011, "ReadyStartMsg");
        intToString.Add(1012, "LoadFinishMsg");
        intToString.Add(1013, "SyncPlayerMsg");
        intToString.Add(1014, "SkillMsg");
        intToString.Add(1015, "HitMsg");
        intToString.Add(1016, "EndMsg");
        intToString.Add(1017, "LeaveBattleMsg");

        stringToInt = intToString.ToDictionary(k => k.Value, p => p.Key);
    }

    public static string GetString(short key)
    {
        string val;
        intToString.TryGetValue(key, out val);
        return val;
    }
    public static short GetInt(string key)
    {
        short val;
        stringToInt.TryGetValue(key, out val);
        return val;
    }

    public static byte[] Serialize(IMessage msg)
    {

        if (msg == null)
            return null;

        using (MemoryStream ms = new MemoryStream())
        {
            msg.WriteTo(ms);
            return ms.ToArray();
        }
    }
    public static T Deserialize<T>(byte[] dataBytes) where T : IMessage<T>, new()
    {
        if (dataBytes == null)
            return default(T);

        using (MemoryStream ms = new MemoryStream())
        {
            ms.Write(dataBytes, 0, dataBytes.Length);
            ms.Seek(0, SeekOrigin.Begin);

            MessageParser<T> parser = new MessageParser<T>(() => new T());
            return parser.ParseFrom(ms);
        }

    }


}

