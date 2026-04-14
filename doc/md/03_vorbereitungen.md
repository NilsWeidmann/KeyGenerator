[← 2. Dateistruktur](02_dateistruktur.md) | [Inhaltsverzeichnis](README.md) | [4. Datenimport →](04_datenimport.md)

---

# 3. Vorbereitungen

Starten Sie die Anwendung `KeyGenerator.exe`.
Es erscheint der Startbildschirm (siehe [05_startbildschirm.md](05_startbildschirm.md)).
Bevor Sie mit dem Import oder der Eingabe von Daten beginnen können, müssen zunächst die grundlegenden Einstellungen vorgenommen werden.

## 3.1 Verzeichnis einstellen

Im oberen Bereich des Startbildschirms sehen Sie das Feld "Verzeichnis".
Hier wird der Dateipfad angezeigt, in dem die Anwendung Dateien sucht und speichert.
Standardmäßig ist das Verzeichnis auf den Speicherort der Anwendung eingestellt.

Wenn Sie ein anderes Verzeichnis verwenden möchten, klicken Sie auf den Button **Durchsuchen** rechts neben dem Textfeld.
Es öffnet sich ein Dialog, in dem Sie den gewünschten Ordner auswählen können.

## 3.2 Referenzraster einstellen

Im unteren linken Bereich des Startbildschirms befinden sich zwei Dropdown-Menüs:

- **Referenzraster für Woche A/B**: Legt die Rastergröße für die Spielwochen A und B fest.
- **Referenzraster für Woche X/Y**: Legt die Rastergröße für die Spielwochen X und Y fest.

Die möglichen Rastergrößen sind **6, 8, 10, 12** und **14**.
Die Voreinstellungen (standardmäßig 12 für A/B und 10 für X/Y) werden beim Programmstart automatisch aus der eingebetteten Konfiguration geladen.
Diese Referenzraster bestimmen, welche Schlüsselzahlen für die jeweiligen Spielwochen auf Vereinsebene maximal vergeben werden können.

**Wichtig:** Alle drei Buttons für den Datenimport (**Manuell**, **Aus Click-TT**, **Aus Datei**) werden erst aktiviert, sobald beide Referenzraster ausgewählt sind.
Da die Standardwerte aus der Konfiguration vorbelegt werden, sind die Buttons beim Start in der Regel sofort verfügbar.

Wenn Sie die Referenzraster nachträglich ändern, werden alle Vereins-Schlüsselzahlen, die den neuen Bereich überschreiten, automatisch zurückgesetzt.

> **Beispiel: Referenzraster**
>
> In unserem Beispiel sind die Referenzraster wie folgt eingestellt:
> - **Referenzraster A/B**: 12 – Die Erwachsenen-Gruppen spielen mit Rastergröße 12.
> - **Referenzraster X/Y**: 10 – Die Jugend- und Damen-Gruppen spielen mit Rastergröße 10.
>
> Das bedeutet: Auf Vereinsebene können für die Wochen A und B Schlüsselzahlen von 1 bis 12 vergeben werden, für die Wochen X und Y Schlüsselzahlen von 1 bis 10.

---

[← 2. Dateistruktur](02_dateistruktur.md) | [Inhaltsverzeichnis](README.md) | [4. Datenimport →](04_datenimport.md)
