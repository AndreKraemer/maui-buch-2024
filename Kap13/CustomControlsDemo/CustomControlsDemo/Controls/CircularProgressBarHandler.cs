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

  public static IPropertyMapper<ProgressBar, CircularProgressBarHandler> Mapper = new PropertyMapper<ProgressBar, CircularProgressBarHandler>(ViewMapper)
  {
    [nameof(IProgress.Progress)] = MapProgress,
    [nameof(IProgress.ProgressColor)] = MapProgressColor
  };

  public static CommandMapper<ProgressBar, IProgressBarHandler> CommandMapper = new(ElementCommandMapper)
  {
  };

  public CircularProgressBarDrawable CircularProgressBarDrawable { get; private set; } = new CircularProgressBarDrawable();

  protected override PlatformGraphicsView CreatePlatformView()
  {
    PlatformGraphicsView platformGraphicsView;

#if ANDROID
    platformGraphicsView = new PlatformGraphicsView(Context);
#else
    platformGraphicsView = new PlatformGraphicsView();
#endif

    platformGraphicsView.Drawable = CircularProgressBarDrawable;

    return platformGraphicsView;
  }

  public static void MapProgress(CircularProgressBarHandler handler, ProgressBar progressBar)
  {
    handler.CircularProgressBarDrawable.Progress = progressBar.Progress;
    handler.Invalidate();
  }

  public static void MapProgressColor(CircularProgressBarHandler handler, ProgressBar progressBar)
  {
    handler.CircularProgressBarDrawable.ProgressColor = progressBar.ProgressColor;
    handler.Invalidate();
  }

  private void Invalidate()
  {
#if IOS || MACCATALYST
    PlatformView.InvalidateDrawable();
#else
    PlatformView.Invalidate();
#endif
  }
}
