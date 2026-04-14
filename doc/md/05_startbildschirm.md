[← 4. Datenimport](04_datenimport.md) | [Inhaltsverzeichnis](README.md) | [6. Schlüsselzahlen generieren →](06_generierung.md)

---

# 5. Startbildschirm

Nach dem Laden oder Importieren von Daten wird der Startbildschirm zur zentralen Arbeitsfläche.
Er ist wie folgt aufgebaut:

- **Oberer Bereich**: Verzeichnisauswahl und Navigationsleiste.
- **Linke Seite**: Steuerelemente für Vereins-/Gruppenauswahl, Aktionsbuttons und Einstellungen.
- **Rechte Seite**: Datentabelle zur Anzeige und Bearbeitung der Mannschaftsdaten.

Zwischen zwei Ansichten kann über die Radiobuttons **Vereinssicht** und **Gruppensicht** unten links gewechselt werden.

Die drei Hauptbuttons sind:
- **Generieren**: Startet die automatische Schlüsselzahlgenerierung (siehe [06_generierung.md](06_generierung.md)).
- **Speichern** (oder **Strg+S**): Speichert den aktuellen Stand als JSON-Datei. Falls noch kein Dateiname und Speicherort ausgewählt wurde, öffnet sich ein entsprechendes Dialogfenster.
- **Sonstiges**: Öffnet das Fenster für zusätzliche Funktionen (siehe [07_sonstige_funktionen.md](07_sonstige_funktionen.md)).

## 5.1 Vereinssicht

In der Vereinssicht wählen Sie im Dropdown-Menü "Vereine" einen Verein aus.
Daraufhin werden alle Mannschaften dieses Vereins in der Tabelle rechts angezeigt.
Die Spielwoche kann bearbeitet werden, alle anderen Spalten sind nur zur Anzeige vorhanden.

### Tabellenspalten

| Spalte | Bedeutung |
|--------|-----------|
| Woche | Zugeordnete Spielwoche (A, B, X, Y oder leer). Bearbeitung: Klicken Sie in die Zelle und geben Sie die gewünschte Woche ein. |
| Gruppe | Name der Gruppe, in der die Mannschaft spielt |
| Team | Name der Mannschaft |
| Schlüssel | Zugewiesene Schlüsselzahl nach der Generierung |
| Wunsch | Mögliche Schlüsselzahlen basierend auf dem Wochenschema des Vereins |

### Farbcodierung in der Vereinssicht

Die Zeilen werden je nach Spielwoche farblich hervorgehoben:

| Spielwoche | Farbe | Bedeutung |
|------------|-------|-----------|
| A | Gelb | Spielwoche A |
| B | Orange | Spielwoche B |
| X | Blau | Spielwoche X |
| Y | Grün | Spielwoche Y |
| – | Weiß | Keine Spielwoche zugeordnet |

### Schlüsselzahlen auf Vereinsebene

Oberhalb der Tabelle befinden sich vier Dropdown-Menüs (**A**, **B**, **X**, **Y**).
Hier können Sie die Schlüsselzahlen auf Vereinsebene einstellen (deutlich komfortabler geht dies über das Fenster zur manuellen Dateneingabe, Abschnitt [04_datenimport.md – Vereinstabelle](04_datenimport.md)).
Die Werte A und B bzw. X und Y sind jeweils aneinander gekoppelt: Wenn Sie eine Schlüsselzahl für A eingeben, wird die entgegengesetzte Schlüsselzahl für B automatisch berechnet (und umgekehrt).

### Spielwochenzuordnung

Um einem Team eine Spielwoche zuzuordnen, klicken Sie in der Spalte "Woche" in die entsprechende Zeile und geben Sie den Buchstaben der Spielwoche ein (`A`, `B`, `X`, `Y`).
Um die Zuordnung zu entfernen, geben Sie `-` ein oder lassen das Feld leer.
Groß- und Kleinschreibung wird toleriert.

> **Beispiel: Vereinssicht – Borussia Düsseldorf vor der Generierung**
>
> Die folgende Tabelle zeigt alle 14 Mannschaften von Borussia Düsseldorf mit entsprechender farblicher Codierung für die Spielwoche.
> Vor der Generierung sind sowohl die Spalte "Schlüssel" als auch die Spalte "Wunsch" leer, da der Verein keine Schlüsselzahlenvorgabe von einer höheren Gliederungsebene hat:
>
> | Woche | Gruppe | Team | Schlüssel | Wunsch |
> |-------|--------|------|-----------|--------|
> | A | Erw. Bezirksoberliga | Borussia Düsseldorf I | | |
> | A | Erw. 1. Bezirksliga 2 | Borussia Düsseldorf II | | |
> | B | Erw. 2. Bezirksliga 2 | Borussia Düsseldorf III | | |
> | B | Erw. 1. Bezirksklasse 4 | Borussia Düsseldorf IV | | |
> | B | Erw. 2. Bezirksklasse 5 | Borussia Düsseldorf V | | |
> | A | Damen Bezirksoberliga | Borussia Düsseldorf I | | |
> | X | Jgd. 19 Bezirksoberliga | Borussia Düsseldorf I | | |
> | Y | Jgd. 19 1. Bezirksliga 1 | Borussia Düsseldorf II | | |
> | X | Jgd. 19 1. Bezirksliga 2 | Borussia Düsseldorf III | | |
> | Y | Jgd. 19 2. Bezirksliga 3 | Borussia Düsseldorf IV | | |
> | X | Jgd. 15 1. Bezirksliga | Borussia Düsseldorf I | | |
> | Y | Jgd. 15 2. Bezirksliga 3 | Borussia Düsseldorf II | | |
> | X | Jgd. 13 2. Bezirksliga 3 | Borussia Düsseldorf I | | |
> | X | Jgd. 13 2. Bezirksliga 3 | Borussia Düsseldorf II | | |

