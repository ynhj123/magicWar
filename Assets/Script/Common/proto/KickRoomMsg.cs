// <auto-generated>
//     Generated by the protocol buffer compiler.  DO NOT EDIT!
//     source: proto/KickRoomMsg.proto
// </auto-generated>
#pragma warning disable 1591, 0612, 3021
#region Designer generated code

using pb = global::Google.Protobuf;
using pbc = global::Google.Protobuf.Collections;
using pbr = global::Google.Protobuf.Reflection;
using scg = global::System.Collections.Generic;
/// <summary>Holder for reflection information generated from proto/KickRoomMsg.proto</summary>
public static partial class KickRoomMsgReflection {

  #region Descriptor
  /// <summary>File descriptor for proto/KickRoomMsg.proto</summary>
  public static pbr::FileDescriptor Descriptor {
    get { return descriptor; }
  }
  private static pbr::FileDescriptor descriptor;

  static KickRoomMsgReflection() {
    byte[] descriptorData = global::System.Convert.FromBase64String(
        string.Concat(
          "Chdwcm90by9LaWNrUm9vbU1zZy5wcm90byI1CgtLaWNrUm9vbU1zZxILCgN1",
          "aWQYASABKAkSDAoEY29kZRgCIAEoCRILCgNtc2cYAyABKAliBnByb3RvMw=="));
    descriptor = pbr::FileDescriptor.FromGeneratedCode(descriptorData,
        new pbr::FileDescriptor[] { },
        new pbr::GeneratedClrTypeInfo(null, new pbr::GeneratedClrTypeInfo[] {
          new pbr::GeneratedClrTypeInfo(typeof(global::KickRoomMsg), global::KickRoomMsg.Parser, new[]{ "Uid", "Code", "Msg" }, null, null, null)
        }));
  }
  #endregion

}
#region Messages
public sealed partial class KickRoomMsg : pb::IMessage<KickRoomMsg> {
  private static readonly pb::MessageParser<KickRoomMsg> _parser = new pb::MessageParser<KickRoomMsg>(() => new KickRoomMsg());
  private pb::UnknownFieldSet _unknownFields;
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public static pb::MessageParser<KickRoomMsg> Parser { get { return _parser; } }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public static pbr::MessageDescriptor Descriptor {
    get { return global::KickRoomMsgReflection.Descriptor.MessageTypes[0]; }
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  pbr::MessageDescriptor pb::IMessage.Descriptor {
    get { return Descriptor; }
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public KickRoomMsg() {
    OnConstruction();
  }

  partial void OnConstruction();

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public KickRoomMsg(KickRoomMsg other) : this() {
    uid_ = other.uid_;
    code_ = other.code_;
    msg_ = other.msg_;
    _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public KickRoomMsg Clone() {
    return new KickRoomMsg(this);
  }

  /// <summary>Field number for the "uid" field.</summary>
  public const int UidFieldNumber = 1;
  private string uid_ = "";
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public string Uid {
    get { return uid_; }
    set {
      uid_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
    }
  }

  /// <summary>Field number for the "code" field.</summary>
  public const int CodeFieldNumber = 2;
  private string code_ = "";
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public string Code {
    get { return code_; }
    set {
      code_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
    }
  }

  /// <summary>Field number for the "msg" field.</summary>
  public const int MsgFieldNumber = 3;
  private string msg_ = "";
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public string Msg {
    get { return msg_; }
    set {
      msg_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
    }
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public override bool Equals(object other) {
    return Equals(other as KickRoomMsg);
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public bool Equals(KickRoomMsg other) {
    if (ReferenceEquals(other, null)) {
      return false;
    }
    if (ReferenceEquals(other, this)) {
      return true;
    }
    if (Uid != other.Uid) return false;
    if (Code != other.Code) return false;
    if (Msg != other.Msg) return false;
    return Equals(_unknownFields, other._unknownFields);
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public override int GetHashCode() {
    int hash = 1;
    if (Uid.Length != 0) hash ^= Uid.GetHashCode();
    if (Code.Length != 0) hash ^= Code.GetHashCode();
    if (Msg.Length != 0) hash ^= Msg.GetHashCode();
    if (_unknownFields != null) {
      hash ^= _unknownFields.GetHashCode();
    }
    return hash;
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public override string ToString() {
    return pb::JsonFormatter.ToDiagnosticString(this);
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public void WriteTo(pb::CodedOutputStream output) {
    if (Uid.Length != 0) {
      output.WriteRawTag(10);
      output.WriteString(Uid);
    }
    if (Code.Length != 0) {
      output.WriteRawTag(18);
      output.WriteString(Code);
    }
    if (Msg.Length != 0) {
      output.WriteRawTag(26);
      output.WriteString(Msg);
    }
    if (_unknownFields != null) {
      _unknownFields.WriteTo(output);
    }
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public int CalculateSize() {
    int size = 0;
    if (Uid.Length != 0) {
      size += 1 + pb::CodedOutputStream.ComputeStringSize(Uid);
    }
    if (Code.Length != 0) {
      size += 1 + pb::CodedOutputStream.ComputeStringSize(Code);
    }
    if (Msg.Length != 0) {
      size += 1 + pb::CodedOutputStream.ComputeStringSize(Msg);
    }
    if (_unknownFields != null) {
      size += _unknownFields.CalculateSize();
    }
    return size;
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public void MergeFrom(KickRoomMsg other) {
    if (other == null) {
      return;
    }
    if (other.Uid.Length != 0) {
      Uid = other.Uid;
    }
    if (other.Code.Length != 0) {
      Code = other.Code;
    }
    if (other.Msg.Length != 0) {
      Msg = other.Msg;
    }
    _unknownFields = pb::UnknownFieldSet.MergeFrom(_unknownFields, other._unknownFields);
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public void MergeFrom(pb::CodedInputStream input) {
    uint tag;
    while ((tag = input.ReadTag()) != 0) {
      switch(tag) {
        default:
          _unknownFields = pb::UnknownFieldSet.MergeFieldFrom(_unknownFields, input);
          break;
        case 10: {
          Uid = input.ReadString();
          break;
        }
        case 18: {
          Code = input.ReadString();
          break;
        }
        case 26: {
          Msg = input.ReadString();
          break;
        }
      }
    }
  }

}

#endregion


#endregion Designer generated code
