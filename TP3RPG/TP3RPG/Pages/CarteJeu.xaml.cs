using SkiaSharp;
using SkiaSharp.Views.Maui;
using TP3RPG.Model;

namespace TP3RPG.Pages;

public partial class CarteJeu : ContentPage
{
    private Carte _Carte;

    public CarteJeu()
    {
        InitializeComponent();
        _Carte = new Carte();
    }

    private void OnPaintSurface(object sender, SKPaintSurfaceEventArgs e)
    {
        var canvas = e.Surface.Canvas;
        canvas.Clear(SKColors.Black);

        float tuileSize = 50;

        foreach (Tuile tuile in _Carte.Tuiles)
        {
            SKPaint paint = new SKPaint();
            
            switch(tuile.Type)
            {
                case "Mur":
                    paint.Color = SKColors.Gray;
                    break;
                case "Herbe":
                    paint.Color = SKColors.Green;
                    break;
                case "Barriere":
                    paint.Color = SKColors.Orange;
                    break;
                case "Toit":
                    paint.Color = SKColors.Red;
                    break;
                default:
                    paint.Color = SKColors.Blue;
                    break;
            }
            

            canvas.DrawRect(tuile.X * tuileSize, tuile.Y * tuileSize, tuileSize, tuileSize, paint);
        }
    }
}