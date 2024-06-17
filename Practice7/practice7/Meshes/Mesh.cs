using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practice7
{
    public interface IMesh
    {
        public Vector3[] GetVertices();
        public int[] GetIndices();

        public Vector2[] GetTextureCoords();
    }
}
