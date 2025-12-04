namespace ExpiryChecker;

public partial class MainPage : ContentPage
{
  public MainPage()
  {
    InitializeComponent();
  }

  private void Button_Clicked(object sender, EventArgs e)
  {
    string? hint;
    string? result;
    var date = ExpiryDatePicker.Date.Value;
    var days = (date - DateTime.Today).TotalDays;

    switch (days)
    {
      case < 0:
        result = $"Hoppla! Dein Produkt ist bereits seit {Math.Abs(days)} Tagen " +
          $"über dem Zenit. Aber keine Sorge!";
        hint = "Manch ein Schätzchen schmeckt auch nach seinem offiziellen Ablaufdatum " +
          "noch prima. Vertrau auf deine Sinne!";
        break;
      case 0:
        result = "Heute ist der große Tag: Dein Produkt sagt Tschüss!";
        hint = "Perfekter Zeitpunkt für ein kulinarisches Highlight. Lass es dir " +
          "schmecken!";
        break;
      case 1:
        result = "Morgen ist es soweit: Dein Produkt verabschiedet sich!";
        hint = "Ideal, um morgen eine Leckerei zu zaubern. Was hast du im Sinn?";
        break;
      case < 7:
        result = "Achtung: Dein Produkt segelt in die Zielgerade und ist in weniger " +
          "als einer Woche fällig!";
        hint = "Ein guter Anlass, kreativ zu werden. Vielleicht probierst du ein neues " +
          "Rezept aus?";
        break;
      default:
        result = "Alles im grünen Bereich: Dein Produkt bleibt dir noch über eine Woche " +
          "treu.";
        hint = "Du hast noch jede Menge Zeit, also kein Grund zur Eile. Wie wär's mit " +
          "einem neuen Rezept?";
        break;
    }

    ExpiryLabel.Text = result;
    ExpiryHintLabel.Text = hint;
  }
}