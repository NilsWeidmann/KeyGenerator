[← 3. Vorbereitungen](03_vorbereitungen.md) | [Inhaltsverzeichnis](README.md) | [5. Startbildschirm →](05_startbildschirm.md)

---

# 4. Datenimport

Der Datenimport kann auf drei Wegen erfolgen, die über die Buttons im Bereich "Datenimport" auf dem Startbildschirm erreichbar sind:

1. **Aus Click-TT** – Import aus einer Click-TT-Exportdatei (Abschnitt 4.1)
2. **Manuell** – Manuelle Eingabe von Vereinen, Gruppen und Mannschaften (Abschnitt 4.2)
3. **Aus Datei** – Laden einer zuvor gespeicherten JSON-Datei (Abschnitt 4.3)

Bei der ersten Verwendung empfiehlt sich der Import aus Click-TT, um sich Tipparbeit zu ersparen.
Anschließend sollten die Daten manuell kontrolliert und ergänzt werden.

## 4.1 Import aus Click-TT

Klicken Sie auf den Button **Aus Click-TT** auf dem Startbildschirm.
Es öffnet sich das Fenster "click-tt Import", das in zwei Bereiche gegliedert ist:

### Oberer Bereich: Gruppen

Der obere Bereich ("Gruppen") dient dem Import der Gruppen und ihrer Mannschaften.

1. Klicken Sie auf den Button **Durchsuchen** rechts oben.
2. Wählen Sie eine CSV-Datei aus, die die Gruppeninformationen enthält (z.B. `Tabellen.csv`).
3. Nach dem Import erscheinen links die Gruppen und rechts die Mannschaften der jeweils ausgewählten Gruppe.
4. Die Rastergrößen der Gruppen werden automatisch anhand der Teamanzahl ermittelt, und können ggf. manuell angepasst werden (Abschnitt 4.2).

### Unterer Bereich: Terminmeldung

Der untere Bereich ("Terminmeldung") dient dem Import der Vereinsinformationen und Spielwochenwünsche.

1. Klicken Sie auf den Button **Durchsuchen** rechts.
2. Wählen Sie eine HTML-Datei aus, die die Terminmeldungen enthält.[^1]
3. Nach dem Import erscheinen links die Vereine mit ihren Schlüsselzahlen und rechts die Mannschaften des jeweils ausgewählten Vereins.

[^1]: Zum heutigen Stand kann diese Datei nicht direkt aus Click-TT heruntergeladen werden, sondern muss in mehreren Schritten per Hand erstellt werden (Näheres dazu im Anhang [A2_terminmeldung_html.md](A2_terminmeldung_html.md)). Wir hoffen, dass in naher Zukunft ein Download der Terminmeldung in Click-TT zur Verfügung steht.

Klicken Sie abschließend auf **Speichern**, um die Daten in eine JSON-Datei zu exportieren.
Der Button befindet sich unten rechts im Fenster.
Beim Schließen des Fensters werden Sie gefragt, ob Sie die Änderungen speichern möchten – sofern Änderungen vorgenommen wurden.

**Nach dem Click-TT-Import verbleibende manuelle Aufgaben:**

| Click-TT übernimmt | Manuell zu ergänzen |
|--------------------|---------------------|
| Vereine anlegen | Vorgegebene Schlüsselzahlen einstellen |
| Gruppen anlegen | Rastergrößen ggf. anpassen |
| Mannschaften anlegen | Ggf. feste Heim-/Auswärtsspieltage für einzelne Teams |
| Zuordnung der Teams zu Spielwochen | Abgleich der Spielwochenangaben mit den Bemerkungsfeldern |

## 4.2 Manueller Datenimport

Die manuelle Eingabe von Daten erfolgt über den Button **Manuell** auf dem Startbildschirm.
Es öffnet sich das Fenster "Dateninput", das in drei nebeneinander angeordnete Bereiche unterteilt ist: eine Gruppentabelle (links), eine Vereinstabelle (Mitte) und eine Mannschaftstabelle (rechts).

### Linke Seite: Gruppentabelle

