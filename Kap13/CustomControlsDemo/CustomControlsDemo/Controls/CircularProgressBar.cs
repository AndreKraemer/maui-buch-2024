#nullable disable


namespace CustomControlsDemo.Controls;

public class CircularProgressBar : GraphicsView, IProgress
{

  private CircularProgressBarDrawable CircularProgressBarDrawable { get; set; }

  public CircularProgressBar()
  {
    Drawable = CircularProgressBarDrawable = new CircularProgressBarDrawable();
  }


  /// <summary>Bindable property for <see cref="ProgressColor"/>.</summary>
  public static readonly BindableProperty ProgressColorProperty =
    BindableProperty.Create(nameof(ProgressColor), typeof(Color),
                            typeof(ProgressBar), null, propertyChanged: ProgressColorChanged);


  private static void ProgressColorChanged(BindableObject bindable, object oldValue, object newValue)
  {
    if (bindable is CircularProgressBar circularProgressBar &&
        circularProgressBar.CircularProgressBarDrawable != null)
    {
      circularProgressBar.CircularProgressBarDrawable.ProgressColor = circularProgressBar.ProgressColor;
      circularProgressBar.Invalidate();
    }
  }

  public Color ProgressColor
  {
    get { return (Color)GetValue(ProgressColorProperty); }
    set { SetValue(ProgressColorProperty, value); }
  }

  /// <summary>Bindable property for <see cref="Progress"/>.</summary>
  public static readonly BindableProperty ProgressProperty =
    BindableProperty.Create(nameof(Progress), typeof(double), typeof(ProgressBar),
                            0d, coerceValue: (bo, v) => double.Clamp((double)v, 0, 1), propertyChanged: ProgressChanged);


  private static void ProgressChanged(BindableObject bindable, object oldValue, object newValue)
  {
    if (bindable is CircularProgressBar circularProgressBar && 
        circularProgressBar.CircularProgressBarDrawable != null)
    {
      circularProgressBar.CircularProgressBarDrawable.Progress = circularProgressBar.Progress;
      circularProgressBar.Invalidate();
    }
  }
  
  public double Progress
  {
    get { return (double)GetValue(ProgressProperty); }
    set { SetValue(ProgressProperty, value); }
  }

}
