[← 3. Datenimport](03_datenimport.md) | [Inhaltsverzeichnis](README.md) | [5. Übersicht →](05_startbildschirm.md)

---

# 4. Manuelle Eingabe

Die Seite **Manuelle Eingabe** ermöglicht die manuelle Eingabe und Bearbeitung von Gruppen, Vereinen und Mannschaften.
Sie ist jederzeit über den gleichnamigen Link in der Seitenleiste erreichbar – auch bevor Daten importiert wurden. In diesem Fall beginnt sie mit einem leeren Datenbestand.

Am oberen Rand der Seite befinden sich drei Schaltflächen: **↶ Rückgängig** (oder **Strg+Z**), **↷ Wiederherstellen** (oder **Strg+Y**) und **Speichern** (blau, oder **Strg+S**). Die Schaltfläche **Speichern** sichert den aktuellen Bearbeitungsstand, ohne die Seite zu verlassen.

Die Seite ist in zwei nebeneinander angeordnete Bereiche unterteilt: eine linke Spalte mit Gruppen- und Vereinstabelle sowie eine rechte Spalte mit der Mannschaftstabelle.

> **Ansicht: Manuelle Eingabe – leere Seite**
>
> Die Seite "Manuelle Eingabe" im Ausgangszustand: Links oben die noch leere Gruppentabelle, links unten die noch leere Vereinstabelle. Die rechte Spalte mit der Mannschaftstabelle erscheint erst, wenn eine Gruppe oder ein Verein ausgewählt wird.
>
> ![Seite „Manuelle Eingabe" – leere Seite](../png/041-click-tt-import-leer.png)

## Linke Spalte: Gruppentabelle

Die obere Tabelle in der linken Spalte zeigt alle Gruppen mit folgenden Spalten:

| Spalte | Bedeutung |
|--------|-----------|
| Gruppe | Name der Gruppe |
| Raster | Rastergröße der Gruppe |

**Gruppen anlegen:**
Klicken Sie auf die Schaltfläche **+ Gruppe**.
Die neue Gruppe wird mit dem Standard-Referenzraster der Spielwochen A/B und dem Standardnamen "Neue Gruppe" angelegt.
Name und Rastergröße können direkt in der Tabelle bearbeitet werden; Umlaute im Gruppennamen werden automatisch ersetzt.
Für die Rastergröße werden nur die für die Gruppe zulässigen Werte zur Auswahl angeboten (siehe [5.2 Gruppenansicht](05_startbildschirm.md#52-gruppenansicht)).

**Gruppe auswählen:**
Klicken Sie auf eine Zeile in der Gruppentabelle, um die zugehörigen Mannschaften in der rechten Spalte anzuzeigen (Gruppenansicht).

**Gruppen löschen:**
Klicken Sie auf das Lösch-Symbol (Papierkorb) in der entsprechenden Zeile.
Eine Gruppe kann nur gelöscht werden, wenn sie keine Mannschaften mehr enthält; andernfalls erscheint eine Fehlermeldung.

> **Ansicht: Manuelle Eingabe – Gruppenansicht**
>
> Die Seite "Manuelle Eingabe" in der Gruppenansicht: Links oben die Gruppen mit Rastergrößen, links unten die Vereine, rechts die Mannschaften der ausgewählten Gruppe.
>
> ![Seite „Manuelle Eingabe" – Gruppenansicht](../png/042-manuell-gruppensicht.png)

## Linke Spalte: Vereinstabelle

Die untere Tabelle in der linken Spalte zeigt alle Vereine mit folgenden Spalten:

| Spalte | Bedeutung |
|--------|-----------|
| Verein | Name des Vereins |
| A | Schlüsselzahl für Spielwoche A (Referenzraster A/B) |
| B | Schlüsselzahl für Spielwoche B (gegenläufig zu A) |
| X | Schlüsselzahl für Spielwoche X (Referenzraster X/Y) |
| Y | Schlüsselzahl für Spielwoche Y (gegenläufig zu X) |

**Vereine anlegen:**
Klicken Sie auf die Schaltfläche **+ Verein**.
Der neue Verein wird mit einem Standardnamen angelegt; Name und Schlüsselzahlen können direkt in der Tabelle bearbeitet werden.
Umlaute im Vereinsnamen werden dabei automatisch ersetzt.
Legen Sie zunächst alle Vereine an, bevor Sie mit der Zuordnung von Mannschaften zu den Gruppen beginnen.

**Verein auswählen:**
Klicken Sie auf eine Zeile in der Vereinstabelle, um die zugehörigen Mannschaften in der rechten Spalte anzuzeigen (Vereinsansicht).
In dieser Ansicht ist auch die Spielwochenzuordnung der Mannschaften sichtbar und bearbeitbar.

**Vorgegebene Schlüsselzahlen einstellen:**
Wenn vom Verband oder Bezirk bereits Schlüsselzahlen für bestimmte Vereine vorgegeben sind, tragen Sie diese in der entsprechenden Spalte (A, B, X oder Y) ein.
Die gegenläufige Schlüsselzahl des Paars (also B zu A, A zu B, Y zu X bzw. X zu Y) wird automatisch berechnet.
Es können nur Zahlen eingetragen werden, die für das eingestellte Referenzraster gültig sind.

**Vereine umbenennen:**
Ändern Sie den Vereinsnamen in der Vereinstabelle.
Die Umbenennung wird automatisch auf die Teamnamen aller Mannschaften des Vereins übertragen.

**Vereine löschen:**
Klicken Sie auf das Lösch-Symbol (Papierkorb) in der entsprechenden Zeile.
Ein Verein kann nur gelöscht werden, wenn keine seiner Mannschaften noch einer Gruppe zugeordnet ist; andernfalls erscheint eine Fehlermeldung mit den betroffenen Gruppen.

> **Beispiel: Vorgegebene Schlüsselzahlen**
>
> Vor der Generierung hatten einige Vereine der Bezirksoberliga bereits vorgegebene Schlüsselzahlen (z.B. vom DTTB oder Verband).
> Der folgende Screenshot zeigt einen Auszug aus der Vereinsansicht:
>
> ![Seite „Manuelle Eingabe" – Vereinsansicht mit vorgegebenen Schlüsselzahlen](../png/042-manuell-vereinssicht-mit-vorgabe.png)
>
> Für den SV Werder Bremen sind Schlüsselzahlen für alle vier Wochen vorgegeben.
> Einige Vereine wie TTS Borsum haben Vorgaben nur für `A`/`B`.
> Andere Vereine wie Borussia Duesseldorf und BV Borussia Dortmund hatten zum Zeitpunkt vor der Generierung noch keine Schlüsselzahlen – diese werden dann vollständig vom Tool ermittelt.
> Die Spalte `B` wird jeweils automatisch aus `A` berechnet (und `Y` aus `X`) – bzw. umgekehrt, wenn `B` oder `Y` eingetragen wird.

## Rechte Spalte: Mannschaftstabelle

Die rechte Spalte erscheint, sobald eine Gruppe oder ein Verein in der linken Spalte ausgewählt wird.
Sie zeigt Mannschaftsdaten in zwei alternativen Anzeigemodi:

- **Gruppenansicht**: Wird aktiviert durch Klick auf eine Zeile in der **Gruppentabelle** (links oben). Zeigt alle Mannschaften der ausgewählten Gruppe. Die Spalte "Woche" ist ausgeblendet.
- **Vereinsansicht**: Wird aktiviert durch Klick auf eine Zeile in der **Vereinstabelle** (links unten). Zeigt alle Mannschaften des ausgewählten Vereins gruppenübergreifend. Die Spalte "Woche" ist sichtbar und bearbeitbar (`A`, `B`, `X`, `Y` oder leer für keine Zuordnung).

Die Spalten der Tabelle entsprechen denen der Übersicht (Woche, Team, Gruppe, Wunsch, Schlüssel, Zusatz); ganz rechts folgt eine Spalte mit dem Lösch-Symbol. Die Spalten Gruppe, Wunsch und Schlüssel sind stets nur zur Anzeige.

Ein Klick auf das Bearbeiten-Symbol (Stift) in der Spalte **Zusatz** öffnet den Zusatz-Dialog (siehe [5.3 Zusätzliche Einstellungen für einzelne Teams](05_startbildschirm.md#53-zusaetzliche-einstellungen-fuer-einzelne-teams)), in dem für einzelne Spieltage Heimspiel-, Auswärtsspiel- und Spielfrei-Vorgaben eingetragen werden können.

**Mannschaften anlegen:**
Wählen Sie zunächst in der Gruppentabelle (links oben) die gewünschte Gruppe aus.
In der oberen rechten Ecke der rechten Spalte erscheint dann die Schaltfläche **+ Team**.
Klicken Sie darauf, um die Daten für eine neue Mannschaft einzugeben.
Wählen Sie dort den Verein aus und tragen Sie ggf. die Mannschaftsnummer ein.
Klicken Sie auf **Hinzufügen**, um die Mannschaft der Gruppe hinzuzufügen, sofern noch Platz vorhanden ist (Rastergröße nicht überschritten).
Die Mannschaftsnummer ist optional; eine Mannschaft ohne Nummer erhält den Namen ihres Vereins.

**Mannschaften umbenennen:**
Der Teamname kann sowohl in der Gruppen- als auch in der Vereinsansicht direkt in der Mannschaftstabelle bearbeitet werden.
Dabei muss der Name weiterhin mit dem Vereinsnamen beginnen (z.B. "Borussia Duesseldorf II").
Ungültige Eingaben werden automatisch zurückgesetzt.

**Mannschaften löschen:**
Klicken Sie auf das Lösch-Symbol (Papierkorb) in der letzten Spalte der entsprechenden Zeile.

**Speichern:**
Am unteren Rand der Seite befinden sich die Schaltflächen **Speichern & zur Übersicht** und **Abbrechen**.
Ein Klick auf **Speichern & zur Übersicht** übernimmt alle Änderungen und wechselt zur Übersichtsseite.
Ein Klick auf **Abbrechen** verwirft die seit dem letzten Speichern vorgenommenen Änderungen und kehrt zur vorherigen Seite zurück.

---

[← 3. Datenimport](03_datenimport.md) | [Inhaltsverzeichnis](README.md) | [5. Übersicht →](05_startbildschirm.md)