Die linke Tabelle zeigt alle Gruppen mit folgenden Spalten:

| Spalte | Bedeutung |
|--------|-----------|
| Gruppe | Name der Gruppe |
| Raster | Rastergröße der Gruppe |

**Gruppen anlegen:**
Tragen Sie in der untersten (leeren) Zeile einen Gruppennamen ein.
Die neue Gruppe wird mit dem Standard-Referenzraster und dem Standardnamen "Neue Gruppe" angelegt.
Name und Rastergröße können direkt in der Tabelle bearbeitet werden.

**Gruppen löschen:**
Markieren Sie die Zeile der zu löschenden Gruppe und drücken Sie die Entfernen-Taste.
Eine Gruppe kann nur gelöscht werden, wenn sie keine Mannschaften mehr enthält; andernfalls erscheint eine Fehlermeldung.

**Gruppe auswählen:**
Klicken Sie auf eine Zeile in der Gruppentabelle, um die zugehörigen Mannschaften in der rechten Tabelle anzuzeigen (Gruppenansicht).
Die Spalte "Woche" ist in diesem Modus ausgeblendet; der Teamname ist direkt bearbeitbar.

### Mittlere Seite: Vereinstabelle

Die mittlere Tabelle zeigt alle Vereine mit folgenden Spalten:

| Spalte | Bedeutung |
|--------|-----------|
| Verein | Name des Vereins |
| A | Schlüsselzahl für Spielwoche A (Referenzraster A/B) |
| B | Schlüsselzahl für Spielwoche B (automatisch berechnet) |
| X | Schlüsselzahl für Spielwoche X (Referenzraster X/Y) |
| Y | Schlüsselzahl für Spielwoche Y (automatisch berechnet) |

**Vereine anlegen:**
Tragen Sie in die unterste (leere) Zeile der Spalte "Verein" den Namen eines neuen Vereins ein.
Umlaute werden dabei automatisch ersetzt.
Legen Sie zunächst alle Vereine an, bevor Sie mit der Zuordnung von Mannschaften zu den Gruppen beginnen.

**Vereine löschen:**
Markieren Sie die Zeile des zu löschenden Vereins und drücken Sie die Entfernen-Taste.
Ein Verein kann nur gelöscht werden, wenn keine seiner Mannschaften noch einer Gruppe zugeordnet ist; andernfalls erscheint eine Fehlermeldung mit den betroffenen Gruppen.

**Vorgegebene Schlüsselzahlen einstellen:**
Wenn vom Verband oder Bezirk bereits Schlüsselzahlen für bestimmte Vereine vorgegeben sind, tragen Sie diese in der entsprechenden Spalte (A, B, X oder Y) ein.
Die gegenläufige Schlüsselzahl (also B zu A, Y zu X usw.) wird automatisch berechnet.
Es können nur Zahlen eingetragen werden, die für das eingestellte Referenzraster gültig sind.

> **Beispiel: Vorgegebene Schlüsselzahlen**
>
> Vor der Generierung hatten einige Vereine der Bezirksoberliga bereits vorgegebene Schlüsselzahlen (z.B. vom Bezirk oder Verband).
> Hier ein Auszug aus der Vereinstabelle:
>
> | Verein | A | B | X | Y |
> |--------|---|---|---|---|
> | SV Werder Bremen | 10 | 4 | 8 | 3 |
> | TTF Liebherr Ochsenhausen | 6 | 12 | | |
> | TTC RöhnSprudel Fulda-Maberzell | 8 | 2 | | |
> | TTC Zugbrücke Grenzau | 9 | 3 | | |
> | ASC Grünwettersbach e.V. | 7 | 1 | | |
> | Post SV Mühlhausen e.V. | 5 | 11 | | |
> | Borussia Düsseldorf | | | | |
> | BV Borussia Dortmund | | | | |
> | TTC Bad Homburg | | | | |
> | … | | | | |
>
> Man sieht: Für den SV Werder Bremen sind als einziger Verein Schlüsselzahlen für alle vier Wochen vorgegeben.
> Einige Vereine wie TTF Liebherr Ochsenhausen haben Vorgaben nur für A/B.
> Andere Vereine wie Borussia Düsseldorf und BV Borussia Dortmund hatten zum Zeitpunkt vor der Generierung noch keine Schlüsselzahlen – diese werden dann vollständig vom Tool ermittelt.
> Die Spalte B wird jeweils automatisch aus A berechnet (und Y aus X).