## 5.2 Gruppensicht

Die Gruppensicht gibt einen Überblick über alle Teams einer Gruppe.
Wählen Sie dazu im Dropdown-Menü "Gruppen" die gewünschte Gruppe aus.

Neben dem Dropdown-Menü erscheint das Feld **Raster**, in dem die Rastergröße der Gruppe angezeigt und geändert werden kann (eine komfortablere Alternative bietet dafür die manuelle Dateneingabe, Abschnitt [04_datenimport.md](04_datenimport.md)).
Die Rastergröße muss mindestens so groß sein wie die Anzahl der Mannschaften in der Gruppe (aufgerundet auf eine gerade Zahl) und darf maximal 14 betragen.

### Tabellenspalten in der Gruppensicht

Die Tabelle zeigt folgende Spalten (die Spalte "Woche" ist in der Gruppensicht *nicht* sichtbar), wobei alle Spalten nur zur Anzeige verfügbar sind:

| Spalte | Bedeutung |
|--------|-----------|
| Gruppe | Name der Gruppe |
| Team | Name der Mannschaft |
| Schlüssel | Zugewiesene oder vorgegebene Schlüsselzahl |
| Wunsch | Mögliche Schlüsselzahlen |

### Farbcodierung in der Gruppensicht

In der Gruppensicht werden die Zeilen nach dem Zuweisungsstatus eingefärbt:

| Farbe | Bedeutung |
|-------|-----------|
| Grün | Gültige Schlüsselzahl zugewiesen, die den Wunsch-Schlüsselzahlen entspricht und in der Gruppe eindeutig ist |
| Blau | Team hat eine Spielwoche und Wunsch-Schlüsselzahlen (Vorgabe durch höhere Ebene), aber noch keine zugewiesene Schlüsselzahl |
| Gelb | Team hat eine Spielwoche, aber weder Schlüsselzahl noch Wunsch-Schlüsselzahlen (d.h. keine Vorgabe durch höhere Ebene); *oder*: Team hat keine Spielwoche, aber Vorgaben für Heim- oder Auswärtsspiele |
| Orange | Schlüsselzahl stimmt nicht mit den Wunsch-Schlüsselzahlen überein; *oder*: die zugewiesene Schlüsselzahl ist in der Gruppe doppelt vergeben (ungelöster Konflikt) |
| Weiß | Team hat keine Spielwoche und keine besonderen Vorgaben |

Die Gruppensicht eignet sich besonders zur **Kontrolle nach der Generierung**, da hier auf einen Blick alle Schlüsselzahlen einer Gruppe sichtbar sind.
Orange eingefärbte Zeilen weisen auf Konflikte hin, die auch im Nachgang über **Sonstiges** → **Konflikte neu auflösen** (siehe [07_sonstige_funktionen.md](07_sonstige_funktionen.md)) behoben werden können.

