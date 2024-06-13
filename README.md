Ein Passwortmanager als Web-App, welche den User erlaubt sich zu registrieren und einzuloggen. Auf der Web-App kann dieser User seine Passwörter eintragen und verwalten.

Reflexion

Das Projekt ist uns eigentlich gut gelungen, das programmieren hat flüssig funktioniert. Es war aber recht schwierig, die Security-Risiken im Auge zu behalten. Wir denken aber, dass uns das trotzdem gelungen ist. Momentan haben wir eigentlich alle Funktionen eingebaut inkl. Login, das Styling haben wir noch fast nicht erledigt, dies wäre nun unser nächster Schritt. Uns geht es gut.

XSS muss nicht implementiert werden denn:
By default, Razor HTML encodes all strings that it is asked to render. This mitigates against XSS attacks. You have to take steps to bypass this protection to render the string as raw HTML by casting to MarkupString in Blazor or using HTML.Raw() in Razor Pages/MVC. At that point, you should take responsibility for any sanitising that your application requires.
https://stackoverflow.com/questions/75236876/blazor-with-signalr-how-is-an-xss-or-other-attack-possible-when-storing-and-re