### Rechte Seite: Mannschaftstabelle

Die rechte Tabelle zeigt Mannschaftsdaten in zwei alternativen Anzeigemodi:

- **Gruppenansicht**: Wird aktiviert durch Klick auf eine Zeile in der **Gruppentabelle** (links). Zeigt alle Mannschaften der ausgewählten Gruppe. Die Spalte "Woche" ist ausgeblendet. Der Teamname (Spalte "Team") ist direkt bearbeitbar.
- **Vereinsansicht**: Wird aktiviert durch Klick auf eine Zeile in der **Vereinstabelle** (Mitte). Zeigt alle Mannschaften des ausgewählten Vereins gruppenübergreifend. Die Spalte "Woche" ist sichtbar und bearbeitbar (`A`, `B`, `X`, `Y` oder leer für keine Zuordnung). Der Teamname ist in diesem Modus nicht bearbeitbar.

Die Spalten der Tabelle entsprechen denen der Hauptansicht (Woche, Gruppe, Team, Schlüssel, Wunsch). Die Spalten Gruppe, Schlüssel und Wunsch sind stets nur zur Anzeige.

Ein **Rechtsklick** auf eine Mannschaft in der Tabelle öffnet den Zusatz-Dialog (siehe [05_startbildschirm.md – Abschnitt 5.3](05_startbildschirm.md)), in dem feste Vorgaben für Heim- und Auswärtsspiele in bestimmten Spielwochen eingetragen werden können.

**Mannschaften anlegen:**
Wählen Sie zunächst in der Gruppentabelle (links) die gewünschte Gruppe aus.
**Doppelklicken** Sie anschließend in der Vereinstabelle (Mitte) auf den Vereinsnamen.
Die Mannschaft wird der Gruppe hinzugefügt, sofern noch Platz vorhanden ist (Rastergröße nicht überschritten).

Der Teamname kann in der Gruppenansicht direkt in der Mannschaftstabelle bearbeitet werden.
Dabei muss der Name weiterhin mit dem Vereinsnamen beginnen (z.B. "Borussia Düsseldorf II").
Ungültige Eingaben werden automatisch zurückgesetzt.

**Vereine umbenennen:**
Ändern Sie den Vereinsnamen in der Vereinstabelle (Mitte).
Die Umbenennung wird automatisch auf die Teamnamen aller Mannschaften des Vereins übertragen.

**Mannschaften löschen:**
Wählen Sie die zu entfernende Mannschaft in der Tabelle aus und drücken Sie die Entfernen-Taste.

**Speichern:**
Das Fenster "Dateninput" besitzt einen **Speichern**-Button (oder **Strg+S**) sowie einen **Abbrechen**-Button.
Beim Schließen des Fensters (über Abbrechen oder das Schließen-Symbol) werden Sie gefragt, ob die Änderungen gespeichert werden sollen – sofern Änderungen vorgenommen wurden.
Wurden keine Änderungen vorgenommen, schließt das Fenster ohne Rückfrage.

## 4.3 Laden aus Datei

Wenn Sie bereits zu einem früheren Zeitpunkt Daten gespeichert haben, können Sie diese über den Button **Aus Datei** laden.
Es öffnet sich ein Dateiauswahldialog, in dem Sie eine JSON-Datei auswählen können (z.B. `Data.json`).
Nach dem Laden werden alle Vereine, Gruppen und Mannschaften in der Anwendung wiederhergestellt.

---

[← 3. Vorbereitungen](03_vorbereitungen.md) | [Inhaltsverzeichnis](README.md) | [5. Startbildschirm →](05_startbildschirm.md)
