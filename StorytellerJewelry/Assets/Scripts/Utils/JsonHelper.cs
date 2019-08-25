﻿using UnityEngine;
using System.Collections;

namespace Assets.Scripts.Utils
{
    public static class JsonHelper
    {
        public static T[] FromJson<T>(string json)
        {
            json = FixJson(json);
            Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(json);
            return wrapper.Items;
        }

        public static string ToJson<T>(T[] array)
        {
            Wrapper<T> wrapper = new Wrapper<T>();
            wrapper.Items = array;
            return JsonUtility.ToJson(wrapper);
        }

        public static string ToJson<T>(T[] array, bool prettyPrint)
        {
            Wrapper<T> wrapper = new Wrapper<T>();
            wrapper.Items = array;
            return JsonUtility.ToJson(wrapper, prettyPrint);
        }

        [System.Serializable]
        private class Wrapper<T>
        {
            public T[] Items;
        }

        private static string FixJson(string value)
        {
            value = "{\"Items\":" + value + "}";
            return value;
        }
    }
}
