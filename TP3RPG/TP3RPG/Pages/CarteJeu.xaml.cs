using SkiaSharp;
using SkiaSharp.Views.Maui;
using TP3RPG.Model;
using TP3RPG.Assets;
using System.Diagnostics;

namespace TP3RPG.Pages;

public partial class CarteJeu : ContentPage
{
    private Carte _carte;
    private Controls _controls;
    private Joueur _joueur;
    private float tuileSize;
    private double minCote;

    public CarteJeu()
    {
        InitializeComponent();
        _carte = new Carte();
        _joueur = _carte.Joueur;
        _controls = new Controls(_joueur);
        _controls.OnJoueurD�plac� += MettreAJourAffichage;
        canvasJoueur.PaintSurface += OnPaintSurfaceJoueur;
    }

    private void MettreAJourAffichage()
    {
        canvasJoueur.InvalidateSurface(); 
    }
    protected override void OnSizeAllocated(double width, double height)
    {
        base.OnSizeAllocated(width, height);
        CalculerTailleCarte(width, height);
    }

    private void CalculerTailleCarte(double width, double height)
    {
        minCote = height>width?width:height;
        tuileSize = (float)(minCote / Carte.TailleCarte);

    }

    private void OnPaintSurfaceCarte(object sender, SKPaintSurfaceEventArgs e)
    {
        var canvas = e.Surface.Canvas;
        canvas.Clear(SKColors.Black);
        float offsetX = (e.Info.Width - (Carte.TailleCarte * tuileSize)) / 2;
        float offsetY = (e.Info.Height - (Carte.TailleCarte * tuileSize)) / 2;


        foreach (Tuile tuile in _carte.Tuiles)
        {
            SKPaint paint = new SKPaint();

            switch (tuile.Type)
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
            canvas.DrawRect(tuile.X * tuileSize + offsetX, tuile.Y * tuileSize + offsetY, tuileSize, tuileSize, paint);
        }
    }
    private void OnPaintSurfaceJoueur(object sender, SKPaintSurfaceEventArgs e)
    {
        var canvas = e.Surface.Canvas;
        canvas.Clear(SKColors.Transparent);
        float offsetX = (e.Info.Width - (Carte.TailleCarte * tuileSize)) / 2;
        float offsetY = (e.Info.Height - (Carte.TailleCarte * tuileSize)) / 2;

        SKPaint paintJoueur = new SKPaint { Color = SKColors.Blue };
        canvas.DrawCircle(_carte.Joueur.X * tuileSize + (tuileSize / 2)+offsetX, _carte.Joueur.Y * tuileSize + (tuileSize / 2)+offsetY, tuileSize / 2, paintJoueur);
    }   
}