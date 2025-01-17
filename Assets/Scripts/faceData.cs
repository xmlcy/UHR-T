/* LCM type definition class file
 * This file was automatically generated by lcm-gen 1.5.0
 * DO NOT MODIFY BY HAND!!!!
 */

using System;
using System.Collections.Generic;
using System.IO;
using LCM.LCM;
 
namespace picoLcmData
{
    public sealed class faceData : LCM.LCM.LCMEncodable
    {
        public float[] bs;
 
        public faceData()
        {
            bs = new float[72];
        }
 
        public static readonly ulong LCM_FINGERPRINT;
        public static readonly ulong LCM_FINGERPRINT_BASE = 0xe3ae873a19f24c87L;
 
        static faceData()
        {
            LCM_FINGERPRINT = _hashRecursive(new List<String>());
        }
 
        public static ulong _hashRecursive(List<String> classes)
        {
            if (classes.Contains("picoLcmData.faceData"))
                return 0L;
 
            classes.Add("picoLcmData.faceData");
            ulong hash = LCM_FINGERPRINT_BASE
                ;
            classes.RemoveAt(classes.Count - 1);
            return (hash<<1) + ((hash>>63)&1);
        }
 
        public void Encode(LCMDataOutputStream outs)
        {
            outs.Write((long) LCM_FINGERPRINT);
            _encodeRecursive(outs);
        }
 
        public void _encodeRecursive(LCMDataOutputStream outs)
        {
            for (int a = 0; a < 72; a++) {
                outs.Write(this.bs[a]); 
            }
 
        }
 
        public faceData(byte[] data) : this(new LCMDataInputStream(data))
        {
        }
 
        public faceData(LCMDataInputStream ins)
        {
            if ((ulong) ins.ReadInt64() != LCM_FINGERPRINT)
                throw new System.IO.IOException("LCM Decode error: bad fingerprint");
 
            _decodeRecursive(ins);
        }
 
        public static picoLcmData.faceData _decodeRecursiveFactory(LCMDataInputStream ins)
        {
            picoLcmData.faceData o = new picoLcmData.faceData();
            o._decodeRecursive(ins);
            return o;
        }
 
        public void _decodeRecursive(LCMDataInputStream ins)
        {
            this.bs = new float[(int) 72];
            for (int a = 0; a < 72; a++) {
                this.bs[a] = ins.ReadSingle();
            }
 
        }
 
        public picoLcmData.faceData Copy()
        {
            picoLcmData.faceData outobj = new picoLcmData.faceData();
            outobj.bs = new float[(int) 72];
            for (int a = 0; a < 72; a++) {
                outobj.bs[a] = this.bs[a];
            }
 
            return outobj;
        }
    }
}

