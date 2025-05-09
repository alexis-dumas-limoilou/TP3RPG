using SkiaSharp;
using SkiaSharp.Views.Maui;
using TP3RPG.Model;
using TP3RPG.Vues;

namespace TP3RPG.Pages;

public partial class CarteJeu : ContentPage
{
    private Carte _Carte;
    private Joystick _Joystick;
    private Joueur _Joueur;

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
    private void CalculerDeplacement(SKPoint touchPosition)
    {
        float dx = touchPosition.X - _Joystick.Center.X;
        float dy = touchPosition.Y - _Joystick.Center.Y;

        string direction = "";

        if (Math.Abs(dx) > Math.Abs(dy))
            direction = dx > 0 ? "droite" : "gauche";
        else
            direction = dy > 0 ? "bas" : "haut";

        _Joueur.SeDeplacer(direction);
    }
}