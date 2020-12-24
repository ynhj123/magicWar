// <auto-generated>
//     Generated by the protocol buffer compiler.  DO NOT EDIT!
//     source: proto/HitMsg.proto
// </auto-generated>
#pragma warning disable 1591, 0612, 3021
#region Designer generated code

using pb = global::Google.Protobuf;
using pbc = global::Google.Protobuf.Collections;
using pbr = global::Google.Protobuf.Reflection;
using scg = global::System.Collections.Generic;
/// <summary>Holder for reflection information generated from proto/HitMsg.proto</summary>
public static partial class HitMsgReflection {

  #region Descriptor
  /// <summary>File descriptor for proto/HitMsg.proto</summary>
  public static pbr::FileDescriptor Descriptor {
    get { return descriptor; }
  }
  private static pbr::FileDescriptor descriptor;

  static HitMsgReflection() {
    byte[] descriptorData = global::System.Convert.FromBase64String(
        string.Concat(
          "ChJwcm90by9IaXRNc2cucHJvdG8iWQoGSGl0TXNnEgsKA3VpZBgBIAEoCRIP",
          "Cgdza2lsbElkGAIgASgFEgkKAXgYAyABKAISCQoBehgEIAEoAhIJCgF5GAUg",
          "ASgCEhAKCHRhcmdldElkGAYgASgJYgZwcm90bzM="));
    descriptor = pbr::FileDescriptor.FromGeneratedCode(descriptorData,
        new pbr::FileDescriptor[] { },
        new pbr::GeneratedClrTypeInfo(null, new pbr::GeneratedClrTypeInfo[] {
          new pbr::GeneratedClrTypeInfo(typeof(global::HitMsg), global::HitMsg.Parser, new[]{ "Uid", "SkillId", "X", "Z", "Y", "TargetId" }, null, null, null)
        }));
  }
  #endregion

}
#region Messages
public sealed partial class HitMsg : pb::IMessage<HitMsg> {
  private static readonly pb::MessageParser<HitMsg> _parser = new pb::MessageParser<HitMsg>(() => new HitMsg());
  private pb::UnknownFieldSet _unknownFields;
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public static pb::MessageParser<HitMsg> Parser { get { return _parser; } }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public static pbr::MessageDescriptor Descriptor {
    get { return global::HitMsgReflection.Descriptor.MessageTypes[0]; }
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  pbr::MessageDescriptor pb::IMessage.Descriptor {
    get { return Descriptor; }
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public HitMsg() {
    OnConstruction();
  }

  partial void OnConstruction();

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public HitMsg(HitMsg other) : this() {
    uid_ = other.uid_;
    skillId_ = other.skillId_;
    x_ = other.x_;
    z_ = other.z_;
    y_ = other.y_;
    targetId_ = other.targetId_;
    _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public HitMsg Clone() {
    return new HitMsg(this);
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

  /// <summary>Field number for the "skillId" field.</summary>
  public const int SkillIdFieldNumber = 2;
  private int skillId_;
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public int SkillId {
    get { return skillId_; }
    set {
      skillId_ = value;
    }
  }

  /// <summary>Field number for the "x" field.</summary>
  public const int XFieldNumber = 3;
  private float x_;
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public float X {
    get { return x_; }
    set {
      x_ = value;
    }
  }

  /// <summary>Field number for the "z" field.</summary>
  public const int ZFieldNumber = 4;
  private float z_;
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public float Z {
    get { return z_; }
    set {
      z_ = value;
    }
  }

  /// <summary>Field number for the "y" field.</summary>
  public const int YFieldNumber = 5;
  private float y_;
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public float Y {
    get { return y_; }
    set {
      y_ = value;
    }
  }

  /// <summary>Field number for the "targetId" field.</summary>
  public const int TargetIdFieldNumber = 6;
  private string targetId_ = "";
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public string TargetId {
    get { return targetId_; }
    set {
      targetId_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
    }
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public override bool Equals(object other) {
    return Equals(other as HitMsg);
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public bool Equals(HitMsg other) {
    if (ReferenceEquals(other, null)) {
      return false;
    }
    if (ReferenceEquals(other, this)) {
      return true;
    }
    if (Uid != other.Uid) return false;
    if (SkillId != other.SkillId) return false;
    if (!pbc::ProtobufEqualityComparers.BitwiseSingleEqualityComparer.Equals(X, other.X)) return false;
    if (!pbc::ProtobufEqualityComparers.BitwiseSingleEqualityComparer.Equals(Z, other.Z)) return false;
    if (!pbc::ProtobufEqualityComparers.BitwiseSingleEqualityComparer.Equals(Y, other.Y)) return false;
    if (TargetId != other.TargetId) return false;
    return Equals(_unknownFields, other._unknownFields);
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public override int GetHashCode() {
    int hash = 1;
    if (Uid.Length != 0) hash ^= Uid.GetHashCode();
    if (SkillId != 0) hash ^= SkillId.GetHashCode();
    if (X != 0F) hash ^= pbc::ProtobufEqualityComparers.BitwiseSingleEqualityComparer.GetHashCode(X);
    if (Z != 0F) hash ^= pbc::ProtobufEqualityComparers.BitwiseSingleEqualityComparer.GetHashCode(Z);
    if (Y != 0F) hash ^= pbc::ProtobufEqualityComparers.BitwiseSingleEqualityComparer.GetHashCode(Y);
    if (TargetId.Length != 0) hash ^= TargetId.GetHashCode();
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
    if (SkillId != 0) {
      output.WriteRawTag(16);
      output.WriteInt32(SkillId);
    }
    if (X != 0F) {
      output.WriteRawTag(29);
      output.WriteFloat(X);
    }
    if (Z != 0F) {
      output.WriteRawTag(37);
      output.WriteFloat(Z);
    }
    if (Y != 0F) {
      output.WriteRawTag(45);
      output.WriteFloat(Y);
    }
    if (TargetId.Length != 0) {
      output.WriteRawTag(50);
      output.WriteString(TargetId);
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
    if (SkillId != 0) {
      size += 1 + pb::CodedOutputStream.ComputeInt32Size(SkillId);
    }
    if (X != 0F) {
      size += 1 + 4;
    }
    if (Z != 0F) {
      size += 1 + 4;
    }
    if (Y != 0F) {
      size += 1 + 4;
    }
    if (TargetId.Length != 0) {
      size += 1 + pb::CodedOutputStream.ComputeStringSize(TargetId);
    }
    if (_unknownFields != null) {
      size += _unknownFields.CalculateSize();
    }
    return size;
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public void MergeFrom(HitMsg other) {
    if (other == null) {
      return;
    }
    if (other.Uid.Length != 0) {
      Uid = other.Uid;
    }
    if (other.SkillId != 0) {
      SkillId = other.SkillId;
    }
    if (other.X != 0F) {
      X = other.X;
    }
    if (other.Z != 0F) {
      Z = other.Z;
    }
    if (other.Y != 0F) {
      Y = other.Y;
    }
    if (other.TargetId.Length != 0) {
      TargetId = other.TargetId;
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
        case 16: {
          SkillId = input.ReadInt32();
          break;
        }
        case 29: {
          X = input.ReadFloat();
          break;
        }
        case 37: {
          Z = input.ReadFloat();
          break;
        }
        case 45: {
          Y = input.ReadFloat();
          break;
        }
        case 50: {
          TargetId = input.ReadString();
          break;
        }
      }
    }
  }

}

#endregion


#endregion Designer generated code
