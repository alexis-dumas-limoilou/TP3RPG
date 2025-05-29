using SkiaSharp;
using SkiaSharp.Views.Maui;
using TP3RPG.Model;
using TP3RPG.Assets;
using TP3RPG.Service;
using System.Windows.Input;
using SkiaSharp.Views.Maui.Controls;

namespace TP3RPG.Pages;

public partial class CarteJeu : ContentPage
{
    public PauseMenu OverlayMenuPublic => OverlayMenu;
    public Frame DialogueBox => dialogueBox;
    public SKCanvasView CanvasEnnemi => canvasEnnemi;
    private Carte _carte;
    private Controls _controls;
    private Joueur _joueur;
    private PNJ _pnj;
    private float tuileSize;
    private double minCote;
    public TaskCompletionSource<int> _choixReponses;
    public TaskCompletionSource<bool> _dialogueContinue;

    public ICommand ControlButtonClickedCommand { get; }

    public CarteJeu(int idCarte)
    {
#if ANDROID
        ControlButtonClickedCommand = new Command<string>(OnControlButtonClicked);
#endif

        InitializeComponent();
        BindingContext = this;
        _carte = CarteService.CreerCarte(idCarte);
        _joueur = _carte.Joueur;
        _pnj = _carte.PNJ;
        _controls = new Controls(_joueur, this);
        _controls.OnJoueurDéplacé += MettreAJourAffichageJoueur;
        canvasJoueur.PaintSurface += OnPaintSurfaceJoueur;
        canvasCarte.PaintSurface += OnPaintSurfaceCarte;
        canvasPNJ.InvalidateSurface();
        if (_carte.PNJ != null)
        {
            canvasPNJ.PaintSurface += OnPaintSurfacePNJ;
        }
        _carte.OnChangementCarte += ChangerCarte;

#if ANDROID
        ButtonUp.IsVisible = true;
        ButtonDown.IsVisible = true;
        ButtonLeft.IsVisible = true;
        ButtonRight.IsVisible = true;
        ButtonInteract.IsVisible = true;
        ButtonPause.IsVisible = true;
#endif
    }

