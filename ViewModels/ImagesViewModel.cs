using System.IO;
using System.Windows.Input;
using Microsoft.Win32;
using VouwwandImages.Models.Products;
using VouwwandImages.Products.FoldingDoors;
using VouwwandImages.UI;

namespace VouwwandImages.ViewModels;

public class ImagesViewModel : ViewModel
{
    private Window _selectedWindow;

    public ImagesViewModel()
    {
        Drawings = new DrawingsViewModel();

        Windows = WindowTypes.Draaiingen;
    }

    public ICommand SaveImagesCommand
    {
        get { return new TargetCommand(SaveImages); }
    }

    private void SaveImages()
    {
        var sfd = new SaveFileDialog();

        if (sfd.ShowDialog() == true)
        {
            string folder = Path.GetDirectoryName(sfd.FileName);
            DrawingFactory factory = new DrawingFactory();
            foreach (Window window in Windows)
            {
                DrawingModel drawing = factory.CreateDrawing(window);
                drawing.SaveImage(Path.Combine(folder, $"{window.Uid}.png"));
            }
        }
    }

    public DrawingsViewModel Drawings { get; set; }

    public WindowCollection Windows { get; set; }

    public Window SelectedWindow
    {
        get { return _selectedWindow; }
        set
        {
            if (SetProperty(ref _selectedWindow, value))
            {
                NotifyPropertyChanged(nameof(SelectedDrawing));
            }
        }
    }

    public DrawingModel SelectedDrawing
    {
        get
        {
            DrawingFactory factory = new DrawingFactory();
            return factory.CreateDrawing(SelectedWindow);
        }
    }
}