using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns.Structural
{
    /*
     #### 1
        A classic example of the Bridge pattern is used in the definition of shapes 
        in an UI environment (see the Bridge pattern Wikipedia entry). 
        The Bridge pattern is a composite of the Template and Strategy patterns.

        It is a common view some aspects of the Adapter pattern in the Bridge pattern. 
        However, to quote from this article:
        At first sight, the Bridge pattern looks a lot like the Adapter pattern 
        in that a class is used to convert one kind of interface to another. 
        However, the intent of the Adapter pattern is to make one or more 
        classes' interfaces look the same as that of a particular class. 
        The Bridge pattern is designed to separate a class's interface from its implementation 
        so you can vary or replace the implementation without changing the client code.



    #### 2
        When do you use the Bridge Pattern?
                    ----Shape---
                  /            \
         Rectangle              Circle
        /         \            /      \
BlueRectangle  RedRectangle BlueCircle RedCircle

    Refactor to:

           ----Shape---                        Color
         /            \                       /   \
Rectangle(Color)   Circle(Color)           Blue   Red

    
    #### 3
    "Adapter makes things work after they're designed; 
     Bridge makes them work before they are. [GoF, p219]"


     */

    public interface IRenderer
    {
        void RenderCircle(float radius);
    }

    public class VectorRenderer : IRenderer
    {
        public void RenderCircle(float radius)
        {
            Console.WriteLine($"Drawing a circle of radius {radius}");
        }
    }

    public class RasterRenderer : IRenderer
    {
        public void RenderCircle(float radius)
        {
            Console.WriteLine($"Drawing pixels for circle of radius {radius}");
        }
    }

    public class PixelRenderer : IRenderer
    {
        public void RenderCircle(float radius)
        {
            Console.WriteLine($"Pixel Rendering... Drawing pixels for circle of radius {radius}");
        }
    }

    public abstract class Shape
    {
        protected IRenderer renderer;

        // a bridge between the shape that's being drawn an
        // the component which actually draws it
        public Shape(IRenderer renderer)
        {
            this.renderer = renderer;
        }

        public abstract void Draw();
        public abstract void Resize(float factor);
    }

    public class Circle : Shape
    {
        private float radius;

        public Circle(IRenderer renderer, float radius) : base(renderer)
        {
            this.radius = radius;
        }

        public override void Draw()
        {
            renderer.RenderCircle(radius);
        }

        public override void Resize(float factor)
        {
            radius *= factor;
        }
    }

    public class DriverBridge
    {
        RasterRenderer raster = new RasterRenderer();
        VectorRenderer vector = new VectorRenderer();
        public void DrawCircle()
        {
            IRenderer renderer;// = driverFactory.GetRenderer(Environment.OSVersion.ToString());
            var circle = new Circle(raster, 5);
            circle.Draw();
            circle.Resize(2);
            circle.Draw();
        }
    }

    public class BridgeExecution
    {
        public void Execute()
        {
            new DriverBridge().DrawCircle();
        }
    }

}
