using System.IO;
using System.Linq;
using System.Windows.Input;
using Microsoft.Win32;
using VouwwandImages.Factories;
using VouwwandImages.Models.ProductDrawings;
using VouwwandImages.UI;

namespace VouwwandImages.ViewModels;

public class ImagesViewModel : ViewModel
{
    private Frame? _selectedFrame;
    private Frames? _selectedFrames;

    public ImagesViewModel()
    {
        Drawings = new DrawingsViewModel();

        FramesCollection = WindowTypes.GetFrame();

        SelectedFrames = FramesCollection.LastOrDefault();
        SelectedFrame = SelectedFrames?.FrameItems.LastOrDefault();
    }

    public ICommand SaveImagesCommand
    {
        get { return new TargetCommand(SaveImages); }
    }

    private void SaveImages()
    {
        if (SelectedFrames == null)
        {
            return;
        }

        var sfd = new SaveFileDialog();

        if (sfd.ShowDialog() == true)
        {
            string folder = Path.GetDirectoryName(sfd.FileName);
            DrawingsFactory factory = new DrawingsFactory();
            foreach (Frame frame in SelectedFrames.FrameItems)
            {
                DrawingModel drawing = factory.CreateDrawing(frame);
                drawing.SaveImage(Path.Combine(folder, $"{frame.Uid}.png"));
            }
        }
    }

    public ICommand SaveAllImagesCommand
    {
        get { return new TargetCommand(SaveAllImages); }
    }

    private void SaveAllImages()
    {
        foreach (var frames in FramesCollection.Where(f => !string.IsNullOrEmpty(f.Directory)))
        {
            DrawingsFactory factory = new DrawingsFactory();
            foreach (Frame frame in frames.FrameItems)
            {
                DrawingModel drawing = factory.CreateDrawing(frame);
                drawing.SaveImage(Path.Combine(frames.Directory, $"{frame.Uid}.png"));
            }
        }
    }

    public DrawingsViewModel Drawings { get; set; }

    public FramesCollection FramesCollection { get; set; } = new();

    public Frames? SelectedFrames
    {
        get { return _selectedFrames; }
        set
        {
            SetProperty(ref _selectedFrames, value);
        }
    }

    public Frame? SelectedFrame
    {
        get { return _selectedFrame; }
        set
        {
            if (SetProperty(ref _selectedFrame, value))
            {
                NotifyPropertyChanged(nameof(SelectedDrawing));
            }
        }
    }

    public DrawingModel SelectedDrawing
    {
        get
        {
            DrawingsFactory factory = new DrawingsFactory();
            return factory.CreateDrawing(SelectedFrame);
        }
    }
}