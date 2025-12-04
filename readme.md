# Codebeispiele zum Buch "Cross-Plattform-Apps mit .NET MAUI entwickeln (2. Auflage)"
In diesem Repository finden Sie die Codebeispiele zu meinem Buch 
"[Cross-Plattform-Apps mit .NET MAUI entwickeln](https://www.hanser-fachbuch.de/fachbuch/artikel/9783446479814)" (ISBN: 
978-3-446-47981-4), erschienen im November 2024 beim Carl Hanser Verlag.

![Buchcover: Cross-Plattform-Apps mit .NET MAUI entwickeln](./images/maui-buch.jpg)

Dieses Buch verfolgt zwei Strategien für die Codebeispiele. In jedem Kapitel-Ordner gibt es Unterordner, die auf dem Wort Demo enden. In diesen Unterordnern finden Sie die Beispielcodes zu den Listings der entsprechenden Kapitel.

Ab Kapitel 8 gibt es außerdem in den meisten Kapitel-Ordnern den Unterordner `DontLetMeExpire`. Dabei handelt es sich um den Quellcode der Beispiel-App, die wir Schritt für Schritt in diesem Buch entwickeln werden. Der Quellcode der Beispiel-App in den jeweiligen Kapitelordnern entspricht dem Fortschritt zum Ende des Kapitels.

Inhalt der Beispiel-App ist die Umsetzung einer App zur Verwaltung von Lebensmittelvorräten

![Screenshot der Beispiel-App Dont Let Me Expire Android, iOS und Windows](./images/dont-let-me-expire-sample-app.jpg)

## Beispiele für .NET 8 und .NET 9, 10

Die Beispiele für dieses Buch wurden ursprünglich für .NET 8 geschrieben. Kurz nach der Veröffentlichung des Buchs kam .NET 9 heraus. Da .NET 9 einige Verbesserungen für .NET MAUI gebracht hat, wurden alle Beispiele auf .NET 9 aktualisieren. Mittlerweile erfolgte eine Aktualisierung auf .NET 10. Hier besteht aktuell jedoch noch ein Problem bei Kapitel 20 mit dem Scannen von Barcodes, da die ursprünglich verwendete Bibliothek unter .NET 10 nicht funktioniert. Um die Beispiele ausführen zu können, benötigen Sie Visual Studio 2026, Update 18.0.2 oder höher. Die ursprünglichen .NET 8 Beispiele befinden sich im Branch `net8`, die Beipsiele für .NET 9 im Branch `net9` .

## Fehler, Korrekturen und Kontakt.
Korrekturen zu fachlichen Fehlern im Buch sowie Rechtschreibkorrekturen finden Sie auf der Webseite zu diesem Buch, die Sie unter https://www.andrekraemer.de/maui-buch erreichen.

Eventuelle Fehler in den Codebeispielen werde ich kontinuierlich auf diesem GitHub-Repository korrigieren. 
Wenn Sie in den Beispielen einen Fehler finden sollten, der noch nicht korrigiert ist, dann lade ich Sie herzlich dazu ein, einen Issue über das GitHub-Repository anzulegen.

Scheuen Sie sich nicht, mich bei Fragen oder Anmerkungen zum Buch direkt per E-Mail unter andre@andrekraemer.de oder auf Twitter unter https://twitter.com/codemurai anzuschreiben.
Bitte haben Sie aber Verständnis dafür, dass ich unter der E-Mail-Adresse nur Fragen, die sich direkt auf den Inhalt des Buchs beziehen, beantworten kann.
Darüber hinausgehende Fragen kläre ich jedoch gerne in Schulungen und Workshops, die Sie auch unter dieser E-Mail-Adresse anfragen können.

## Links

- [Kostenlose Leseprobe herunterladen](https://www.bic-media.com/widget/?isbn=9783446480643&showExtraDownloadButton=yes&buyButton=no)
- [Buch bei Hanser-Fachbuch bestellen](https://www.hanser-fachbuch.de/fachbuch/artikel/9783446479814)
- [Buch bei Amazon.de bestellen](https://www.amazon.de/Cross-Plattform-Apps-NET-MAUI-entwickeln-programmieren/dp/3446479813/)
- [Webseite des Autors zum Buch](https://www.andrekraemer.de/maui-buch)
- [Schulung zum Buch](https://www.andrekraemer.de/training/app-entwicklung/cross-plattform-apps-mit-net-maui-entwickeln/)
- [Projektunterstützung anfragen](https://qualitybytes.de/services/mobile-apps/)

## Troubleshooting

### Fehler: Could not find android.jar for API level
Falls Sie beim Kompilieren der Beispiele folgenden Fehler erhalten, fehlt bei Ihnen das entsprechende Android-SDK:

```
Could not find android.jar for API level 34. This means the Android SDK platform for API level 34 is not installed. Either install it in the Android SDK Manager (Tools > Android > Android SDK Manager...), or change the Xamarin.Android project to target an API version that is installed. (C:\Program Files (x86)\Android\android-sdk\platforms\android-34\android.jar missing.)
```

Zur Fehlerbehebung müssen Sie lediglich das fehlende Android-SDK nachinstallieren. Öffnen Sie dazu in Visual Studio den *Android-SDK-Manager* im Menü *Extras | Android*