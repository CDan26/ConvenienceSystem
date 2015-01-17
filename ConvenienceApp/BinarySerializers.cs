using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.IO;

namespace ConvenienceBackend
{
    class BinarySerializers
    {

        //////////////////////////
        // New (Binary) Serialization methods
        //////////////////////////


        /*** Dictionaries of String/Double ***/

        public static void SerializeDictSD(Dictionary<string, double> dictionary, BinaryWriter writer)
        {
            //BinaryWriter writer = new BinaryWriter(stream);
            writer.Write(dictionary.Count);
            foreach (var kvp in dictionary)
            {
                writer.Write(kvp.Key);
                writer.Write(kvp.Value);
            }
            writer.Flush();
        }

        public static Dictionary<string, double> DeserializeDictSD(BinaryReader reader)
        {
            //BinaryReader reader = new BinaryReader(stream);
            int count = reader.ReadInt32();
            var dictionary = new Dictionary<string, double>(count);
            for (int n = 0; n < count; n++)
            {
                var key = reader.ReadString();
                var value = reader.ReadDouble();
                dictionary.Add(key, value);
            }
            return dictionary;
        }

        /*** Dictionaries of String/String ***/

        public static void SerializeDictS(Dictionary<string, string> dictionary, BinaryWriter writer)
        {
            //BinaryWriter writer = new BinaryWriter(stream);
            writer.Write(dictionary.Count);
            foreach (var kvp in dictionary)
            {
                writer.Write(kvp.Key);
                writer.Write(kvp.Value);
            }
            writer.Flush();
        }

        public static Dictionary<string, string> DeserializeDictS(BinaryReader reader)
        {
            //BinaryReader reader = new BinaryReader(stream);
            int count = reader.ReadInt32();
            var dictionary = new Dictionary<string, string>(count);
            for (int n = 0; n < count; n++)
            {
                var key = reader.ReadString();
                var value = reader.ReadString();
                dictionary.Add(key, value);
            }
            return dictionary;
        }

        /*** Lists of Tuples of String/String ***/

        public static void SerializeListS(List<Tuple<string, string>> dictionary, BinaryWriter writer)
        {
            //BinaryWriter writer = new BinaryWriter(stream);
            writer.Write(dictionary.Count);
            foreach (var kvp in dictionary)
            {
                writer.Write(kvp.Item1);
                writer.Write(kvp.Item2);
            }
            writer.Flush();
        }

        public static List<Tuple<string, string>> DeserializeListtS(BinaryReader reader)
        {
            //BinaryReader reader = new BinaryReader(stream);
            int count = reader.ReadInt32();
            //var dictionary = new List<Tuple<string, string>>(count);
            var dictionary = new List<Tuple<string, string>>();
            for (int n = 0; n < count; n++)
            {
                var key = reader.ReadString();
                var value = reader.ReadString();
                dictionary.Add(new Tuple<string, string>(key, value));
            }
            return dictionary;
        }

        /*** Lists of Tuples of String/String/Double/String ***/

        public static void SerializeListSSDS(List<Tuple<string, string, double, string>> dictionary, BinaryWriter writer)
        {
            //BinaryWriter writer = new BinaryWriter(stream);
            writer.Write(dictionary.Count);
            foreach (var kvp in dictionary)
            {
                writer.Write(kvp.Item1);
                writer.Write(kvp.Item2);
                writer.Write(kvp.Item3);
                writer.Write(kvp.Item4);
            }
            writer.Flush();
        }

        public static List<Tuple<string, string, double, string>> DeserializeListtSSDS(BinaryReader reader)
        {
            //BinaryReader reader = new BinaryReader(stream);
            int count = reader.ReadInt32();
            var dictionary = new List<Tuple<string, string, double, string>>(count);
            for (int n = 0; n < count; n++)
            {
                var i1 = reader.ReadString();
                var i2 = reader.ReadString();
                var i3 = reader.ReadDouble();
                var i4 = reader.ReadString();
                dictionary.Add(new Tuple<string, string, double, string>(i1, i2, i3, i4));
            }
            return dictionary;
        }

        /*** Lists of Tuples of int/String/Double/String/string ***/

        public static void SerializeListISDSS(List<Tuple<int, string, double, string, string>> dictionary, BinaryWriter writer)
        {
            //BinaryWriter writer = new BinaryWriter(stream);
            writer.Write(dictionary.Count);
            foreach (var kvp in dictionary)
            {
                writer.Write(kvp.Item1);
                writer.Write(kvp.Item2);
                writer.Write(kvp.Item3);
                writer.Write(kvp.Item4);
                writer.Write(kvp.Item5);
            }
            writer.Flush();
        }

        public static List<Tuple<int, string, double, string, string>> DeserializeListtISDSS(BinaryReader reader)
        {
            //BinaryReader reader = new BinaryReader(stream);
            int count = reader.ReadInt32();
            var dictionary = new List<Tuple<int, string, double, string, string>>();
            for (int n = 0; n < count; n++)
            {
                var i1 = reader.ReadInt32();
                var i2 = reader.ReadString();
                var i3 = reader.ReadDouble();
                var i4 = reader.ReadString();
                var i5 = reader.ReadString();
                dictionary.Add(new Tuple<int, string, double, string, string>(i1,i2,i3,i4,i5));
            }
            return dictionary;
        }


        /*** Lists of Tuples of int/String/Double/String ***/

        public static void SerializeListISDS(List<Tuple<int, string, double, string>> dictionary, BinaryWriter writer)
        {
            //BinaryWriter writer = new BinaryWriter(stream);
            writer.Write(dictionary.Count);
            foreach (var kvp in dictionary)
            {
                writer.Write(kvp.Item1);
                writer.Write(kvp.Item2);
                writer.Write(kvp.Item3);
                writer.Write(kvp.Item4);
            }
            writer.Flush();
        }

        public static List<Tuple<int, string, double, string>> DeserializeListtISDS(BinaryReader reader)
        {
            //BinaryReader reader = new BinaryReader(stream);
            int count = reader.ReadInt32();
            var dictionary = new List<Tuple<int, string, double, string>>();
            for (int n = 0; n < count; n++)
            {
                var i1 = reader.ReadInt32();
                var i2 = reader.ReadString();
                var i3 = reader.ReadDouble();
                var i4 = reader.ReadString();
                dictionary.Add(new Tuple<int, string, double, string>(i1, i2, i3, i4));
            }
            return dictionary;
        }
    }
}
