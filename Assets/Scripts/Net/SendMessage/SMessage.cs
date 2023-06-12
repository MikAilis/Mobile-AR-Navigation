
using UnityEngine;

namespace NetworkTools
{
    public class SMessage : SendMessage
    {
        public bool end;
        public string user;

  
        
        public override WWWForm ToWWWForm()
        {
            throw new System.NotImplementedException();
        }
    }
}