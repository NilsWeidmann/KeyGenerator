[← 5. Startbildschirm](05_startbildschirm.md) | [Inhaltsverzeichnis](README.md) | [7. Sonstige Funktionen →](07_sonstige_funktionen.md)

---

# 6. Schlüsselzahlen generieren

Nachdem alle Daten eingegeben und alle Wünsche der Vereine berücksichtigt worden sind, kann die eigentliche Generierung der Schlüsselzahlen beginnen.

1. Klicken Sie im Startbildschirm auf den Button **Generieren**.
2. Es erscheint das Fenster "Bitte Warten" mit einem Fortschrittsbalken und der verbleibenden Zeit.
3. Vor der Generierung wird automatisch eine Sicherheitskopie des aktuellen Standes angelegt, die Sie bei Bedarf später wiederherstellen können (siehe [7.1 Backup laden](07_sonstige_funktionen.md#71-backup-laden)).
4. Sie können die Generierung jederzeit über den Button **Abbrechen** vorzeitig beenden.

**Hinweis:** Vor der Generierung wird eine Plausibilitätsprüfung durchgeführt.
Wenn widersprüchliche Vorgaben erkannt werden (z.B. inkonsistente Spieltagsvorgaben für Teams desselben Vereins in derselben Spielwoche), erscheint eine Fehlermeldung.

## 6.1 Konflikte beheben

Nach Ablauf der Laufzeit oder wenn die optimale Lösung gefunden wurde, erscheint das Fenster "Alternativen", falls Konflikte aufgetreten sind (was der Normalfall sein sollte).
Ein Konflikt liegt vor, wenn mehrere Teams einer Gruppe dieselbe Schlüsselzahl beanspruchen.

Das Fenster zeigt folgende Informationen:

- **Konflikt**: Dropdown-Menü zur Auswahl des zu bearbeitenden Konflikts.
- **Gruppe**: Die betroffene Gruppe.
- **Team 1–3**: Die betroffenen Mannschaften (maximal drei pro Konflikt).
- **Wunsch**: Die eigentlich gewünschten Schlüsselzahlen der Mannschaften.
- **Schlüssel**: Dropdown-Menüs, über die Sie den Mannschaften jeweils eine andere Schlüsselzahl zuweisen können.

Zur Auswahl stehen die ursprünglich gewünschte Schlüsselzahl sowie bis zu zwei ähnliche Schlüsselzahlen, die noch frei sind.

**Vorschlag:** Über den Button **Vorschlag** können Sie einen automatischen Lösungsvorschlag generieren lassen, bei dem die Konflikte zufällig aufgelöst werden. Dies sollten Sie im Interesse der betroffenen Vereine nur zu Testzwecken tun, und nicht zur Ermittlung der finalen Schlüsselzahlen!

Klicken Sie auf **OK**, nachdem Sie jeden Konflikt gelöst haben.
Falls ein Konflikt noch nicht aufgelöst ist oder für eine Mannschaft keine Schlüsselzahl vergeben wurde, erscheint eine entsprechende Fehlermeldung.
Sie können die Konfliktauflösung beenden, indem Sie auf den Button **Abbrechen** klicken oder das Fenster schließen und die Sicherheitsabfrage bestätigen.
Die verbliebenen Konflikte können zu einem späteren Zeitpunkt über **Sonstiges** → **Konflikte neu auflösen** (siehe [7.4 Konflikte neu auflösen](07_sonstige_funktionen.md#74-konflikte-neu-aufloesen)) aufgelöst werden.

## 6.2 Abschluss der Generierung

Nachdem alle Konflikte behoben wurden, wird die Zuweisung der Schlüsselzahlen abgeschlossen.
In diesem Schritt werden die noch freien Schlüsselzahlen an die verbleibenden Mannschaften ohne Spielwochenvorgaben zugewiesen.
Anschließend erscheint eine Erfolgsmeldung ("Die Schlüsselzahlen wurden erfolgreich generiert!").

Falls die Generierung nicht erfolgreich war, kann dies folgende Ursachen haben:
- Widersprüchliche Vorgaben (z.B. unvereinbare Heim-/Auswärtsspielvorgaben).
- Nicht ausreichende Laufzeit – erhöhen Sie die maximale Laufzeit und versuchen Sie es erneut.
- Zu viele feste Vorgaben, die eine Lösung unmöglich machen.

## 6.3 Kontrolle und Übergabe

Nach der Generierung empfiehlt es sich, die Ergebnisse in der **Gruppensicht** (siehe [5.2 Gruppensicht](05_startbildschirm.md#52-gruppensicht)) zu kontrollieren.
Da nun alle Teams eine Schlüsselzahl haben sollten, sind alle Zeilen grün oder orange (im Konfliktfall) eingefärbt.
Mithilfe dieser Übersicht können Sie die Schlüsselzahlen in Click-TT oder Ihr Verwaltungssystem übertragen.

> **Beispiel: Gruppensicht – Bezirksoberliga Erwachsene nach der Generierung**
>
> Nach der Generierung hat jedes Team eine Schlüsselzahl erhalten.
> Alle Zeilen sind nun **grün** (oder **orange**, falls Konflikte auftreten):
>
> | Farbe | Gruppe | Team | Schlüssel | Wunsch |
> |-------|--------|------|-----------|--------|
> | Grün | Erw. BOL | TTC Schwalbe Bergneustadt I | 3 | 3 |
> | Grün | Erw. BOL | 1. FC Saarbrücken-TT I | 4 | |
> | Grün | Erw. BOL | SV Werder Bremen III | 10 | 10 |
> | Grün | Erw. BOL | TTF Liebherr Ochsenhausen II | 12 | 12 |
> | Grün | Erw. BOL | TTC RöhnSprudel Fulda-Maberzell II | 8 | 8 |
> | Grün | Erw. BOL | BV Borussia Dortmund I | 6 | 6 |
> | Grün | Erw. BOL | Post SV Mühlhausen II | 11 | 11 |
> | Grün | Erw. BOL | Borussia Düsseldorf I | 5 | 5 |
> | Grün | Erw. BOL | TTC Zugbrücke Grenzau II | 9 | 9 |
> | Grün | Erw. BOL | TTC Bad Homburg I | 2 | 2 |
> | Grün | Erw. BOL | TSV Bad Königshofen I | 7 | 7 |
> | Grün | Erw. BOL | ASC Grünwettersbach e.V. II | 1 | 1 |
>
> Alle 12 Schlüsselzahlen (1–12) sind genau einmal vergeben – es gibt keine Konflikte.
> Die Spalte "Wunsch" zeigt die aus der Vereins-Schlüsselzahl abgeleiteten Wunschzahlen.
> Beim 1. FC Saarbrücken-TT I ist kein Wunsch eingetragen, da der Verein keine vorgegebene Schlüsselzahl und auch keine Spielwoche hatte – die Zahl 4 wurde frei zugewiesen.

> **Beispiel: Vereinssicht – Borussia Düsseldorf nach der Generierung**
>
> Die folgende Tabelle zeigt für alle 14 Mannschaften von Borussia Düsseldorf die ermittelten Schlüsselzahlen.
> Durch die Generierung werden die Vereins-Schlüsselzahlen **A=5, B=11, X=5, Y=10** ermittelt, sodass folgende Daten angezeigt werden:
>
> | Woche | Gruppe | Team | Schlüssel | Wunsch |
> |-------|--------|------|-----------|--------|
> | A | Erw. Bezirksoberliga | Borussia Düsseldorf I | 5 | 5 |
> | A | Erw. 1. Bezirksliga 2 | Borussia Düsseldorf II | 5 | 5 |
> | B | Erw. 2. Bezirksliga 2 | Borussia Düsseldorf III | 11 | 11 |
> | B | Erw. 1. Bezirksklasse 4 | Borussia Düsseldorf IV | 11 | 11 |
> | B | Erw. 2. Bezirksklasse 5 | Borussia Düsseldorf V | 11 | 11 |
> | A | Damen Bezirksoberliga | Borussia Düsseldorf I | 5 | 5 |
> | X | Jgd. 19 Bezirksoberliga | Borussia Düsseldorf I | 5 | 5 |
> | Y | Jgd. 19 1. Bezirksliga 1 | Borussia Düsseldorf II | 10 | 10 |
> | X | Jgd. 19 1. Bezirksliga 2 | Borussia Düsseldorf III | 5 | 5 |
> | Y | Jgd. 19 2. Bezirksliga 3 | Borussia Düsseldorf IV | 10 | 10 |
> | X | Jgd. 15 1. Bezirksliga | Borussia Düsseldorf I | 5 | 5 |
> | Y | Jgd. 15 2. Bezirksliga 3 | Borussia Düsseldorf II | 10 | 10 |
> | X | Jgd. 13 2. Bezirksliga 3 | Borussia Düsseldorf I | 5 | 5 |
> | X | Jgd. 13 2. Bezirksliga 3 | Borussia Düsseldorf II | 4 | 5 |
>
> An diesem Beispiel lassen sich mehrere Aspekte ablesen:
> - Alle Teams in Woche A erhalten Schlüsselzahl **5** (die Vereins-Schlüsselzahl für Spielwoche A).
> - Alle Teams in Woche B erhalten Schlüsselzahl **11** – die gegenläufige Zahl zu 5 im 12er-Raster.
> - Alle Teams in Woche X erhalten Schlüsselzahl **5** (Vereins-Schlüsselzahl für Spielwoche X im 10er-Raster).
> - Alle Teams in Woche Y erhalten Schlüsselzahl **10** – die gegenläufige Zahl zu 5 im 10er-Raster.
> - **Ausnahme**: Borussia Düsseldorf II in der Jugend 13 hat Schlüsselzahl **4** statt 5, obwohl es in Woche X spielt. Das liegt daran, dass zwei Teams desselben Vereins in derselben Gruppe spielen und daher nicht dieselbe Schlüsselzahl erhalten können. Die Zahl 4 ist im 10er-Raster eine *ähnliche* Schlüsselzahl zu 5.

Zusätzlich können Sie die Ergebnisse über das Menü **Sonstiges** → **Ergebnisse exportieren** als CSV-Datei abspeichern (siehe [7.2 Ergebnisse exportieren](07_sonstige_funktionen.md#72-ergebnisse-exportieren)).

---

[← 5. Startbildschirm](05_startbildschirm.md) | [Inhaltsverzeichnis](README.md) | [7. Sonstige Funktionen →](07_sonstige_funktionen.md)
