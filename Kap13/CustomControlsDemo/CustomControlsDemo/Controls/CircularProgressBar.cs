#nullable disable
namespace CustomControlsDemo.Controls;

public class CircularProgressBar : GraphicsView, IProgress
{
  /// <summary>Bindable property for <see cref="ProgressColor"/>.</summary>
  public static readonly BindableProperty ProgressColorProperty =
    BindableProperty.Create(nameof(ProgressColor), typeof(Color),
                            typeof(ProgressBar), null);

  /// <summary>Bindable property for <see cref="Progress"/>.</summary>
  public static readonly BindableProperty ProgressProperty =
    BindableProperty.Create(nameof(Progress), typeof(double), typeof(ProgressBar),
                            0d, coerceValue: (bo, v) => double.Clamp((double)v, 0, 1));


  public CircularProgressBar()
  {
    Drawable = new CircularProgressBarDrawable(this);
  }

  public Color ProgressColor
  {
    get { return (Color)GetValue(ProgressColorProperty); }
    set { SetValue(ProgressColorProperty, value); Invalidate(); }
  }

  public double Progress
  {
    get { return (double)GetValue(ProgressProperty); }
    set { SetValue(ProgressProperty, value); Invalidate(); }
  }

}
