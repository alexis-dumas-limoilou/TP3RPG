using SkiaSharp.Views.Maui;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SkiaSharp.Views.Maui.Controls;

namespace TP3RPG.Vues
{
    public class Joystick : SKCanvasView
    {
        public SKPoint Center;
        private SKPoint currentTouch;
        private float radius = 100;
        private bool isDragging = false;

        public Joystick()
        {
            EnableTouchEvents = true;
            Center = new SKPoint(radius, radius);
            currentTouch = Center;
            Touch += OnTouch;
        }

        private void OnTouch(object sender, SKTouchEventArgs e)
        {
            if (e.ActionType == SKTouchAction.Pressed || e.ActionType == SKTouchAction.Moved)
            {
                isDragging = true;
                currentTouch = e.Location;
                InvalidateSurface();
            }
            else if (e.ActionType == SKTouchAction.Released)
            {
                isDragging = false;
                currentTouch = Center; // Retour au centre
                InvalidateSurface();
            }
        }

        protected override void OnPaintSurface(SKPaintSurfaceEventArgs e)
        {
            var canvas = e.Surface.Canvas;
            canvas.Clear(SKColors.Transparent);

            // Dessin du joystick
            using (var paint = new SKPaint { Color = SKColors.Gray, Style = SKPaintStyle.Fill })
            {
                canvas.DrawCircle(Center, radius, paint);
            }

            // Dessin du bouton central (joystick)
            using (var paint = new SKPaint { Color = SKColors.Blue, Style = SKPaintStyle.Fill })
            {
                canvas.DrawCircle(currentTouch, radius / 2, paint);
            }
        }
    }
}
