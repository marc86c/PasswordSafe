# PasswordSafe
Ein Passwortmanager als Web-App, welche den User erlaubt sich zu registrieren und einzuloggen. Auf der Web-App kann dieser User seine Passwörter eintragen und Sicher verwalten.

# Clonen und Starten des Projektes
Als Umgebung empfehlen wir Visual Studio 2022.

Nach dem das Repo gecloned wurde, muss man die PasswordSafe.sln öffnen. Im Visual Studio Rechtsklick auf das aller erste Item:

![image](https://github.com/marc86c/PasswordSafe/assets/108449981/8380e684-acba-4e84-bc2f-522dcd2165ec)

Dann auf Properties:

![image](https://github.com/marc86c/PasswordSafe/assets/108449981/e4209b96-a2b1-4dbb-834a-49323e597f39)

Und folgende Einstellungen übernehmen:

![image](https://github.com/marc86c/PasswordSafe/assets/108449981/33a872ee-1f3c-4f23-aeb1-f38615d564ae)

Fenster wieder schliessen und Startbutton drücken:

![image](https://github.com/marc86c/PasswordSafe/assets/108449981/54608220-1380-40dc-9120-513a0bf3a70b)


# Reflexion und Zwischenabgaben

## Stand 16.05.2024
Projekt erstellt, erste Gedangen gemacht und das Login/Registrierung eingebaut. Das Projekt ist uns eigentlich gut gelungen, das programmieren hat flüssig funktioniert. Es war aber recht schwierig, die Security-Risiken im Auge zu behalten. Wir denken aber, dass uns das trotzdem gelungen ist. Momentan haben wir eigentlich alle Funktionen eingebaut inkl. Login, das Styling haben wir noch fast nicht erledigt, dies wäre nun unser nächster Schritt. Uns geht es gut.

## Stand 23.05.2024
Projekt erweitert. Fehlende Funktionalität implementiert, Security verbessert und falsche Funktionalität gefixt.


## Stand 06.06.2024
Projekt erweitert. Fehlende Funktionalität implementiert, Styling begonnen.

## Stand 13.06.2024:
Das Styling und fehlende Sicherheitsfunktionen wurden eingebaut. Das .gitignore macht uns noch "Probleme", es ignoriert nicht alles was wir eingetragen haben. 
Ansonsten ist und das Projekt gut gelungen. Wir haben fast ausschliesslich in der Schule daran gearbeitet und zeitlich war es auch kein Problem. Die Kommunikaton war gut, aber hätte auch besser sein können.

Beim nächsten mal würden wir wahrscheinlich das .gitignore zu beginn einfügen und die Aufgaben gleich zu beginn schneller und effektiver aufteilen.

# Grundkonzept

## Technology
Als Technology haben wir Blazor und Asp.net API benutzt.

## Projektstruktur
Unser Repo besteht aus 3 Projekten, PasswordSafeUI, dies ist unser Frontend. Das PasswordSafe ist unsere API + Datenbank. Als letztes haben wir noch das PasswordSafeCommon Projekt, hier handelt es sich um ein Projekt, in welchem Objekte sind, welche im UI sowie in der API gebrauch werden, mit diesem Vorgehen haben wir keine Abhängigkeiten von der API zum UI und umgekehrt.


## Abläufe

### Registrierung:
Bei der Registrierung wird zuerst die Komplexität des Passwortes überprüft:
```
   if (passwd.Length < 8 )
       return "Das Passwort muss mindestens 8 Zeichen lang sein";
   if (!passwd.Any(char.IsUpper))
       return "Das Passwort muss ein Grossbuchstaben enthalten";
   if (!passwd.Any(char.IsLower))
       return "Das Passwort muss ein Kleinbuchstaben enthalten";
   if (passwd.Contains(" "))
       return "Leerzeichen sind nicht erlaubt";
   string specialCh = @"%!@#$%^&*()?/>.<,:;'\|}]{[_~`+=-" + "\"";
   char[] specialChArr = specialCh.ToCharArray();
   foreach (char ch in specialChArr)
   {
       if (passwd.Contains(ch))
           return "Keine Spezialzeichen erlaubt";
   }
```

 Danach wird eine Request an die API geschickt, das Passwort zusammen mit dem Salt gehashed und im JSON-File gespeichert.

### Login:
Beim Login wird eine Request an die API gesendet, das Passwort zusammen mit dem Salt gehashed mit dem bereits vorhandenen gehashed Passwort verglichen. Falls dies nicht der Fall ist, returnt die API eine Fehlermeldung. Sonst sendet die API einen SessionToken zurück, welcher im UI temopär gespeichert wird (in dieser Session). Dieser Token wird bei jedem nächsten Request ans API mitgegeben und dort Validiert. Dies verhindert, dass User über die URL auf die Daten von anderen Usern gelangt.

### Logout:
Beim Logout wird der SessionToken im UI gelöscht.

### Daten
Wenn man die Daten erstellt, werden diese zusammen mit dem SessionToken an die API gesendet. Wenn der SessionToken gültig ist, wird der das Passwort verschlüsselt und gespeichert.
Beim holen der Daten, werden nach validierung des SessionTokens die Passwörter entschlüsselt und zurück gegeben, diese werden zuerst mit "****" im UI angezeigt. Erst beim klicken auf "Öffnen" sieht an das plaintext Passwort. Man kann auch nur ein Passwort gleichzeitig in Plaintext anzeigen.

## Weitere Security-Funktionen
XSS muss nicht implementiert werden denn:
By default, Razor HTML encodes all strings that it is asked to render. This mitigates against XSS attacks. You have to take steps to bypass this protection to render the string as raw HTML by casting to MarkupString in Blazor or using HTML.Raw() in Razor Pages/MVC. At that point, you should take responsibility for any sanitising that your application requires.
https://stackoverflow.com/questions/75236876/blazor-with-signalr-how-is-an-xss-or-other-attack-possible-when-storing-and-re

Falls andere Frontend-Technologien benutzt werden, müsste man die Daten unbedingt nochmals validieren.

## Endpoints

### api/auth/register POST
Dieser Endpoint ist für das Registieren zuständig.

Erwartet einen Username und ein Passwort.
Liefert einen Boolean.

### api/auth/login POST
Dieser Endpoint ist für das Login zuständig.

Erwartet einen Username und ein Passwort.
Liefert einen SessionToken.

### api/User/User/Data PUT
Kann einen User bearbeiten z.B. um einen Authentifizierungsatz hinzuzufügen.

Erwartet einen User und einen SessionToken.

### api/User/User/{Username}/Data
Liefert die Daten des Users.
