using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using LitJson;
using UnityEngine;
using Object = UnityEngine.Object;


namespace NetworkTools
{
    public class RMessage : RecvMessage
    {
        public string RESPOND;
        public List<GameObject> prefabs;
        


        public override void SetJsonData(JsonData data)
        {
            RESPOND = data["RESPOND"].ToString();
            for (int i = 0; i < data["prefabs"].Count; i++)
            {
                GameObject newPrefab = Object.Instantiate<GameObject>(new GameObject());
                newPrefab.name = data["prefabs"][i]["prefab_name"].ToString();
                newPrefab.transform.position = String2Vector(data["prefabs"][i]["position"].ToString());
                newPrefab.transform.rotation = Quaternion.Euler(String2Vector(data["prefabs"][i]["rotation"].ToString()));
                newPrefab.transform.localScale = String2Vector(data["prefabs"][i]["scale"].ToString());
                prefabs.Add(newPrefab);
            }
            throw new System.NotImplementedException();
        }
        

        private Vector3 String2Vector(String s)
        {
            float[] positionValues = new float[3];
            string[] positionStrings = Regex.Split(s, @"[^\d\.]+");
            for (int j = 0; j < 3; j++) {
                positionValues[j] = float.Parse(positionStrings[j]);
            }
            return new Vector3(positionValues[0], positionValues[1], positionValues[2]);
        }
    }
}