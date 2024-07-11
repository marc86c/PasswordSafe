# PasswordSafe
Ein Passwortmanager als Web-App, welche den User erlaubt sich zu registrieren und einzuloggen. Auf der Web-App kann dieser User seine Passwörter eintragen und Sicher verwalten.

## Clonen und Starten des Projektes
Als Umgebung empfehlen wir Visual Studio 2022.

Nach dem das Repo gecloned wurde, muss man die PasswordSafe.sln öffnen. Im Visual Studio Rechtsklick auf das aller erste Item:

![image](https://github.com/marc86c/PasswordSafe/assets/108449981/8380e684-acba-4e84-bc2f-522dcd2165ec)

Dann auf Properties:

![image](https://github.com/marc86c/PasswordSafe/assets/108449981/e4209b96-a2b1-4dbb-834a-49323e597f39)

Und folgende Einstellungen übernehmen:

![image](https://github.com/marc86c/PasswordSafe/assets/108449981/33a872ee-1f3c-4f23-aeb1-f38615d564ae)

Fenster wieder schliessen und Startbutton drücken:

![image](https://github.com/marc86c/PasswordSafe/assets/108449981/54608220-1380-40dc-9120-513a0bf3a70b)

# Projekt M183
## Reflexion und Zwischenabgaben

### Stand 16.05.2024
Projekt erstellt, erste Gedangen gemacht und das Login/Registrierung eingebaut. Das Projekt ist uns eigentlich gut gelungen, das programmieren hat flüssig funktioniert. Es war aber recht schwierig, die Security-Risiken im Auge zu behalten. Wir denken aber, dass uns das trotzdem gelungen ist. Momentan haben wir eigentlich alle Funktionen eingebaut inkl. Login, das Styling haben wir noch fast nicht erledigt, dies wäre nun unser nächster Schritt. Uns geht es gut.

### Stand 23.05.2024
Projekt erweitert. Fehlende Funktionalität implementiert, Security verbessert und falsche Funktionalität gefixt.


### Stand 06.06.2024
Projekt erweitert. Fehlende Funktionalität implementiert, Styling begonnen.

### Stand 13.06.2024:
Das Styling und fehlende Sicherheitsfunktionen wurden eingebaut. Das .gitignore macht uns noch "Probleme", es ignoriert nicht alles was wir eingetragen haben. 

### Reflexion
Wir haben fast ausschliesslich in der Schule daran gearbeitet und zeitlich war es auch kein Problem. Die Kommunikaton war gut, aber hätte auch besser sein können.
Beim nächsten mal würden wir wahrscheinlich das .gitignore zu beginn einfügen und die Aufgaben gleich zu beginn schneller und effektiver aufteilen. Am besten würden wir bei nächsten Projekt, eine Sprache, Umgebung usw benutzen, mit welcher alle Teammitglieder bereits vertraut sind, denn nun hat es viele Erklärungen gebraucht.

## Grundkonzept

### Technology
Als Technology haben wir Blazor und Asp.net API benutzt.

### Projektstruktur
Unser Repo besteht aus 3 Projekten, PasswordSafeUI, dies ist unser Frontend. Das PasswordSafe ist unsere API + Datenbank. Als letztes haben wir noch das PasswordSafeCommon Projekt, hier handelt es sich um ein Projekt, in welchem Objekte sind, welche im UI sowie in der API gebrauch werden, mit diesem Vorgehen haben wir keine Abhängigkeiten von der API zum UI und umgekehrt.


### Abläufe

#### Registrierung:
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

#### Login:
Beim Login wird eine Request an die API gesendet, das Passwort zusammen mit dem Salt gehashed mit dem bereits vorhandenen gehashed Passwort verglichen. Falls dies nicht der Fall ist, returnt die API eine Fehlermeldung. Sonst sendet die API einen SessionToken zurück, welcher im UI temopär gespeichert wird (in dieser Session). Dieser Token wird bei jedem nächsten Request ans API mitgegeben und dort Validiert. Dies verhindert, dass User über die URL auf die Daten von anderen Usern gelangt. Das heisst jegliche Seiten ausser Login und Register sind gelockt, bis der User eingeloggt ist. 

#### Logout:
Beim Logout wird der SessionToken im UI gelöscht und somit werden jegliche Seiten ausser Register und Login "gelockt".

#### Daten
Wenn man die Daten erstellt, werden diese zusammen mit dem SessionToken an die API gesendet. Wenn der SessionToken gültig ist, wird der das Passwort verschlüsselt und gespeichert.
Beim holen der Daten, werden nach validierung des SessionTokens die Passwörter entschlüsselt und zurück gegeben. 

### Weitere Security-Funktionen

#### Anzeigen des Passwortes
Um ungewollte Zuschauer nicht direkt alle Passwörter zu präsentieren, werden die Passwörter zuerst mit "****" im UI angezeigt. Erst beim klicken auf "Öffnen" sieht man das plaintext Passwort. Man kann auch nur ein Passwort gleichzeitig in Plaintext anzeigen, beim öffnen eines anderes Passwortes, wird das vorher geöffnete Passwort geschlossen.

#### XSS muss nicht implementiert werden denn:

'By default, Razor HTML encodes all strings that it is asked to render. This mitigates against XSS attacks. You have to take steps to bypass this protection to render the string as raw HTML by casting to MarkupString in Blazor or using HTML.Raw() in Razor Pages/MVC. At that point, you should take responsibility for any sanitising that your application requires.'

Quelle https://stackoverflow.com/questions/75236876/blazor-with-signalr-how-is-an-xss-or-other-attack-possible-when-storing-and-re

Falls andere Frontend-Technologien benutzt werden, müsste man die Daten unbedingt nochmals validieren.


#### SQL-Injection
Um SQL-Injection mussten wir uns keine Gedanken machen, da wir keine Datenbank haben, nur JSON-Files ;)

### Endpoints

#### api/auth/register POST
Dieser Endpoint ist für das Registieren zuständig.

Erwartet einen Username und ein Passwort.
Liefert einen Boolean.

#### api/auth/login POST
Dieser Endpoint ist für das Login zuständig.

Erwartet einen Username und ein Passwort.
Liefert einen SessionToken.

#### api/User/User/Data PUT
Anhand der Parameter wird überprüft, ob der User berechtigt ist, die Daten zu updaten.
Kann einen User bearbeiten z.B. um einen Authentifizierungs-Datensatz hinzuzufügen.

Erwartet einen User und einen SessionToken.
Liefert die User Daten, ähnlich wie beim nächsten Endpoint.

#### api/User/User/{Username}/Data GET
Anhand der Parameter wird überprüft, ob der User berechtigt ist, die Daten zu holen.

Erwartet den Username und den Sessionkey.
Liefert die Daten des Users.

# Projekt M323

Dieses Projekt basiert auf dem im M183 erstellte Projekt "PasswordSafe". Hier wurde der PasswordSafe mit weitern nützlichen Features ausgestatten:
* Kategoriesierung der Daten
   - Somit kann man nach z.B. SecurityDaten für die Accounts von sozialen Medien filtern, ohne jeden einzelnen Eintrag zu suchen.
* Suche
  - Mit der Hilfe der Suche kann man Eintrage nach dem Namen des Providers oder des Usernames suchen, somit muss man nicht durch alle Seiten gehen, um den richtigen Eintrag zu finden.
* Sortierung
   - Mit Hilfe der Sortierung kann man sowohl die Eintrage nach Provider, aber auch dem Username Sortieren.
* Pagination
   - Durch die Pagination bleibt die Seite übersichtlich, man hat nun keine endlichlos lange Liste mehr, sondern immer schön 10 Einträge pro Seite.

## Suche
Für die Suche haben wir ein FluentSearch Komponent benutzt, dieser löst nach Eingabe (mit Debounce) diese filterung aus:

```
public IQueryable<AuthenticationData> AuthenticationDatas => !string.IsNullOrEmpty(filterCriteria) ? 
    user.AuthenticationDatas.Where(x => x.Username.ToLower().Contains(filterCriteria) || x.Provider.ToLower().Contains(filterCriteria)).AsQueryable(): 
    user.AuthenticationDatas.AsQueryable();
```

Bei diesem Code filtern wir mit Hilfe von Linq-Queries (.Where()) nach den Authenifizierungsdaten, welche entweder den gesuchten Text im Username oder im Provider enthält.

## FluentDataGrid
Für die Sortierung und die Pagination haben wir das FluentDataGrid und den FluentPaginator benutzt. Diese zwei Komponenten sind in der Library (NuGet) "FluentUI" verfügbar. Durch das Einsetzen dieser Komponenten, wird das Filtern und die Pagination sehr vereinfacht und ist daher eine sehr gute Möglichkeit, an eigener Logik zu sparen.

So sieht unser Code aus:
```<FluentDataGrid TGridItem="AuthenticationData" Items="AuthenticationDatas" Pagination="State" Style="overflow: auto">
    <PropertyColumn Property="@(x => x.Provider)" Title="Provider" Sortable="true"></PropertyColumn>
    <PropertyColumn Property="@(x => x.Username)" Title="Username" Sortable="true"></PropertyColumn>
    <TemplateColumn Title="Type" Context="context">
        <FluentCombobox Style="overflow: auto; max-width: 100%" TOption="AuthenticationDataType" Items="Types" @bind-SelectedOption="context.Type" Placeholder="type"></FluentCombobox>
    </TemplateColumn>
    <TemplateColumn Context="context" Title="Password">
        @if (openPasswordIndex.HasValue && openPasswordIndex.Value == user.AuthenticationDatas.IndexOf(context))
        {
            <input readonly @bind-value="@context.Password" class="input-field"></input>
        }
        else
        {
            <input readonly placeholder="*****" class="input-field" />
        }
    </TemplateColumn>
    <TemplateColumn Context="context" Title="Password anzeigen">
        @if (openPasswordIndex.HasValue && openPasswordIndex.Value == user.AuthenticationDatas.IndexOf(context))
        {
            <button @onclick="@( () => OpenPassword(null))" class="toggle-button">Schließen</button>
        }
        else
        {
            <button @onclick="@(() => OpenPassword(user.AuthenticationDatas.IndexOf(context)))" class="toggle-button">Öffnen</button>
        }
    </TemplateColumn>
    <TemplateColumn Title="Delete">
        <button @onclick="@(async () => await DeleteData(user.AuthenticationDatas.IndexOf(context)))" class="action-button">X</button>
    </TemplateColumn>
</FluentDataGrid>
<FluentPaginator State="State"></FluentPaginator>
```

Dank diesem DataGrid mussten wir für die Funktionen keine eigene Funktionale Programmierung anwenden. Jedoch beschreiben wir hier den Code, welcher vermutlich dahinter steckt:
Sortierung:
Bei der Sortierung wird wahrschneinlich das OrderBy benutzt, z.B:
```
//Sortieren nach dem Username
var orderedAuthenticationDatas = user.AuthenticationDatas.OrderBy(x => x.Username);
```
Hier geben wir das Property mit, nach welchem gefiltert wird.

Bei unserer App kann man auch nach einem weiteren Property filtern:
```
//Sortieren nach dem Provider
var orderedAuthenticationDatas = user.AuthenticationDatas.OrderBy(x => x.Provider);
```

Für die Pagination haben wir die FluentPaginator Komponente benutzt. Bei diesem Komponenten steck wahrschneinlich etwas ähnliches wie folgender Code dahinter:
 ```
//Beispiel:
//5 Einträge pro Seite
//Gerade befinden wir uns auf Seite 2

const ItemsPerPage = 5;
var currentPage = 2;

var pagedAuthenticationDatas = user.AuthenticationDatas.Skip(ItemsPerPage * (currentPage-1)).Take(ItemsPerPage);
```

Bei diesem Code wird die Methode .Skip() benutzt, die Methode erwartet eine Zahl als Parameter. Wie der Methodenname schon aussagent, "Skipped" es die ersten x Einträge der List.
Als nächstes wird die Methode .Take() benutzt, diese erwartet ebenfalls eine Zahl als Parameter. Wie auch der Methodenname aussagt, nimmt man die nächsten x Einträge der List. In unserem Code währe das Eintrag 6-10; 

## Kategorien
Für die Kategorien haben wir ein enum definiert:

```
public enum AuthenticationDataType
{
   Private = 1,
   School = 2,
   Work = 3,
   Finances = 4,
   SocialMedia = 5,
   Health = 6,
   Entertainment = 7,
   Other = 8
}
```

Hier haben wir uns verschiedene Kategorien überlegt, welche der User auswählen kann, um seine Daten zu kategorisieren.
