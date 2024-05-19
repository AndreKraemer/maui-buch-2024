using Microsoft.Maui.Graphics.Platform;
using Microsoft.Maui.Handlers;

namespace CustomControlsDemo.Controls;

public class CircularProgressBarHandler : ViewHandler<ProgressBar, PlatformGraphicsView>
{

  public CircularProgressBarHandler() : base(Mapper, CommandMapper)
  {
  }

  public CircularProgressBarHandler(IPropertyMapper? mapper)
    : base(mapper ?? Mapper, CommandMapper)
  {
  }

  public CircularProgressBarHandler(IPropertyMapper? mapper, CommandMapper? commandMapper)
    : base(mapper ?? Mapper, commandMapper ?? CommandMapper)
  {
  }

  public static IPropertyMapper<ProgressBar, IViewHandler> Mapper = new PropertyMapper<ProgressBar, CircularProgressBarHandler>(ViewMapper)
  {
    [nameof(IProgress.Progress)] = MapProgress,
    [nameof(IProgress.ProgressColor)] = MapProgressColor
  };

  public static CommandMapper<ProgressBar, IProgressBarHandler> CommandMapper = new(ElementCommandMapper)
  {
  };

  protected override PlatformGraphicsView CreatePlatformView()
  {
    var platformGraphicsView = new PlatformGraphicsView
    {
      Drawable = new CircularProgressBarDrawable(VirtualView)
    };
    return platformGraphicsView;
  }

  public static void MapProgress(CircularProgressBarHandler handler, ProgressBar progressBar)
  {
    handler.PlatformView.Invalidate();
  }

  public static void MapProgressColor(CircularProgressBarHandler handler, ProgressBar progressBar)
  {
    handler.PlatformView.Invalidate();
  }
}
