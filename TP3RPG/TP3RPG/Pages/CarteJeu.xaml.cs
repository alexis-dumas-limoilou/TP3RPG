using SkiaSharp;
using SkiaSharp.Views.Maui;
using TP3RPG.Model;
using TP3RPG.Assets;

namespace TP3RPG.Pages;

public partial class CarteJeu : ContentPage
{
    private Carte _Carte;
    private Controls _Controls;
    private Joueur _Joueur;
    private float Hauteur;
    public CarteJeu()
    {
        InitializeComponent();
        _Carte = new Carte();
        _Joueur = new Joueur("Nicolas");
        _Controls = new Controls(_Joueur);
    }

    private void OnPaintSurface(object sender, SKPaintSurfaceEventArgs e)
    {
        var canvas = e.Surface.Canvas;
        canvas.Clear(SKColors.Black);
        Hauteur = (float)Window.Height;
       
        float tuileSize = Hauteur/Carte.TailleCarte;

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