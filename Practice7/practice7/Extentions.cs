using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practice7
{
    public static class Extentions
    {
        public static Vector3 FromString(this Vector3 vector, string input)
        {
            var rawValues = input.Split(';');
            var x = float.Parse(rawValues[0]);
            var y = float.Parse(rawValues[1]);
            var z = float.Parse(rawValues[2]);
            return new Vector3(x, y, z);
        }

        public static void FromString(this Transform transform, string input)
        {
            var rawValues = input.Split("\r\n");
            
            for(var i =0; i < 3; i++)
            {
                rawValues[i] = rawValues[i].Replace("(","").Replace(")", "").Replace(" ", "");
            }
            transform.position.FromString(rawValues[0]).Deconstruct(out var tx, out var ty, out var tz);
            transform.selfRotation.FromString(rawValues[2]).Deconstruct(out var rx, out var ry, out var rz);

            transform.AddTranslation(tx,ty,tz);
            transform.SetScale(transform.scale.FromString(rawValues[1]));
            transform.SelfRotate(rx,ry,rz);
        }
    }
}
