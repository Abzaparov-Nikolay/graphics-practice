using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practice7
{
    public class Light
    {
        public Vector3 Position;
        public Vector3 Color;

        public Light(Vector3 position, Vector3 color) 
        { 
            this.Color= color;
            this.Position = position;
        }
    }
}