> **Beispiel: Gruppensicht – Bezirksoberliga Erwachsene vor der Generierung**
>
> Die folgende Tabelle zeigt die Bezirksoberliga Erwachsene (Rastergröße 12, 12 Teams) im Zustand *vor* der Generierung.
> Zu diesem Zeitpunkt waren viele Spielwochen bereits zugeordnet, aber die Schlüsselzahlen noch nicht vergeben:
>
> | Farbe | Gruppe | Team | Schlüssel | Wunsch |
> |-------|--------|------|-----------|--------|
> | Gelb | Erw. BOL | TTC Schwalbe Bergneustadt I | | |
> | Weiß | Erw. BOL | 1. FC Saarbrücken-TT I | | |
> | Blau | Erw. BOL | SV Werder Bremen III | | 10 |
> | Blau | Erw. BOL | TTF Liebherr Ochsenhausen II | | 12 |
> | Blau | Erw. BOL | TTC RöhnSprudel Fulda-Maberzell II | | 8 |
> | Gelb | Erw. BOL | BV Borussia Dortmund I | | |
> | Blau | Erw. BOL | Post SV Mühlhausen II | | 11 |
> | Gelb | Erw. BOL | Borussia Düsseldorf I | | |
> | Blau | Erw. BOL | TTC Zugbrücke Grenzau II | | 9 |
> | Gelb | Erw. BOL | TTC Bad Homburg I | | |
> | Gelb | Erw. BOL | TSV Bad Königshofen I | | |
> | Blau | Erw. BOL | ASC Grünwettersbach e.V. II | | 1 |
>
> Hier sind drei Farben zu erkennen:
> - **Blau**: Teams, deren Verein bereits eine vorgegebene Schlüsselzahl hat (z.B. SV Werder Bremen mit A=10). Die Wunsch-Schlüsselzahlen können berechnet werden, aber die Mannschafts-Schlüsselzahl fehlt noch.
> - **Gelb**: Teams, deren Verein noch keine Schlüsselzahl hat (z.B. Borussia Düsseldorf, BV Borussia Dortmund). Es können weder Wunsch noch Schlüsselzahl angezeigt werden.
> - **Weiß**: 1. FC Saarbrücken-TT I hat keine Spielwoche (Woche = `-`) und somit auch keinen Wunsch.

## 5.3 Zusätzliche Einstellungen für einzelne Teams

Mit einem **Rechtsklick** auf eine Mannschaft in der Tabelle (sowohl in der Vereins- als auch in der Gruppensicht) öffnet sich das Fenster "Zusatz".
Hier können Sie folgende Einstellungen vornehmen:

### Spielwoche festlegen

Im Dropdown-Menü "Woche" können Sie die Spielwoche des Teams auswählen (`-`, `A`, `B`, `X`, `Y`).
Die Einstellung kann genauso über die Vereinssicht auf dem Startbildschirm (Abschnitt 5.1) oder über die manuelle Dateneingabe (Abschnitt [04_datenimport.md](04_datenimport.md)) vorgenommen werden.

### Heim- und Auswärtsspieltage festlegen

Unterhalb der Spielwochenauswahl befindet sich eine Tabelle mit zwei Spalten:

| Spalte | Bedeutung |
|--------|-----------|
| Spieltag | Nummer des Spieltags (1, 2, 3, …) |
| Heim/Auswärts | Dropdown-Menü mit den Optionen `-` (keine Vorgabe), `Heimspiel` oder `Auswärtsspiel` |

Die Anzahl der angezeigten Spieltage wird dynamisch anhand der Rastergröße der Gruppe bestimmt.
Bei einer Gruppe mit Rastergröße 12 werden beispielsweise 11 Spieltage angezeigt, bei Rastergröße 10 entsprechend 9.

Bestätigen Sie Ihre Eingaben mit **OK** oder verwerfen Sie sie mit **Abbrechen**.

**Hinweis:** Verwenden Sie diese Funktion sparsam, da sie die Schlüsselzahlfindung erheblich erschwert und unter Umständen eine gute Lösung verhindern kann.

## 5.4 Begrenzung der Laufzeit

Da die Ermittlung der Schlüsselzahlen ein komplexes Optimierungsproblem darstellt, kann die maximale Laufzeit für die Generierung begrenzt werden.
Wenn die maximale Laufzeit erreicht ist, wird die beste bis dahin gefundene Lösung angezeigt.
Die Einstellung befindet sich unten links auf dem Startbildschirm:

- **Maximale Laufzeit**: Geben Sie die Laufzeit in Minuten und Sekunden ein.

Der Standardwert beträgt **2 Minuten 0 Sekunden**, was im Normalfall für eine gute Lösung ausreicht.
Sie können den Zeitraum verlängern, um eventuell eine noch bessere Lösung zu erhalten.
Wenn das Programm vor Ablauf der Zeit die optimale Lösung findet, wird die Suche automatisch vorzeitig beendet.

## 5.5 Rückgängig und Wiederherstellen

Manuelle Änderungen können sowohl in der Hauptansicht als auch im Dateninput-Fenster rückgängig gemacht und wiederhergestellt werden:

- **Strg+Z** oder Button **Rückgängig**: Letzte Änderung rückgängig machen.
- **Strg+Y** oder Button **Wiederherstellen**: Rückgängig gemachte Änderung wiederherstellen.

Die Buttons **Rückgängig** und **Wiederherstellen** sind jeweils nur aktiv, wenn entsprechende Operationen in der Historie vorhanden sind.
Die Änderungshistorie wird beim Laden neuer Daten sowie beim Start einer neuen Generierung automatisch zurückgesetzt.
Um auf den Stand vor der Generierung zurückzukehren, wird automatisch ein Backup angelegt (siehe [07_sonstige_funktionen.md – Abschnitt 7.1](07_sonstige_funktionen.md)).

---

[← 4. Datenimport](04_datenimport.md) | [Inhaltsverzeichnis](README.md) | [6. Schlüsselzahlen generieren →](06_generierung.md)
