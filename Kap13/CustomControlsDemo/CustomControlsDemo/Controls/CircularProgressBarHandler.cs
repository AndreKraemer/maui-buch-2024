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
    PlatformGraphicsView platformGraphicsView;

#if ANDROID
    platformGraphicsView = new PlatformGraphicsView(Context);
#else
  platformGraphicsView = new PlatformGraphicsView();
#endif

    platformGraphicsView.Drawable = new CircularProgressBarDrawable();

    return platformGraphicsView;
  }

  public static void MapProgress(CircularProgressBarHandler handler, ProgressBar progressBar)
  {
    ((CircularProgressBarDrawable)handler.PlatformView.Drawable).Progress = progressBar.Progress;
#if IOS || MACCATALYST
    handler.PlatformView.InvalidateDrawable();
#else
    handler.PlatformView.Invalidate();
#endif
  }

  public static void MapProgressColor(CircularProgressBarHandler handler, ProgressBar progressBar)
  {
    ((CircularProgressBarDrawable)handler.PlatformView.Drawable).ProgressColor = progressBar.ProgressColor;
#if IOS || MACCATALYST
    handler.PlatformView.InvalidateDrawable();
#else
    handler.PlatformView.Invalidate();
#endif
  }
}