    private void MettreAJourAffichageJoueur()
    {
        _joueur.X = _carte.Joueur.X;
        _joueur.Y = _carte.Joueur.Y;

        canvasJoueur.InvalidateSurface();
    }
    private void MettreAJourAffichageCarte()
    {
        canvasCarte.InvalidateSurface();
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
                    paint.Color = new SKColor(128, 128, 128);
                    paint.Style = SKPaintStyle.Fill;
                    paint.Shader = SKShader.CreateLinearGradient(
                        new SKPoint(0, 0),
                        new SKPoint(50, 50),
                        new SKColor[]
                        {
                            new SKColor(110, 110, 110),
                            new SKColor(150, 150, 150),
                            new SKColor(180, 180, 180)
                        },
                        null,
                        SKShaderTileMode.Clamp);
                    paint.MaskFilter = SKMaskFilter.CreateBlur(SKBlurStyle.Normal, 1);
                    break;
                case "Herbe":
                    var rand = new Random(tuile.X * 1000 + tuile.Y); // graine stable pour chaque position
                    byte r = (byte)rand.Next(0, 20);
                    byte g = (byte)(120 + rand.Next(30)); // vert variable
                    byte b = (byte)rand.Next(0, 20);
                    paint.Color = new SKColor(r, g, b);
                    paint.Style = SKPaintStyle.Fill;
                    
                    break;
                case "Barriere":
                    SKBitmap motifBarriere = new SKBitmap((int)tuileSize, (int)tuileSize);
                    using (var c = new SKCanvas(motifBarriere))
                    {
                        var fond = new SKPaint { Color = SKColors.SaddleBrown, Style = SKPaintStyle.Fill };
                        c.DrawRect(0, 0, tuileSize, tuileSize, fond);

                        var rainure = new SKPaint { Color = SKColors.Brown, Style = SKPaintStyle.Fill };
                        for (int i = 0; i < tuileSize; i += 6)
                        {
                            c.DrawRect(i, 0, 2, tuileSize, rainure); // lignes verticales = planches
                        }
                    }
                    paint.Shader = SKShader.CreateBitmap(motifBarriere, SKShaderTileMode.Repeat, SKShaderTileMode.Repeat);
                    break;
                case "Toit":
                    SKBitmap motifToit = new SKBitmap((int)tuileSize, (int)tuileSize);
                    using (var c = new SKCanvas(motifToit))
                    {
                        var fond = new SKPaint { Color = SKColors.DarkRed, Style = SKPaintStyle.Fill };
                        c.DrawRect(0, 0, tuileSize, tuileSize, fond);

                        var ligne = new SKPaint { Color = SKColors.Black, Style = SKPaintStyle.Fill };
                        for (int i = 0; i < tuileSize; i += 5)
                        {
                            c.DrawRect(0, i, tuileSize, 1, ligne); // Lignes horizontales
                        }
                    }
                    paint.Shader = SKShader.CreateBitmap(motifToit, SKShaderTileMode.Repeat, SKShaderTileMode.Repeat); break;
                case "PorteOuverte":
                    paint.Color = SKColors.Black;
                    break;
                case "Parquet":
                    SKBitmap motifParquet = new SKBitmap((int)tuileSize, (int)tuileSize);
                    using (var c = new SKCanvas(motifParquet))
                    {
                        var fond = new SKPaint { Color = SKColors.SaddleBrown, Style = SKPaintStyle.Fill };
                        c.DrawRect(0, 0, tuileSize, tuileSize, fond);

                        var veinure = new SKPaint { Color = new SKColor(101, 67, 33), Style = SKPaintStyle.Fill };

                        // Lignes horizontales irrégulières = fibres du bois
                        for (int i = 3; i < tuileSize; i += 5)
                        {
                            c.DrawRect(0, i, tuileSize, 1, veinure);
                        }

                        // Eventuellement quelques nœuds
                        veinure.Color = new SKColor(90, 50, 20, 120);
                        c.DrawCircle(tuileSize / 2, tuileSize / 2, 3, veinure);
                    }
                    paint.Shader = SKShader.CreateBitmap(motifParquet, SKShaderTileMode.Repeat, SKShaderTileMode.Repeat);

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

    private void OnPaintSurfacePNJ(object sender, SKPaintSurfaceEventArgs e)
    {
        if (_carte.PNJ != null)
        {
            var canvas = e.Surface.Canvas;
            canvas.Clear(SKColors.Transparent);
            float offsetX = (e.Info.Width - (Carte.TailleCarte * tuileSize)) / 2;
            float offsetY = (e.Info.Height - (Carte.TailleCarte * tuileSize)) / 2;
            SKPaint paintPNJ = new SKPaint { Color = SKColors.Purple };
            canvas.DrawCircle(_carte.PNJ.X * tuileSize + (tuileSize / 2) + offsetX, _carte.PNJ.Y * tuileSize + (tuileSize / 2) + offsetY, tuileSize / 2, paintPNJ);
        }
    }
    private void OnPaintSurfaceEnnemi(object sender, SKPaintSurfaceEventArgs e)
    {
        if (_carte.EnnemiVisible && _carte.Ennemi != null)
        {
            var canvas = e.Surface.Canvas;
            canvas.Clear(SKColors.Transparent);
            float offsetX = (e.Info.Width - (Carte.TailleCarte * tuileSize)) / 2;
            float offsetY = (e.Info.Height - (Carte.TailleCarte * tuileSize)) / 2;
            SKPaint paintEnnemi = new SKPaint { Color = SKColors.Red };
            canvas.DrawCircle(_carte.Ennemi.X * tuileSize + (tuileSize / 2) + offsetX, _carte.Ennemi.Y * tuileSize + (tuileSize / 2) + offsetY, tuileSize / 2, paintEnnemi);
        }
    }

    public void ChangerCarte(int numCarte)
    {
        _ = EffectuerChangementCarte(numCarte);
    }

    private async Task EffectuerChangementCarte(int numCarte)
    {
        await this.FadeTo(0, 300);
        MettreAJourAffichageCarte();
        if (_controls != null)
        {
            _controls.Dispose();
        }
        MainThread.BeginInvokeOnMainThread(() =>
        {
            GameManager.CarteActuelle =new CarteJeu(numCarte);
            App.Current.MainPage = GameManager.CarteActuelle;
        });

    }

    public async Task AfficherDialogue(string message)
    {
           
        if (lblDialogue == null || DialogueBox == null || _pnj == null)
            return;
        _dialogueContinue = new TaskCompletionSource<bool>();

        MainThread.BeginInvokeOnMainThread(async () =>
        {
            lblNomPNJ.Text = _pnj.Nom;
            lblDialogue.Text = message;
            DialogueBox.IsVisible = true;

            await Task.Delay(50);
            double boxWidth = DialogueBox.Width;

            int retries = 5;
            while (boxWidth <= 1 && retries > 0)
            {
                await Task.Delay(50);
                boxWidth = DialogueBox.Width;
                retries--;
            }

            DialogueBox.WidthRequest = Math.Max(5 * tuileSize, boxWidth);

            var (offsetX, offsetY) = CalculerOffset();
            double posX = (_pnj.X * tuileSize + (tuileSize / 2) + offsetX) - (boxWidth / 2);
            double posY = (_pnj.Y * tuileSize + offsetY) - (3 * tuileSize);

            DialogueBox.TranslationX = posX;
            DialogueBox.TranslationY = posY;

            await DialogueBox.FadeTo(1, 250);
        });

        await _dialogueContinue.Task;
        _dialogueContinue = null;
    }

    public async Task<int> AfficherDialogueAvecChoix(List<string> options)
    {
        _choixReponses = new TaskCompletionSource<int>();
        MainThread.BeginInvokeOnMainThread(async () =>
        {
            choiceButtons.Children.Clear();

            for (int i = 0; i < options.Count; i++)
            {
                int index = i;
                var bouton = new Button { Text = options[i] };

                bouton.Clicked += (s, e) =>
                {
                    choiceButtons.IsVisible = false;
                    _choixReponses.TrySetResult(index);
                };

                choiceButtons.Children.Add(bouton);
            }

            choiceButtons.IsVisible = true;
        });
        return await _choixReponses.Task;
    }

    public async Task FermerDialogue()
    {
        if (DialogueBox.IsVisible)
        {
            await DialogueBox.FadeTo(0, 200);
            MainThread.BeginInvokeOnMainThread(() =>
            {
                DialogueBox.IsVisible = false;
            });
        }
    }

    (double offsetX, double offsetY) CalculerOffset()
    {
        double canvasWidth = canvasCarte.Width;
        double canvasHeight = canvasCarte.Height;

        double offsetX = (canvasWidth - (Carte.TailleCarte * tuileSize)) / 2;
        double offsetY = (canvasHeight - (Carte.TailleCarte * tuileSize)) / 2;

        return (offsetX, offsetY);
    }

    public void AjouterMonstre()
    {
        if (GameManager.CarteActuelle is CarteJeu carteJeu)
        {
            _carte.EnnemiVisible = true;
            carteJeu.canvasEnnemi.InvalidateSurface();
        }
    }
}