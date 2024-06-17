using OpenTK.Graphics.ES11;
using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practice7
{
    public class GameObject
    {
        public Transform transform;
        public Renderer renderer;
        public IMesh mesh;
        public Texture texture;

        public GameObject(IMesh mesh)
        {
            this.mesh = mesh;
            transform = new Transform();
            renderer = new Renderer(this);
        }

        public GameObject(IMesh mesh, string texturePath)
        {
            this.mesh = mesh;
            transform = new Transform();
            texture = new Texture(texturePath);
            renderer = new Renderer(this);
        }

        public void Draw()
        {
            renderer.Draw();
        }
    }
}
